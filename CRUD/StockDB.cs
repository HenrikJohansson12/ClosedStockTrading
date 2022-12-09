using Dapper;
using MySqlConnector;

class StockDB : DBConnection
{
    public List <Stock> ReadAllStocks() 
    {
        
         string query = "SELECT id AS Id,listing_id AS ListingID, name AS Name, ticker AS Ticker," +
         "sector AS Sector FROM stocks;"; //Kör en innerjoin på denna direkt. 
        

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


     public List <Stock> StocksByAccountId(int accountId) //TODO fixa bättre namn
    {
        var parameters = new DynamicParameters();
        parameters.Add("@AccountId",accountId);

         string query = "SELECT stocks.id AS Id, stocks.listing_id AS ListingId, stocks.name AS Name,"+
         " stocks.ticker AS Ticker, stocks.sector AS Sector, listing.name AS ListingName,"+
         " stocks_to_account.amount AS AmountOnCustomerAccount FROM stocks_to_account" +
         " INNER JOIN stocks ON stocks_to_account.stock_id = stocks.id"+
         " INNER JOIN listing ON listing.id = stocks.listing_id"+
         " WHERE stocks_to_account.account_id = @AccountId;";
        

        using (var connection = DBConnect())
        {
            try
            {
                List<Stock> listOfStocksByAccountId = connection.Query<Stock>(query,parameters).ToList();
                return listOfStocksByAccountId;
            }
            
            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
}