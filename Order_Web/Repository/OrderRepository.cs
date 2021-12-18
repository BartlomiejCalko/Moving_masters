using Order_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Order_Web.Repository.IRepostitory
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public OrderRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
