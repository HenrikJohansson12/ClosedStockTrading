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

            catch (System.Exception e)
            {
                throw e;
            }

        }
    }

    public List<ActiveOrder> GetCompatibleSellOrders(int stockId, double pricePerStock)
    {
        //Vi letar efter en säljorder och vi vill sortera på de som vill sälja billigast först. 
        var parameters = new DynamicParameters();
        parameters.Add("@StockId", stockId);
        parameters.Add("@PricePerStock", pricePerStock);
       
        string query = "SELECT id AS Id,stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock," +
         "amount AS Amount, is_buy_order AS IsBuyOrder, order_date_time AS OrderTimeStamp, is_active AS IsActive FROM active_orders " +
         "WHERE is_active = true AND is_buy_order = false AND stock_id = @StockId AND price_per_stock <= @PricePerStock"+
         " ORDER BY price_per_stock ASC;";

        using (var connection = DBConnect())
        {
            try
            {
                List<ActiveOrder> compatableOrders = connection.Query<ActiveOrder>(query,parameters).ToList();
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
        //Vi letar efter en köporder så kollar vi så att priset är mindre eller lika med köparens pris. 
        //Säljaren kommer få sitt efterfrågade pris oavsett vilken order vi säljer så vi sorterar på äldsta först så blir det rättvist. 
        var parameters = new DynamicParameters();
        parameters.Add("@StockId", stockId);
        parameters.Add("@PricePerStock", pricePerStock);
       
        string query = "SELECT id AS Id,stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock," +
         "amount AS Amount, is_buy_order AS IsBuyOrder, order_date_time AS OrderTimeStamp, is_active AS IsActive FROM active_orders " +
         "WHERE is_active = true AND is_buy_order = true AND stock_id = @StockId AND price_per_stock >= @PricePerStock"+
         " ORDER BY order_date_time ASC;";

        using (var connection = DBConnect())
        {
            try
            {
                List<ActiveOrder> compatableOrders = connection.Query<ActiveOrder>(query,parameters).ToList();
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
                if (highestActiveBuyPrice != null) //TODO
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
        parameters.Add("@Id",id);
        string query = "SELECT id AS Id,stock_id AS StockId, account_id AS AccountId, price_per_stock AS PricePerStock," +
         "amount AS Amount, is_buy_order AS IsBuyOrder, order_date_time AS OrderTimeStamp, is_active AS IsActive FROM active_orders " +
         "WHERE id = @Id;";

        using (var connection = DBConnect())
        {
            try
            {
                ActiveOrder result = connection.QuerySingle<ActiveOrder>(query,parameters);
                return result;
            }

            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
}

