using MySqlConnector;
using Dapper;

class ActiveOrderDB : DBConnection
{
    public int CreateActiveOrder(ActiveOrder myActiveOrder)
    {
        var parameters = new DynamicParameters(myActiveOrder);

        string query = "INSERT INTO active_orders (stock_id, account_id, price_per_stock, amount, is_buy_order," +
        "order_date_time, is_active)" +
        "VALUES(@StockId, @AccountId, @PricePerStock, @Amount, @IsBuyOrder, @OrderTimeStamp, @IsActive);" +
        "SELECT MAX(id) FROM active_orders;";

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

    public void CloseActiveOrder(int orderId)
    {

    }

    public List<ActiveOrder> GetAllActiveOrders()
    {

        string query = "SELECT id AS Id,stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock," +
         "amount AS Amount, is_buy_order AS IsBuyOrder, order_date_time AS OrderTimeStamp, is_active AS IsActive FROM active_orders " +
         "WHERE is_active = true;";

        using (var connection = DBConnect())
        {
            try
            {
                var result = connection.Query<ActiveOrder>(query).ToList();
                return result;
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
}

/*

id	
stock_id	
account_id	
price_per_stock	
amount	
is_buy_order	
order_date_time	
is_active
*/