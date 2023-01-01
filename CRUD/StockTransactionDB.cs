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

    public List<StockTransaction> GetAllStockTransactionsByAccountId(int accountId)
    {
        var parameters = new DynamicParameters ();
        parameters.Add("@AccountId",accountId);
        string query = "SELECT stock_transactions.id AS Id, buyer_account_id AS BuyerAccountId, seller_account_id AS SellerAccountId, "+
        "price_per_stock AS PricePerStock, amount AS Amount, transaction_time AS TransactionTime, buyer_courtage AS BuyerCourtage, "+
        "seller_courtage AS SellerCourtage, stock_id AS StockId, stocks.name AS StockName, listing.name AS ListingName "+
        "FROM `stock_transactions` " +
        "INNER JOIN stocks ON stocks.id = stock_id "+ 
        "INNER JOIN listing ON stocks.listing_id = listing.id "+
        "WHERE buyer_account_id = @AccountId OR seller_account_id = @AccountId;";

        using (var connection = DBConnect())
        {
            try
            {
                var result = connection.Query<StockTransaction>(query,parameters).ToList();
                return result;
            }

            catch (System.Exception e)
            {
                throw e;
            }

        }
    
    }

    public double GetLatestStockTransactionPrice(int stockId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(@"StockId",stockId);
        //Returnerar endast priset på den senaste ordern som gick till avslut. 
        string query = "SELECT price_per_stock FROM stock_transactions  "+
        " WHERE transaction_time = ( SELECT MAX(transaction_time) FROM stock_transactions WHERE stock_id = @StockId );";

        using (var connection = DBConnect())
        {
            try
            {
                double latestStockTransactionPrice = connection.ExecuteScalar<double>(query, parameters);
                return latestStockTransactionPrice;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}