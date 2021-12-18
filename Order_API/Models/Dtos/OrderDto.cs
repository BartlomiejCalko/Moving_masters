using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Order_API.Models.Order;

namespace Order_API.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        [Required]
        public string ReferanceNumber { get; set; }
        [Required]
        public string ClientNewAddress { get; set; }
        public AvailableServices ChoosenService { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public string Description { get; set; }
    }
}
