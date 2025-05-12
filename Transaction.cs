abstract class Transaction {
  // Fields
  protected decimal _amount;
  protected bool _success;
  private bool _executed;
  private bool _reversed;
  private DateTime _datestamp;

  // Properties
  public abstract bool Success { get; }
  public bool Executed { get { return this._executed; } }
  public bool Reversed { get { return this._reversed; } }
  public DateTime Datestamp { get { return this._datestamp; } }

  // Constructor
  public Transaction(decimal amount) {
    this._amount = amount;
    this._success = false;
    this._executed = false;
    this._reversed = false;
    this._datestamp = DateTime.Now;
  }

  // abstract methods
  public abstract void Print();

  // virtual methods with default implementations
  public virtual void Execute() {
    this._executed = true;
    this._success = true;
    this._datestamp = DateTime.Now;
  }
  public virtual void Rollback() {
    this._reversed = true;
    this._datestamp = DateTime.Now;
  }
}