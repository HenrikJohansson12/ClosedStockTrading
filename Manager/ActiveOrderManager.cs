class ActiveOrderManager
{
    public ActiveOrder GetCompatibleSellOrder(ActiveOrder myActiveOrder)
    {
        ActiveOrderDB activeOrderDB = new();
        List<ActiveOrder> compatibleOrders = new();
        //Vi hämtar en sorterad lista med matchande ordrar.         
        compatibleOrders = activeOrderDB.GetCompatibleSellOrders(myActiveOrder.StockId,myActiveOrder.PricePerStock);
        //Är listan tom returerar vi null. 
        if (compatibleOrders.Count == 0)
        {
            return null;
        }

        else
        {   //Finns det något i listan så returnerar vi det första objektet. 
            ActiveOrder compatibleOrder = compatibleOrders[0];
            return compatibleOrder;
        }
 
    } 

 public ActiveOrder GetCompatibleBuyOrder(ActiveOrder myActiveOrder)
    {
        ActiveOrderDB activeOrderDB = new();
        List<ActiveOrder> compatibleOrders = new();
        //Vi hämtar en sorterad lista med matchande ordrar.         
        compatibleOrders = activeOrderDB.GetCompatibleBuyOrders(myActiveOrder.StockId,myActiveOrder.PricePerStock);
        //Är listan tom returerar vi null. 
        if (compatibleOrders.Count == 0)
        {
            return null;
        }

        else
        {   //Finns det något i listan så returnerar vi det första objektet. 
            ActiveOrder compatibleOrder = compatibleOrders[0];
            return compatibleOrder;
        }
 
    } 

}