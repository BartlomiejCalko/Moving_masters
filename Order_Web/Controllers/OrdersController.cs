using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Order_Web.Models;
using Order_Web.Models.ViewModel;
using Order_Web.Repository.IRepostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IOrderRepository _orderRepo;

        public OrdersController(ICustomerRepository customerRepo, IOrderRepository orderRepo)
        {
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
        }

        public IActionResult Index()
        {
            return View(new Order() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Customer> customersList = await _customerRepo.GetAllAsync(StaticDetails.CustomerAPIPath);

            OrderViewModel objVM = new OrderViewModel()
            {
                CustomerList = customersList.Select(i => new SelectListItem {
                    Text = i.LastName,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                return View(objVM);
            }
            objVM.Order = await _orderRepo.GetAsync(StaticDetails.OrderAPIPath, id.GetValueOrDefault());
            if (objVM.Order == null)
            {
                return NotFound();
            }

            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(OrderViewModel orderViewModel)
        {

            if (orderViewModel.Order.Id == 0)
            {
                await _orderRepo.CreateAsync(StaticDetails.OrderAPIPath, orderViewModel.Order);
            }
            else
            {
                await _orderRepo.UpdateAsync(StaticDetails.OrderAPIPath + orderViewModel.Order.Id, orderViewModel.Order);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAllOrders()
        {
            return Json(new { data = await _orderRepo.GetAllAsync(StaticDetails.OrderAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _orderRepo.DeleteAsync(StaticDetails.OrderAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete successful" });
            }
            return Json(new { success = false, message = "Delete not successful" });
        }




    }
}
