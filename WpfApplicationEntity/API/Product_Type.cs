using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Product_Type
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string name { get; set; }
        public virtual ICollection<Shop> shop { get; set; }
    }
}
