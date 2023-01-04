internal class Program
{
    private static void Main(string[] args)
    {    
        CustomerDB customerDB = new();
        LoggedInCustomerGUI loggedInUserGUI = new();
        
        Console.WriteLine("Welcome till STOCK TECH \n[1] Log in as customer\n[2] Register new as customer (Currently not available)");

        if (Console.ReadKey(true).KeyChar == '1')
        {
            bool runLoop = true;
            while (runLoop == true)
            {
                 System.Console.WriteLine("Enter personal-number as yyyymmdd-xxxx");
                string personalNumber = Console.ReadLine();
                System.Console.WriteLine("Enter password");
                string password = Console.ReadLine();

                if (customerDB.CustomerLogIn(personalNumber, password) != null)
                {
                    Customer loggedInCustomer = customerDB.CustomerLogIn(personalNumber, password);
                    runLoop = false;
                    //Succesfull login start the GUI main menu. 
                    loggedInUserGUI.MainMenu(loggedInCustomer);

                    break;                   
                }

                else Console.WriteLine("Wrong username/password, please try again");
            }            
        }      
    } 
}