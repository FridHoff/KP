using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public string shelf_life { get; set; }   
        public virtual Product_Type type { get; set; }
        public virtual ICollection<Production_plan> plan { get; set; }
        public virtual ICollection<Product_in_stock> Product_In_Stocks { get; set; }
        public virtual ICollection<Order> order { get; set; }

    }
}
