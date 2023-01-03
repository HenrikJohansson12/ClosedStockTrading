class StockTransaction
{   //Database properties
    public int Id { get; set; }
    public int BuyerAccountId { get; set; }
    public int SellerAccountId { get; set; }
    public double PricePerStock { get; set; }
    public int Amount { get; set; }
    public DateTime TransactionTime { get; set; }
    public double BuyerCourtage { get; set; }
    public double SellerCourtage { get; set; }
    public int StockId { get; set; }

    //Runtime

    public double BuyerTransactionSum { get; set; }
    public double SellerTransactionSum { get; set; }
    public string StockName { get; set; }
    public string ListingName { get; set; }
    public void CalculateTotalTransactionSum ()
    {
        BuyerTransactionSum = (-PricePerStock*Amount)-BuyerCourtage;
        SellerTransactionSum = (PricePerStock*Amount)-SellerCourtage;

    }
}