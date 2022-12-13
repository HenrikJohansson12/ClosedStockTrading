internal class Program
{
    private static void Main(string[] args)
    {
        ActiveOrderDB _activeOrderDB = new();
        StockDB stockDB = new();
        StockAccountDB hej = new();
        ActiveOrderDB hejhej = new();
        ListingDB listingDB = new();
        List<Stock> Stocks = stockDB.ReadAllStocks();
        Stocks = listingDB.SetListingName(Stocks);
        CustomerDB customerDB = new();
        LoggedInUserGUI loggedInUserGUI = new();


        Console.WriteLine(_activeOrderDB.GetHighestActiveBuyPrice(5));
        Console.WriteLine(_activeOrderDB.GetLowestActiveSellPrice(5));


        Console.WriteLine("Välkommen till STOCK TECH \n[1] Logga in\n[2] Registrera ny kund (Inte tillgänglig)");

        if (Console.ReadKey(true).KeyChar == '1')
        {
            bool runLoop = true;
            while (runLoop == true)
            {
                //  System.Console.WriteLine("Ange personnummer");
                string personalNumber = "19991231-1234";
                //  System.Console.WriteLine("Ange lösenord");
                string password = "password";
                if (customerDB.CustomerLogIn(personalNumber, password) != null)
                {
                    Customer loggedInCustomer = customerDB.CustomerLogIn(personalNumber, password);
                    runLoop = false;
                    loggedInUserGUI.MainMenu(loggedInCustomer);

                    break;
                    
                    
                }
                else Console.WriteLine("Felaktigt användarnamn eller lösenord");
            }
            
            

        }

        string header = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-10}", "Id", "Ticker", "Name", "Sector", "List");
        Console.WriteLine(header);
        foreach (var stock in Stocks)
        {
            string content = string.Format("{0,-10} {1,-10} {2,-30} {3,-25} {4,-10}",
                              stock.Id, stock.Ticker, stock.Name, stock.Sector, stock.ListingName);
            Console.WriteLine($"{content}");
        }

       

    }
  
}