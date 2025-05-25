using AutoMapper;
using DTO;
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
        private readonly IMapper _iMapper;
        IOrderRepository _iOrderRepository;

        public OrderService(IOrderRepository iOrderRepository, IMapper iMapper)
        {
            _iMapper = iMapper;
            _iOrderRepository = iOrderRepository;
        }
        public async Task<OrderDTO> AddOrder(OrderDTO orderDTO)
        {
            var order = _iMapper.Map<Order>(orderDTO);
            var res = await _iOrderRepository.AddOrder(order);
            return _iMapper.Map<OrderDTO>(res);
        }
    }
}
