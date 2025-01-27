using MediatR;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Shared;

namespace Sales_Date_Prediction.Application.Features.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery() : IRequest<BaseResponse<IEnumerable<Customer>>>;
}
