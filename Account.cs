class Account {
  // instance variables
  private decimal _balance;
  private string _name;

  // constructor
  public Account(string name, decimal balance) {
    this._name = name;
    this._balance = balance;
  }

  // accessor methods
  public string Name { get { return this._name; } }

  // mutator methods
  public bool deposit(decimal amount) {
    if (amount > 0) {
      this._balance += amount;
      return true;
    } else return false;
  }
  public bool withdraw(decimal amount) {
    if (amount > this._balance) return false;
    if (amount < 0) return false;
    this._balance -= amount;
    return true;
  }

  // printing methods
  public void print() {
    Console.WriteLine("Name: " + this._name);
    Console.WriteLine("Balance: " + this._balance);
  }
}