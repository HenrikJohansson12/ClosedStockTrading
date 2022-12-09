// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
StockDB stockDB = new();
StockAccountDB hej = new();
ActiveOrderDB hejhej = new();
ListingDB listingDB = new();
List <Stock> Stocks = stockDB.ReadAllStocks();
Stocks = listingDB.SetListingName(Stocks);
CustomerDB customerDB = new();

UI myUi = new();
myUi.MainMenu();


ActiveOrder myActiveOrder = new();
/*myActiveOrder.AccountId = 2;
myActiveOrder.Amount = 5;
myActiveOrder.IsActive = true;
myActiveOrder.IsBuyOrder = true;
myActiveOrder.OrderTimeStamp = DateTime.Now;
myActiveOrder.PricePerStock = 2.2;
myActiveOrder.StockId= 5;
*/
int latestId;
//latestId = hejhej.CreateActiveOrder(myActiveOrder);
//Console.WriteLine(latestId);
List <ActiveOrder> myActiveOrders = new();
myActiveOrders = hejhej.GetAllActiveOrders();




String header = String.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-10}", "Id", "Ticker", "Name", "Sector", "List");
Console.WriteLine(header);
foreach (var stock in Stocks)
{
    string content= String.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-10}",
                      stock.Id, stock.Ticker, stock.Name, stock.Sector, stock.ListingName);
    Console.WriteLine($"{content}");
}

ActiveOrder myBuyOrder = CreateActiveBuyOrderObject();
hejhej.CreateActiveOrder(myBuyOrder);
              
    static ActiveOrder CreateActiveBuyOrderObject()
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



   


