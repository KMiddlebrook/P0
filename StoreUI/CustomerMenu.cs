namespace UI;
using Models;
using StoreDL;
public class CustomerMenu
{
    public void customerMenu(Customer currentCustomer)
    {
        Console.WriteLine($"{currentCustomer.Id}");
        DBRepo dbRepoStores = new DBRepo();
        // List<Order> SelectedStoreOrder = new List<Order>();
        List<Inventory> StoreInventory = new List<Inventory>();
        List<Order> newOrderList = new List<Order>();
        List<LineItem> cart = new List<LineItem>();
        bool exit = false;
        Console.WriteLine("Welcome back!");
        Console.WriteLine(" ");
        while(!exit)
        {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("[1] See All Stores");
        Console.WriteLine("[2] Shop");
        Console.WriteLine("[3] See Cart");
        Console.WriteLine("[4] See Order History");
        Console.WriteLine("[x] Sign Out");
        string? input = Console.ReadLine();

            switch(input)
            {
                case "1":
                List<Storefront> allstores = dbRepoStores.GetAllStores();
                foreach(Storefront store in allstores)
                {
                    Console.WriteLine($"{store.Name}\n{store.Address}\n{store.City}\n{store.State}");
                    Console.WriteLine("------------------------");
                }
                break;
                case "2":
                    bool stopAdding = false;
                    List<Storefront> selectStores = dbRepoStores.GetAllStores();
                    Console.WriteLine("Select a Store:");
                    for(int i = 0;i < selectStores.Count;i++)
                    {
                        Console.WriteLine($"[{i}] {selectStores[i].Name}");
                    }
                    int selection = Int32.Parse(Console.ReadLine() ?? "");

                    Console.WriteLine($"You Selected:\n{selectStores[selection].Name}\n------------------------");

                    while(!stopAdding)
                    {
                        LineItem addToCart = new LineItem();
                        Product productToAdd = new Product();
                        Console.WriteLine("Select an item to add to cart: ");

                        StoreInventory = dbRepoStores.GetStoreInventory(selectStores[selection]);
                        for (int i=0; i<StoreInventory.Count;i++ )
                        {
                            Console.WriteLine($"[{i}] Name: {StoreInventory[i].Item.ProductName}\nDescription: {StoreInventory[i].Item.Description}");
                            Console.WriteLine($"Price: {StoreInventory[i].Item.Price}");
                            Console.WriteLine("------------------------");
                        }
                        int selectedProduct = Int32.Parse(Console.ReadLine());
                        Console.WriteLine($"you selected: {StoreInventory[selectedProduct].Item.ProductName}");
                        Console.WriteLine("How many would you like to add:");
                        int quantity = Int32.Parse(Console.ReadLine() ?? "");
                        //set values to product
                        productToAdd.ProductName = StoreInventory[selectedProduct].Item.ProductName;
                        productToAdd.Description = StoreInventory[selectedProduct].Item.Description;
                        productToAdd.Price = StoreInventory[selectedProduct].Item.Price;
                        productToAdd.ID = StoreInventory[selectedProduct].Item.ID;
                        //set values to lineitem
                        addToCart.Item = productToAdd;
                        addToCart.Quantity = quantity;
                        //add to cart list
                        cart.Add(addToCart);

                        Console.WriteLine("would you like to add another? [y/n]");
                        string? addAnother = Console.ReadLine();
                        if(addAnother == "n")
                        {
                            stopAdding = true;
                        }
                    }
                break;
                case "3":
                    foreach(LineItem item in cart)
                    {
                        Console.WriteLine($"Name: {item.Item.ProductName} {item.Quantity}");
                    }
                break;
                case "4":
                Console.WriteLine("Order History");
                foreach(Order OrderHistory in currentCustomer.Orders)
                {
                    Console.WriteLine($"Customer: {OrderHistory.Customer}\nDate of purchase: {OrderHistory.OrderDate}\nPurchase Total: {OrderHistory.Total}");
                }
                break;
                case "x":
                    exit = true;
                break;
                default:
                    Console.WriteLine("Invalid Input");
                break;
            }
        }
        
    }
}