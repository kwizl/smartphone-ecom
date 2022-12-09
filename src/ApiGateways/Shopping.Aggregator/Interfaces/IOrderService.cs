﻿using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
