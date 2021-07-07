using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagement
{
    class StockManager
    {
        public void DisplayStocks(List<StocksUtility.Stocks> stockList)
        {
            foreach (StocksUtility.Stocks i in stockList)
            {
                Console.WriteLine($"Name ={i.name}\nVolume={i.volume}\nPrice={i.price}\n");
            }
        }
        
        public void CalculateValueOfEachStock(List<StocksUtility.Stocks> stockList)
        {
            foreach (StocksUtility.Stocks i in stockList)
            {
                Console.WriteLine($"Name :{i.name}\nVolume:{i.volume}\nPrice:{i.price}\n");
                Console.WriteLine($"Total value of {i.name} share is {i.volume * i.price}\n");

            }
        }
        public void CalculateValueOfAllStocks(List<StocksUtility.Stocks> stockList)
        {
            int totalShareOfCompanies = 0;
            int valueOfEachCompany;
           
            foreach (StocksUtility.Stocks i in stockList)
            {
                Console.WriteLine($"Name :{i.name}\nVolume:{i.volume}\nPrice:{i.price}\n");
                valueOfEachCompany = i.volume * i.price;
                Console.WriteLine($"Total value of {i.name} share is {valueOfEachCompany}");
                totalShareOfCompanies += valueOfEachCompany;

            }
            Console.WriteLine("Total Values Of all Stocks");
            Console.WriteLine($"Total Value of all Stocks  {totalShareOfCompanies}");
        }
    }
}
