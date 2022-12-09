using Dapper;
using MySqlConnector;
class StockAccountDB : DBConnection
{

    public List <StockAccount> GetCustomerStockAccountFromDataBase (int customerId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CustomerId",customerId);

        string query = "SELECT stock_account.id AS Id, customer_id AS CustomerID, account_type_id as AccountTypeId,"+
        " balance_in_sek AS BalanceInSek, account_type.name AS AccountName, account_type.tax_rate AS TaxRate" +
        " FROM `stock_account` INNER JOIN account_type ON account_type_id = account_type.id WHERE stock_account.customer_id = @CustomerId;";
         
          using (var connection = DBConnect())
         try
            {
                List <StockAccount> stockAccountList = connection.Query<StockAccount>(query,parameters).ToList();
                return stockAccountList;          
            }
         
            catch (System.Exception e)
            {
                throw e;
            }

    }

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