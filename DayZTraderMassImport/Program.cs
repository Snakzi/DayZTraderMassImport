using System;
using System.IO;
using TinyINI;
using System.Text;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DayZ Trader Mass Import 0.1.1.1.1.1.1");

            var pathImport = "import.txt";
            var pathExport = "export.txt";

            var iniSettings = new IniFile("settings.ini");

            var quantity = iniSettings.Read("Quantity", "General");
            var buyPrice = iniSettings.Read("BuyPrice", "General");
            var sellPrice = iniSettings.Read("SellPrice", "General");

            if(!string.IsNullOrEmpty(quantity) && !string.IsNullOrEmpty(buyPrice) && !string.IsNullOrEmpty(sellPrice))
            {
                Console.WriteLine("[+] Settings found. Quantity: " + quantity + ", Buy Price: " + buyPrice + ", Sell Price: " + sellPrice);

            }

            if(string.IsNullOrEmpty(quantity))
            {
                Console.WriteLine("Enter quantity:");
                string desiredQuantity = Console.ReadLine();
                iniSettings.Write("Quantity", desiredQuantity, "General");
            }

            if (string.IsNullOrEmpty(buyPrice))
            {
                Console.WriteLine("Enter Buyprice:");
                string desiredBuyPrice = Console.ReadLine();
                iniSettings.Write("BuyPrice", desiredBuyPrice, "General");
            }

            if (string.IsNullOrEmpty(sellPrice))
            {
                Console.WriteLine("Enter Sellprice:");
                string desiredSellPrice = Console.ReadLine();
                iniSettings.Write("SellPrice", desiredSellPrice, "General");
            }

            if (!File.Exists(pathImport))
            {
                Console.WriteLine("import.txt not found, exiting...");
            } 
            else
            {
                int linesCount = File.ReadAllLines(pathImport).Length;
                Console.WriteLine(linesCount + " items found");
                Console.WriteLine("Press any key to start writing...");
                Console.ReadLine();

                const Int32 BufferSize = 128;

                using (var FileStream = File.OpenRead(pathImport))
                {
                    using (var streamReader = new StreamReader(FileStream, Encoding.UTF8, true, BufferSize))
                    {
                        using (var streamWriter = new StreamWriter(pathExport, false))
                        {
                            string line;
                            for(int i = 0; i < linesCount; i++)
                            {
                                Console.WriteLine("[+] starting");
                                Console.WriteLine("-----");
                                while ((line = streamReader.ReadLine()) != null)
                                {
                                    streamWriter.WriteLine(line + ", *, 1000, 5");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("[+] " + line + " processed");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine("-----");
                                }
                                Console.WriteLine("[+] Done :)");
                                Console.ReadLine();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
