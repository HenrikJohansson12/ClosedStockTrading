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
        int myOrderId= activeOrderDB.CreateActiveOrder(myActiveOrder);
        while (true)
        {
        
         myActiveOrder = activeOrderDB.GetActiveOrderById(myOrderId); 
        
        if (myActiveOrder.IsActive == false)
        {
            
            Console.WriteLine("Nu ska loopen avslutas");
            break;
        }  
        
       
        //Hämtar en sorterad lista med kompatibla ordrar. 
        List<ActiveOrder> matchingOrders = activeOrderManager.GetCompatibleSellOrders(myActiveOrder);
        
        //Ifall jag köper. 
        if (myActiveOrder.IsBuyOrder == true)
        {
            //Kollar ifall antalet på köpordern är större än säljordern. 
            if (myActiveOrder.Amount>matchingOrders[0].Amount)
            {
                int activeAmount = myActiveOrder.Amount;
                //Nu vet vi att säljordern kan uppfyllas helt. 
                //Vi sätter säljOrdern till inaktiv. 
                activeOrderDB.CloseActiveOrder(matchingOrders[0].Id);
  
                ActiveOrder myFullFilledOrder = new();
                //Skapar en kopia av min aktiva köporder. 
                myFullFilledOrder = myActiveOrder;
                //Ändrar antalet i köp-ordern till det som gick till avslut i säljordern så dom matchar. 
                myFullFilledOrder.Amount = matchingOrders[0].Amount;
                myFullFilledOrder.IsActive = false;
                //Sparar i Databasen. 
                activeOrderDB.CreateActiveOrder(myFullFilledOrder);

                //Nu måste vi ändra antalet på den köpordern som är kvar i DB. 
                int newAmount = activeAmount - matchingOrders[0].Amount;
                activeOrderDB.UpdateAmountInActiveOrder(myActiveOrder.Id,newAmount);
                myActiveOrder.Amount = newAmount;

                //Slutligen sparar vi ordrarna i en stock transaktion in i databasen. 
                StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder,matchingOrders[0]);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);
                
            }

            else if ((myActiveOrder.Amount<matchingOrders[0].Amount)) //Är antalet på säljordern större än köpordern. 
            {
                int activeAmount = matchingOrders[0].Amount;
                //Nu vet vi att köpordern kan uppfyllas helt. 
                //Vi sätter vår köporder till inaktiv. 
                activeOrderDB.CloseActiveOrder(myActiveOrder.Id);

                
                ActiveOrder sellerFullFilledOrder = new();
                //Skapar en kopia av säljordern. 
                sellerFullFilledOrder = matchingOrders[0];
                //Ändrar antalet i sälj-ordern till det som gick till avslut i KÖPordern så dom matchar. 
                sellerFullFilledOrder.Amount = myActiveOrder.Amount;
                sellerFullFilledOrder.IsActive = false;
                //Sparar i Databasen. 
                activeOrderDB.CreateActiveOrder(sellerFullFilledOrder);

                //Nu måste vi ändra antalet på den säljordern som är kvar i DB. 
                int newAmount = activeAmount - sellerFullFilledOrder.Amount;
                activeOrderDB.UpdateAmountInActiveOrder(sellerFullFilledOrder.Id,newAmount);
                
                //Spara ner stock transaktion i databasen. 
               
                 StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder,sellerFullFilledOrder);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);
                //Se till att pengar och aktier byter ägare utifrån ett transaction object. 
               
            }

            else //Antal på köp och sälj är lika stora. 
            {
                //Båda ordrarna kan sättas som inaktiva i databasen.
                //Börjar med vår köporder
                activeOrderDB.CloseActiveOrder(myActiveOrder.Id);
                //Sen säljordern. 
                activeOrderDB.CloseActiveOrder(matchingOrders[0].Id);

                 StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder,matchingOrders[0]);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);
                //Se till att pengar och aktier byter ägare utifrån ett transaction object. 


            }
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