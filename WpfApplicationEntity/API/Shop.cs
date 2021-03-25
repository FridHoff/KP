using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Shop
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int number { get; set; }
    }
}
