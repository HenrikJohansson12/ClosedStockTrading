class ActiveOrderManager
{
    public ActiveOrder GetCompatibleSellOrder(ActiveOrder myActiveOrder)
    {
        ActiveOrderDB activeOrderDB = new();
        List<ActiveOrder> compatibleOrders = new();
        //Retrieves a sorted list of compatible order from the database        
        compatibleOrders = activeOrderDB.GetCompatibleSellOrders(myActiveOrder.StockId, myActiveOrder.PricePerStock);
        //If the list is empty we return null. 
        if (compatibleOrders.Count == 0)
        {
            return null;
        }

        else
        {   //We return the first object in the list. 
            ActiveOrder compatibleOrder = compatibleOrders[0];
            return compatibleOrder;
        }

    }

    public ActiveOrder GetCompatibleBuyOrder(ActiveOrder myActiveOrder)
    {
        ActiveOrderDB activeOrderDB = new();
        List<ActiveOrder> compatibleOrders = new();
        //Retrieves a sorted list of compatible order from the database           
        compatibleOrders = activeOrderDB.GetCompatibleBuyOrders(myActiveOrder.StockId, myActiveOrder.PricePerStock);
        //If the list is empty we return null. 
        if (compatibleOrders.Count == 0)
        {
            return null;
        }

        else
        {   //We return the first object in the list.  
            ActiveOrder compatibleOrder = compatibleOrders[0];
            return compatibleOrder;
        }

    }


}