class StockTransactionManager
{
    
    public StockTransaction CreateStockTransactionObject(ActiveOrder buyOrder, ActiveOrder sellOrder)
    {   
        
        StockTransaction stockTransaction = new();

        stockTransaction.BuyerAccountId = buyOrder.AccountId;
        stockTransaction.SellerAccountId = sellOrder.AccountId;

        //TODO skapa metod för att hämta courtage priser. Kanske läsa in en publik lista/dictionary som sätter priset. 
        stockTransaction.BuyerCourtage = 0;
        stockTransaction.SellerCourtage = 0;

        stockTransaction.TransactionTime = DateTime.Now;

        // Är köppriset högre än säljspriset används säljpriset. 
        if (buyOrder.PricePerStock>sellOrder.PricePerStock)
        {
            stockTransaction.PricePerStock = sellOrder.PricePerStock;
        }
        else stockTransaction.PricePerStock = buyOrder.PricePerStock;

        if (buyOrder.Amount == sellOrder.Amount)
        {
            stockTransaction.Amount = buyOrder.Amount;
        }
        
        else if (true)
        {
            
        }

        return stockTransaction;
    }

}