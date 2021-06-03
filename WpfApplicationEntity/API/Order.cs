using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string date { get; set; }
        [Required]
        public bool status { get; set; }
        [Required]
        public int count { get; set; }
        public virtual Customer customer { get; set; }
        public virtual Employee employee { get; set; }
        public virtual Product product { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
