class LoggedInCustomerGUI
{
    public void MainMenu(Customer loggedInCustomer)
    {   //Laddar in den inloggade kundens konton samt aktier tillhörande dessa konton. 
       loggedInCustomer = RefreshCustomer(loggedInCustomer);
        
        Console.Clear();
        Console.WriteLine($"Welcome {loggedInCustomer.Name}\n\n");

        while (true)
        {
            Console.WriteLine("These are your stock accounts. Select an account to proceed");

            PrintStockAccountInfo(loggedInCustomer.CustomerStockAccounts);
           
            int accountSelect = Convert.ToInt32(Console.ReadLine());
            
            int accountId = loggedInCustomer.CustomerStockAccounts[accountSelect - 1].Id;
            StockAccount selectedStockAccount = new();
            selectedStockAccount = loggedInCustomer.CustomerStockAccounts[accountSelect - 1];
            bool menuLoop = true;

            while (menuLoop == true)
            {   
               loggedInCustomer = RefreshCustomer(loggedInCustomer);
               selectedStockAccount = loggedInCustomer.CustomerStockAccounts[accountSelect-1];

                Console.WriteLine("\n\n");
                Console.WriteLine("[1] See stocks on account");
                Console.WriteLine("[2] Buy stocks");
                Console.WriteLine("[3] Sell stocks");
                Console.WriteLine("[4] See my active orders");
                Console.WriteLine("[5] See my transaction history");
                Console.WriteLine("[6] Switch stock account\n\n");

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

    public Customer RefreshCustomer(Customer loggedInCustomer)
    {
        loggedInCustomer.CustomerStockAccounts = LoadCustomerAccounts(loggedInCustomer.Id);
        loggedInCustomer.CustomerStockAccounts = LoadCustomerStocks(loggedInCustomer.CustomerStockAccounts);
        loggedInCustomer.CustomerStockAccounts = RefreshStockPrices(loggedInCustomer.CustomerStockAccounts);
        return loggedInCustomer;
        
    }

    public void CreateSellOrder(StockAccount selectedStockAccount)
    {
        SellOrderManager sellOrderManager = new();
        ActiveOrder mySellOrder = new();
        bool closedTransaction;
        bool objectCreatedSuccessfully = false;

        while (objectCreatedSuccessfully == false)
        {
            //Visa alla aktier som går att sälja. 
            PrintCustomerStocksOnAccount(selectedStockAccount);
            //Skapar säljorderobjektet. 
            mySellOrder = CreateActiveSellOrderObject(selectedStockAccount.Id);

            //Kollar så stockId och amount finns på kundens konto. 
            foreach (var stock in selectedStockAccount.OwnedStocks)
            {
                if (stock.Id == mySellOrder.StockId && stock.AmountOnCustomerAccount >= mySellOrder.Amount == true)
                {
                  objectCreatedSuccessfully = true;
                    break;
                }
            }

            if (objectCreatedSuccessfully == false) System.Console.WriteLine("The stock or the number of stocks you are trying to sell does not exist on your account");

        }
        
        closedTransaction = sellOrderManager.FullFillSellOrder(mySellOrder);
        if (closedTransaction == true)
        {
            Console.WriteLine("You have successfully sold the stock");
        }
        else System.Console.WriteLine("No buyers were found at your desired price \n Your order will remain active until we find a buyer");


    }
    public void CreateBuyOrder(int accountId)
    {
        BuyOrderManager buyOrderManager = new();
        System.Console.WriteLine("Here are the stocks you can buy");
        Thread.Sleep(1000);
        Console.WriteLine("\n\n\n");
        PrintAllStocks();
        Console.WriteLine("\n\n\n");
        ActiveOrder myActiveOrder = CreateActiveBuyOrderObject(accountId);

        if (buyOrderManager.FullFillBuyOrder(myActiveOrder) == true)
        {
            System.Console.WriteLine("You have successfully bought the stock");
        }
        else System.Console.WriteLine("No sellers were found at your desired price \n Your order will remain active until we find a seller");

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
        {   //Hämtar in de senaste priserna från active order och slutförda transaktioner. 
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
                
            }
        }
        return customerStockAccounts;
    }
    public ActiveOrder CreateActiveBuyOrderObject(int accountId)
    {
        ActiveOrder myBuyOrder = new();
        myBuyOrder.IsBuyOrder = true;
        myBuyOrder.IsActive = true;
        System.Console.WriteLine("Enter the id of the stock you want to buy");
        myBuyOrder.StockId = Convert.ToInt32(Console.ReadLine()); 
        System.Console.WriteLine("Enter the price you want to buy it for");
        myBuyOrder.PricePerStock = Convert.ToDouble(Console.ReadLine());
        System.Console.WriteLine("Enter the amount you want to buy");
        myBuyOrder.Amount = Convert.ToInt32(Console.ReadLine());
        myBuyOrder.OrderTimeStamp = DateTime.Now;

        myBuyOrder.AccountId = accountId;

        return myBuyOrder;

    }

    public ActiveOrder CreateActiveSellOrderObject(int accountId)
    {
        ActiveOrder mySellOrder = new();
        mySellOrder.IsBuyOrder = false;
        mySellOrder.IsActive = true;
        System.Console.WriteLine("Enter the id of the stock you want to sell");
        mySellOrder.StockId = Convert.ToInt32(Console.ReadLine());
        System.Console.WriteLine("Enter the price you want to sell for");
        mySellOrder.PricePerStock = Convert.ToDouble(Console.ReadLine());
        System.Console.WriteLine("Enter the amount you want to sell");
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

        Console.WriteLine(string.Format("{0,-25} {1,-20} {2,-30} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10}", "Time stamp", "Stock", "List", "Type", "Price per stock", "Amount","Courtage","Sum"));

        foreach (var transaction in stockTransactions)
        {
            transaction.CalculateTotalTransactionSum();
            string type;
            if (transaction.BuyerAccountId == accountId) type = "Buy";
            else type = "Sell";
            double courtage, transactionSum;
            if (type == "Sell") 
            {
                courtage = transaction.SellerCourtage; 
                transactionSum = transaction.SellerTransactionSum;
            }
            else 
            {courtage = transaction.BuyerCourtage; 
            transactionSum = transaction.BuyerTransactionSum;
            }
           
            string content = string.Format("{0,-25} {1,-20} {2,-30} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10}", transaction.TransactionTime, transaction.StockName, transaction.ListingName, type, transaction.PricePerStock, transaction.Amount,courtage,transactionSum);
            Console.WriteLine(content);

        }

    }
}