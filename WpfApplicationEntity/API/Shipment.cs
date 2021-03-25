using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Shipment
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string departure_date { get; set; }
        [Required]
        public string receiving_date { get; set; }
        [Required]
        public int count { get; set; }
        public virtual ICollection<Order> order { get; set; }        
    }
}
