class TransferTransaction {
  private Account _fromAccount;
  private Account _toAccount;

  private decimal _amount;
  private DepositTransaction _deposit;
  private WithdrawTransaction _withdraw;

  private bool _executed;
  private bool _success;
  private bool _reversed;

  // Constructor
  public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) {
    this._fromAccount = fromAccount;
    this._toAccount = toAccount;
    this._amount = amount;
    this._executed = false;
    this._success = false;
    this._reversed = false;

    this._deposit = new DepositTransaction(toAccount, amount);
    this._withdraw = new WithdrawTransaction(fromAccount, amount);
  }

  // Getter
  public bool Executed { get { return this._executed; } }
  public bool Success { get { return this._success; } }
  public bool Reversed { get { return this._reversed; } }

  // Print transfer transaction details
  public void print() {
    Console.WriteLine("Transfer Transaction");
    Console.WriteLine($"Transferred ${this._amount} from {this._fromAccount.Name}'s account to {this._toAccount.Name}'s account.");
    Console.WriteLine("Executed: " + this._executed);
    Console.WriteLine("Success: " + this._success);
    Console.WriteLine("Reversed: " + this._reversed);
    this._deposit.print();
    this._withdraw.print();
    Console.WriteLine();
    
  }

  public void execute() {
    if (!this._executed) {
      try {
        this._withdraw.execute();
        this._deposit.execute();
        this._success = true;
        this._executed = true;
      } catch (InvalidOperationException e) {
        this._success = false;
        this._executed = true;
        try {
          this._withdraw.rollback();
          this._deposit.rollback();
        } catch {} // Ignore rollback exceptions
        finally { // print the error message
          throw new InvalidOperationException("Transfer failed: " + e.Message);
        }
      }
    } else throw new InvalidOperationException("Transaction already executed.");
  }

  public void rollback() {
    if (this._executed && this._success && !this._reversed) {
      this._deposit.rollback();
      this._withdraw.rollback();
      this._reversed = true;
    } else throw new InvalidOperationException("Transaction not executed or already reversed.");
  }
}