using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    public class Product_Type
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string name { get; set; }
        public virtual Shop shop { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
