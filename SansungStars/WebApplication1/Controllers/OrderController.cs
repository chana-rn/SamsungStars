using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamsungStars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderDTO order)
        {
            try
            {
                //in service layer
                //if(order.OrderDate == DateTime.MinValue || order.OrderSum<0 || order.UserId<0)
                //    return BadRequest("Invalid order data");
                OrderDTO res = await _orderService.AddOrder(order);
                return Ok(res);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        }
    
}
