using Microsoft.EntityFrameworkCore;
using Order_API.Data;
using Order_API.Models;
using Order_API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_API.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            return Save();
        }

        public ICollection<Order> GetAllOrders()
        {
            return _context.Orders.Include(c => c.Customer).OrderBy(c => c.Id).ToList();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Include(c => c.Customer).FirstOrDefault(c => c.Id == orderId);
        }

        public ICollection<Order> GetOrdersForCustomer(int customerId)
        {
            return _context.Orders.Include(c => c.Customer).Where(c => c.CustomerId == customerId).ToList();
        }

        public bool OrderExists(string refNumber)
        {
            bool orderExists = _context.Orders.Any(c => c.ReferanceNumber == refNumber);
            return orderExists;
        }
        public bool OrderExistsId(int id)
        {
            bool orderExists = _context.Orders.Any(c => c.Id == id);
            return orderExists;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            return Save();
        }
    }
}
