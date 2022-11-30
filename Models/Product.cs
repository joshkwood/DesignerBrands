namespace DesignerBrands.Models;

public class Product
{
    public decimal NormalPrice { get; set; }
    public decimal ClearancePrice { get; set; }
    public int QuantityInStock { get; set; }
    public bool IsPriceHidden { get; set; }
}