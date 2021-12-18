using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Order_API.Models.Order;

namespace Order_API.Models.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        public string ReferanceNumber { get; set; }
        [Required]
        public string ClientNewAddress { get; set; }
        public AvailableServices ChoosenService { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public string Description { get; set; }
    }
}
