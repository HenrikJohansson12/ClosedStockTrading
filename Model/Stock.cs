class Stock
{
    public int Id { get; set; }
    public int ListingId { get; set; }
    public string Name { get; set; }
    public string Ticker { get; set; }
    public string Sector { get; set; }

    //Runtime properties
    public string ListingName { get; set; }

    public double LastKnownPrice { get; set; }
    public double LowestActiveSellPrice { get; set; }
    public double HighestActiveBuyPrice { get; set; }
    public int AmountOnCustomerAccount { get; set; }
  



}
