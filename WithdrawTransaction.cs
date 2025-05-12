class WithdrawTransaction: Transaction {
  private Account _account;

  // Constructor
  // Initializes the transaction with the account and amount
  public WithdrawTransaction(Account account, decimal amount): base(amount) {
    this._account = account;
  }

  // Getter
  public override bool Success { get { return this._success; } }

  // Print withdraw transaction details
  public override void Print() {
    Console.WriteLine("Withdraw Transaction");
    Console.WriteLine("Amount: " + _amount);
    Console.WriteLine("Executed: " + this.Executed);
    Console.WriteLine("Success: " + _success);
    Console.WriteLine("Reversed: " + this.Reversed);
  }

  public override void Execute() {
    if (!this.Executed) {
      if (this._account.withdraw(this._amount)) {
        base.Execute();
      } else throw new InvalidOperationException("Insufficient funds.");
    } else throw new InvalidOperationException("Transaction already executed.");
  }

  public override void Rollback() {
    if (this.Executed && this._success && !this.Reversed) {
      this._account.deposit(this._amount);
      base.Rollback();
    } else throw new InvalidOperationException("Transaction not executed or already reversed.");
  }
}