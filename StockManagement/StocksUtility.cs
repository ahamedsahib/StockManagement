using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagement
{
    class StocksUtility
    {
        public List<Stocks> stockList { get; set; }
        public LinkedList<UserStocks> userStockList { get; set; }
        public class UserStocks
        {
            public string shareholder { get; set; }

            public string name { get; set; }
            public int volume { get; set; }
            public int price { get; set; }
        }
        public class Stocks
        {
            public string name { get; set; }
            public int volume { get; set; }
            public int price { get; set; }
        }
    }
}
