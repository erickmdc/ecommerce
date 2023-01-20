namespace Razor.App.Models;

public class BasketModel
{
    public string UserName { get; set; } = string.Empty;
    public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
    public decimal TotalPrice { get; set; }
}
