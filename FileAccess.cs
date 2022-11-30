using DesignerBrands.Models;
using Type = DesignerBrands.Models.Type;

namespace DesignerBrands;

public class FileAccess
{
    public FileInput ReadInputFile(string fileLocation)
    {
        FileInput fileInputs = new FileInput();
        List<Type> newTypeList = new List<Type>();
        List<Product> newProductList = new List<Product>();

        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), fileLocation);

        using (StreamReader sr = new StreamReader(path))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');
                Type newType = new Type();
                Product newProduct = new Product();
                
                if (values[0] == "Type")
                {
                    newType.Id = values[1];
                    newType.TypeDisplayName = values[2];
                    newTypeList.Add(newType);
                }
                else
                {
                    newProduct.NormalPrice = decimal.Parse(values[1]);
                    newProduct.ClearancePrice = decimal.Parse(values[2]);
                    newProduct.QuantityInStock = Int32.Parse(values[3]);
                    newProduct.IsPriceHidden = bool.Parse(values[4]);
                    newProductList.Add(newProduct);
                }

                fileInputs.Types = newTypeList;
                fileInputs.Products = newProductList;
            }
        }
        
        return fileInputs;
    }
}