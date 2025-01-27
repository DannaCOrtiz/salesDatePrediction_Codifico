using Dapper;
using Sales_Date_Prediction.Domain.DTOs;
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
    public class SalesDatePredictionRepository : ISalesDatePredictionRepository
    {
        private readonly IDbConnection _dbConnection;

        public SalesDatePredictionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<SalesDatePredictionDTO>> GetSalesDatePrediction()
        {
            using (var connection = _dbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Option", 100);

                using (var results = await connection.QueryMultipleAsync("dbo.SP_SalesDatePrediction_Queries", parameters, commandType: CommandType.StoredProcedure))
                {
                    var salesSatePrediction = await results.ReadAsync<SalesDatePredictionDTO>();
                    return (salesSatePrediction);
                }
            }
        }
    }
}
