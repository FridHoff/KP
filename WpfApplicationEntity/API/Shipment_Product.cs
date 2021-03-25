using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Shipment_Product
    {
        [Key]
        public int ID { get; set; }        
        [Required]
        public int count { get; set; }
        [Required]
        public Shipment shipment { get; set; }
        public virtual ICollection<Product_in_stock> product { get; set; }
    }
}
