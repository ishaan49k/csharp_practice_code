using System;
using System.Collections.Generic;



namespace solidapp{

/*

// SRP
-------

In this code, make a bank account class on which we can do deposit and withdraw operations. To implement SRP, remove the transactions
functions from the account class and make a separate transaction class to handle them.



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




*/


// ----------------x------------------x-------------------


/*

// OPC
// ----

// To enforce OPC, make an interface for each class that has multiple types of objects! RRRRRRRR

// In the previous code, all transactions were handled by the same class - Transaction. If we want to add new types of transactions, then we
// will have to modify this class - violation of OPC. To avoid this, we create an interface ITransaction and make a separate class for all 
// types of transactions. Similarly, for account, make an interface and implement different account types as separate classes.



// Account interface representing the common operations for bank accounts
public interface IAccount{
    int AccountNumber { get; set; }
    string AccountHolder { get; set; }
    decimal Balance { get; set; }
}



// BankAccount class implementing the IAccount interface
public class BankAccount: IAccount{
    public int AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(int accountNumber, string accountHolder, decimal balance){
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
    }
}



// ITransaction interface representing the common operations for transactions
public interface ITransaction{
    void Execute(IAccount account, decimal amount);
}



// DepositTransaction class implementing the ITransaction interface
public class DepositTransaction: ITransaction{
    public void Execute(IAccount account, decimal amount){
        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into account {account.AccountNumber}. New balance: ${account.Balance}");
    }
}



// WithdrawalTransaction class implementing the ITransaction interface
public class WithdrawalTransaction: ITransaction{
    public void Execute(IAccount account, decimal amount){
        if (account.Balance >= amount){
            account.Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount} from account {account.AccountNumber}. New balance: ${account.Balance}");
        }
        else{
            Console.WriteLine($"Insufficient funds in account {account.AccountNumber} to withdraw ${amount}.");
        }
    }
}



// Report interface representing the common operations for generating reports
public interface IReport{
    void GenerateReport(IAccount account);
}




// AccountReport class implementing the IReport interface
public class AccountReport: IReport{
    public void GenerateReport(IAccount account){
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Account Holder: {account.AccountHolder}");
        Console.WriteLine($"Balance: ${account.Balance}");
    }
}




class Program
{
    static void Main(){
        
        // Create a bank account
        IAccount account = new BankAccount(12345, "Ishaan", 1000);

        // Perform transactions
        ITransaction depositTransaction = new DepositTransaction();
        depositTransaction.Execute(account, 500);

        ITransaction withdrawalTransaction = new WithdrawalTransaction();
        withdrawalTransaction.Execute(account, 200);

        // Generate a report
        IReport report = new AccountReport();
        report.GenerateReport(account);
    }
}


}



*/




// ------------------x----------------------x----------------

/*

// Liskov Substitution Principal

Make a more specialized version of BankAccount called SavingsAccount. In all the functions that take in BankAccount, we can pass 
in SavingsAccount.



// IAccount interface representing the common operations for bank accounts
public interface IAccount{
    int AccountNumber { get; set;}
    string AccountHolder { get; set; }
    decimal Balance { get; set; }
}



// BankAccount class implementing the IAccount interface
public class BankAccount : IAccount{
    public int AccountNumber { get; set;}
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(int accountNumber, string accountHolder, decimal balance)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
    }
}



// SavingsAccount class, a more specialized type of BankAccount which has extra property of InterestRate
public class SavingsAccount: BankAccount{
    public decimal InterestRate { get; set;}

    public SavingsAccount(int accountNumber, string accountHolder, decimal balance, decimal interestRate): base(accountNumber, accountHolder, balance){
        InterestRate = interestRate;
    }
}



// ITransaction interface representing the common operations for transactions
public interface ITransaction{
    void Execute(IAccount account, decimal amount);
}



// DepositTransaction class implementing the ITransaction interface
public class DepositTransaction: ITransaction{
    public void Execute(IAccount account, decimal amount){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into account {account.AccountNumber}. New balance: ${account.Balance}");
    }
}



// WithdrawalTransaction class implementing the ITransaction interface
public class WithdrawalTransaction : ITransaction{
    public void Execute(IAccount account, decimal amount){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        if (account.Balance >= amount){
            account.Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount} from account {account.AccountNumber}. New balance: ${account.Balance}");
        }
        else{
            Console.WriteLine($"Insufficient funds in account {account.AccountNumber} to withdraw ${amount}.");
        }
    }
}



// IReport interface representing the common operations for generating reports
public interface IReport{
    void GenerateReport(IAccount account);
}




// AccountReport class implementing the IReport interface
public class AccountReport: IReport{
    public void GenerateReport(IAccount account){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Account Holder: {account.AccountHolder}");
        Console.WriteLine($"Balance: ${account.Balance}");
    }
}





class Program{
    static void Main(){

        // Create a savings account (a more specialized type of bank account)
        IAccount savingsAccount = new SavingsAccount(56789, "Ishaan", 2000, 0.05m);

        // Perform transactions
        ITransaction depositTransaction = new DepositTransaction();
        depositTransaction.Execute(savingsAccount, 500);

        ITransaction withdrawalTransaction = new WithdrawalTransaction();
        withdrawalTransaction.Execute(savingsAccount, 200);

        // Generate a report
        IReport report = new AccountReport();
        report.GenerateReport(savingsAccount);
    }
}

}

*/


/*

Read Interface Segmented Principle from notes. - This code is already in accordance with it.

*/




/*

Dependency Inversion Principle

To modify the code, add a TransactionExecuter Interface and make the Deposit and Withdraw classes depend on this interface.

*/



// IAccount interface representing the common operations for bank accounts
public interface IAccount{
    int AccountNumber { get; set;}
    string AccountHolder { get; set; }
    decimal Balance { get; set; }
}



// BankAccount class implementing the IAccount interface
public class BankAccount : IAccount{
    public int AccountNumber { get; set;}
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(int accountNumber, string accountHolder, decimal balance)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
    }
}



