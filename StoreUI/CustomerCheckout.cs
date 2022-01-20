namespace UI;
using Models;
using StoreDL;

public class CustomerCheckout
{

    public void customerCheckout(Customer incomingCustomer, Storefront selectedStorefront)
    {
        bool exit = false;
        bool stopAdding = false;
        List<Inventory> StoreInventory = new List<Inventory>();
        DBRepo dbRepoOrders = new DBRepo();
        List<LineItem> cart = new List<LineItem>();
        Order newOrder = new Order();

        Console.WriteLine($"You Selected:\n{selectedStorefront.Name}\n******************");
        while(!exit)
        {
            Console.WriteLine("[1] Add Items to Cart");
            Console.WriteLine("[2] See Cart");
            Console.WriteLine("[3] Checkout");
            Console.WriteLine("[4]Go back");
            string? input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    
                    while(!stopAdding)
                    {
                        LineItem addToCart = new LineItem();
                        Product productToAdd = new Product();
                        Console.WriteLine("Select an item to add to cart: ");

                        StoreInventory = dbRepoOrders.GetStoreInventory(selectedStorefront);
                        for (int i=0; i<StoreInventory.Count;i++ )
                        {
                            Console.WriteLine($"[{i}] Name: {StoreInventory[i].Item.ProductName}\nDescription: {StoreInventory[i].Item.Description}");
                            Console.WriteLine($"Price: {StoreInventory[i].Item.Price}");
                            Console.WriteLine("**************");
                        }
                        int selectedProduct = Int32.Parse(Console.ReadLine());
                        Console.WriteLine($"you selected: {StoreInventory[selectedProduct].Item.ProductName}");
                        Console.WriteLine("How many would you like to add:");
                        int quantity = Int32.Parse(Console.ReadLine());
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
                case "2":
                foreach(LineItem item in cart)
                    {
                        Console.WriteLine($"Name: {item.Item.ProductName} {item.Quantity}");
                    }
                    newOrder.LineItems = cart;
                    newOrder.CalculateTotal();
                    Console.WriteLine($"Total: {newOrder.Total}");
                    if(newOrder.Total == 0)
                    {
                        Console.WriteLine("No items in cart");
                    }
                    
                break;
                case "3":
                bool checkoutSuccessfull = false;
                if(newOrder.Total == 0) 
                {
                    Console.WriteLine("Add items to cart");
                }
                if(newOrder.Total > 0)
                {
                    newOrder.OrderDate = DateOnly.FromDateTime(DateTime.Now).ToString();
                    dbRepoOrders.AddToOrders(incomingCustomer,selectedStorefront,newOrder);
                    Console.WriteLine("Your order has been submitted");
                    Console.WriteLine($"Thank you for shopping at {selectedStorefront.Name}");
                    checkoutSuccessfull = true;
                    if(checkoutSuccessfull == true)
                    {
                        exit =true;
                    }
                }
                break;
                case "4":
                exit = true;
                break;
                default:
                Console.WriteLine("invalid input");
                break;
            }
        }
    }
}