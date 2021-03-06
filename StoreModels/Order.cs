namespace Models;

public class Order
{
    public int OrderNumber { get; set; }
    public string? OrderDate { get; set; }
    public string? Customer { get; set; }
    public string? StoreName { get; set; }
    public List<LineItem>? LineItems { get; set; }
    public decimal Total { get; set; }
    public decimal CalculateTotal() {
        //a method that would go through each lineitem in LineItems property
        //and sets the total property of the particular order object
        decimal total = 0;
        if(this.LineItems?.Count > 0)
        {
            foreach(LineItem lineitem in this.LineItems)
            {
                //multiply the product's price by how many we're buying
                total += lineitem.Item.Price * lineitem.Quantity;
            }
        }
        this.Total = total;
        return total;
    }
}