enum MenuOption {
  AddAccount = 0,
  Withdraw = 1,
  Deposit = 2,
  Print = 3,
  Transfer = 4,
  PrintTransactionHistory = 5,
  Rollback = 6,
  Quit = 7
}

class BankSystem {
  static decimal getDecimal(string text) {
    while (true) {
      try {
        Console.Write(text);
        decimal value = Convert.ToDecimal(Console.ReadLine());
        if (value < 0) {
          Console.WriteLine("Value cannot be negative.");
          continue;
        }
        return value;
      } catch {
        Console.WriteLine("Invalid input, make sure the input is a number.");
      }
    }
  }
  static void StopConsoleReset() {
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
  }

  static MenuOption ReadUserOption() {
    do {
      Console.Clear();
      Console.WriteLine();
      Console.WriteLine("0. Add new Account");
      Console.WriteLine("1. Withdraw");
      Console.WriteLine("2. Deposit");
      Console.WriteLine("3. Print");
      Console.WriteLine("4. Transfer");
      Console.WriteLine("5. Print Transaction History");
      Console.WriteLine("6. Rollback Transaction");
      Console.WriteLine("7. Quit");

      int option;
      while (true) {
        try {
          Console.Write("Enter your choice: ");
          option = Convert.ToInt32(Console.ReadLine());
        } catch {
          Console.WriteLine("Invalid input, make sure the input is a number.");
          continue;
        }

        if (option < 0 || option > (int)MenuOption.Quit) {
          Console.WriteLine("Invalid option. Please try again.");
          continue;
        }
        break; // break out of loop
      }

      // confirming choice
      while (true) {
        try {
          Console.Write("Are you sure? (y/n): ");
          char confirm = Convert.ToChar(Console.ReadLine() ?? "");
          if (confirm == 'y') // going through with choice
            return (MenuOption)option;
          else if (confirm == 'n') break; // restarting loop
          else continue; // restarting loop
        } catch {
          Console.WriteLine("Invalid input, make sure the input is a character.");
          continue;
        }
      }
    } while (true);
  }

  static void DoAddAccount(Bank bank) {
    Console.Write("Enter account name: ");
    string name = Console.ReadLine() ?? "";
    decimal balance = getDecimal("Enter initial balance: ");
    bank.addAccount(new Account(name, balance));

  }

  static Account? FindAccount(Bank bank, string text) {
    Console.Write(text);
    string name = Console.ReadLine() ?? "";
    Account? account = bank.getAccount(name);
    if (account == null) {
      Console.WriteLine("Account not found.");
      StopConsoleReset();
      return null;
    }
    return account;
  }
  static Account? FindAccount(Bank bank) {
    return FindAccount(bank, "Enter account name: ");
  }

  static void DoDeposit(Bank bank) {
    decimal amount = getDecimal("Enter amount to deposit: ");
    Account? account = FindAccount(bank);
    if (account == null) return; // if account not found
    bank.executeTransaction(new DepositTransaction(account, amount));
    StopConsoleReset();
  }

  static void DoWithdraw(Bank bank) {
    decimal amount = getDecimal("Enter amount to withdraw: ");
    Account? account = FindAccount(bank);
    if (account == null) return; // if account not found
    bank.executeTransaction(new WithdrawTransaction(account, amount));
    StopConsoleReset();
  }

  static void DoPrint(Bank bank) {
    Account? account = FindAccount(bank);
    if (account == null) return; // if account not found
    account.print();
    StopConsoleReset();
  }

  static void DoTransfer(Bank bank) {
    Account? fromAccount = FindAccount(bank, "Enter sender account name: ");
    if (fromAccount == null) return; // if account not found

    Account? toAccount = FindAccount(bank, "Enter receiver account name: ");
    if (toAccount == null) return; // if account not found

    if (fromAccount == toAccount) {
      Console.WriteLine("Cannot transfer to the same account.");
      StopConsoleReset();
      return;
    }

    decimal amount = getDecimal("Enter amount to transfer: ");
    bank.executeTransaction(new TransferTransaction(fromAccount, toAccount, amount));
    StopConsoleReset();
  }

  // Rollback transaction
  static void DoRollback(Bank bank) {
    int id;
    Transaction transaction;
    while (true) {
      try {
        Console.Write("Enter transaction ID to rollback: ");
        id = Convert.ToInt32(Console.ReadLine());
        transaction = bank.getTransaction(id);
        break; // break out of loop
      } catch (Exception e) {
        Console.WriteLine("Invalid input: " + e.Message);
      }
    }
    bank.rollbackTransaction(transaction);
    StopConsoleReset();
  }

  static void Main() {
    Bank bank = new Bank();
    bank.addAccount(new Account("Samridh", 2000));

    while (true) {
      MenuOption option = ReadUserOption();
      switch (option) {
        case MenuOption.AddAccount:
          DoAddAccount(bank);
          break;
        case MenuOption.Withdraw:
          DoWithdraw(bank);
          break;
        case MenuOption.Deposit:
          DoDeposit(bank);
          break;
        case MenuOption.Print:
          DoPrint(bank);
          break;
        case MenuOption.Transfer:
          DoTransfer(bank);
          break;
        case MenuOption.PrintTransactionHistory:
          bank.printTransactionHistory();
          StopConsoleReset();
          break;
        case MenuOption.Rollback:
          DoRollback(bank);
          break;
        case MenuOption.Quit:
          return;
      }
    }
  }
}