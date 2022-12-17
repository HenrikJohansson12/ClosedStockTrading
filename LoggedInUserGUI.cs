class LoggedInUserGUI
{
    public void MainMenu(Customer loggedInCustomer)
    {   //Laddar in den inloggade kundens konton samt aktier tillhörande dessa konton. 
        loggedInCustomer.CustomerStockAccounts =LoadCustomerAccounts(loggedInCustomer.Id);
        loggedInCustomer.CustomerStockAccounts = LoadCustomerStocks(loggedInCustomer.CustomerStockAccounts);
        

        Console.WriteLine($"Välkommen {loggedInCustomer.Name}\n\n");

        Console.WriteLine("Här är dina konton. Välj ett konto för att gå vidare");

        PrintStockAccountInfo(loggedInCustomer.CustomerStockAccounts);

        int accountSelect = Convert.ToInt32(Console.ReadLine());
        int accountId = loggedInCustomer.CustomerStockAccounts[accountSelect-1].Id;
        StockAccount selectedStockAccount = new ();
        selectedStockAccount = loggedInCustomer.CustomerStockAccounts[accountSelect-1];

        
        while (true)
        {                
        Console.WriteLine("[1] Se aktier på kontot");
        Console.WriteLine("[2] Köpa aktier");
        Console.WriteLine("[3] Sälja aktier");
        Console.WriteLine("[4] Se aktiva ordrar");
        Console.WriteLine("[5] Se historik");
        var keypress = Console.ReadKey(true).KeyChar;
        switch (keypress)
        {
            case '1': PrintCustomerStocksOnAccount(selectedStockAccount); break;
                           
            case '2': CreateBuyOrder(accountId); break;
          
            case '3': CreateSellOrder(selectedStockAccount); break;

            default: break;
        }
        }
        //Här printa kontoinfo samt möjlighet att välja ett konto. 

        //När man valt konto ska man kunna göra följande. 
        // 1. Se aktielista med aktuella kurser samt kunna göra en refresh härifrån. 
        // 2. Sälja aktier. Man ska bara kunna sälja det man äger så tänker att man får välja med ett ID och sen gå vidare till köp där man skriver pris och antal. 
        // 3. Köpa aktier. Här ska man kunna välja på alla tillgängliga aktier i en lista. [KLAR]
        // 4. Kunna se sina aktiva ordrar och ta bort dom. 
        // 4. Se köp och sälj historik
        // 5. BonusFeature, kunna sätta in och ta ut pengar med hjälp en ny många till många tabell. 
        loggedInCustomer.CustomerStockAccounts = LoadCustomerAccounts(loggedInCustomer.Id);
       
        

        // Sekvensen för att skapa en order. 
        ActiveOrderDB activeOrderDB = new();
        ActiveOrderManager activeOrderManager = new();
        StockTransactionManager stockTransactionManager = new();
         List <StockTransaction> myStockTransactions = new();
         BuyOrderManager buyOrderManager = new();
        //Skapa objektet av indata. 
       

        Console.Read();


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
    public void CreateSellOrder (StockAccount selectedStockAccount)
    {  
        SellOrderManager sellOrderManager = new();
        ActiveOrder mySellOrder = new();
        bool closedTransaction;
        bool successfulObjectCreated = false;
        
     while (successfulObjectCreated == false)
    {
        //Visa alla aktier som går att sälja. 
       PrintCustomerStocksOnAccount(selectedStockAccount);
        int stockId, amount;
        double pricePerStock;

        Console.WriteLine("Ange ID på den aktie du vill sälja");
        stockId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Ange vilket pris du vill sälja för");
        pricePerStock = Convert.ToDouble(Console.ReadLine());
        System.Console.WriteLine("Hur många vill du sälja?");
        amount = Convert.ToInt32(Console.ReadLine());

        
        mySellOrder.IsBuyOrder = false;
        mySellOrder.IsActive = true;
       //TODO är nog snyggare att använda constructor i klassen. 
        mySellOrder.StockId = stockId;
        mySellOrder.Amount = amount;
        mySellOrder.PricePerStock = pricePerStock;
        mySellOrder.OrderTimeStamp = DateTime.Now;
        mySellOrder.AccountId = selectedStockAccount.Id; 
        //Kollar så stockId och amount finns på kundens konto. 
       foreach (var stock in selectedStockAccount.OwnedStocks)
       {
            if (stock.Id == mySellOrder.StockId && stock.AmountOnCustomerAccount>=mySellOrder.Amount== true)
            {
                
                successfulObjectCreated = true;
                break;
            }
         
       }
         
        if (successfulObjectCreated == false) System.Console.WriteLine("Aktien finns inte på ditt konto eller du försöker sälja fler än du äger");

    }

   closedTransaction = sellOrderManager.FullFillSellOrder(mySellOrder);
   if (closedTransaction)
   {
    Console.WriteLine("Din säljorder gick till avslut");
   }
   else System.Console.WriteLine("Din order gick inte till avslut");

       
    }
    public void CreateBuyOrder (int accountId)
    {
        BuyOrderManager buyOrderManager = new();
        System.Console.WriteLine("Här kommer alla aktier som går att köpa");
        Thread.Sleep(1000);
        Console.WriteLine("\n\n\n");
        PrintAllStocks();
        Console.WriteLine("\n\n\n");
        ActiveOrder myActiveOrder = CreateActiveBuyOrderObject(accountId);
        
        if (buyOrderManager.FullFillBuyOrder(myActiveOrder) == true)
        {
            System.Console.WriteLine("Din order gick till avslut!");
        } 
        else System.Console.WriteLine("Din order gick inte till avslut");

    }

    public void PrintStockAccountInfo(List<StockAccount> customerStockAccounts)
    {   
        Console.WriteLine(string.Format("{0,-5} {1,-25} {2,-15} {3,-10} {4,-15}","Id","Account name","Stock value","Cash","Total value"));
        int stockAccountIndex = 1;
        foreach (var stockAccount in customerStockAccounts)
        {   stockAccount.RefreshTotalStockValue();
            Console.WriteLine(string.Format("{0,-5} {1,-25} {2,-15} {3,-10} {4,-15}",stockAccountIndex,stockAccount.AccountName,stockAccount.TotalStockValue,stockAccount.BalanceInSek,stockAccount.TotalStockValue+stockAccount.BalanceInSek));
            stockAccountIndex ++;
        }

    }

    public List<StockAccount> LoadCustomerAccounts(int customerId)
    {
        StockAccountDB stockAccountDB = new();
        //Hämta lista med aktiekonton
        List<StockAccount> customerStockAccounts = stockAccountDB.GetCustomerStockAccountFromDataBase(customerId);

        return customerStockAccounts;
    }

    public void PrintAllStocks()
    {   StockDB stockDB = new();
        ActiveOrderDB activeOrderDB = new();
        ListingDB listingDB = new();
        StockTransactionDB stockTransactionDB = new();
        List<Stock> stocks = stockDB.ReadAllStocks();
        stocks = listingDB.SetListingName(stocks);
        
        foreach (var stock in stocks)
            {
                stock.HighestActiveBuyPrice = activeOrderDB.GetHighestActiveBuyPrice(stock.Id);
                stock.LowestActiveSellPrice = activeOrderDB.GetLowestActiveSellPrice(stock.Id);
                stock.LastKnownPrice = stockTransactionDB.GetLatestStockTransactionPrice(stock.Id);           
            }

        Console.WriteLine(string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-15} {6,-15} {7,-15}", "Id", "Ticker", "Name", "Sector", "List", "Highest Buy", "Lowest Sell", "Last sold for"));
        
        
            foreach (var stock in stocks)
            {
                string content = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-15} {6,-15} {7,-15}",
                stock.Id, stock.Ticker, stock.Name, stock.Sector, stock.ListingName, stock.HighestActiveBuyPrice, stock.LowestActiveSellPrice, stock.LastKnownPrice);
                Console.WriteLine($"{content}");
            }           
    }
     public void PrintCustomerStocksOnAccount(StockAccount stockAccount)
    {   
        ActiveOrderDB activeOrderDB = new();
        StockTransactionDB stockTransactionDB = new();
        
        foreach (var stock in stockAccount.OwnedStocks)
            {
                stock.HighestActiveBuyPrice = activeOrderDB.GetHighestActiveBuyPrice(stock.Id);
                stock.LowestActiveSellPrice = activeOrderDB.GetLowestActiveSellPrice(stock.Id);  
                stock.LastKnownPrice = stockTransactionDB.GetLatestStockTransactionPrice(stock.Id);             
            }

        Console.WriteLine(string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-6} {6,-15} {7,-15} {8,-15}", "Id", "Ticker", "Name", "Sector", "List","Amount", "Highest Buy", "Lowest Sell", "Last sold for"));
        
        
            foreach (var stock in stockAccount.OwnedStocks)
            {
                string content = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-6} {6,-15} {7,-15} {8,-15}",
                stock.Id, stock.Ticker, stock.Name, stock.Sector, stock.ListingName,stock.AmountOnCustomerAccount, stock.HighestActiveBuyPrice, stock.LowestActiveSellPrice, stock.LastKnownPrice);
                Console.WriteLine($"{content}");
            }           
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
    public ActiveOrder CreateActiveBuyOrderObject(int accountId)
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

        myBuyOrder.AccountId = accountId; 

        return myBuyOrder;

    }

        public ActiveOrder CreateActiveSellOrderObject(int accountId)
    {
        ActiveOrder mySellOrder = new();
        mySellOrder.IsBuyOrder = true;
        mySellOrder.IsActive = true;
        System.Console.WriteLine("Ange ID på den aktie du vill sälja");
        mySellOrder.StockId = Convert.ToInt32(Console.ReadLine());
        System.Console.WriteLine("Hur mycket vill du sälja för??");
        mySellOrder.PricePerStock = Convert.ToDouble(Console.ReadLine());
        System.Console.WriteLine("Hur många vill du Sälja");
        mySellOrder.Amount = Convert.ToInt32(Console.ReadLine());
        
        mySellOrder.OrderTimeStamp = DateTime.Now;

        mySellOrder.AccountId = accountId;

        return mySellOrder;

    }
}