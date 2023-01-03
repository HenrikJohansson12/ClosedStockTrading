class StockTransactionManager
{
    
    public StockTransaction CreateStockTransactionObject(ActiveOrder buyOrder, ActiveOrder sellOrder)
    {   
        
        StockTransaction stockTransaction = new();

        stockTransaction.BuyerAccountId = buyOrder.AccountId;
        stockTransaction.SellerAccountId = sellOrder.AccountId;

        stockTransaction.StockId = buyOrder.StockId;

        //TODO Create a method that reads the courtage prices from the database.  
        stockTransaction.BuyerCourtage = 0;
        stockTransaction.SellerCourtage = 0;

        stockTransaction.TransactionTime = DateTime.Now;
        //Use the selling price since its the lowest. 
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



    public void UpdateStockAccountBalanceAfterStockTransaction(StockTransaction stockTransaction)
    {
        StockAccountDB stockAccountDB = new();
        
        stockTransaction.CalculateTotalTransactionSum();
       //Remove the balance from the buyer account
        stockAccountDB.UpdateBalanceStockAccount(stockTransaction.BuyerAccountId,stockTransaction.BuyerTransactionSum);
        //Add the balace to the seller account
        stockAccountDB.UpdateBalanceStockAccount(stockTransaction.SellerAccountId,stockTransaction.SellerTransactionSum);

    }

    public void UpdateStocksToAccountAfterStockTransaction(StockTransaction stockTransaction)
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

        //Antalet aktier behöver tas bort från användaren.
        int amount = -stockTransaction.Amount;
        stocksToAccountDB.UpdateStocksToAccount(stockTransaction.SellerAccountId,stockTransaction.StockId,amount);
    }
}