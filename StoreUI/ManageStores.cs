namespace UI;
using Models;
using StoreDL;

public class store
{
    
    ManageInventory ManageInventory = new ManageInventory();
    List<Inventory> StoreInventory = new List<Inventory>();
    
    public void manageStores(List<Storefront> IncomingStores)
    {
        DBRepo dbRepo = new DBRepo();
        bool goBack = false;

        while(!goBack)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Create New Store");
            Console.WriteLine("[2] Manage Inventory");
            Console.WriteLine("[3] Admin Dashboard");
            string? adminInput = Console.ReadLine();

            switch(adminInput)
            {
                case "1":
                    Storefront newStore = new Storefront();
                    Console.WriteLine("Enter Store Name:");
                    string? newName = Console.ReadLine();
                    Console.WriteLine("Enter Address:");
                    string? newAddress = Console.ReadLine();
                    Console.WriteLine("Enter City:");
                    string? newCity = Console.ReadLine();
                    Console.WriteLine("Enter State:");
                    string? newState = Console.ReadLine();
                    newStore.Name = newName;
                    newStore.Address= newAddress;
                    newStore.City = newCity;
                    newStore.State = newState;
                    dbRepo.AddStore(newStore);
                break;
                case "2":
                    DBRepo dbRepoStores = new DBRepo();
                    List<Storefront> allstores = dbRepoStores.GetAllStores();
                    Console.WriteLine("Select a store:");
                    for(int i = 0; i < allstores.Count; i++)
                    {
                        Console.WriteLine($"[{i}] {allstores[i].Name}");
                    }
                    int selection = Int32.Parse(Console.ReadLine() ?? "");
                    ManageInventory.manageInventory(allstores[selection], selection);
                break;
                case "3":
                    goBack = true;
                break;
                default:
                    Console.WriteLine("Invalid Input");
                break;
            }
        }
    }
}