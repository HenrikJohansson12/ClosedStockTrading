class StockAccount
{   //Database properties
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int AccountTypeId { get; set; }
    public double BalanceInSek { get; set; }

    //Runtime
    public string AccountName { get; set; }
    public double TaxRate { get; set; }

    public List <Stock> OwnedStocks { get; set; }

    public double TotalStockValue { get; set; }


    public void CalculateTotalStockValue()
    {
        foreach (var stock in OwnedStocks)
        {
            TotalStockValue = TotalStockValue+(stock.LastKnownPrice*stock.AmountOnCustomerAccount);
        }
    }
}