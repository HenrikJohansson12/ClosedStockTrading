class ActiveOrderManager
{
    public List<ActiveOrder> GetCompatibleSellOrders(ActiveOrder myActiveOrder)
    {
        ActiveOrderDB activeOrderDB = new();
        List<ActiveOrder> compatibleOrders = new();
         bool isBuyOrder = false;
        //Ordern vi ska kolla ska vara motsatsen mot ordern som precis lagts. 
        if (myActiveOrder.IsBuyOrder == true)
        {
           isBuyOrder = false;
        }
        compatibleOrders = activeOrderDB.GetAllCompatableActiveOrders(isBuyOrder,myActiveOrder.StockId,myActiveOrder.PricePerStock);

        //Om vi letar efter en köporder sorterar vi listan efter den äldsta köpordern. 
        if (isBuyOrder == true)
        {
            compatibleOrders.OrderBy(x=>x.OrderTimeStamp);
        }
        //Letar vi efter en säljorder vill vi köpa av den som säljer för lägst pris. 
        else
        {
            compatibleOrders.OrderBy(x=>x.PricePerStock); //TODO FUNKAR JU INTE
        }

       return compatibleOrders;
    
    } 


}