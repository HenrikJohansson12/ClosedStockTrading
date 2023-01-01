class ActiveOrder
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public int AccountId { get; set; }
    public double PricePerStock { get; set; }
    public int Amount { get; set; }
    public bool IsBuyOrder { get; set; }
    public DateTime OrderTimeStamp { get; set; }
    public bool IsActive { get; set; }

    //Runtime
    public string StockName { get; set; }
    public string ListingName { get; set; }


}