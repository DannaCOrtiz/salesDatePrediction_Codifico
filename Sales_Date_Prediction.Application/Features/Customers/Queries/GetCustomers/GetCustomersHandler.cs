
using MediatR;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Repositories;
using Sales_Date_Prediction.Domain.Shared;

namespace Sales_Date_Prediction.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, BaseResponse<IEnumerable<Customer>>>
    {
        private readonly ICustomerRepository _customerRepository;


        public GetCustomersHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<BaseResponse<IEnumerable<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepository.GetCustomersAsync();
                if (customer.Count() > 0)
                {
                    return Response.SuccessResponse(customer, true, 200, "OK");
                }
                else return Response.CustomResponse<IEnumerable<Customer>>(204, "No se encontraron datos");
            }
            catch
            {
                return Response.CustomResponse<IEnumerable<Customer>>(500, "Ha ocurrido un error");
            }
        }
    }

}
