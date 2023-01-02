using MySqlConnector;
using Dapper;
class ListingDB : DBConnection
{
    public List<Stock> SetListingName(List<Stock> stocks)
    {
        
        foreach (var Stock in stocks)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Stock.ListingId);
            string query = "SELECT name FROM `listing` WHERE id = @Id;";


            using (var connection = DBConnect())
            {
                try
                {
                    string name = connection.ExecuteScalar<string>(query, parameters);
                    Stock.ListingName = name;

                }

                catch (System.Exception e)
                {
                    throw e;
                    
                }
            }

        }

        return stocks;
    }
}