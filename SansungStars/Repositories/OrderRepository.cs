using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
  public  class OrderRepository : IOrderRepository
    {
        SamsungStarsContext _samsungStarsContext;

        public OrderRepository(SamsungStarsContext samsungStarsContext)
        {
            _samsungStarsContext = samsungStarsContext;
        }
        public async Task<Order> AddOrder(Order order)
        {
            await _samsungStarsContext.Orders.AddAsync(order);
            await _samsungStarsContext.SaveChangesAsync();
            //return await Task.FromResult(order);
            //return order;
        }
    }
}
