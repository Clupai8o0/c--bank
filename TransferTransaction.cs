class TransferTransaction : Transaction {
  private Account _fromAccount;
  private Account _toAccount;
  private DepositTransaction _deposit;
  private WithdrawTransaction _withdraw;

  // Constructor
  public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount) {
    this._fromAccount = fromAccount;
    this._toAccount = toAccount;
    this._deposit = new DepositTransaction(toAccount, amount);
    this._withdraw = new WithdrawTransaction(fromAccount, amount);
  }

  // Getter
  public override bool Success { get { return this._success; } }

  // Print transfer transaction details
  public override void Print() {
    Console.WriteLine("Transfer Transaction");
    Console.WriteLine($"Transferred ${this._amount} from {this._fromAccount.Name}'s account to {this._toAccount.Name}'s account.");
    Console.WriteLine("Executed: " + this.Executed);
    Console.WriteLine("Success: " + this._success);
    Console.WriteLine("Reversed: " + this.Reversed);
    this._deposit.Print();
    this._withdraw.Print();
    Console.WriteLine();
  }

  public override void Execute() {
    if (!this.Executed) {
      base.Execute();
      try {
        this._withdraw.Execute();
        this._deposit.Execute();
      } catch (InvalidOperationException e) {
        this._success = false; // mark the transaction as failed
        try {
          this._withdraw.Rollback();
          this._deposit.Rollback();
        } catch { } // Ignore rollback exceptions
        finally { // print the error message
          throw new InvalidOperationException("Transfer failed: " + e.Message);
        }
      }
    } else throw new InvalidOperationException("Transaction already executed.");
  }

  public override void Rollback() {
    if (this.Executed && this._success && !this.Reversed) {
      this._deposit.Rollback();
      this._withdraw.Rollback();
      base.Rollback();
    } else throw new InvalidOperationException("Transaction not executed or already reversed.");
  }
}