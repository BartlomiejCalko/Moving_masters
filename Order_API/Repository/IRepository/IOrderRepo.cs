using Order_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_API.Repository.IRepository
{
    public interface IOrderRepo
    {
        ICollection<Order> GetAllOrders();
        ICollection<Order> GetOrdersForCustomer(int customerId);
        Order GetOrder(int orderId);
        bool OrderExists(string refNumber);
        bool OrderExistsId(int id);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
