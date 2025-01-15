namespace Delta.Domain.Models;

public class Sale
{
    public int Id { get; set; }
    public string ManagerName { get; set; }
    public DateTime UploadDate { get; set; }
    public string Client { get; set; }
    public string Product { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
