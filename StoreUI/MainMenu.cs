namespace UI;
using Models;
using StoreDL;

public class MainMenu
{
    public void mainMenuStart()
    {

        store manageStores = new store();
        CustomerMenu customerSignIn = new CustomerMenu();
        ManagerMenu managerPortal = new ManagerMenu();
        DBRepo dbRepo = new DBRepo();
        List<Customer> customers = dbRepo.GetAllCustomers();

        Customer Manager = new Customer();
        string managerUsername = "Admin";
        string managerPassword = "admin123";
        Manager.UserName = managerUsername;
        Manager.Password = managerPassword;
        AllCustomers.allCustomers.Add(Manager);

        bool exit = false;
        Console.WriteLine("Welcome to Batons LA!");

        while (!exit)
        {
            Console.WriteLine(" ");
            Console.WriteLine("===== Main Menu =====");
            Console.WriteLine(" ");
            Console.WriteLine("[1] Sign In");
            Console.WriteLine("[2] Create New Account");
            Console.WriteLine("[3] Admin Login");
            Console.WriteLine("[x] Exit");
            string? userInput = Console.ReadLine();


            switch (userInput)
            {
                case "1": 
                
                    bool successfulLogin = false;

                    Console.Write("Enter Username: ");
                    string? CustomerUsername = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string? CustomerPassword = Console.ReadLine();
                    
                    foreach(Customer existing in customers)
                    {
                        if(CustomerUsername == existing.UserName && CustomerPassword == existing.Password )
                        {
                            customerSignIn.customerMenu(existing);
                            successfulLogin = true;
                        }
                    }
                    if(!successfulLogin)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("Invalid username or password.");
                        Console.WriteLine("Please try again.");
                    }
                break;
                case "2":
                    Customer newCustomer = new Customer();
                    Console.Write("Enter a Username: ");
                    string? username = Console.ReadLine();
                    Console.Write("Enter a Password: ");
                    string? password = Console.ReadLine();
                    Console.Write("Enter an Email: ");
                    string? email = Console.ReadLine();
                    newCustomer.UserName = username;
                    newCustomer.Password = password;
                    newCustomer.Email = email;
                    dbRepo.AddCustomer(newCustomer);
                
                    Console.WriteLine("Success! Your account has been created.");
                break;
                case "3":
                    bool ManagerLogIn = false;
                    
                    Console.Write("Enter Username: ");
                    string? Username = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string? Password = Console.ReadLine();
                    if(Username == Manager.UserName && Password == Manager.Password )
                    {
                        managerPortal.managerMenu();
                        ManagerLogIn = true;
                    }
                    else if(!ManagerLogIn)
                    {
                        Console.WriteLine("Invalid Username or Password");
                    }
                break;
                case "x":
                    exit = true;
                break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                break;
            }
        }
    }
}