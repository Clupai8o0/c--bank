class DepositTransaction {
  private Account _account;
  private decimal _amount;
  private bool _executed;
  private bool _success;
  private bool _reversed;

  // Constructor
  // Initializes the transaction with the account and amount
  public DepositTransaction(Account account, decimal amount) {
    this._account = account;
    this._amount = amount;
    this._executed = false;
    this._success = false;
    this._reversed = false;
  }

  // Getter
  public bool Executed { get { return this._executed; } }
  public bool Success { get { return this._success; } }
  public bool Reversed { get { return this._reversed; } }

  // Print withdraw transaction details
  public void print() {
    Console.WriteLine("Deposit Transaction");
    Console.WriteLine("Amount: " + _amount);
    Console.WriteLine("Executed: " + _executed);
    Console.WriteLine("Success: " + _success);
    Console.WriteLine("Reversed: " + _reversed);
  }

  public void execute() {
    if (!this._executed) {
      if (this._account.deposit(this._amount)) {
        this._success = true;
        this._executed = true;
      } else {
        this._success = false;
        this._executed = true;
        throw new InvalidOperationException("Invalid amount");
      }
    } else throw new InvalidOperationException("Transaction already executed.");
  }

  public void rollback() {
    if (this._executed && this._success && !this._reversed) {
      this._account.withdraw(this._amount);
      this._reversed = true;
    } else throw new InvalidOperationException("Transaction not executed or already reversed.");
  }
}