using Razor.App.Models;

namespace Razor.App.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrdersByUsername(string username);
}
