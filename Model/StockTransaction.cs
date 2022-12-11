class StockTransaction
{
    public int Id { get; set; }
    public int BuyerAccountId { get; set; }
    public int SellerAccountId { get; set; }
    public double PricePerStock { get; set; }
    public int Amount { get; set; }
    public DateTime TransactionTime { get; set; }
    public double BuyerCourtage { get; set; }
    public double SellerCourtage { get; set; }
}