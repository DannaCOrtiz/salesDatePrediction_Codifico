using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Sales_Date_Prediction.Application.Features.Customers.Queries.GetCustomers;
using Sales_Date_Prediction.Application.Features.Employees.Queries.GetAllEmployees;
using Sales_Date_Prediction.Application.Features.Order.Queries.GetOrders;
using Sales_Date_Prediction.Application.Features.Products.Queries.GetAllProducts;
using Sales_Date_Prediction.Application.Features.Shippers.Queries.GetAllShippers;
using Sales_Date_Prediction.Domain.Repositories;
using Sales_Date_Prediction.Infrastructure.Repositories;
using System.Data;

namespace Sales_Date_Prediction.Infrastructure
{
    public static class InfrastructureIndependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Registrar conexión con Dapper
            services.AddScoped<IDbConnection>(provider => new SqlConnection(connectionString));
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetCustomersQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetAllEmployeesQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetOrdersQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetAllProductsQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetAllShippersQuery>();
            });

            // Registrar el repositorio
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShipperRepository, ShipperRepository>();
            services.AddScoped<ISalesDatePredictionRepository, SalesDatePredictionRepository>();

            return services;
        }
    }
}
