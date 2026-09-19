// ATM Transaction History

using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM_Transaction
{
    //1. Defining data model
    public class Transaction_History
    {
        public int Transaction_ID {get; set;}
        public string Type {get; set;}
        public decimal Amount {get; set;}
        public string Referrence {get; set;}
    }

   
    class Program
    {
        //2. The Data Store (In-Memory List)
        static List<Transaction_History> database = new List<Transaction_History>();
        static int nextID = 1;

        static void Main(string[] args)
        {
            //3. Create some initial data
            CreateTransaction("Receive",500m, "Biasiswa");
            CreateTransaction("Transfer", 12m, "Zus Coffee");

            while (true)
            {
                Console.WriteLine("\n--- ATM Operation Menu ---");
                Console.WriteLine("1. CREATE (Add Transaction)");
                Console.WriteLine("2. READ (See Transaction History)");
                Console.WriteLine("3. UPDATE (Edit Transaction)");
                Console.WriteLine("4. DELETE (Delete Transaction)");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Choose an option (1-5): ");

                // Take user input (choice)
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        
                        Console.WriteLine("Receive or Transfer?");
                        Console.WriteLine("A. Receive");
                        Console.WriteLine("B. Transfer");
                        string typeInput = Console.ReadLine().ToUpper(); // Convert to uppercase for safety
                        
                        // 1. Declare the variable OUTSIDE the switch
                        string transactionType = "";
                        
                        switch (typeInput)
                        {
                            case "A":
                                transactionType = "Receive"; // 2. Assign the value
                                break;                       // 3. Add the required break
                        
                            case "B":
                                transactionType = "Transfer";
                                break;
                        
                            default:
                                Console.WriteLine("Invalid option selected.");
                                transactionType = "Unknown";
                                break;
                        }
                        
                        // Now you can safely use the data later in your code:
                        Console.WriteLine($"You selected: {transactionType}");
                        Console.WriteLine("Enter amount");
                        decimal amount = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter referrence");
                        string referrence = Console.ReadLine();
                        CreateTransaction(transactionType, amount, referrence);
                        break;

                    case "2":
                        ReadHistory();
                        break;

                    case "3":
                        Console.WriteLine("Enter the Transaction ID to update: ");
                        int updateID = int.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Enter new type transaction: ");
                        string updateType = Console.ReadLine();
                        
                        Console.WriteLine("Enter new amount: ");
                        decimal updateAmount = decimal.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Enter new referrence: ");
                        string updateReferrence = Console.ReadLine();
                        
                        UpdateTransaction(updateID, updateType, updateAmount, updateReferrence);
                        
                        break;

                    case "4":
                        Console.Write("Enter Transaction ID to delete: ");
                        int deleteID = int.Parse(Console.ReadLine());
                        DeleteTransaction(deleteID);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                        
                }
            }
        }

        // CRUD Methods

        // C - CREATE
        static void CreateTransaction(string transactionType, decimal amount, string referrence)
        {
            // Initializing Objects
            Transaction_History newTransaction = new Transaction_History 
            { 
                Transaction_ID = nextID++, 
                Type = transactionType,
                Amount = amount, 
                Referrence = referrence
            };

            database.Add(newTransaction);
            Console.WriteLine($"[Success] Transaction Recorded: {transactionType} RM{amount} for {referrence} (ID: {newTransaction.Transaction_ID})");
        }

        // R - READ

        static void ReadHistory()
        {
            Console.WriteLine("\n--- Transaction History ---");
            if (database.Count == 0)
            {
                // display no record if there's no recorded transactions
                Console.WriteLine(" No transaction recorded.");
                return;
            }

            // display every recorded transactions
            foreach (var p in database)
            {
                Console.WriteLine($"Transaction ID: {p.Transaction_ID}  | Type: {p.Type} | Amount: {p.Amount} | Referrence: {p.Referrence}");
            }
            
        }

        // U - UPDATE

        static void UpdateTransaction(int id, string newType, decimal newAmount, string newReferrence)
        {
            // FInd the item matching ID by using LINQ
            Transaction_History transaction = database.FirstOrDefault(p => p.Transaction_ID == id);

            if (transaction != null)
            {
                transaction.Type = newType;
                transaction.Amount = newAmount;
                transaction.Referrence = newReferrence;
                Console.WriteLine($"[Success] Transaction record for {id} has been updated.");
            }
            else
            {
                Console.WriteLine($"[Error] Transaction record with ID {id} not found.");
            }
        }

        // D - DELETE

        static void DeleteTransaction(int id)
        {
            Transaction_History transaction = database.FirstOrDefault(p => p.Transaction_ID == id);

            if (transaction != null)
            {
                database.Remove(transaction);
                Console.WriteLine("Transaction with ID: {id} is successfully removed.");
            }
            else
            {
                Console.WriteLine("Transaction ID {id} is not found.");
            }
        }
    }
}

