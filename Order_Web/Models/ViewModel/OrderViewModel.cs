using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_Web.Models.ViewModel
{
    public class OrderViewModel
    {
        public IEnumerable<SelectListItem> CustomerList { get; set; }
        public Order Order { get; set; }
    }
}
