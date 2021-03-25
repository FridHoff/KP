using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfApplicationEntity.API
{
    class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string fName { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string lName { get; set; }
        [Required]
        public string position { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string birth_date { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string pisitipn_set_date { get; set; }       
        public virtual ICollection<Shop> shop { get; set; }
    }
}
