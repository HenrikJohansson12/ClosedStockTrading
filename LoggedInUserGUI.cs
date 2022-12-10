class LoggedInUserGUI
{
    public void MainMenu(Customer loggedInCustomer)
    {



        loggedInCustomer.CustomerStockAccounts = LoadCustomerAccounts(loggedInCustomer.Id);






   


        string header = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-15} {6,-15} {7,-15}", "Id", "Ticker", "Name", "Sector", "List", "Highest Buy", "Lowest Sell", "Last sold for");
        Console.WriteLine(header);
        foreach (var stockAccount in loggedInCustomer.CustomerStockAccounts)
        {
            foreach (var stock in stockAccount.OwnedStocks)
            {
                string content = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-15} {6,-15} {7,-15}",
                stock.Id, stock.Ticker, stock.Name, stock.Sector, stock.ListingName, stock.HighestActiveBuyPrice, stock.LowestActiveSellPrice, stock.LastKnownPrice);
                Console.WriteLine($"{content}");
            }
        }

    }



    public List<StockAccount> LoadCustomerAccounts(int customerId)
    {
        StockAccountDB stockAccountDB = new();
        //Hämta lista med aktiekonton
        List<StockAccount> customerStockAccounts = stockAccountDB.GetCustomerStockAccountFromDataBase(customerId);

        return customerStockAccounts;
    }

    public List<StockAccount> LoadCustomerStocks(List<StockAccount> customerStockAccounts)
    {
        StockDB stockDB = new();

        //Hämtar en lista med aktier på som finns på varje konto. 
        foreach (var stockAccount in customerStockAccounts)
        {
            stockAccount.OwnedStocks = stockDB.StocksByAccountId(stockAccount.Id);
        }
        return customerStockAccounts;
    }

    public List<StockAccount> RefreshStockPrices(List<StockAccount> customerStockAccounts)
    {
        ActiveOrderDB activeOrderDB = new();

        foreach (var stockAccount in customerStockAccounts)
        {
            foreach (var stock in stockAccount.OwnedStocks)
            {
                stock.HighestActiveBuyPrice = activeOrderDB.GetHighestActiveBuyPrice(stock.Id);
                stock.LowestActiveSellPrice = activeOrderDB.GetLowestActiveSellPrice(stock.Id);
                //Lägg även till här från historiken
            }
        }
        return customerStockAccounts;
    }
    public ActiveOrder CreateActiveBuyOrderObject()
    {
        ActiveOrder myBuyOrder = new();
        myBuyOrder.IsBuyOrder = true;
        myBuyOrder.IsActive = true;
        System.Console.WriteLine("Vilken aktie vill du köpa");
        myBuyOrder.StockId = Convert.ToInt32(Console.ReadLine());
        System.Console.WriteLine("Hur många vill du köpa");
        myBuyOrder.Amount = Convert.ToInt32(Console.ReadLine());
        System.Console.WriteLine("Till vilket pris?");
        myBuyOrder.PricePerStock = Convert.ToDouble(Console.ReadLine());
        myBuyOrder.OrderTimeStamp = DateTime.Now;

        myBuyOrder.AccountId = 3;

        return myBuyOrder;

    }

}