using Sales_Date_Prediction.Application.Features.Order.Commands;
using Sales_Date_Prediction.Domain.DTOs;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Date_Prediction.Domain.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Orders>> GetOrdersAsync();
        Task<IEnumerable<OrdersByCustomerDTO>> GetOrderByCustomerAsync(int custid);
        Task<bool> PostCreateOrderWithProductAsync(CreateOrderWithProductCommand orderWithProductDTO);

    }
}
