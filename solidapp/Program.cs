using System;
using System.Collections.Generic;



namespace solidapp{



// Account class responsible for managing account details
// this class only stores the details of an account
public class BankAccount{
    public int AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(int accountNumber, string accountHolder, decimal balance){
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
    }
}



// Transaction class responsible for handling account transactions
public class Transaction{
    public void Deposit(BankAccount account, decimal amount){
        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into account {account.AccountNumber}. New balance: ${account.Balance}");
    }

    public void Withdraw(BankAccount account, decimal amount){
        if (account.Balance >= amount){
            account.Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount} from account {account.AccountNumber}. New balance: ${account.Balance}");
        }
        else{
            Console.WriteLine($"Insufficient funds in account {account.AccountNumber} to withdraw ${amount}.");
        }
    }
}

// Report class responsible for generating account reports
public class AccountReport
{
    public void GenerateReport(BankAccount account){
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Account Holder: {account.AccountHolder}");
        Console.WriteLine($"Balance: ${account.Balance}");
    }
}



class Program
{
    static void Main()
    {
        // Create a bank account
        BankAccount account = new BankAccount(12345, "John Doe", 1000);

        // Perform transactions
        Transaction transaction = new Transaction();
        transaction.Deposit(account, 500);
        transaction.Withdraw(account, 200);

        // Generate a report
        AccountReport report = new AccountReport();
        report.GenerateReport(account);
    }
}


}
