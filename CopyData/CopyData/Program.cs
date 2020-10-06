using System;
using System.Text;
using System.Threading.Tasks;

namespace CopyData
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                Console.WriteLine("Type full path to the source file");
                var inputPath = Console.ReadLine();

                Console.WriteLine("Type full path to the destination file");
                var outputFolder = Console.ReadLine();

                using Copier copier = new Copier(new FileReader(inputPath, 650, Encoding.ASCII),
                    new FileWriter(outputFolder, Encoding.ASCII));
                await copier.CopyAsync();

                Console.WriteLine("Copying is done");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
