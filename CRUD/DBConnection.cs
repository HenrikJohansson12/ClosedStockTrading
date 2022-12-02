
using MySqlConnector;


 public class DBConnection
{
      //Parent class that handles the server and user/pw for the database connection
      public MySqlConnection DBConnect()
    {   //TODO Ändra namn på databasen till engelska. 
         var connection = new MySqlConnection("Server=localhost;Database=aktiehandel;Uid=root;");
        return connection;
    }
}