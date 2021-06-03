using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    public class Product_in_stock
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int count { get; set; }
        [Required]
        public string manufacture_date { get; set; }        
        public virtual ICollection<Shipment> Shipment { get; set; }
        public virtual Employee employee { get; set; }
        public virtual Shop shop { get; set; }
        public virtual Product product { get; set; }
    }
}
