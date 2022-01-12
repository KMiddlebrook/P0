namespace Models;

public class Storefront
{
    public int ID { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Name { get; set; }
    public List<Inventory> Inventories { get; set; }
    public List<Order> Orders { get; set; }
}