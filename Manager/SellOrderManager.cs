class SellOrderManager
{
    ActiveOrderDB activeOrderDB = new();
    ActiveOrderManager activeOrderManager = new();
    StockTransactionManager stockTransactionManager = new();

    public bool FullFillSellOrder(ActiveOrder mySellOrder)
    {
        ActiveOrder matchingBuyOrder = new();

        //Spara i databasen och returnera dess ID. 
        int myOrderId = activeOrderDB.CreateActiveOrder(mySellOrder);
       
        //Får vi inte tillbaka nån matchande order så returnerar vi false. 
           if (activeOrderManager.GetCompatibleBuyOrder(mySellOrder) == null)
           {
                return false; 
           } 
           else   
           {

        //Startar en loop
        while (true)
        {
            //Läser in objektet från databasen med orderid. 
            mySellOrder = activeOrderDB.GetActiveOrderById(myOrderId);

            if (mySellOrder.IsActive == false)
            {
                //Ordern gick till avslut.  
                return true;
            }


           matchingBuyOrder = activeOrderManager.GetCompatibleBuyOrder(mySellOrder);
        

            //Kollar ifall antalet på säljordern är större än köpordern. 
            if (mySellOrder.Amount > matchingBuyOrder.Amount)
            {
                int activeAmount = mySellOrder.Amount;
                //Nu vet vi att köpordern kan uppfyllas helt. 
                //Vi sätter köpordern till inaktiv. 
                activeOrderDB.CloseActiveOrder(matchingBuyOrder.Id);

                ActiveOrder myFullFilledSellOrder = new();
                //Skapar en kopia av min aktiva säljorder. 
                myFullFilledSellOrder = mySellOrder;
                //Ändrar antalet i kopian till det som gick till avslut i köpordern så dom matchar. 
                myFullFilledSellOrder.Amount = matchingBuyOrder.Amount;
                myFullFilledSellOrder.IsActive = false;
                //Sparar i Databasen. 
                activeOrderDB.CreateActiveOrder(myFullFilledSellOrder);

                //Nu måste vi ändra antalet på den säljordern som är kvar i DB. 
                int newAmount = activeAmount - matchingBuyOrder.Amount;
                activeOrderDB.UpdateAmountInActiveOrder(mySellOrder.Id, newAmount);
                mySellOrder.Amount = newAmount;

                //Slutligen sparar vi ordrarna i en stock transaktion in i databasen. 
                StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(matchingBuyOrder, myFullFilledSellOrder);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);

            }

            else if ((mySellOrder.Amount < matchingBuyOrder.Amount)) //Är antalet på köpordern större än säljordern. 
            {
                int activeAmount = matchingBuyOrder.Amount;
                //Nu vet vi att vår sälj-order kan uppfyllas helt. 
                //Vi sätter vår säljorder till inaktiv. 
                activeOrderDB.CloseActiveOrder(mySellOrder.Id);


                ActiveOrder buyerFullFilledOrder = new();
                //Skapar en kopia av köpordern. 
                buyerFullFilledOrder = matchingBuyOrder;
                //Ändrar antalet i köpordern till det som gick till avslut i säljordern så dom matchar. 
                buyerFullFilledOrder.Amount = mySellOrder.Amount;
                buyerFullFilledOrder.IsActive = false;
                //Sparar i Databasen. 
                activeOrderDB.CreateActiveOrder(buyerFullFilledOrder);

                //Nu måste vi ändra antalet på den köpordern som är kvar i DB. 
                int newAmount = activeAmount - buyerFullFilledOrder.Amount;
                activeOrderDB.UpdateAmountInActiveOrder(buyerFullFilledOrder.Id, newAmount);

                //Spara ner stock transaktion i databasen. 

                StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(buyerFullFilledOrder, mySellOrder);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);
                

            }

            else //Antal på köp och sälj är lika stora. 
            {
                //Båda ordrarna kan sättas som inaktiva i databasen.
                //Börjar med vår sälj-order
                activeOrderDB.CloseActiveOrder(mySellOrder.Id);
                //Sen köpordern. 
                activeOrderDB.CloseActiveOrder(matchingBuyOrder.Id);

                StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(matchingBuyOrder, mySellOrder);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);
                 

            }

           }
        }
    }
}