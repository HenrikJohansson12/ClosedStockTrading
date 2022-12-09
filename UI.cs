class UI
{  
    public void MainMenu ()
    {
         Customer loggedInCustomer = new();
         

         loggedInCustomer = LogInMenu ();

         loggedInCustomer.GetCustomerStockAccountsFromDataBase(); //Använda delegat??

        foreach (var stockAccount in loggedInCustomer.CustomerStockAccounts)
        {
            stockAccount.GetOwnedStocksFromDatabase();
        }

        foreach (var stockAccount in loggedInCustomer.CustomerStockAccounts)
        {
            foreach (var Stock in stockAccount.OwnedStocks)
            {
                Console.WriteLine(Stock.Id + " " + Stock.ListingId + " " + Stock.ListingName + " " + Stock.Name + " " + Stock.Sector + " " + Stock.Ticker);
            }
        }




    }


    public Customer LogInMenu() //TODO kolla så den verkligen returnerar null vid fel. 
    {
        CustomerDB customerDB = new ();
        

        while (true)
        {
          //  System.Console.WriteLine("Ange personnummer");
            string personalNumber = "19991231-1234";
          //  System.Console.WriteLine("Ange lösenord");
            string password = "password";
            if (customerDB.CustomerLogIn(personalNumber, password) != null)
            {
                Customer loggedInCustomer = customerDB.CustomerLogIn(personalNumber, password);

                return loggedInCustomer;
            }
            else Console.WriteLine("Felaktigt användarnamn eller lösenord");
        }

    }

    

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