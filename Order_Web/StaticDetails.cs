using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_Web
{
    public static class StaticDetails
    {
        public static string APIBaseUrl = "https://localhost:44397/";
        public static string CustomerAPIPath = APIBaseUrl + "api/Customer/";
        public static string OrderAPIPath = APIBaseUrl + "api/Order/";
    }
}
