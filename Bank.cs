class Bank {
  private List<Account> _accounts;
  private List<Transaction> _transactions;

  // Constructor
  public Bank() { // 
    this._accounts = new List<Account>();
    this._transactions = new List<Transaction>();
  }

  // Bank account management
  public void addAccount(Account account) {
    this._accounts.Add(account);
  }
  public Account? getAccount(string name) {
    foreach (Account account in this._accounts) {
      if (account.Name == name) {
        return account;
      }
    }
    return null;
  }

  // Transaction executer
  public void executeTransaction(Transaction transaction) {
    try {
      _transactions.Add(transaction);
      
      transaction.Execute();
      transaction.Print();
    } catch (InvalidOperationException e) {
      Console.WriteLine(e.Message);
    }
  }

  // Rollbank transaction
  public void rollbackTransaction(Transaction transaction) {
    try {
      transaction.Rollback();
      transaction.Print();
    } catch (InvalidOperationException e) {
      Console.WriteLine(e.Message);
    }
  }

  // Print transaction history
  public void printTransactionHistory() {
    Console.WriteLine("\nTransaction History:");
    Console.WriteLine("=====================================");
    for (int i = 0; i < _transactions.Count; i++) {
      Transaction transaction = _transactions[i];

      Console.WriteLine("Transaction Timestamp: " + transaction.Datestamp.ToString());
      Console.WriteLine("Transaction ID: " + i);
      transaction.Print();
      Console.WriteLine();
    }
  }

  // Get Transaction
  public Transaction getTransaction(int id) {
    if (id < 0 || id >= _transactions.Count) {
      throw new ArgumentOutOfRangeException("Invalid transaction ID.");
    }
    return _transactions[id];
  }
}