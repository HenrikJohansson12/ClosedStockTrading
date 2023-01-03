using MySqlConnector;
using Dapper;

class ActiveOrderDB : DBConnection
{
    public int SaveActiveOrder(ActiveOrder myActiveOrder) //Saves the active order objects and return its ID from the database.  
    {
        var parameters = new DynamicParameters(myActiveOrder);

        string query = "INSERT INTO active_orders (stock_id, account_id, price_per_stock, amount, is_buy_order," +
        "order_time_stamp, is_active)" +
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
        var parameters = new DynamicParameters();
        parameters.Add("@Id", orderId);

        string query = "UPDATE active_orders SET is_active = 0 WHERE id = @Id;";

        using (var connection = DBConnect())
        {
            try
            {
                connection.Execute(query, parameters);

            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

    }



    public void UpdateAmountInActiveOrder(int orderId, int newAmount)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", orderId);
        parameters.Add("@Amount", newAmount);

        string query = "UPDATE active_orders SET amount = @Amount WHERE id = @Id;";

        using (var connection = DBConnect())
        {
            try
            {
                connection.Execute(query, parameters);

            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

    }
    public List<ActiveOrder> GetAllActiveOrdersByAccountId(int accountId)
    {   //Innerjoin stockName and listingName. 
        var parameters = new DynamicParameters();
        parameters.Add("@AccountId", accountId);
        string query = "SELECT active_orders.id AS Id, stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock, " +
        "amount AS Amount, is_buy_order AS IsBuyOrder, order_time_stamp AS OrderTimeStamp, is_active AS IsActive, stocks.name AS StockName, listing.name AS ListingName " +
        "FROM active_orders " +
        "INNER JOIN stocks ON stock_id = stocks.id " +
        "INNER JOIN listing ON stocks.listing_id = listing.id" +
        " WHERE is_active = true AND account_id = @AccountId;";



        using (var connection = DBConnect())
        {
            try
            {
                var result = connection.Query<ActiveOrder>(query, parameters).ToList();
                return result;
            }

            catch (System.Exception e)
            {
                throw e;
            }

        }
    }

    public List<ActiveOrder> GetCompatibleSellOrders(int stockId, double pricePerStock)
    {
        //We are looking for a compatible sell order and therfore sorts on the one with the lowest price first. 
        var parameters = new DynamicParameters();
        parameters.Add("@StockId", stockId);
        parameters.Add("@PricePerStock", pricePerStock);

        string query = "SELECT id AS Id,stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock," +
         "amount AS Amount, is_buy_order AS IsBuyOrder, order_time_stamp AS OrderTimeStamp, is_active AS IsActive FROM active_orders " +
         "WHERE is_active = true AND is_buy_order = false AND stock_id = @StockId AND price_per_stock <= @PricePerStock" +
         " ORDER BY price_per_stock ASC;";

        using (var connection = DBConnect())
        {
            try
            {
                List<ActiveOrder> compatableOrders = connection.Query<ActiveOrder>(query, parameters).ToList();
                return compatableOrders;
            }

            catch (System.Exception e)
            {
                throw e;
            }

        }
    }


    public List<ActiveOrder> GetCompatibleBuyOrders(int stockId, double pricePerStock)
    {

        //Since we are looking for a buyorder we check that the price is less than or equal to the buyer price. 
        //The seller will get their asked price regardless of which order we take but for fairness we pick the oldest active order first from the database. 
        var parameters = new DynamicParameters();
        parameters.Add("@StockId", stockId);
        parameters.Add("@PricePerStock", pricePerStock);

        string query = "SELECT id AS Id,stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock," +
         "amount AS Amount, is_buy_order AS IsBuyOrder, order_time_stamp AS OrderTimeStamp, is_active AS IsActive FROM active_orders " +
         "WHERE is_active = true AND is_buy_order = true AND stock_id = @StockId AND price_per_stock >= @PricePerStock" +
         " ORDER BY order_time_stamp ASC;";

        using (var connection = DBConnect())
        {
            try
            {
                List<ActiveOrder> compatableOrders = connection.Query<ActiveOrder>(query, parameters).ToList();
                return compatableOrders;
            }

            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
    public double GetHighestActiveBuyPrice(int stockId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StockId", stockId);

        string query = "SELECT MAX(price_per_stock) FROM active_orders" +
                        " WHERE is_buy_order = TRUE AND is_active = TRUE AND stock_id = @StockId;";


        using (var connection = DBConnect())
        {
            try
            {
                double highestActiveBuyPrice = connection.QuerySingle<double>(query, parameters);
                if (highestActiveBuyPrice != null)
                {
                    return highestActiveBuyPrice;
                }
                else return highestActiveBuyPrice = 0;
            }

            catch (System.Data.DataException)
            {
                double returnvalue = 0;
                return returnvalue;
            }

            catch (System.Exception e)
            {
                throw e;
            }

        }
    }


    public double GetLowestActiveSellPrice(int stockId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StockId", stockId);

        string query = "SELECT MIN(price_per_stock) FROM active_orders" +
                        " WHERE is_buy_order = FALSE AND is_active = TRUE AND stock_id = @StockId;";


        using (var connection = DBConnect())
        {
            try
            {
                double lowestActiveSellPrice = connection.QuerySingle<double>(query, parameters);

                if (lowestActiveSellPrice != null)
                {
                    return lowestActiveSellPrice;
                }
                else return lowestActiveSellPrice = 0;
            }

            catch (System.Data.DataException)
            {
                double returnvalue = 0;
                return returnvalue;
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }
    }


    public ActiveOrder GetActiveOrderById(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        string query = "SELECT id AS Id,stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock," +
         "amount AS Amount, is_buy_order AS IsBuyOrder, order_time_stamp AS OrderTimeStamp, is_active AS IsActive FROM active_orders " +
         "WHERE id = @Id;";

        using (var connection = DBConnect())
        {
            try
            {
                ActiveOrder result = connection.QuerySingle<ActiveOrder>(query, parameters);
                return result;
            }

            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
}

