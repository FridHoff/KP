using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Order
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string date { get; set; }
        [Required]
        public bool status { get; set; }        
        public virtual ICollection<Employee> Employee { get; set; }        
        public virtual ICollection<Customer> customer { get; set; }
    }
}
