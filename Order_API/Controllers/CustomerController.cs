using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_API.Models;
using Order_API.Models.Dtos;
using Order_API.Repository.IRepository;
using System.Collections.Generic;

namespace Order_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "CustomerOpenAPI")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepo customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CustomerDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetCustomers()
        {
            var custList = _customerRepo.GetAllCustomers();
            var custDto = new List<CustomerDto>();
            foreach (var cust in custList)
            {
                custDto.Add(_mapper.Map<CustomerDto>(cust));
            }

            return Ok(custDto);
        }

        /// <summary>
        /// Get customer by Id
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <returns></returns>
        [HttpGet("{customerId:int}", Name = "GetCustomerById")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetCustomerById(int customerId)
        {
            var custId = _customerRepo.GetCustomer(customerId);
            if (custId == null)
            {
                return NotFound();
            }
            var custDto = _mapper.Map<CustomerDto>(custId);
            return Ok(custDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_customerRepo.CustomerExists(customerDto.Phone))
            {
                ModelState.AddModelError("", "Customer with this phone number exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cust = _mapper.Map<Customer>(customerDto);

            if (!_customerRepo.CreateCustomer(cust))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the customer: {cust.LastName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCustomerById", new { customerId = cust.Id }, cust);
        }

        [HttpPatch("{customerId:int}", Name = "UpdateCustomer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCustomer(int customerId, [FromBody] CustomerDto customerDto)
        {
            if (customerDto == null || customerId!=customerDto.Id)
            {
                return BadRequest(ModelState);
            }
            var customerObj = _mapper.Map<Customer>(customerDto);
            if (!_customerRepo.UpdateCustomer(customerObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the customer: {customerObj.LastName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{customerId:int}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCustomer(int customerId)
        {
            if (!_customerRepo.CustomerExists(customerId))
            {
                return NotFound();
            }
            var customerObj = _customerRepo.GetCustomer(customerId);
            if (!_customerRepo.DeleteCustomer(customerObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the customer: {customerObj.LastName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
