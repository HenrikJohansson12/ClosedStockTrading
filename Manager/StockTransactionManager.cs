class StockTransactionManager
{
    
    public StockTransaction CreateStockTransactionObject(ActiveOrder buyOrder, ActiveOrder sellOrder)
    {   
        
        StockTransaction stockTransaction = new();

        stockTransaction.BuyerAccountId = buyOrder.AccountId;
        stockTransaction.SellerAccountId = sellOrder.AccountId;

        stockTransaction.StockId = buyOrder.StockId;

        //TODO skapa metod för att hämta courtage priser. Kanske läsa in en publik lista/dictionary som sätter priset. 
        stockTransaction.BuyerCourtage = 0;
        stockTransaction.SellerCourtage = 0;

        stockTransaction.TransactionTime = DateTime.Now;
        //Säljpriset är det som används
        stockTransaction.PricePerStock = sellOrder.PricePerStock;
        
       stockTransaction.Amount = buyOrder.Amount;

        return stockTransaction;
    }


    public int SaveStockTransactionToDataBase (StockTransaction myStockTransaction)
    {
        StockTransactionDB stockTransactionDB = new();
        int transactionId = stockTransactionDB.SaveStockTransactionToDataBase(myStockTransaction);
       return transactionId;
    }



    public void StockTransactionToStockAccount(StockTransaction stockTransaction)
    {
        StockAccountDB stockAccountDB = new();
        //Räkna ut beloppen. 
        stockTransaction.CalculateTotalTransactionSum();
        //Ta bort summan från köpkontot. 
        stockAccountDB.UpdateBalanceStockAccount(stockTransaction.BuyerAccountId,stockTransaction.BuyerTransactionSum);
        //Lägg på summan på köpkontot. 
        stockAccountDB.UpdateBalanceStockAccount(stockTransaction.SellerAccountId,stockTransaction.SellerTransactionSum);

    }

    public void StockTransactionToStocksToAccount(StockTransaction stockTransaction)
    {
        //Vi måste kolla ifall köparen redan har aktien. Vi vill inte att det ska gå in dubletter i databasen. 
        StocksToAccountDB stocksToAccountDB = new();
        StocksToAccount stocksToAccount = new();

        if (stocksToAccountDB.CheckIfStockExistOnCustomerAccount(stockTransaction.StockId,stockTransaction.BuyerAccountId) == true)
        {
            //Finns redan aktien så behöver vi ändra på kunden. 
            stocksToAccountDB.UpdateStocksToAccount(stockTransaction.BuyerAccountId,stockTransaction.StockId,stockTransaction.Amount);
        } 
        else //Finns den inte behöver den skapas på nytt. 
        {
            stocksToAccountDB.CreateNewStocksToAccount(stockTransaction.BuyerAccountId,stockTransaction.StockId,stockTransaction.Amount);
        }
        //Säljaren kollar vi innan denne lägger säljordern. 

        //Antalet aktier behöver tas bort från användaren.
        int amount = -stockTransaction.Amount;
        stocksToAccountDB.UpdateStocksToAccount(stockTransaction.SellerAccountId,stockTransaction.StockId,amount);
    }
}