// SavingsAccount class, a more specialized type of BankAccount which has extra property of InterestRate
public class SavingsAccount: BankAccount{
    public decimal InterestRate { get; set;}

    public SavingsAccount(int accountNumber, string accountHolder, decimal balance, decimal interestRate): base(accountNumber, accountHolder, balance){
        InterestRate = interestRate;
    }
}




// ITransactionExecutor interface representing the common operations for executing transactions
public interface ITransactionExecutor
{
    void ExecuteTransaction(IAccount account, decimal amount);
}

// DepositTransactionExecutor class implementing the ITransactionExecutor interface
public class DepositTransactionExecutor : ITransactionExecutor
{
    public void ExecuteTransaction(IAccount account, decimal amount)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into account {account.AccountNumber}. New balance: ${account.Balance}");
    }
}

// WithdrawalTransactionExecutor class implementing the ITransactionExecutor interface
public class WithdrawalTransactionExecutor : ITransactionExecutor
{
    public void ExecuteTransaction(IAccount account, decimal amount)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        if (account.Balance >= amount)
        {
            account.Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount} from account {account.AccountNumber}. New balance: ${account.Balance}");
        }
        else
        {
            Console.WriteLine($"Insufficient funds in account {account.AccountNumber} to withdraw ${amount}.");
        }
    }
}

// IReportGenerator interface representing the common operations for generating reports
public interface IReportGenerator
{
    void GenerateReport(IAccount account);
}

// AccountReportGenerator class implementing the IReportGenerator interface
public class AccountReportGenerator : IReportGenerator
{
    public void GenerateReport(IAccount account)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

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
        // Create a savings account (a more specialized type of bank account)
        IAccount savingsAccount = new SavingsAccount(56789, "Ishaan", 2000, 0.05m);


        // Inject dependencies through interfaces
        ITransactionExecutor depositExecutor = new DepositTransactionExecutor();
        ITransactionExecutor withdrawalExecutor = new WithdrawalTransactionExecutor();
        IReportGenerator reportGenerator = new AccountReportGenerator();

        // Perform transactions using the injected dependencies
        depositExecutor.ExecuteTransaction(savingsAccount, 500);
        withdrawalExecutor.ExecuteTransaction(savingsAccount, 200);

        // Generate a report using the injected dependency
        reportGenerator.GenerateReport(savingsAccount);
    }
}

}