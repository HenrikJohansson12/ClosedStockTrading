class BuyOrderManager
{
    ActiveOrderDB activeOrderDB = new();
    ActiveOrderManager activeOrderManager = new();
    StockTransactionManager stockTransactionManager = new();

    public bool FullFillBuyOrder(ActiveOrder myActiveOrder)
    {
        ActiveOrder matchingOrder = new();

        //Spara i databasen och returnera dess ID. 
        int myOrderId = activeOrderDB.CreateActiveOrder(myActiveOrder);

        //Startar en loop
        while (true)
        {
            //Läser in objektet från databasen med orderid. 
            myActiveOrder = activeOrderDB.GetActiveOrderById(myOrderId);

            if (myActiveOrder.IsActive == false)
            {
                //Ordern gick till avslut.  
                return true;
            }


           //Får vi inte tillbaka nån matchande order så returnerar vi false. 
           if (activeOrderManager.GetCompatibleSellOrder(myActiveOrder) == null)
           {
                return false; 
           } 
           else   
           {
           
           
           matchingOrder = activeOrderManager.GetCompatibleSellOrder(myActiveOrder);
           


            //Kollar ifall antalet på köpordern är större än säljordern. 
            if (myActiveOrder.Amount > matchingOrder.Amount)
            {
                int activeAmount = myActiveOrder.Amount;
                //Nu vet vi att säljordern kan uppfyllas helt. 
                //Vi sätter säljOrdern till inaktiv. 
                activeOrderDB.CloseActiveOrder(matchingOrder.Id);

                ActiveOrder myFullFilledOrder = new();
                //Skapar en kopia av min aktiva köporder. 
                myFullFilledOrder = myActiveOrder;
                //Ändrar antalet i köp-ordern till det som gick till avslut i säljordern så dom matchar. 
                myFullFilledOrder.Amount = matchingOrder.Amount;
                myFullFilledOrder.IsActive = false;
                //Sparar i Databasen. 
                activeOrderDB.CreateActiveOrder(myFullFilledOrder);

                //Nu måste vi ändra antalet på den köpordern som är kvar i DB. 
                int newAmount = activeAmount - matchingOrder.Amount;
                activeOrderDB.UpdateAmountInActiveOrder(myActiveOrder.Id, newAmount);
                myActiveOrder.Amount = newAmount;

                //Slutligen sparar vi ordrarna i en stock transaktion in i databasen. 
                StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder, matchingOrder);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);

            }

            else if ((myActiveOrder.Amount < matchingOrder.Amount)) //Är antalet på säljordern större än köpordern. 
            {
                int activeAmount = matchingOrder.Amount;
                //Nu vet vi att köpordern kan uppfyllas helt. 
                //Vi sätter vår köporder till inaktiv. 
                activeOrderDB.CloseActiveOrder(myActiveOrder.Id);


                ActiveOrder sellerFullFilledOrder = new();
                //Skapar en kopia av säljordern. 
                sellerFullFilledOrder = matchingOrder;
                //Ändrar antalet i sälj-ordern till det som gick till avslut i KÖPordern så dom matchar. 
                sellerFullFilledOrder.Amount = myActiveOrder.Amount;
                sellerFullFilledOrder.IsActive = false;
                //Sparar i Databasen. 
                activeOrderDB.CreateActiveOrder(sellerFullFilledOrder);

                //Nu måste vi ändra antalet på den säljordern som är kvar i DB. 
                int newAmount = activeAmount - sellerFullFilledOrder.Amount;
                activeOrderDB.UpdateAmountInActiveOrder(sellerFullFilledOrder.Id, newAmount);

                //Spara ner stock transaktion i databasen. 

                StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder, sellerFullFilledOrder);
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
                activeOrderDB.CloseActiveOrder(matchingOrder.Id);

                StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder, matchingOrder);
                stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                //Efter transaktionen ska pengar byta ägare. 
                stockTransactionManager.StockTransactionToStockAccount(stockTransaction);
                //Slutligen ska aktierna byta ägare. 
                stockTransactionManager.StockTransactionToStocksToAccount(stockTransaction);
                //Se till att pengar och aktier byter ägare utifrån ett transaction object. 


            }

           }
        }
    }
}