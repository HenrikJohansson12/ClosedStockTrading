class LoggedInCustomerGUI
{
    public void MainMenu(Customer loggedInCustomer)
    {   //Laddar in den inloggade kundens konton samt aktier tillhörande dessa konton. 
        loggedInCustomer.CustomerStockAccounts = LoadCustomerAccounts(loggedInCustomer.Id);
        loggedInCustomer.CustomerStockAccounts = LoadCustomerStocks(loggedInCustomer.CustomerStockAccounts);
        loggedInCustomer.CustomerStockAccounts = RefreshStockPrices(loggedInCustomer.CustomerStockAccounts);

        Console.WriteLine($"Välkommen {loggedInCustomer.Name}\n\n");

        while (true)
        {
            Console.WriteLine("Här är dina konton. Välj ett konto för att gå vidare");

            PrintStockAccountInfo(loggedInCustomer.CustomerStockAccounts);

            int accountSelect = Convert.ToInt32(Console.ReadLine());
            int accountId = loggedInCustomer.CustomerStockAccounts[accountSelect - 1].Id;
            StockAccount selectedStockAccount = new();
            selectedStockAccount = loggedInCustomer.CustomerStockAccounts[accountSelect - 1];
            bool menuLoop = true;

            while (menuLoop == true)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("[1] Se aktier på kontot");
                Console.WriteLine("[2] Köpa aktier");
                Console.WriteLine("[3] Sälja aktier");
                Console.WriteLine("[4] Se aktiva ordrar");
                Console.WriteLine("[5] Se historik");
                Console.WriteLine("[6] Byt konto\n\n");

                var keypress = Console.ReadKey(true).KeyChar;
                
                switch (keypress)
                {
                    case '1': PrintCustomerStocksOnAccount(selectedStockAccount); break;

                    case '2': CreateBuyOrder(accountId); break;

                    case '3': CreateSellOrder(selectedStockAccount); break;

                    case '4': PrintActiveOrders(selectedStockAccount.Id); break;

                    case '5': PrintTransactionHistory(selectedStockAccount.Id); break;

                    case '6': menuLoop = false; break;

                    default: break;
                }
            }

        }

    }


    public void CreateSellOrder(StockAccount selectedStockAccount)
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
                if (stock.Id == mySellOrder.StockId && stock.AmountOnCustomerAccount >= mySellOrder.Amount == true)
                {

                    successfulObjectCreated = true;
                    break;
                }

            }

            if (successfulObjectCreated == false) System.Console.WriteLine("Aktien finns inte på ditt konto eller du försöker sälja fler än du äger");

        }

        closedTransaction = sellOrderManager.FullFillSellOrder(mySellOrder);
        if (closedTransaction == true)
        {
            Console.WriteLine("Din säljorder gick till avslut");
        }
        else System.Console.WriteLine("Din order gick inte till avslut");


    }
    public void CreateBuyOrder(int accountId)
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
        Console.WriteLine(string.Format("{0,-5} {1,-25} {2,-15} {3,-10} {4,-15}", "Id", "Account name", "Stock value", "Cash", "Total value"));
        int stockAccountIndex = 1;
        foreach (var stockAccount in customerStockAccounts)
        {
            stockAccount.RefreshTotalStockValue();
            Console.WriteLine(string.Format("{0,-5} {1,-25} {2,-15} {3,-10} {4,-15}", stockAccountIndex, stockAccount.AccountName, stockAccount.TotalStockValue, stockAccount.BalanceInSek, stockAccount.TotalStockValue + stockAccount.BalanceInSek));
            stockAccountIndex++;
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
    {
        StockDB stockDB = new();
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

        Console.WriteLine(string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-6} {6,-15} {7,-15} {8,-15} {9,-10}", "Id", "Ticker", "Name", "Sector", "List", "Amount", "Highest Buy", "Lowest Sell", "Last closure", "Total value"));


        foreach (var stock in stockAccount.OwnedStocks)
        {
            double totalValueOnCustomerAccount = totalValueOnCustomerAccount = stock.LastKnownPrice * stock.AmountOnCustomerAccount;
            string content = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-25} {5,-6} {6,-15} {7,-15} {8,-15} {9,-10}",
            stock.Id, stock.Ticker, stock.Name, stock.Sector, stock.ListingName, stock.AmountOnCustomerAccount, stock.HighestActiveBuyPrice, stock.LowestActiveSellPrice, stock.LastKnownPrice, totalValueOnCustomerAccount);
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
        StockTransactionDB stockTransactionDB = new();

        foreach (var stockAccount in customerStockAccounts)
        {
            foreach (var stock in stockAccount.OwnedStocks)
            {
                stock.HighestActiveBuyPrice = activeOrderDB.GetHighestActiveBuyPrice(stock.Id);
                stock.LowestActiveSellPrice = activeOrderDB.GetLowestActiveSellPrice(stock.Id);
                stock.LastKnownPrice = stockTransactionDB.GetLatestStockTransactionPrice(stock.Id);
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

    public void PrintActiveOrders(int accountId)
    {
        ActiveOrderDB activeOrderDB = new();
        List<ActiveOrder> activeOrders = new();
        //Hämta listan med aktiva ordrar. 
        activeOrders = activeOrderDB.GetAllActiveOrdersByAccountId(accountId);

        //Skriv ut

        Console.WriteLine(string.Format("{0,-25} {1,-20} {2,-30} {3,-10} {4,-10} {5,-10}", "Time stamp", "Stock", "List", "Type", "Price", "Amount"));

        foreach (var order in activeOrders)
        {
            string type;
            if (order.IsBuyOrder == true) type = "Buy";
            else type = "Sell";

            string content = string.Format("{0,-25} {1,-20} {2,-30} {3,-10} {4,-10} {5,-10}", order.OrderTimeStamp, order.StockName, order.ListingName, type, order.PricePerStock, order.Amount);
            Console.WriteLine(content);

        }

    }


    public void PrintTransactionHistory(int accountId)
    {
        StockTransactionDB stockTransactionDB = new();
        List<StockTransaction> stockTransactions = new();
        //Hämta listan med transaktioner. 
        stockTransactions = stockTransactionDB.GetAllStockTransactionsByAccountId(accountId);

        //Skriv ut

        Console.WriteLine(string.Format("{0,-25} {1,-20} {2,-30} {3,-10} {4,-10} {5,-10}", "Time stamp", "Stock", "List", "Type", "Price", "Amount"));

        foreach (var transaction in stockTransactions)
        {
            string type;
            if (transaction.BuyerAccountId == accountId) type = "Buy";
            else type = "Sell";

            string content = string.Format("{0,-25} {1,-20} {2,-30} {3,-10} {4,-10} {5,-10}", transaction.TransactionTime, transaction.StockName, transaction.ListingName, type, transaction.PricePerStock, transaction.Amount);
            Console.WriteLine(content);

        }

    }
}