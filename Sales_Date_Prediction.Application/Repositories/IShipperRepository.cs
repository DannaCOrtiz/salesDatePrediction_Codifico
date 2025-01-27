﻿using Sales_Date_Prediction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Date_Prediction.Domain.Repositories
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> GetShipperAsync();

    }
}
