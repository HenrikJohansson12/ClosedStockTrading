class LoggedInUserGUI
{  
    public void MainMenu (Customer loggedInCustomer)
    {
        
         ActiveOrderDB activeOrderDB = new();

         
        //Hämta aktiekonton
         loggedInCustomer.GetCustomerStockAccountsFromDataBase(); //Använda delegat??

        //Hämta aktier på varje konto. 
        foreach (var stockAccount in loggedInCustomer.CustomerStockAccounts)
        {
            stockAccount.GetOwnedStocksFromDatabase();
        }


        foreach (var stockAccount in loggedInCustomer.CustomerStockAccounts)
        {
            foreach (var Stock in stockAccount.OwnedStocks)
            {
               Stock.HighestActiveBuyPrice = activeOrderDB.GetHighestActiveBuyPrice(Stock.Id);
               Stock.LowestActiveSellPrice = activeOrderDB.GetLowestActiveSellPrice(Stock.Id);

            }
        }


        string header = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-15} {6,-15} {7,-15}", "Id", "Ticker", "Name", "Sector", "List", "Highest Buy", "Lowest Sell", "Last sold for");
        Console.WriteLine(header);
         foreach (var stockAccount in loggedInCustomer.CustomerStockAccounts)
        {
            foreach (var stock in stockAccount.OwnedStocks)
            {
               string content = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-15} {6,-15} {7,-15}",
                              stock.Id, stock.Ticker, stock.Name, stock.Sector, stock.ListingName, stock.HighestActiveBuyPrice, stock.LowestActiveSellPrice,stock.LastKnownPrice);
                 Console.WriteLine($"{content}");
            }
        }

    }


    
    

    public ActiveOrder CreateActiveBuyOrderObject()
    {
        ActiveOrder myBuyOrder = new();
        myBuyOrder.IsBuyOrder = true;
        myBuyOrder.IsActive = true;
        System.Console.WriteLine("Vilken aktie vill du köpa");
        myBuyOrder.StockId = Convert.ToInt32( Console.ReadLine());
        System.Console.WriteLine("Hur många vill du köpa");
        myBuyOrder.Amount= Convert.ToInt32( Console.ReadLine());
        System.Console.WriteLine("Till vilket pris?");
        myBuyOrder.PricePerStock = Convert.ToDouble( Console.ReadLine());
        myBuyOrder.OrderTimeStamp = DateTime.Now;

        myBuyOrder.AccountId = 3;

        return myBuyOrder;

    }

}