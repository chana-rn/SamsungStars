using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _iOrderRepository;

        public OrderService(IOrderRepository iOrderRepository)
        {
            _iOrderRepository = iOrderRepository;
        }
        public async Task<Order> AddOrder(Order order)
        {
            return await _iOrderRepository.AddOrder(order);
        }
    }
}
