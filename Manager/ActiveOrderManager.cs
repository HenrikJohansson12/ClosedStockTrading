class ActiveOrderManager
{
    public List<ActiveOrder> LookForCompatibleOrdersAfterPlacingOrder(ActiveOrder myActiveOrder)
    {
        ActiveOrderDB activeOrderDB = new();
        List<ActiveOrder> compatibleOrders = new();
        //Ordern vi ska kolla ska vara motsatsen mot ordern som precis lagts. 
        bool isBuyOrder = !myActiveOrder.IsBuyOrder;
        
        compatibleOrders = activeOrderDB.GetAllCompatableActiveOrders(isBuyOrder,myActiveOrder.StockId,myActiveOrder.PricePerStock);

        //Om vi letar efter en köporder sorterar vi listan efter den äldsta köpordern. 
        if (isBuyOrder == true)
        {
            compatibleOrders.OrderBy(x=>x.OrderTimeStamp);
        }
        //Letar vi efter en säljorder vill vi köpa av den som säljer för lägst pris. 
        else
        {
            compatibleOrders.OrderByDescending(x=>x.PricePerStock);
        }

       return compatibleOrders;
    
    } 
}