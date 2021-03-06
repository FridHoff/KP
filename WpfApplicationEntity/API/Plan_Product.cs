using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Plan_Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int count { get; set; }
        public virtual ICollection<Production_plan> plan { get; set; }
        public virtual ICollection<Product> product { get; set; }
    }
}
