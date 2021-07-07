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
            Console.WriteLine("1.Display Stocks \n2.Calculate value of each stock \n3.Calculate Total value of stocks\n4.Buy Stocks\n5.Sell Stocks\n6.Print Report\n7.Exit\nEnter an Option");
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
                    manager.BuyStocks(jsonFilePath);
                    Console.WriteLine("Succesfully Bought");
                    break;
                case 5:
                    manager.SellStocks(jsonFilePath);
                    Console.WriteLine("Succesfully Sell Stocks");
                    break;
                case 6:
                    Console.WriteLine("********Print Report*************");
                    manager.CalculateValueOfAllStocks(utility.stockList);
                    break;
                case 7:
                    Console.WriteLine("Exited");
                    break;
            }

        }
    }
}
