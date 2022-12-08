using Dapper;
using MySqlConnector;

class StockDB : DBConnection
{
    public List <Stock> ReadAllStocks() //Varför ingen dynamic parameter?
    {
         string query = "SELECT id AS Id,listing_id AS ListingID, name AS Name, ticker AS Ticker," +
         "sector AS Sector FROM stocks;";
        

        using (var connection = DBConnect())
        {
            try
            {
                var result = connection.Query<Stock>(query).ToList();
                return result;
            }
            
            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
}