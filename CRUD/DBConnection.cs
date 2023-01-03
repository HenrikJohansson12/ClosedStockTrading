
using MySqlConnector;

public class DBConnection
{
    //Parent class with the Connect method that the other CRUD-classes are inhereting 
    public MySqlConnection DBConnect()
    {
        var connection = new MySqlConnection("Server=localhost;Database=stock_trading;Uid=root;");
        return connection;
    }
}