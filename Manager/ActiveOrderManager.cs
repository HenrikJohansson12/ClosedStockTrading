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
            compatibleOrders.Sort(); //TODO FUNKAR JU INTE
        }

       return compatibleOrders;
    
    } 

    public ActiveOrder  CompleteAndSplitOrder(ActiveOrder completedOrder,ActiveOrder splitOrder)
    {
        ActiveOrderDB activeOrderDB = new();
        //Först sätta completedOrder som slutförd. 
        activeOrderDB.CloseActiveOrder(completedOrder.Id);
        //Sen ändra antal kvarvarande på den andra ordern. 
        int updatedAmount = splitOrder.Amount-completedOrder.Amount;
        activeOrderDB.UpdateAmountInActiveOrder(splitOrder.Id,updatedAmount);
       //Nu sparar vi ett nytt record i DB med antalet som gick till avslut och sätter den till inaktiv.
       //Detta gör vi för att hålla historiken intakt.  
        splitOrder.Amount = completedOrder.Amount;
        splitOrder.IsActive = false;
        //Sparar objektet i DB. 
      splitOrder.Id = activeOrderDB.CreateActiveOrder(splitOrder); 
      return splitOrder;
               

    }
}