namespace Models;

public class Inventory 
{
    public int ID { get; set; }
    public int StoreId { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public Product Item { get; set; }
}