using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;
public class GetOrdersListQuery : IRequest<List<OrderVm>>
{
    public string UserName { get; set; } = string.Empty;

    public GetOrdersListQuery(string userName)
    {
        UserName = userName;
    }
}