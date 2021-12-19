using Microsoft.AspNetCore.Mvc;
using Order_Web.Models;
using Order_Web.Repository.IRepostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View(new Customer() { });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            Customer obj = new Customer();
            if(id == null)
            {
                return View(obj);
            }
            obj = await _customerRepository.GetAsync(StaticDetails.CustomerAPIPath, id.GetValueOrDefault());
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Customer customer)
        {
            
                if (customer.Id == 0)
                {
                    await _customerRepository.CreateAsync(StaticDetails.CustomerAPIPath, customer);
                }
                else
                {
                    await _customerRepository.UpdateAsync(StaticDetails.CustomerAPIPath + customer.Id, customer);
                }
                return RedirectToAction(nameof(Index));        
        }

        public async Task<IActionResult> GetAllCustomers()
        {
            return Json(new { data = await _customerRepository.GetAllAsync(StaticDetails.CustomerAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _customerRepository.DeleteAsync(StaticDetails.CustomerAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete successful" });
            }
            return Json(new { success = false, message = "Delete not successful" });
        }


    }
}
