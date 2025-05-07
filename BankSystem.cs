enum MenuOption {
  Withdraw = 0,
  Deposit = 1,
  Print = 2,
  Transfer = 3,
  Quit = 4
}

class BankSystem {
  static void StopConsoleReset() {
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
  }

  static MenuOption ReadUserOption() {
    do {
      Console.Clear();
      Console.WriteLine();
      Console.WriteLine("0. Withdraw");
      Console.WriteLine("1. Deposit");
      Console.WriteLine("2. Print");
      Console.WriteLine("3. Transfer");
      Console.WriteLine("4. Quit");
      Console.Write("Enter your choice: ");
      int option = Convert.ToInt32(Console.ReadLine());

      // making sure choice is in bounds 
      if (option >= 0 && option <= 3) {
        // confirming choice
        Console.Write("Are you sure? (y/n): ");
        char confirm = Convert.ToChar(Console.ReadLine() ?? "");
        if (confirm == 'y') // going through with choice
          return (MenuOption)option;
        else continue; // restarting loop
      }

      // invalid choice
      Console.WriteLine("Invalid option. Please try again.");
    } while (true);
  }

  static void DoDeposit(Account account) {
    Console.Write("Enter amount to deposit: ");
    decimal amount = Convert.ToDecimal(Console.ReadLine());
    
    DepositTransaction transaction = new DepositTransaction(account, amount);
    try {
      transaction.execute();
      transaction.print();
    } catch (InvalidOperationException e) {
      Console.WriteLine(e.Message);
    }
    StopConsoleReset();
  }

  static void DoWithdraw(Account account) {
    Console.Write("Enter amount to withdraw: ");
    decimal amount = Convert.ToDecimal(Console.ReadLine());

    WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
    try {
      transaction.execute();
      transaction.print();
    } catch (InvalidOperationException e) {
      Console.WriteLine(e.Message);
    }
    StopConsoleReset();
  }

  static void DoPrint(Account account) {
    account.print();
    StopConsoleReset();
  }

  static void DoTransfer(Account fromAccount, Account toAccount) {
    Console.Write("Enter amount to transfer: ");
    decimal amount = Convert.ToDecimal(Console.ReadLine());

    TransferTransaction transaction = new TransferTransaction(fromAccount, toAccount, amount);
    try {
      transaction.execute();
      transaction.print();
    } catch (InvalidOperationException e) {
      Console.WriteLine(e.Message);
    }
    StopConsoleReset();
  }

  static void Main() {
    Account account = new Account("Samridh", 0);
    Account account2 = new Account("John", 0);
    while (true) {
      MenuOption option = ReadUserOption();
      switch (option) {
        case MenuOption.Withdraw:
          DoWithdraw(account);
          break;
        case MenuOption.Deposit:
          DoDeposit(account);
          break;
        case MenuOption.Print:
          DoPrint(account);
          break;
        case MenuOption.Transfer:
          DoTransfer(account, account2);
          break;
        case MenuOption.Quit:
          return;
      }
    }
  }
}