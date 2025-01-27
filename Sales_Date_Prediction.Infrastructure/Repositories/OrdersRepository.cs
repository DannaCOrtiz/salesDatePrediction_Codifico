﻿using Dapper;
using Sales_Date_Prediction.Application.Features.Order.Commands;
using Sales_Date_Prediction.Domain.DTOs;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Repositories;
using Sales_Date_Prediction.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Date_Prediction.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrdersRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<OrdersByCustomerDTO>> GetOrderByCustomerAsync(int custid)
        {
            using (var connection = _dbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Option", 200);
                parameters.Add("custid", custid);

                using (var results = await connection.QueryMultipleAsync("dbo.SP_SalesDatePrediction_Queries", parameters, commandType: CommandType.StoredProcedure))
                {
                    var orders = await results.ReadAsync<OrdersByCustomerDTO>();
                    return (orders);
                }
            }
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync()
        {
            using (var connection = _dbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Option", 800);

                using (var results = await connection.QueryMultipleAsync("dbo.SP_SalesDatePrediction_Queries", parameters, commandType: CommandType.StoredProcedure))
                {
                    var orders = await results.ReadAsync<Orders>();
                    return (orders);
                }
            }
        }

        public async Task<bool> PostCreateOrderWithProductAsync(CreateOrderWithProductCommand orderWithProductDTO)
        {
            using (var connection = _dbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@custid", orderWithProductDTO.custid);
                parameters.Add("@EmpID", orderWithProductDTO.empid);
                parameters.Add("@ShipperID", orderWithProductDTO.shipperid);
                parameters.Add("@ShipName", orderWithProductDTO.shipname);
                parameters.Add("@ShipAddress", orderWithProductDTO.shipaddress);
                parameters.Add("@ShipCity", orderWithProductDTO.shipcity);
                parameters.Add("@OrderDate", orderWithProductDTO.orderdate);
                parameters.Add("@RequiredDate", orderWithProductDTO.requireddate);
                parameters.Add("@ShippedDate", orderWithProductDTO.shippeddate);
                parameters.Add("@Freight", orderWithProductDTO.freight);
                parameters.Add("@ShipCountry", orderWithProductDTO.shipcountry);
                parameters.Add("@ProductID", orderWithProductDTO.productid);
                parameters.Add("@UnitPrice", orderWithProductDTO.unitprice);
                parameters.Add("@Qty", orderWithProductDTO.qty);
                parameters.Add("@Discount", orderWithProductDTO.discount);

                using (var results = await connection.QueryMultipleAsync("dbo.SP_SalesDatePrediction_Commands", parameters, commandType: CommandType.StoredProcedure))
                {
                    var orders = await results.ReadFirstAsync<bool>();
                    return orders;
                }
            }
        }
    }
}
