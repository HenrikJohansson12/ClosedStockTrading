
using MySqlConnector;

 public class DBConnection
{
      //Parent class that handles the server and user/pw for the database connection
      public MySqlConnection DBConnect()
    {   
         var connection = new MySqlConnection("Server=localhost;Database=stock_trading;Uid=root;");
        return connection;
    }
}