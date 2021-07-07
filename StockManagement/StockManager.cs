using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockManagement
{
    class StockManager
    {
        private static LinkedList<string> transactions = new LinkedList<string>();
        private static LinkedList<string> transactionsDateTime = new LinkedList<string>();
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

        public void DisplayStocks(LinkedList<StocksUtility.UserStocks> stockList)
        {
            Console.WriteLine("-------------USER STOCKS-----------");
            foreach (StocksUtility.UserStocks userStocks in stockList)
            {
                Console.WriteLine($"Shareholder name = {userStocks.shareholder}\nName ={userStocks.name}\nVolume={userStocks.volume}\nPrice={userStocks.price}\n***********");
            }
        }
        
        public void CalculateValueOfAllStocks(LinkedList<StocksUtility.UserStocks> stockList)
        {
            int totalShareOfCompanies = 0;
            int valueOfEachCompany;

            foreach (StocksUtility.UserStocks userStocks in stockList)
            {
                Console.WriteLine($"Shareholder name = {userStocks.shareholder}\nName :{userStocks.name}\nVolume:{userStocks.volume}\nPrice:{userStocks.price}\n");
                valueOfEachCompany = userStocks.volume * userStocks.price;
                Console.WriteLine($"Total value of {userStocks.name} share is {valueOfEachCompany}");
                totalShareOfCompanies += valueOfEachCompany;

            }
            Console.WriteLine("Total Values Of all Stocks");
            Console.WriteLine($"Total Value of all Stocks  {totalShareOfCompanies}");
        }
        public void BuyStocks(string jsonFilePathOfStocks)
        {
            DateTime dateTime = DateTime.Now;
            string transactionsStatus = string.Empty;
            StocksUtility StockUtility = JsonConvert.DeserializeObject<StocksUtility>(File.ReadAllText(jsonFilePathOfStocks));


            DisplayStocks(StockUtility.stockList);
            Console.WriteLine("Enter your name:");
            string shareholdername = Console.ReadLine();
            Console.WriteLine("Enter the name of the stock you want to buy:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter how many volumes you want to buy:");
            int volume = Convert.ToInt32(Console.ReadLine());
            bool check = CheckAvailablity(name, volume, StockUtility.stockList);
            if (check)
            {

                StocksUtility.Stocks result = StockUtility.stockList.Find(item => item.name.Equals(name));
                result.volume -= volume;
                
                StocksUtility.UserStocks user = new StocksUtility.UserStocks();
                user.name = name;
                user.volume = volume;
                user.price = result.price;
                user.shareholder = shareholdername;
                if (CheckExists(StockUtility.userStockList, user.name))
                {
                    foreach (StocksUtility.UserStocks i in StockUtility.userStockList)
                    {
                        if (i.name.Equals(name) && i.volume >= volume && i.shareholder.Equals(shareholdername))
                        {
                            i.volume += user.volume;

                        }
                    }

                }
                else
                {
                    StockUtility.userStockList.AddLast(user);
                }

                File.WriteAllText(jsonFilePathOfStocks, JsonConvert.SerializeObject(StockUtility));
                transactionsStatus = $"{shareholdername} ---> Transaction Done on Buying {user.name} of volume = {user.volume} , worth = {user.volume * user.price} ";
                transactions.AddLast(transactionsStatus);
                transactionsDateTime.AddLast(transactionsStatus + "at " + dateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                Console.WriteLine($"You Purchased {user.name} of volume = {user.volume} , worth = {user.volume * user.price} ");


            }
            else
            {
                Console.WriteLine(" Stock Not available");
            }


        }
        
        public void SellStocks(string jsonFilePathOfStocks)
        {
            DateTime dateTime = DateTime.Now;
            string transactionsStatus = string.Empty;
            StocksUtility StockUtility = JsonConvert.DeserializeObject<StocksUtility>(File.ReadAllText(jsonFilePathOfStocks));


            DisplayStocks(StockUtility.userStockList);//display list of user stocks
            Console.WriteLine("Enter your name:");
            string shareHolderName = Console.ReadLine();

            foreach (StocksUtility.UserStocks i in StockUtility.userStockList)
            {
                if (i.shareholder.Equals(shareHolderName))
                {
                    Console.WriteLine($"Shareholder name = {i.shareholder}\nName ={i.name}\nVolume={i.volume}\nPrice={i.price}\n***********");
                }
            }
            Console.WriteLine("Enter the name of the stock you want to Sell:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter how many volumes you want to Sell:");
            int volume = Convert.ToInt32(Console.ReadLine());
            bool check = CheckAvailablity(name, volume, StockUtility.userStockList);
            if (check)
            {
                StocksUtility.Stocks user = new StocksUtility.Stocks();
                foreach (StocksUtility.UserStocks i in StockUtility.userStockList)
                {
                    if (i.name.Equals(name) && i.volume >= volume && i.shareholder.Equals(shareHolderName))
                    {
                        i.volume -= volume;
                        user.price = i.price;
                    }
                }

                user.name = name;
                user.volume = volume;

                if (CheckExists(StockUtility.stockList, user.name))
                {
                    StocksUtility.Stocks res = StockUtility.stockList.Find(item => item.name.Equals(user.name));
                    res.volume += user.volume;
                }
                else
                {
                    StockUtility.stockList.Add(user);
                }

                File.WriteAllText(jsonFilePathOfStocks, JsonConvert.SerializeObject(StockUtility));
                transactionsStatus = $"{shareHolderName} --->Transaction Done on Selling {user.name} of volume = {user.volume} , worth = {user.volume * user.price} ";
                Console.WriteLine($"You Sold {user.name} of volume = {user.volume} , worth = {user.volume * user.price} ");
                transactions.AddLast(transactionsStatus);
                transactionsDateTime.AddLast(transactionsStatus + "at " + dateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                Console.WriteLine($"You Purchased {user.name} of volume = {user.volume} , worth = {user.volume * user.price} ");

            }
            else
            {
                Console.WriteLine("Enough Stock Not available to sell");
            }


        }
        public void PrintReport()
        {

            if (transactions.Count > 0)
            {
                Console.WriteLine("------USER TRANSACTIONS-------");
                foreach (string i in transactionsDateTime)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.WriteLine("No transactions Done");
            }
        }

        //Checking Availablity of the stock to know whether the stock availablity is greater than the Buyer(user) required
        public bool CheckAvailablity(string nameOfStock, int volumeOfStock, List<StocksUtility.Stocks> stockList)
        {
            StocksUtility.Stocks result = stockList.Find(item => item.name.Equals(nameOfStock));
            if (result.name.Equals(nameOfStock) && result.volume >= volumeOfStock)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     
        public bool CheckAvailablity(string nameOfStock, int volumeOfStock, LinkedList<StocksUtility.UserStocks> stockList)
        {
            //StocksUtility.UserStocks result = stockList.Find(item => item.name.Equals(nameOfStock));
            foreach (StocksUtility.UserStocks i in stockList)
            {
                if (i.name.Equals(nameOfStock) && i.volume >= volumeOfStock)
                {
                    return true;
                }
            }
            return false;
        }

        //checking if the share of the company already exists
        public bool CheckExists(LinkedList<StocksUtility.UserStocks> stockList, string name)
        {
            foreach (StocksUtility.UserStocks i in stockList)
            {
                if (i.name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
        
        
        public bool CheckExists(List<StocksUtility.Stocks> stockList, string name)
        {
            foreach (StocksUtility.Stocks i in stockList)
            {
                if (i.name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
