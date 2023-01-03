class SellOrderManager
{
    ActiveOrderDB activeOrderDB = new();
    ActiveOrderManager activeOrderManager = new();
    StockTransactionManager stockTransactionManager = new();

    public bool FullFillSellOrder(ActiveOrder mySellOrder)
    {
        ActiveOrder matchingBuyOrder = new();

        //Saving the orderobject in the database and return its ID
        int myOrderId = activeOrderDB.SaveActiveOrder(mySellOrder);

        //If we cant find any matching order at all we return false. 
        if (activeOrderManager.GetCompatibleBuyOrder(mySellOrder) == null)
        {
            return false;
        }
        else
        {


            while (true)
            {
                //Reads the order again from the database to check if its set to inactive. 
                mySellOrder = activeOrderDB.GetActiveOrderById(myOrderId);

                if (mySellOrder.IsActive == false)
                {
                    //Our order was set to inactive and sucessfull. 
                    return true;
                }

                //Retrieving the matching order. 
                matchingBuyOrder = activeOrderManager.GetCompatibleBuyOrder(mySellOrder);

                //Checking if the amount on the sellorder is higher than the buyorder. 
                if (mySellOrder.Amount > matchingBuyOrder.Amount)
                {
                    int activeAmount = mySellOrder.Amount;
                    //Now we know that the full amount on the buyorder will be fullfilled. 
                    //We can then close the sellorder. 
                    activeOrderDB.CloseActiveOrder(matchingBuyOrder.Id);

                    ActiveOrder myFullFilledSellOrder = new();
                    //Create a copy of the active sellorder. 
                    myFullFilledSellOrder = mySellOrder;
                    //Changing the amount in the copy to amount that was fullfilled in the buyorder so that they match. 
                    myFullFilledSellOrder.Amount = matchingBuyOrder.Amount;
                    myFullFilledSellOrder.IsActive = false;
                    //Saving to database. . 
                    activeOrderDB.SaveActiveOrder(myFullFilledSellOrder);

                    //The remaining amount on the sellorder needs to be changed in the database. 
                    int newAmount = activeAmount - matchingBuyOrder.Amount;
                    activeOrderDB.UpdateAmountInActiveOrder(mySellOrder.Id, newAmount);
                    mySellOrder.Amount = newAmount;

                    //Both orders turns into a stocktransaction and gets saved in the database. 
                    StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(matchingBuyOrder, myFullFilledSellOrder);
                    stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                    //Now the balance on buyer and seller accounts needs to be updated. 
                    stockTransactionManager.UpdateStockAccountBalanceAfterStockTransaction(stockTransaction);
                    //Finally the stocks switches owner. 
                    stockTransactionManager.UpdateStocksToAccountAfterStockTransaction(stockTransaction);

                }

                else if ((mySellOrder.Amount < matchingBuyOrder.Amount)) //Är antalet på köpordern större än säljordern. 
                {
                    int activeAmount = matchingBuyOrder.Amount;
                    //Now we know that the full amount on the sellorder will be fullfilled. 
                    //We can then close the sellorder. 
                    activeOrderDB.CloseActiveOrder(mySellOrder.Id);


                    ActiveOrder buyerFullFilledOrder = new();
                    //Create a copy of the active buyorder. 
                    buyerFullFilledOrder = matchingBuyOrder;
                    //Changing the amount in the copy to amount that was fullfilled in the sellorder so that they match. 
                    buyerFullFilledOrder.Amount = mySellOrder.Amount;
                    buyerFullFilledOrder.IsActive = false;
                    //Saving to database. 
                    activeOrderDB.SaveActiveOrder(buyerFullFilledOrder);

                    //The remaining amount on the buyorder needs to be changed in the database. 
                    int newAmount = activeAmount - buyerFullFilledOrder.Amount;
                    activeOrderDB.UpdateAmountInActiveOrder(buyerFullFilledOrder.Id, newAmount);

                    //Both orders turns into a stocktransaction and gets saved in the database. 
                    StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(buyerFullFilledOrder, mySellOrder);
                    stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                    //Now the balance on buyer and seller accounts needs to be updated. 
                    stockTransactionManager.UpdateStockAccountBalanceAfterStockTransaction(stockTransaction);
                    //Finally the stocks switches owner. 
                    stockTransactionManager.UpdateStocksToAccountAfterStockTransaction(stockTransaction);

                }

                else //The amount on buy and sell are equal.
                {
                    //Both orders will be set to inactive in the database. 
                    activeOrderDB.CloseActiveOrder(mySellOrder.Id);
                    activeOrderDB.CloseActiveOrder(matchingBuyOrder.Id);
                    //Creating and saving the stock transaction to the database. 
                    StockTransaction stockTransaction = stockTransactionManager.CreateStockTransactionObject(matchingBuyOrder, mySellOrder);
                    stockTransactionManager.SaveStockTransactionToDataBase(stockTransaction);

                    //Now the balance on buyer and seller accounts needs to be updated. 
                    stockTransactionManager.UpdateStockAccountBalanceAfterStockTransaction(stockTransaction);
                    //Finally the stocks switches owner. 
                    stockTransactionManager.UpdateStocksToAccountAfterStockTransaction(stockTransaction);

                }
            }
        }
    }
}