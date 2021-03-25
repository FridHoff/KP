using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Product_in_stock
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int count { get; set; }
        [Required]
        public string manufacture_date { get; set; }        
        public virtual ICollection<Shop> shop { get; set; }
        public virtual ICollection<Product> product { get; set; }
        public virtual ICollection<Employee> employee { get; set; }
    }
}
