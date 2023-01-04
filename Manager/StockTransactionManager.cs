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


    public int SaveStockTransactionToDataBase(StockTransaction myStockTransaction)
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
        stockAccountDB.UpdateBalanceStockAccount(stockTransaction.BuyerAccountId, stockTransaction.BuyerTransactionSum);
        //Add the balace to the seller account
        stockAccountDB.UpdateBalanceStockAccount(stockTransaction.SellerAccountId, stockTransaction.SellerTransactionSum);

    }

    public void UpdateStocksToAccountAfterStockTransaction(StockTransaction stockTransaction)
    {
        //We need to check if the buying customer already owns the stock. 
        StocksToAccountDB stocksToAccountDB = new();
        StocksToAccount stocksToAccount = new();

        if (stocksToAccountDB.CheckIfStockExistOnCustomerAccount(stockTransaction.StockId, stockTransaction.BuyerAccountId) == true)
        {
            //If it exists we update the amount. 
            stocksToAccountDB.UpdateStocksToAccount(stockTransaction.BuyerAccountId, stockTransaction.StockId, stockTransaction.Amount);
        }
        else //If not we save it as a new record. 
        {
            stocksToAccountDB.CreateNewStocksToAccount(stockTransaction.BuyerAccountId, stockTransaction.StockId, stockTransaction.Amount);
        }

        //Finnally remove the amount from the seller. 
        int amount = -stockTransaction.Amount;
        stocksToAccountDB.UpdateStocksToAccount(stockTransaction.SellerAccountId, stockTransaction.StockId, amount);
    }
}