
using Order_API.Models;
using System.Collections.Generic;

namespace Order_API.Repository.IRepository
{
    public interface ICustomerRepo
    {
        ICollection<Customer> GetAllCustomers();
        Customer GetCustomer(int customerId);
        bool CustomerExists(string phone);
        bool CustomerExists(int id);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        bool Save();

    }
}
