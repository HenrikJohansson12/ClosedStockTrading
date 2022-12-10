class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PersonalNumber { get; set; }
    public string Password { get; set; }

    //Runtime

    public List<StockAccount> CustomerStockAccounts { get; set; }


   
}   