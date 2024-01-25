using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography;



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




/*

// RURAL ACCOUNT CODE


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


    public BankAccount(int accountNumber, string accountHolder, decimal balance){
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
public interface ITransactionExecutor{
    void ExecuteTransaction(IAccount account, decimal amount);
}



// DepositTransactionExecutor class implementing the ITransactionExecutor intrface
public class DepositTransactionExecutor : ITransactionExecutor{
    public void ExecuteTransaction(IAccount account, decimal amount){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into account {account.AccountNumber}. New balance: ${account.Balance}");
    }
}


// withdrawalTransactionExecutor class impleenting the ITransactionExecutor interface
public class WithdrawalTransactionExecutor : ITransactionExecutor{
    public void ExecuteTransaction(IAccount account, decimal amount){
        if(account == null){
            throw new ArgumentNullException(nameof(account));
        }

        if(account.Balance >= amount){
            account.Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount} from account {account.AccountNumber}. New balance: ${account.Balance}");
        }
        else{
            Console.WriteLine($"Insufficient funds in account {account.AccountNumber} to withdraw ${amount}.");
        }
    }
}



// IReportGenerator interface representing the common operations fr generating reports
public interface IReportGenerator{
    void GenerateReport(IAccount account);
}



// AccountReportGenerator class implements the IReportGenerator interface
public class AccountReportGenerator : IReportGenerator{
    public void GenerateReport(IAccount account){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Account Holder: {account.AccountHolder}");
        Console.WriteLine($"Balance: ${account.Balance}");
    }
}





public class RuralAccountReportGenerator : AccountReportGenerator{
    public void GenerateReport(RuralAccount account){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        GenerateReport(account);
        Console.WriteLine($"OpeningCredit: ${account.OpeningCredit}");
    }
}






// use the predefined classes to handle logic for transaction on RuralAccount
public class RuralAccountTransactionService{

    private DepositTransactionExecutor depositExecutor;
    private WithdrawalTransactionExecutor withdrawalExecutor;

    // constructor
    public RuralAccountTransactionService(DepositTransactionExecutor DepositExecutor, WithdrawalTransactionExecutor WithdrawalExecutor){
        depositExecutor = DepositExecutor;
        withdrawalExecutor = WithdrawalExecutor;
    }


    // function to deposit into a rural acc
    public void Deposit(RuralAccount ruralAccount, decimal amount){
        depositExecutor.ExecuteTransaction(ruralAccount, amount);
        // add some extra feature here: for now, add opening credit to acc balance
        ruralAccount.Balance += ruralAccount.OpeningCredit;
    }


    public void Withdraw(RuralAccount ruralAccount, decimal amount){
        if(ruralAccount.Balance> 0){
            withdrawalExecutor.ExecuteTransaction(ruralAccount, amount);
        }
    }
}




public class RuralAccount: IAccount{
    public int AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }
    public int OpeningCredit { get; set; }

    public RuralAccount(int accountNumber, string accountHolder, decimal balance, int openingCredit){
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
        OpeningCredit = openingCredit;
        Balance += openingCredit;
    }

}



public class GovtScheme{

    public List<RuralAccount> ruralAccounts;
    public event Action<RuralAccount, decimal> DepositEvent;

    public GovtScheme(){
        DepositEvent = null;
        ruralAccounts = new List<RuralAccount>();
    }

    public void DepositInAll(decimal amount){
        foreach(RuralAccount ra in ruralAccounts){
            ra.Balance += amount;

            // trigger the event for each account
            DepositEvent.Invoke(ra, amount);
            Console.WriteLine($"inserted {amount} into {ra.AccountNumber}");
        }
    }
}





class Program{
    static void Main(){

        // create a savings account (a more specialized type of bank account)
        RuralAccount ra1 = new RuralAccount(0001, "Ishaan1", 2000, 10000);
        RuralAccount ra2 = new RuralAccount(0002, "Ishaan2", 4000, 10000);

        DepositTransactionExecutor depositExecutor = new DepositTransactionExecutor();
        WithdrawalTransactionExecutor withdrawalExecutor = new WithdrawalTransactionExecutor();
        RuralAccountTransactionService ruralTransactionExecutor = new RuralAccountTransactionService(depositExecutor, withdrawalExecutor);
        RuralAccountReportGenerator reportGenerator = new RuralAccountReportGenerator();
        GovtScheme govtScheme = new GovtScheme();
        govtScheme.DepositEvent += ruralTransactionExecutor.Deposit;

        govtScheme.ruralAccounts.Add(ra1);
        govtScheme.ruralAccounts.Add(ra2);


        // perform transactions using the injected dependencies
        ruralTransactionExecutor.Deposit(ra1, 500);
        ruralTransactionExecutor.Withdraw(ra1, 200);

        govtScheme.DepositInAll(100000);
        // generate a report using the injected dependency
        reportGenerator.GenerateReport(ra1);
        reportGenerator.GenerateReport(ra2);
    }
}

}



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


    public BankAccount(int accountNumber, string accountHolder, decimal balance){
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
public interface ITransactionExecutor{
    void ExecuteTransaction(IAccount account, decimal amount);
}




public class DepositTransactionExecutor : ITransactionExecutor{
    public void ExecuteTransaction(IAccount account, decimal amount){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into account {account.AccountNumber}. New balance: ${account.Balance}");
        
        if(account is RuralAccount){

        }
    }
}



public class WithdrawalTransactionExecutor : ITransactionExecutor{
    public void ExecuteTransaction(IAccount account, decimal amount){
        if(account == null){
            throw new ArgumentNullException(nameof(account));
        }

        if(account.Balance >= amount){
            account.Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount} from account {account.AccountNumber}. New balance: ${account.Balance}");
        }
        else{
            Console.WriteLine($"Insufficient funds in account {account.AccountNumber} to withdraw ${amount}.");
        }
    }
}



// IReportGenerator interface representing the common operations fr generating reports
public interface IReportGenerator{
    void GenerateReport(IAccount account);
}



// AccountReportGenerator class implements the IReportGenerator interface
public class AccountReportGenerator : IReportGenerator{
    public void GenerateReport(IAccount account){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Account Holder: {account.AccountHolder}");
        Console.WriteLine($"Balance: ${account.Balance}");
    }
}




// to generate reports for rural accounts
public class RuralAccountReportGenerator : AccountReportGenerator{
    public void GenerateReport(RuralAccount account){
        if(account == null){
            throw new ArgumentNullException(nameof(account));
        }

        GenerateReport(account);
        Console.WriteLine($"OpeningCredit: ${account.OpeningCredit}");
    }
}






// use the predefined classes to handle logic for transaction on RuralAccount
public class RuralAccountTransactionService{

    private DepositTransactionExecutor depositExecutor;
    private WithdrawalTransactionExecutor withdrawalExecutor;

    // constructor
    public RuralAccountTransactionService(DepositTransactionExecutor DepositExecutor, WithdrawalTransactionExecutor WithdrawalExecutor){
        depositExecutor = DepositExecutor;
        withdrawalExecutor = WithdrawalExecutor;
    }


    // function to deposit into a rural acc
    public void Deposit(RuralAccount ruralAccount, decimal amount){
        depositExecutor.ExecuteTransaction(ruralAccount, amount);
        // add some extra feature here: for now, add opening credit to acc balance
        ruralAccount.Balance += ruralAccount.OpeningCredit;
    }


    public void Withdraw(RuralAccount ruralAccount, decimal amount){
        if(ruralAccount.Balance> 0){
            withdrawalExecutor.ExecuteTransaction(ruralAccount, amount);
        }
    }
}




public class RuralAccount: IAccount{
    public int AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }
    public int OpeningCredit { get; set; }

    public RuralAccount(int accountNumber, string accountHolder, decimal balance, int openingCredit, GovtScheme govtScheme){
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
        OpeningCredit = openingCredit;
        Balance += openingCredit;
        govtScheme.ruralAccounts.Add(this);
    }

}



public class GovtScheme{

    public List<RuralAccount> ruralAccounts;
    public event Action<RuralAccount, decimal> DepositEvent;

    public GovtScheme(){
        DepositEvent = null;
        ruralAccounts = new List<RuralAccount>();
    }

    public void DepositInAll(decimal amount){
        foreach(RuralAccount ra in ruralAccounts){
            // trigger the event for each account
            DepositEvent(ra, amount);
        }
    }
}








// transaction management interface
public interface ITransactionManager{
    void ExecuteDeposit(IAccount account, decimal amount);
    void ExecuteWithdrawal(IAccount account, decimal amount);
}



// transaction manager class for normal BankAccounts and its sub types
public class NormalAccountTransactionManager : ITransactionManager{
    public void ExecuteDeposit(IAccount account, decimal amount){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into account {account.AccountNumber}. New balance: ${account.Balance}");
    }

    public void ExecuteWithdrawal(IAccount account, decimal amount){
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



// transaction manager for rural bank accounts
public class RuralAccountTransactionManager : ITransactionManager{
    public void ExecuteDeposit(IAccount account, decimal amount){
        if(account == null){
            throw new ArgumentNullException(nameof(account));
        }

        // add extra logic here
        account.Balance += amount;
        Console.WriteLine($"Deposited ${amount} into rural account {account.AccountNumber}. New balance: ${account.Balance}");
    }



    public void ExecuteWithdrawal(IAccount account, decimal amount){
        if (account == null){
            throw new ArgumentNullException(nameof(account));
        }

        // add extra logic here
        if (account.Balance >= amount){
            account.Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount} from rural account {account.AccountNumber}. New balance: ${account.Balance}");
        }
        else{
            Console.WriteLine($"Insufficient funds in rural account {account.AccountNumber} to withdraw ${amount}.");
        }
    }
}





class Program{
    static void Main(){

        // govt scheme event class
        GovtScheme govtScheme = new GovtScheme();

        // create 2 rural accounts
        RuralAccount ra1 = new RuralAccount(0001, "Ishaan1", 2000, 10000, govtScheme);
        RuralAccount ra2 = new RuralAccount(0002, "Ishaan2", 4000, 10000, govtScheme);

        // transaction managers
        NormalAccountTransactionManager normalTransactionManager = new NormalAccountTransactionManager();
        RuralAccountTransactionManager ruralTransactionManager = new RuralAccountTransactionManager();

        govtScheme.DepositEvent += ruralTransactionManager.ExecuteDeposit;

        ruralTransactionManager.ExecuteDeposit(ra1, 500);
        ruralTransactionManager.ExecuteWithdrawal(ra1, 200);

        govtScheme.DepositInAll(100000);

        RuralAccountReportGenerator reportGenerator = new RuralAccountReportGenerator();
        reportGenerator.GenerateReport(ra1);
        reportGenerator.GenerateReport(ra2);
    }
}

}
