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

        //Kollar så att antalet aktier i köporder matchar i säljorder. 
        if (buyOrder.Amount == sellOrder.Amount)
        {
            stockTransaction.Amount = buyOrder.Amount;
        }
        
        //
        else if (buyOrder.Amount>sellOrder.Amount)
        {
            stockTransaction.Amount = sellOrder.Amount;

           buyOrder.Amount = buyOrder.Amount - sellOrder.Amount;
           CreateOverFlowOrder(buyOrder);
            //TODO Använda delegater här istället?
        }

        else
        {
            stockTransaction.Amount = buyOrder.Amount;
            sellOrder.Amount = sellOrder.Amount - buyOrder.Amount;
           CreateOverFlowOrder(sellOrder);
        }

        return stockTransaction;
    }

    public void CreateOverFlowOrder (ActiveOrder activeOrder)
    {
        ActiveOrderDB activeOrderDB = new ActiveOrderDB();
        activeOrderDB.CreateActiveOrder(activeOrder);

    }
}