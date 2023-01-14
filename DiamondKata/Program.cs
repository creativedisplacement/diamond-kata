

namespace DiamondKata
{
    internal partial class Program
    {
        
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter a letter");
            var characterString = Console.ReadLine();
            
            IDiamond diamond = new Diamond();
            var plotDiamond = diamond.PlotDiamond(characterString);
            
            Console.WriteLine(plotDiamond);
            
            Console.WriteLine("Press any key to exit");
            
            Console.ReadKey();
        }
    }
}