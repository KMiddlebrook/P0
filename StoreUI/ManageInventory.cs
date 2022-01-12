namespace UI;
using Models;
using StoreDL;

public class ManageInventory
{
    public void manageInventory(Storefront IncomingStore, int selection )
    {
        bool exit = false;
        while(!exit)
        {
            Console.WriteLine("What would you like to do?");
            //Console.WriteLine("[1] Create New Product");
            Console.WriteLine("[2] Add Product to Inventory");
            Console.WriteLine("[3] View All Products");
            Console.WriteLine("[x] Exit");
            string? input = Console.ReadLine();

            switch(input)
            {
                /*
                case "1":
                    DBRepo addProduct = new DBRepo();
                    Product newProduct = new Product();
                    Console.WriteLine("Enter Product Name:");
                    string? productName = Console.ReadLine();
                    newProduct.ProductName = productName;
                    Console.WriteLine("Enter Product Description");
                    string? productDescription = Console.ReadLine();
                    newProduct.Description = productDescription;
                    Console.WriteLine("Enter Product Price");
                    decimal productPrice = Decimal.Parse(Console.ReadLine());
                    newProduct.Price= productPrice;
                    addProduct.AddProduct(newProduct);
                break; */
                case "2":
                    Console.WriteLine("Select a product to add:");
                    DBRepo dbProducts = new DBRepo();
                    List<Product> allProducts = dbProducts.GetAllProducts();
                        for (int i = 0; i < allProducts.Count;i++)
                        {
                            Console.WriteLine($"[{i}] ID: {allProducts[i].ID} Product Name: {allProducts[i].ProductName}");
                            Console.WriteLine($"Description: {allProducts[i].Description}");
                            Console.WriteLine($"Price: {allProducts[i].Price}");
                        }
                    int selectedProduct = Int32.Parse(Console.ReadLine());
                    Console.Write("Quantity: ");
                    int productQuantity = Int32.Parse(Console.ReadLine());
                    Inventory addToInventory = new Inventory ();
                    addToInventory.Quantity= productQuantity;
                    addToInventory.ProductID = selectedProduct;
                    addToInventory.StoreId = IncomingStore.ID;
                    dbProducts.AddToInventory(addToInventory);
                break;
                case "3":
                    DBRepo dbStoreInventory = new DBRepo();
                    List<Inventory> storeInventory= dbStoreInventory.GetStoreInventory(IncomingStore);
                    foreach (Inventory inventory in storeInventory)
                    {
                        // Console.WriteLine($"{inventory.Quantity}");
                        Console.WriteLine($"ID: {inventory.Item.ID}");
                        Console.WriteLine($"Item: {inventory.Item.ProductName} Description: {inventory.Item.Description}");
                        Console.WriteLine($"Price: {inventory.Item.Price} Quantity: {inventory.Quantity}");
                        Console.WriteLine("************************");
                        
                    }
                    break;
                case "4":
                    exit = true;
                break;
                default:
                    Console.WriteLine("Invalid Input");
                break;
            }
        }
    }
}