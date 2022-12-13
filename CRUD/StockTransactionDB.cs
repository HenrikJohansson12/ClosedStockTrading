using Dapper;
using MySqlConnector;
class StockTransactionDB : DBConnection
{
    public int SaveStockTransactionToDataBase(StockTransaction myStockTransaction)
    {
        var parameters = new DynamicParameters(myStockTransaction);

        string query = "INSERT INTO stock_transactions (buyer_account_id, seller_account_id, price_per_stock, amount, transaction_time," +
        "buyer_courtage, seller_courtage, stock_id)" +
        "VALUES(@BuyerAccountId, @SellerAccountId, @PricePerStock, @Amount, @TransactionTime, @BuyerCourtage, @SellerCourtage, @StockId);" +
        "SELECT MAX(id) FROM stock_transactions;";

        using (var connection = DBConnect())
        {
            try
            {
                var id = connection.ExecuteScalar<int>(query, parameters);
                return id;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}