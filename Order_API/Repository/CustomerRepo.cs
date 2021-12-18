using Order_API.Data;
using Order_API.Models;
using Order_API.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Order_API.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            return Save();
        }

        public bool CustomerExists(string phone)
        {
            bool custExists = _context.Customers.Any(a => a.Phone.ToLower().Trim() == phone.ToLower().Trim());
            return custExists;

        }

        public bool CustomerExists(int id)
        {
            bool custExists = _context.Customers.Any(c => c.Id == id);
            return custExists;
        }

        public bool DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            return Save();
        }

        public ICollection<Customer> GetAllCustomers()
        {
            return _context.Customers.OrderBy(c => c.LastName).ToList();
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == customerId);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            return Save();
        }
    }
}
