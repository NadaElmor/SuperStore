using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Entities.Order_Aggregate;
using SuperStore.Core.Services.Contracts;
using SuperStore.DTOs;
using SuperStore.Errors;

namespace SuperStore.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrdersController(IMapper mapper,IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<Order?>> CreateOrder(OrderDto orderDto)
        {
            var Address = _mapper.Map<Address>(orderDto.ShippingAddress);
            var Order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, Address);
            if (Order is null) return BadRequest(new ApiResponse(400));
            return Ok(Order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>?>> GetAllOrdersForUser(string buyerEmail)
        {
            var Orders =_orderService.GetAllOrdersForUserAsync(buyerEmail);
            return Ok(Orders);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Order?>> GetOrderForUser(string buyerEmail,int OrderId)
        {
            var Order = _orderService.GetOrderByIdForUserAsync(buyerEmail, OrderId);
            if (Order is null) return NotFound(new ApiResponse(404));
            return Ok(Order);
        }
       
    }
}
