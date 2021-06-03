using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    public class Shop
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string number { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Product_Type> type { get; set; }
        public virtual ICollection<Production_plan> plan { get; set; }
        public virtual ICollection<Product_in_stock> product { get; set; }
    }
}
