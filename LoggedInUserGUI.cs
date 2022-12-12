class LoggedInUserGUI
{
    public void MainMenu(Customer loggedInCustomer)
    {

        Console.WriteLine($"Välkommen {loggedInCustomer.Name}\n\n");

        //Här printa kontoinfo samt möjlighet att välja ett konto. 

        //När man valt konto ska man kunna göra följande. 
        // 1. Se aktielista med aktuella kurser samt kunna göra en refresh härifrån. 
        // 2. Sälja aktier. Man ska bara kunna sälja det man äger så tänker att man får välja med ett ID och sen gå vidare till köp där man skriver pris och antal. 
        // 3. Köpa aktier. Här ska man kunna välja på alla tillgängliga aktier i en lista. 
        // 4. Kunna se sina aktiva ordrar och ta bort dom. 
        // 4. Se köp och sälj historik
        // 5. BonusFeature, kunna sätta in och ta ut pengar med hjälp en ny många till många tabell. 
        loggedInCustomer.CustomerStockAccounts = LoadCustomerAccounts(loggedInCustomer.Id);
       

        // Sekvensen för att skapa en order. 
        ActiveOrderDB activeOrderDB = new();
        ActiveOrderManager activeOrderManager = new();
        StockTransactionManager stockTransactionManager = new();
         List <StockTransaction> myStockTransactions = new();
        //Skapa objektet av indata. 
        ActiveOrder myActiveOrder = CreateActiveBuyOrderObject();
        //Spara i databasen och returnera dess ID. 
        myActiveOrder.Id = activeOrderDB.CreateActiveOrder(myActiveOrder);
        //Hämta en lista med kompatibla ordrar. 
        List<ActiveOrder> matchingOrders = activeOrderManager.LookForCompatibleOrdersAfterPlacingOrder(myActiveOrder);
        
        //Ifall jag köper. 
        if (myActiveOrder.IsBuyOrder == true)
        {
                        
             for (int i = 0; i < matchingOrders.Count; i++)
             {       
            //Skapar en stocktransaction object och lägger till i listan. 
            myStockTransactions.Add(stockTransactionManager.CreateStockTransactionObject(myActiveOrder,matchingOrders[i]));
            
          //kollar ifall antalet på säljordern är större än antalet på köpordern. 
            if (matchingOrders[i].Amount>myActiveOrder.Amount)
            {   
                //Min köporder är slutförd och sätts som inaktiv samtidigt som säljordern delas upp. 
                activeOrderManager.CompleteAndSplitOrder(myActiveOrder,matchingOrders[i]);
               //Sparar transaktionen till db. 
               stockTransactionManager.SaveStockTransactionToDataBase(myStockTransactions[i]);
                //For loopen avslutas. 
                break; 
            }

            //Är min köporder större betyder det att jag behöver köpa från ytterligare en order. 
            else
            {
                //Istället blir den första säljordern slutförd och behöver sparas till databasen. 
                //Min köporder behöver nu splittas upp. 
                activeOrderManager.CompleteAndSplitOrder(matchingOrders[i],myActiveOrder);
                myActiveOrder.Amount = myActiveOrder.Amount - matchingOrders[i].Amount;
                //Sparar transaktionen till DB. 
                stockTransactionManager.SaveStockTransactionToDataBase(myStockTransactions[i]);
            }
           //Loopen börjar om. 
        }
        }
      





   


       /* string header = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-15} {6,-15} {7,-15}", "Id", "Ticker", "Name", "Sector", "List", "Highest Buy", "Lowest Sell", "Last sold for");
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
        */
    }
    
    public void PrintStockAccountInfo(List<StockAccount> customerStockAccounts)
    {   
        Console.WriteLine(string.Format("{0,-5} {1,-20} {2,-15} {3,-15} {4,-15}","Id","Account name","Stock value","Cash","Total value"));

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

        myBuyOrder.AccountId = 3; //TODO

        return myBuyOrder;

    }

}