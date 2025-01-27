using Dapper;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Date_Prediction.Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly IDbConnection _dbConnection;

        public ShipperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Shipper>> GetShipperAsync()
        {
            using (var connection = _dbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Option", 400);

                using (var results = await connection.QueryMultipleAsync("dbo.SP_SalesDatePrediction_Queries", parameters, commandType: CommandType.StoredProcedure))
                {
                    var shipper = await results.ReadAsync<Shipper>();
                    return (shipper);
                }
            }
        }
    }
}
