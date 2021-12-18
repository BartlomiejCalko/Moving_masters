using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_API.Models;
using Order_API.Models.Dtos;
using Order_API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "OrderOpenAPI")]
    public class OrderController : Controller
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepo orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<OrderDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrders()
        {
            var ordList = _orderRepo.GetAllOrders();
            var ordDto = new List<OrderDto>();
            foreach (var ord in ordList)
            {
                ordDto.Add(_mapper.Map<OrderDto>(ord));
            }

            return Ok(ordDto);
        }

        /// <summary>
        /// Get order by Id
        /// </summary>
        /// <param name="orderId">Id of order</param>
        /// <returns></returns>
        [HttpGet("{orderId:int}", Name = "GetOrderById")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetOrderById(int orderId)
        {
            var ordId = _orderRepo.GetOrder(orderId);
            if (ordId == null)
            {
                return NotFound();
            }
            var ordDto = _mapper.Map<OrderDto>(ordId);
            return Ok(ordDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_orderRepo.OrderExists(orderDto.ReferanceNumber))
            {
                ModelState.AddModelError("", "Order exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ord = _mapper.Map<Order>(orderDto);

            if (!_orderRepo.CreateOrder(ord))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the order: {ord.Id}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetOrderById", new { orderId = ord.Id }, ord);
        }

        [HttpPatch("{orderId:int}", Name = "UpdateOrder")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateOrder(int orderId, [FromBody] OrderUpdateDto orderDto)
        {
            if (orderDto == null || orderId != orderDto.Id)
            {
                return BadRequest(ModelState);
            }
            var orderObj = _mapper.Map<Order>(orderDto);
            if (!_orderRepo.UpdateOrder(orderObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the order: {orderObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{orderId:int}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrder(int orderId)
        {
            if (!_orderRepo.OrderExistsId(orderId))
            {
                return NotFound();
            }
            var orderObj = _orderRepo.GetOrder(orderId);
            if (!_orderRepo.DeleteOrder(orderObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the order: {orderObj.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
