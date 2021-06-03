using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    public class Production_plan
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string date { get; set; }
        [Required]
        public int count { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Shop shop { get; set; }        

    }
}
