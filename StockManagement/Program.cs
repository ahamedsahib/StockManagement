using Newtonsoft.Json;
using System;
using System.IO;

namespace StockManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            StockManager manager = new StockManager();

            Console.WriteLine("Welcome To Stock Management Program");
            string jsonFilePath = @"C:\Users\ahamed\source\repos\StockManagement\StockManagement\Stock.json";
            StocksUtility utility = JsonConvert.DeserializeObject<StocksUtility>(File.ReadAllText(jsonFilePath));
            Console.WriteLine("-------MENU-------");
            Console.WriteLine("1.Display Stocks \n2.Calculate value of each stock \n3.Calculate Total value of stocks\n4.Exit\nEnter an Option");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    manager.DisplayStocks(utility.stockList);
                    break;
                case 2:
                    manager.CalculateValueOfEachStock(utility.stockList);
                    break;
                case 3:
                    manager.CalculateValueOfAllStocks(utility.stockList);
                    break;
                case 4:
                    Console.WriteLine("Exited!!!!!");
                    break;
            }

        }
    }
}
