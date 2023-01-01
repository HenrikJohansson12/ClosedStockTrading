internal class Program
{
    private static void Main(string[] args)
    {    
        CustomerDB customerDB = new();
        LoggedInCustomerGUI loggedInUserGUI = new();

        Console.WriteLine("Välkommen till STOCK TECH \n[1] Logga in\n[2] Registrera ny kund (Inte tillgänglig)");

        if (Console.ReadKey(true).KeyChar == '1')
        {
            bool runLoop = true;
            while (runLoop == true)
            {
                 System.Console.WriteLine("Ange personnummer");
                string personalNumber = Console.ReadLine();
                System.Console.WriteLine("Ange lösenord");
                string password = Console.ReadLine();

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
    } 
}