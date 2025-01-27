﻿using MediatR;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Date_Prediction.Application.Features.Order.Queries.GetOrders
{
    public record GetOrdersQuery() : IRequest<BaseResponse<IEnumerable<Orders>>>;

}
