// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

StockAccountDB hej = new();
ActiveOrderDB hejhej = new();

ActiveOrder myActiveOrder = new();
myActiveOrder.AccountId = 2;
myActiveOrder.Amount = 5;
myActiveOrder.IsActive = true;
myActiveOrder.IsBuyOrder = true;
myActiveOrder.OrderTimeStamp = DateTime.Now;
myActiveOrder.PricePerStock = 2.2;
myActiveOrder.StockId= 5;
int latestId;
//latestId = hejhej.CreateActiveOrder(myActiveOrder);
//Console.WriteLine(latestId);
List <ActiveOrder> myActiveOrders = new();
myActiveOrders = hejhej.GetAllActiveOrders();

foreach (var item in myActiveOrders)
{
   Console.WriteLine(item.AccountId);
}
