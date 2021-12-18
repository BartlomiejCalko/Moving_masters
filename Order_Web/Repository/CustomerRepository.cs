using Order_Web.Models;
using Order_Web.Repository.IRepostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Order_Web.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public CustomerRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
