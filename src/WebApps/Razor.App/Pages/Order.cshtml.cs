using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.App.Models;
using Razor.App.Services;

namespace Razor.App;

public class OrderModel : PageModel
{
    private readonly IOrderService _orderService;

    public OrderModel(IOrderService orderService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        var userName = "erick";
        Orders = await _orderService.GetOrdersByUsername(userName);

        return Page();
    }       
}
