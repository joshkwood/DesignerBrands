using DesignerBrands;
using DesignerBrands.Models;

namespace DesignerBrandsTests;

public class Tests
{
    private RunReport _runReport = new RunReport();
    private Product testClearanceProduct = new Product()
    {
        NormalPrice = (decimal) 59.99,
        ClearancePrice = (decimal) 49.99,
        QuantityInStock = 5,
        IsPriceHidden = false
    };
    private Product testNormalProduct = new Product()
    {
        NormalPrice = (decimal) 29.99,
        ClearancePrice = (decimal) 29.99,
        QuantityInStock = 6,
        IsPriceHidden = false
    };
    private Product testPriceInCartProduct = new Product()
    {
        NormalPrice = (decimal) 89.99,
        ClearancePrice = (decimal) 59.99,
        QuantityInStock = 7,
        IsPriceHidden = true
    };
    private Product testNotEnoughStockProduct = new Product()
    {
        NormalPrice = (decimal) 89.99,
        ClearancePrice = (decimal) 59.99,
        QuantityInStock = 2,
        IsPriceHidden = true
    };

    private ReportData testClearanceReportData = new ReportData()
    {
        TypeDisplayName = "Clearance Price",
        LowPrice = (decimal)39.99,
        HighPrice = (decimal)89.99,
        Quantity = 28
    };

    private ReportData testNormalReportData = new ReportData()
    {
        TypeDisplayName = "Normal Price",
        LowPrice = (decimal)39.99,
        HighPrice = (decimal)39.99,
        Quantity = 10
    };

    private ReportData testPriceInCartReportData = new ReportData()
    {
        TypeDisplayName = "Price in Cart",
        LowPrice = (decimal)69.99,
        HighPrice = 0,
        Quantity = 30
    };

    private List<ReportData> testReportData = new List<ReportData>() { };

    

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CountClearanceProducts()
    {
        _runReport.ClearanceProcess(testClearanceProduct);
        int totalAmount = _runReport.clearancePriceCount;
        Assert.AreEqual(1, totalAmount);
    }
    
    [Test]
    public void CountNormalProducts()
    {
        _runReport.NormalPriceProcess(testNormalProduct);
        int totalAmount = _runReport.normalPriceCount;
        Assert.AreEqual(1, totalAmount);
    }
    [Test]
    public void CountPriceInCartProducts()
    {
        _runReport.PriceInCartProcess(testPriceInCartProduct);
        int totalAmount = _runReport.priceInCartCount;
        Assert.AreEqual(1, totalAmount);
    }
    
    [Test]
    public void NotEnoughStockProducts()
    {
        _runReport.NotEnoughStockProcess(testNotEnoughStockProduct);
        int totalAmount = _runReport.notEnoughStockCount;
        Assert.AreEqual(1, totalAmount);
    }

    [Test]
    public void TestSuccessfulPrintReport()
    {
        testReportData.Add(testClearanceReportData);
        testReportData.Add(testNormalReportData);
        testReportData.Add(testPriceInCartReportData);
        bool result = _runReport.PrintReport(testReportData, 3);
        Assert.AreEqual(true, result);
    }
    [Test]
    public void TestFailedPrintReport()
    {
        //pass empty report data list
        bool result = _runReport.PrintReport(testReportData, 1);
        Assert.AreEqual(false, result);
    }

}
