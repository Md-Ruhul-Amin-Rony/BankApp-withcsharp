using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Channels;
using System.Xml.Linq;


namespace SubAccounts
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a dictionary to store the username, password, and list of sub accounts for each user
            Dictionary<string, Tuple<string, List<Tuple<string, double>>>> users = new Dictionary<string, Tuple<string, List<Tuple<string, double>>>>();
            /*users[key]: the value of the dictionary element with the key key. The value is a Tuple<string, List<Tuple<string, >>>.
            users[key].Item1: the first element of the tuple, which is a string (the password).
            users[key].Item2: the second element of the tuple, which is a list of tuples (the sub accounts).
            users[key].Item2[index]: the element at the index position in the list of tuples. This is a Tuple<string, double> representing a sub account.
            users[key].Item2[index].Item1: the first element of the sub account tuple, which is a string (the sub account name).
            users[key].Item2[index].Item2: the second element of the sub account tuple, which is a double (the balance of the sub account).*/

           
            users.Add("user1", Tuple.Create("password1", new List<Tuple<string, double>> { Tuple.Create("checking", 100.00), Tuple.Create("savings", 200.00), Tuple.Create("investment", 300.00), Tuple.Create("vacation", 400.00)}));
            users.Add("user2", Tuple.Create("password2", new List<Tuple<string, double>> { Tuple.Create("checking", 100.00),Tuple.Create("emergency", 200.00) }));
            users.Add("user3", Tuple.Create("password3", new List<Tuple<string, double>> { Tuple.Create("checking", 100.00), Tuple.Create("savings", 200.00), Tuple.Create("pension", 300.00) }));
            users.Add("user4", Tuple.Create("password4", new List<Tuple<string, double>> { Tuple.Create("checking", 100.00), Tuple.Create("savings", 200.00), Tuple.Create("investment", 300.00), Tuple.Create("emergency", 400.00),Tuple.Create("pension", 500.00) }));
            users.Add("user5", Tuple.Create("password5", new List<Tuple<string, double>> { Tuple.Create("checking", 100.00), Tuple.Create("savings", 200.00), Tuple.Create("investment", 300.00), Tuple.Create("emergency", 400.00), Tuple.Create("vacation", 500.00),Tuple.Create("family",600.00) }));


            List<Tuple<string, string,string, double, DateTime>> transactionHistory = new List<Tuple<string, string, string, double , DateTime>>();
            //create tuple list to store all the transaction history with acountdetails and datetime.
            //Tuple<string, string, string, double, DateTime>[] transactionHistory= transactionhistory.ToArray();
        
            int loginAttempts = 0;
            bool success = true;// Prompt the user to enter their login information
            while (success)
            {
            Main:
            Console.WriteLine("Welcome to Banking System. For Login follow the instructions below: ");
            Console.WriteLine();
            Console.WriteLine("You can take 3 attempts to successfully logged in, otherwise program will shut down automatically");


            Console.WriteLine("===================================================================");
            Console.WriteLine();
            Console.Write("Enter your username: ");
            string username = Console.ReadLine().ToLower();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            




                // Validate the login information
                if (users.ContainsKey(username) && users[username].Item1 == password)
                {
                    Console.WriteLine("=================");
                    Console.WriteLine();

                    Console.WriteLine("Login successful!");

                    bool exit = false;
                    while (!exit)
                    {
                        // Making options for user to choose by typing the number.
                        Console.WriteLine("======================================");
                        Console.WriteLine();
                        Console.WriteLine("What do you want to do? Please type the number that you want to do: ");
                        Console.WriteLine("1. Show Account details:");
                        Console.WriteLine("2. Transfer money between sub accounts");
                        Console.WriteLine("3. Withdraw Money:");
                        Console.WriteLine("4. Log out from your account");
                        Console.WriteLine("5. Deposit money:");
                        Console.WriteLine("6. Check the balance of a sub account");
                        Console.WriteLine("7. Show transactions details of each account:");
                        Console.WriteLine("8.To Close the program: ");
                        Console.WriteLine("=====================================");
                        Console.Write("Enter the number you choose to do and Tryck Enter button: ");

                        int choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                // To show accounts details:
                                double total = 0;
                                Console.WriteLine();
                                Console.WriteLine("Welcome to your account!");
                                Console.WriteLine("========================");
                                Console.WriteLine();
                                Console.WriteLine("Your sub accounts are:");
                                foreach (var subAccount in users[username].Item2)
                                {
                                    Console.WriteLine($"{subAccount.Item1}: {subAccount.Item2:C}");
                                    /* the code provided, {subAccount.Item1}: {subAccount.Item2:C} is a string interpolation expression. 
                                      It is used to include the values of the Item1 and Item2 properties of the subAccount variable in a string.
                                     The subAccount variable is of type Tuple<string, double>, and it represents a sub account for the user, 
                                    with the Item1 property representing the name of the sub account and the Item2 property representing the balance of the sub account.
                                    The :C format specifier in the string interpolation expression is used to format the value of subAccount.Item2 as a currency value.*/
                                    total += subAccount.Item2;
                                    /*The statement total += subAccount.Item2 is adding the balance 
                                     of the current sub-account represented by the variable subAccount to the total balance.*/
                                }
                                Console.WriteLine($"Total balance: {total:C}");
                                break;
                            case 2:
                                // Transfer money between sub accounts
                                Console.WriteLine("Enter the name of the source sub account: ");
                                string sourceName = Console.ReadLine().ToLower();
                                Console.WriteLine("Enter the name of the destination sub account: ");
                                string destinationName = Console.ReadLine().ToLower();
                                Console.WriteLine("Enter the amount of money you want to transfer: ");
                                double amount = double.Parse(Console.ReadLine());
                                // destination sub accounts
                                /*  'sourceIndex' and 'destinationIndex' are variables of type int that are initialized to -1. 
                                  These variables are used to store the indices of the source and destination sub accounts in the list of sub accounts for the user.
                                    The sourceIndex variable is used to store the index of the source sub account, which is the sub account from which the money is being transferred.
                                The destinationIndex variable is used to store the index of the destination sub account, which is the sub account to which the money is being transferred. */
                                int sourceIndex = -1;
                                int destinationIndex = -1;
                                for (int i = 0; i < users[username].Item2.Count; i++)
                                {
                                    if (users[username].Item2[i].Item1 == sourceName)
                                    {
                                        sourceIndex = i;
                                    }
                                    if (users[username].Item2[i].Item1 == destinationName)
                                    {
                                        destinationIndex = i;
                                    }
                                }

                                // Validate the input
                                if (sourceIndex != -1 && destinationIndex != -1 && users[username].Item2[sourceIndex].Item2 >= amount)
                                {
                                    // Transfer the money from the source sub account to the destination sub account
                                    users[username].Item2[sourceIndex] = Tuple.Create(sourceName, users[username].Item2[sourceIndex].Item2 - amount);
                                    users[username].Item2[destinationIndex] = Tuple.Create(destinationName, users[username].Item2[destinationIndex].Item2 + amount);

                                    transactionHistory.Add(Tuple.Create(username, sourceName, destinationName, amount, DateTime.Now));
                                    //adding all the transactions to the transactionHistory List.


                                    Console.WriteLine("Transfer successful!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Transfer failed.");
                                }
                                break;
                            case 3:
                                // Withdraw money
                                Console.WriteLine("Enter the name of the sub account: ");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter the amount to withdraw: ");
                                amount = double.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the password of the account: ");
                                string enteredPassword = Console.ReadLine();

                                // Find the index of the sub account
                                int index = -1;
                                for (int i = 0; i < users[username].Item2.Count; i++)
                                {
                                    if (users[username].Item2[i].Item1 == name)
                                    {
                                        index = i;
                                        break;
                                    }
                                }

                                // Validate the input
                                if (index != -1 && enteredPassword == users[username].Item1 && users[username].Item2[index].Item2 >= amount)
                                {
                                    // Withdraw the money
                                    users[username].Item2[index] = Tuple.Create(name, users[username].Item2[index].Item2 - amount);

                                    transactionHistory.Add(Tuple.Create(username, name, "Withdraw", -amount, DateTime.Now));


                                    Console.WriteLine("Withdrawal successful!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Withdrawal failed.Try again by choosing from the menubar below:");
                                }
                                break;

                            case 4:
                                // Log out
                                exit = true;
                                goto Main;
                                break;


                            case 5:
                                // Deposit money
                                Console.WriteLine("Enter the name of the sub account: ");
                                name = Console.ReadLine();
                                Console.WriteLine("Enter the amount to deposit: ");
                                amount = double.Parse(Console.ReadLine());

                                // Find the index of the sub account
                                index = -1;
                                for (int i = 0; i < users[username].Item2.Count; i++)
                                {
                                    if (users[username].Item2[i].Item1 == name)
                                    {
                                        index = i;
                                        break;
                                    }
                                }

                                // Validate the input
                                if (index != -1)
                                {
                                    // Deposit the money
                                    users[username].Item2[index] = Tuple.Create(name, users[username].Item2[index].Item2 + amount);
                                    /* is updating the balance of the current sub-account at the specific index of the list of sub-accounts.
                                     The old sub-account tuple is being replaced with the new tuple which has the updated balance.
                                    This way the deposit amount is added to the existing balance of the sub-account.*/
                                    transactionHistory.Add(Tuple.Create(username, name, "Deposit", amount, DateTime.Now));


                                    Console.WriteLine("Deposit successful!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Deposit failed.");
                                }
                                break;
                            case 6:
                                // Check the balance of a sub account
                                Console.WriteLine("Enter the name of the sub account: ");
                                name = Console.ReadLine().ToLower();

                                // Find the index of the sub account
                                index = -1;    /* index is a variable of type int that is used to store the index of a sub account in the list of sub accounts for the user. It is initialized to -1, 
                                                which is used as a sentinel(a specail value to terminate a loop) value to indicate that the index of the sub account has not been found yet.
                                                This code iterates through the list of sub accounts for the user, and for each sub account, 
                                                it compares the name of the sub account (stored in users[username].Item2[i].Item1) to the name of the sub account being searched for (stored in name). 
                                                If the name of the sub account matches the name being searched for, it sets the value of index to the index of the sub account in the list (stored in i) and breaks out of the loop. */
                                for (int i = 0; i < users[username].Item2.Count; i++)
                                {
                                    if (users[username].Item2[i].Item1 == name)
                                    {
                                        index = i;
                                        break;
                                    }
                                }

                                // Validate the input
                                if (index != -1)
                                {
                                    Console.WriteLine($"The balance of sub account {name} is {users[username].Item2[index].Item2:C}.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please try again.");
                                }
                                break;


                            case 7:
                                // View transaction history
                                Console.WriteLine("Transaction history:");
                                foreach (var history in transactionHistory)
                                {
                                    if (history.Item1 == username)
                                        Console.WriteLine($"From Account: {history.Item2} To Account: {history.Item3}, Amount: {history.Item4:C}, Date: {history.Item5:G}");
                                }

                                break;
                            case 8:// To close the program.
                           
                                Console.WriteLine("Please tap Enter to close the program.");
                                exit = true;
                                success = false;
                                break;









                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                }
                else
                {
                    loginAttempts++;
                    Console.WriteLine("Invalid login information. Please try again.");
                    Console.WriteLine("============================================");
                    Console.WriteLine("your Attempt to log in is: "+ loginAttempts);
                    if (loginAttempts>=3)
                    {
                        Console.WriteLine("You have typed invalid input for 3 times and now program is closing.");
                        Console.WriteLine("Please tryck the Enter button to close the program");
                        success = false;
                    }

                    else
                    {
                        success= true;  
                    }
                }
            }
        }
        
	

	}
       
	

	}





