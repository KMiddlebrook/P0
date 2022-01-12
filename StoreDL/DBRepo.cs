namespace StoreDL;

using Microsoft.Data.SqlClient;
using System.Data;
public class DBRepo
{
    private string _connectionString;
    public DBRepo()
    {
        _connectionString = File.ReadAllText("connectionString.txt");
    }
    //List of customers
    public List<Customer> GetAllCustomers()
    {
        List<Customer> allCustomers = new List<Customer>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string myQuery = "SELECT * FROM Customer";
            
            using (SqlCommand cmd = new SqlCommand(myQuery, connection))
            {
                //try {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer myCustomer = new Customer();
                        myCustomer.Id = reader.GetInt32(0);
                        myCustomer.UserName = reader.GetString(1);
                        myCustomer.Password = reader.GetString(2);
                        myCustomer.Email = reader.GetString(3);
                        allCustomers.Add(myCustomer);
                    }
                }
                //}
                //catch (Exception e) {
                 //   Console.WriteLine(e.Message);
                //}
            } 
            connection.Close();
        }
        return allCustomers;
    }

    //List of Storefronts
    public List<Storefront> GetAllStores()
    {
        List<Storefront> allStores = new List<Storefront>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string myQuery = "SELECT * FROM StoreFront";
            
            using(SqlCommand cmd = new SqlCommand(myQuery, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Storefront store = new Storefront();
                        store.ID = reader.GetInt32(0);
                        store.Name = reader.GetString(1);
                        store.Address = reader.GetString(2);
                        store.City = reader.GetString(3);
                        store.State = reader.GetString(4);
                        allStores.Add(store);
                    }
                }
            } 
            connection.Close();
        }
        return allStores;
    }

    //List of cutomer orders
    public List<Order> GetAllOrders(Customer incomingCustomer)
    {
        List<Order> allOrders = new List<Order>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string myQuery = $"SELECT Orders.ID, Orders.OrderDate, Storefront.Name, Customer.Username, Orders.TOTAL FROM Orders INNER JOIN Customer ON Orders.Customer_ID = Customer.ID INNER JOIN Storefront ON Orders.StoreFront_ID = Storefront.ID WHERE Customer_ID='{incomingCustomer.Id}'";
            
            using(SqlCommand cmd = new SqlCommand(myQuery, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Order order = new Order();
                        order.OrderNumber= reader.GetInt32(0);
                        order.OrderDate= reader.GetString(1);
                        order.StoreName= reader.GetString(2);
                        order.Customer = reader.GetString(3);
                        order.Total = reader.GetDecimal(4);
                        allOrders.Add(order);
                    }
                }
            } 
            connection.Close();
        }
        return allOrders;
    }

        //Inventory for stores
    public List<Inventory> GetStoreInventory(Storefront IncomingStore)
    {
        Storefront incomingStore = IncomingStore;
        List<Inventory> storeInventory = new List<Inventory>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string myQuery = $"SELECT Inventory.ID, Inventory.Quantity, Inventory.ID,Product.Name,Product.Description, Product.Price, StoreFront_ID FROM Inventory INNER JOIN Product ON Inventory.ProductID = Product.ID WHERE StoreFront_ID='{IncomingStore.ID}'";
            
            
            using(SqlCommand cmd = new SqlCommand(myQuery, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Inventory inventory = new Inventory();
                        Product storeProduct = new Product();
                        inventory.ID = reader.GetInt32(0);
                        inventory.Quantity = reader.GetInt32(1);
                        inventory.StoreId = reader.GetInt32(6);
                        storeProduct.ProductName = reader.GetString(3);
                        storeProduct.Description = reader.GetString(4);
                        storeProduct.Price = reader.GetDecimal(5);
                        inventory.Item = storeProduct;
                        storeProduct.ID=reader.GetInt32(2);
                        storeInventory.Add(inventory);
                    }
                }
            } 
            connection.Close();
        }
        return storeInventory;
    }

    //List of all products
    public List<Product> GetAllProducts()
    {
        List<Product> allProduct = new List<Product>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string myQuery = "SELECT * FROM Product";
            
            using(SqlCommand cmd = new SqlCommand(myQuery, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Product product = new Product();
                        product.ID = reader.GetInt32(0);
                        product.ProductName = reader.GetString(1);
                        product.Description= reader.GetString(2);
                        product.Price = reader.GetDecimal(3);
                        allProduct.Add(product);
                    }
                }
            } 
            connection.Close();
        }
        return allProduct;
    }

    
    //add new customers
    public void AddCustomer(Customer customerToAdd)
    {
        DataSet customerSet = new DataSet();
        string selectCmd = "SELECT * FROM Customer";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                
                dataAdapter.Fill(customerSet, "Customer");

                DataTable customerTable = customerSet.Tables["Customer"];
                DataRow newRow = customerTable.NewRow();
                    newRow["Username"]= customerToAdd.UserName;
                    newRow["Password"]= customerToAdd.Password;
                    newRow["Email"]= customerToAdd.Email;
                customerTable.Rows.Add(newRow);
                
                string insertCmd = $"INSERT INTO Customer (Username, Password, Email) VALUES ('{customerToAdd.UserName}','{customerToAdd.Password}','{customerToAdd.Email}')";
                
                dataAdapter.InsertCommand= new SqlCommand(insertCmd, connection);
                
                dataAdapter.Update(customerTable);
            }
        }
    }

    // Add new products to storefront
    public void AddProduct(Product productToAdd)
    {
        DataSet customerSet = new DataSet();
        string selectCmd = "SELECT * FROM Product";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                
                dataAdapter.Fill(customerSet, "Product");

                DataTable customerTable = customerSet.Tables["Product"];
                DataRow newRow = customerTable.NewRow();
                    newRow["Name"]= productToAdd.ProductName;
                    newRow["Description"]= productToAdd.Description;
                    newRow["Price"]= productToAdd.Price;
                customerTable.Rows.Add(newRow);
                
                string insertCmd = $"INSERT INTO Product (Name, Description, Price) VALUES ('{productToAdd.ProductName}','{productToAdd.Description}','{productToAdd.Price}')";
                
                dataAdapter.InsertCommand= new SqlCommand(insertCmd, connection);
                
                dataAdapter.Update(customerTable);
            }
        }
    }

    //Add to inventory
    public void AddToInventory(Inventory inventoryToAdd)
    {
        DataSet customerSet = new DataSet();
        string selectCmd = "SELECT * FROM Inventory";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                
                dataAdapter.Fill(customerSet, "Inventory");

                DataTable customerTable = customerSet.Tables["Inventory"];
                DataRow newRow = customerTable.NewRow();
                    newRow["Quantity"]= inventoryToAdd.Quantity;
                    newRow["ProductID"]= inventoryToAdd.ProductID;
                    newRow["StoreFront_ID"]=inventoryToAdd.StoreId;
                customerTable.Rows.Add(newRow);
                
                string insertCmd = $"INSERT INTO Inventory (Quantity, ProductID, StoreFront_ID) VALUES ('{inventoryToAdd.Quantity}','{inventoryToAdd.ProductID}','{inventoryToAdd.StoreId}')";
                
                dataAdapter.InsertCommand= new SqlCommand(insertCmd, connection);
                
                dataAdapter.Update(customerTable);
            }
        }
    }
    //add new order
    public void AddToOrders(Customer incomingCustomer, Storefront selectedStore,Order incomingOrder)
    {
        DataSet orderSet = new DataSet();
        string selectCmd = "SELECT * FROM Orders";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                
                dataAdapter.Fill(orderSet, "Orders");

                DataTable orderTable = orderSet.Tables["Orders"];
                DataRow newRow = orderTable.NewRow();
                    newRow["OrderDate"]= incomingOrder.OrderDate;
                    newRow["StoreFront_ID"]= selectedStore.ID;
                    newRow["Customer_ID"]=incomingCustomer.Id;
                    newRow["TOTAL"] = incomingOrder.Total;
                orderTable.Rows.Add(newRow);
                
                string insertCmd = $"INSERT INTO Orders (OrderDate, StoreFront_ID, Customer_ID, TOTAL) VALUES ('{incomingOrder.OrderDate}', '{selectedStore.ID}', '{incomingCustomer.Id}', '{incomingOrder.Total}')";
                
                dataAdapter.InsertCommand= new SqlCommand(insertCmd, connection);
                
                dataAdapter.Update(orderTable);
            }
        }
    }
}