class UI
{  
    public ActiveOrder CreateActiveBuyOrderObject()
    {
        ActiveOrder myBuyOrder = new();
        myBuyOrder.IsBuyOrder = true;
        myBuyOrder.IsActive = true;
        System.Console.WriteLine("Vilken aktie vill du köpa");
        myBuyOrder.StockId = Convert.ToInt32( Console.ReadLine());
        System.Console.WriteLine("Hur många vill du köpa");
        myBuyOrder.Amount= Convert.ToInt32( Console.ReadLine());
        System.Console.WriteLine("Till vilket pris?");
        myBuyOrder.PricePerStock = Convert.ToDouble( Console.ReadLine());
        myBuyOrder.OrderTimeStamp = DateTime.Now;

        myBuyOrder.AccountId = 3;

        return myBuyOrder;

    }

}