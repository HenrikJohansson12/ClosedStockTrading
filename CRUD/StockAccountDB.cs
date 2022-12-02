using Dapper;
using MySqlConnector;
class StockAccountDB : DBConnection
{
    public void UpdateBalanceStockAccount(int accountId, double depositionAmount)
    //Uppdaterar saldot på kundens aktiekonto. 
    {
        var parameters = new DynamicParameters();
        parameters.Add("@id",accountId);
        parameters.Add("@depositionAmount",depositionAmount);
         string query = "UPDATE stock_account SET balance_in_sek = balance_in_sek + @depositionAmount WHERE id = @id;";

        using (var connection = DBConnect())
        {
            try
            {
                var result = connection.Execute(query,parameters);              
            }
         
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}