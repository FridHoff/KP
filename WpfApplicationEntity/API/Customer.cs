using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Customer
    {
        [Key]
        public int  ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phone { get; set; }       
    }
}
