using Dapper;
using MySqlConnector;


class StocksToAccountDB : DBConnection
{
    public bool CheckIfStockExistOnCustomerAccount(int stockId, int accountId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StockId",stockId);
        parameters.Add("@AccountId",accountId);
         string query = "SELECT id AS Id, account_id AS AccountId, stock_id AS StockId, amount AS Amount "
         + "FROM stocks_to_account WHERE stock_id = @StockId AND account_id = @AccountId ;";

        using (var connection = DBConnect())
        {
            try
            {   //Hittar den ett objekt returnera true
                StocksToAccount result = connection.QuerySingle<StocksToAccount>(query,parameters);
               return true;      
            }
            //Hittar den ingen data returnera false. 
            catch (System.InvalidOperationException)
            {
                return false;
            }
            catch (System.Exception e)
            {
                 throw e;

            }
        }
    }

    public void CreateNewStocksToAccount (int accountId, int stockId,int amount)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Amount",amount);
        parameters.Add("@StockId",stockId);
        parameters.Add("@AccountId",accountId);

        string query = "INSERT INTO `stocks_to_account` (`account_id`, `stock_id`, `amount`) "+ 
        "VALUES ( @AccountId, @StockId, @Amount);";


        using (var connection = DBConnect())
        {
            try
            {
                var result = connection.ExecuteScalar(query,parameters); 
                           
            }
         
            catch (System.Exception e)
            {
                 throw e;

            }
        }
    }

    public void UpdateStocksToAccount (int accountId, int stockId,int amount) 
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Amount",amount);
        parameters.Add("@StockId",stockId);
        parameters.Add("@AccountId",accountId);


        string query = "UPDATE stocks_to_account SET amount = amount + @Amount WHERE account_id = @AccountId AND stock_id = @StockId;";


        using (var connection = DBConnect())
        {
            try
            {
                var result = connection.ExecuteScalar(query,parameters); 
                           
            }
         
            catch (System.Exception e)
            {
                 throw e;

            }
        }
    }
}   
