class BuyOrderManager
{
    ActiveOrderDB activeOrderDB = new();
    ActiveOrderManager activeOrderManager = new();
    StockTransactionManager stockTransactionManager = new();

    public bool FullFillBuyOrder(ActiveOrder myActiveOrder)
    {
        ActiveOrder matchingOrder = new();

        //Saving the orderobject in the database and return its ID
        int myOrderId = activeOrderDB.SaveActiveOrder(myActiveOrder);


        while (true)
        {
            //Reads the order again from the database to check if its set to inactive. 
            myActiveOrder = activeOrderDB.GetActiveOrderById(myOrderId);

            if (myActiveOrder.IsActive == false)
            {
                //Our order was set to inactive and sucessfull. 
                return true;
            }


            //If we cant find any matching order at all we return false. 
            if (activeOrderManager.GetCompatibleSellOrder(myActiveOrder) == null)
            {
                return false;
            }
            else
            {

                //Retrieving the matching order. 
                matchingOrder = activeOrderManager.GetCompatibleSellOrder(myActiveOrder);

                //Checking if the amount on the buyorder is higher than the sellorder. 
                if (myActiveOrder.Amount > matchingOrder.Amount)
                {
                    int activeAmount = myActiveOrder.Amount;
                    //Now we know that the full amount on the sellorder will be fullfilled. 
                    //We can then close the sellorder. 
                    activeOrderDB.CloseActiveOrder(matchingOrder.Id);

                    ActiveOrder myFullFilledBuyOrder = new();
                    //Create a copy of the active buyorder. 
                    myFullFilledBuyOrder = myActiveOrder;
                    //Changing the amount in the copy to amount that was fullfilled in the sellorder so that they match. 
                    myFullFilledBuyOrder.Amount = matchingOrder.Amount;
                    myFullFilledBuyOrder.IsActive = false;
                    //Saving to database. 
                    activeOrderDB.SaveActiveOrder(myFullFilledBuyOrder);

                    //The remaining amount on the buyorder needs to be changed in the database. 
                    int newAmount = activeAmount - matchingOrder.Amount;
                    activeOrderDB.UpdateAmountInActiveOrder(myActiveOrder.Id, newAmount);
                    myActiveOrder.Amount = newAmount;

                    //Both orders turns into a stocktransaction and gets saved in the database. 
                    StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myFullFilledBuyOrder, matchingOrder);
                    stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                    //Now the balance on buyer and seller accounts needs to be updated. 
                    stockTransactionManager.UpdateStockAccountBalanceAfterStockTransaction(stockTransaction);
                    //Finally the stocks switches owner. 
                    stockTransactionManager.UpdateStocksToAccountAfterStockTransaction(stockTransaction);

                }

                else if ((myActiveOrder.Amount < matchingOrder.Amount))
                {
                    int activeAmount = matchingOrder.Amount;
                    //Instead we know that the full amount on the buyorder will be fullfilled
                    //We set our buyorder to inactive.  
                    activeOrderDB.CloseActiveOrder(myActiveOrder.Id);


                    ActiveOrder sellerFullFilledOrder = new();
                    //Creating a copy of the sellorder.  
                    sellerFullFilledOrder = matchingOrder;
                    //Changes the amount to have it match the closed buyorder. 
                    sellerFullFilledOrder.Amount = myActiveOrder.Amount;
                    sellerFullFilledOrder.IsActive = false;
                    //Saving to database. 
                    activeOrderDB.SaveActiveOrder(sellerFullFilledOrder);

                    //Now the remaining amount is changed on the original sellorder in the database. 
                    int newAmount = activeAmount - sellerFullFilledOrder.Amount;
                    activeOrderDB.UpdateAmountInActiveOrder(sellerFullFilledOrder.Id, newAmount);

                    //Both orders turns into a stocktransaction and gets saved in the database.
                    StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder, sellerFullFilledOrder);
                    stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                    //Now the balance on buyer and seller accounts needs to be updated. 
                    stockTransactionManager.UpdateStockAccountBalanceAfterStockTransaction(stockTransaction);
                    //Finally the stocks switches owner. 
                    stockTransactionManager.UpdateStocksToAccountAfterStockTransaction(stockTransaction);


                }

                else //The amount on buy and sell are equal. 
                {
                    //Both orders will be set to inactive in the database. 

                    activeOrderDB.CloseActiveOrder(myActiveOrder.Id);
                    activeOrderDB.CloseActiveOrder(matchingOrder.Id);
                    //Creating and saving the stock transaction to the database. 
                    StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(myActiveOrder, matchingOrder);
                    stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);


                    stockTransactionManager.UpdateStockAccountBalanceAfterStockTransaction(stockTransaction);
                    //Now the balance on buyer and seller accounts needs to be updated. 
                    stockTransactionManager.UpdateStocksToAccountAfterStockTransaction(stockTransaction);
                    //Finally the stocks switches owner. 


                }

            }
        }
    }
}