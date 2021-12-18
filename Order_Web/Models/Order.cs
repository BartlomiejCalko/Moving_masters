using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Order_Web.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string ReferanceNumber { get; set; }
        [Required]
        public string ClientNewAddress { get; set; }
        public enum AvailableServices { Moving = 1, Packing, Cleaning }
        public AvailableServices ChoosenService { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Description { get; set; }
    }
}
