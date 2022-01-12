namespace UI;
using StoreDL;
using Models;

public class ManagerMenu
{
    List<Inventory> StoreInventory = new List<Inventory>();
    
    public void managerMenu()
    {
    
        store manageStores = new store();
        bool exit = false;

        while(!exit)
        {
            Console.WriteLine("----Admin Dashboard----");
            Console.WriteLine("[1] Manage Store Info");
            Console.WriteLine("[2] View All Stores");
            Console.WriteLine("[3] View All Customers ");
            Console.WriteLine("[4] Exit");
            string? input = Console.ReadLine();

            switch(input)
            {
                case "1":
                DBRepo dbManagerStore = new DBRepo();
                List<Storefront> manageStore = dbManagerStore.GetAllStores();
                manageStores.manageStores(manageStore);
                break;
                case "2":
                DBRepo dbRepoStores = new DBRepo();
                List<Storefront> allstores = dbRepoStores.GetAllStores();
                foreach(Storefront store in allstores)
                    {
                        Console.WriteLine($"{store.Name}\n{store.Address}\n{store.City}\n{store.State}");
                        Console.WriteLine("************************************");
                    }
                break;
                case "3":
                DBRepo dbRepo = new DBRepo();
                List<Customer> customers = dbRepo.GetAllCustomers();
                foreach(Customer existingCustomers in customers)
                {
                    Console.WriteLine($"Customer: {existingCustomers.UserName} Email: {existingCustomers.Email}");
                    Console.WriteLine("***************************");
                }
                break;
                case "4":
                exit = true;
                break;
                default:
                Console.WriteLine("Invalid input");
                break;
            }
        }
    }
}