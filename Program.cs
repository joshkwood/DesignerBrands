using DesignerBrands.Models;

namespace DesignerBrands;

public class Program
{
    static void Main(string[] args)
    {
        FileAccess fileAccess = new FileAccess();
        RunReport runReport = new RunReport();
        FileInput fileInput = fileAccess.ReadInputFile(args[0]);
        
        runReport.ProcessInput(fileInput);
    }
}