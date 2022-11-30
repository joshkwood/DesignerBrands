namespace DesignerBrands.Models;

public class ReportData
{
    public int Id { get; set; }
    public string TypeDisplayName { get; set; }
    public decimal LowPrice { get; set; }
    public decimal HighPrice { get; set; }
    public int Quantity { get; set; } = 0;
    
}