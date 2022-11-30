using DesignerBrands.Models;
using System.Linq;

namespace DesignerBrands;

public class RunReport
{
    private ReportData clearancePriceReport = new ReportData()
    {
        TypeDisplayName = "Clearance Price"
    };
    private ReportData normalPriceReport = new ReportData()
    {
        TypeDisplayName = "Normal Price"
    };
    private ReportData priceInCartReport = new ReportData()
    {
        TypeDisplayName = "Price in Cart"
    };
    private List<ReportData> reportDataList = new List<ReportData>(){};
    public int priceInCartCount = 0;
    public int clearancePriceCount = 0;
    public int normalPriceCount = 0;
    public int notEnoughStockCount = 0;
    private decimal priceInCartHigh = 0;
    private decimal priceInCartLow = 100;
    private decimal clearancePriceHigh = 0;
    private decimal clearancePriceLow = 100;
    private decimal normalPriceHigh = 0;
    private decimal normalPriceLow = 100;
    public bool ProcessInput(FileInput fileInput)
    {
        bool success = false;
        if (fileInput.Products.Count == 0)
        {
            PrintReport(reportDataList, fileInput.Types.Count);
            return success;
        }
        else
        {
            for (int i = 0; i < fileInput.Products.Count; i++)
            {
                if (fileInput.Products[i].QuantityInStock < 3)
                {
                    NotEnoughStockProcess(fileInput.Products[i]);
                    success = true;
                }
                else if (fileInput.Products[i].IsPriceHidden)
                {
                    PriceInCartProcess(fileInput.Products[i]);
                    success = true;
                }
                else if (fileInput.Products[i].ClearancePrice < fileInput.Products[i].NormalPrice)
                {
                    ClearanceProcess(fileInput.Products[i]);
                    success = true;
                }
                else
                {
                    NormalPriceProcess(fileInput.Products[i]);
                    success = true;
                }
            }

            reportDataList.Add(normalPriceReport);
            reportDataList.Add(clearancePriceReport);
            reportDataList.Add(priceInCartReport);
            List<ReportData> sortedReportList = reportDataList.OrderByDescending(o => o.Quantity).ToList();
            PrintReport(sortedReportList, fileInput.Types.Count);
            return success;
        }
    }
    public bool PrintReport(List<ReportData> reportDataList, int typeCount)
    {
        bool success = false;
        if (reportDataList.Count == 0)
        {
            Console.WriteLine("There is no data to report at this time.");
            return success;
        }
        else
        {
            for(int i = 0; i < typeCount; i++)
            {
                if (reportDataList[i].Quantity == 0 || reportDataList[i].TypeDisplayName == "Price in Cart")
                {
                    Console.WriteLine($"{reportDataList[i].TypeDisplayName}: {reportDataList[i].Quantity} products");
                    success = true;
                } else if (reportDataList[i].HighPrice == 0 || reportDataList[i].LowPrice == 0)
                {
                    if (reportDataList[i].HighPrice == 0)
                    {
                        Console.WriteLine($"{reportDataList[i].TypeDisplayName}: {reportDataList[i].Quantity} products @ {reportDataList[i].LowPrice}");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine($"{reportDataList[i].TypeDisplayName}: {reportDataList[i].Quantity} products @ {reportDataList[i].HighPrice}");
                        success = true;
                    }
                }
                else
                {
                    Console.WriteLine($"{reportDataList[i].TypeDisplayName}: {reportDataList[i].Quantity} products @ {reportDataList[i].LowPrice}-{reportDataList[i].HighPrice}");
                    success = true;
                }
            }
        }
        return success;
    }
    public void NotEnoughStockProcess(Product product)
    {
        notEnoughStockCount ++;
    }
    public void PriceInCartProcess(Product product)
    {
        priceInCartCount ++;
        priceInCartReport.Quantity = priceInCartCount;
        if(product.ClearancePrice < product.NormalPrice)
        {
            ClearanceProcess(product);
        }
        else
        {
            NormalPriceProcess(product);
        }
    }

    public void ClearanceProcess(Product product)
    {
        clearancePriceCount ++;
        clearancePriceReport.Quantity = clearancePriceCount;
        if (product.ClearancePrice < clearancePriceLow)
        {
            clearancePriceLow = product.ClearancePrice;
            clearancePriceReport.LowPrice = clearancePriceLow;
        } else if (product.ClearancePrice > clearancePriceLow && product.ClearancePrice > clearancePriceHigh)
        {
            clearancePriceHigh = product.ClearancePrice;
            clearancePriceReport.HighPrice = clearancePriceHigh;
        }
    }

    public void NormalPriceProcess(Product product)
    {
        normalPriceCount++;
        normalPriceReport.Quantity = normalPriceCount;
        if (product.ClearancePrice < normalPriceLow)
        {
            normalPriceLow = product.ClearancePrice;
            normalPriceReport.LowPrice = normalPriceLow;
        } else if (product.ClearancePrice > normalPriceLow && product.ClearancePrice > normalPriceHigh)
        {
            normalPriceHigh = product.ClearancePrice;
            normalPriceReport.HighPrice = normalPriceHigh;
        }
    }

}