using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplicationEntity.API
{
    class DatabaseRequest
    {
        public struct NewEmployee
        {
            public int ID { get; set; }
            public string fName { get; set; }
            public string name { get; set; }
            public string lName { get; set; }
            public string position { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public string birth_date { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string position_set_date { get; set; }
            public ICollection<Shop> shop { get; set; }
            public NewEmployee(int Id, string fName, string name, string lName, string position,
                string login, string password, string birth_date, string address, string phone,
                string position_set_date, ICollection<Shop> shop)
            {
                this.ID = Id;
                this.fName = fName;
                this.name = name;
                this.lName = lName;
                this.position = position;
                this.login = login;
                this.password = password;
                this.birth_date = birth_date;
                this.address = address;
                this.phone = phone;
                this.position_set_date = position_set_date;
                this.shop = shop;
            }
        }
        static DatabaseRequest()
        {
        }
        public static bool IsEmployee(MyDBContext objectMyDBContext, string login, string password)
        {
            var tmp = (
                from tmpUser in objectMyDBContext.Employees.ToList<Employee>()
                where tmpUser.login.CompareTo(login) == 0 && tmpUser.password.CompareTo(password) == 0
                select tmpUser
                      ).ToList();
            if (tmp.Count == 1)
                return true;
            return false;
        }
        public static IEnumerable<NewEmployee> GetEmployeesWithShops(MyDBContext objectMyDBContext)
        {
            return (
                from tmpEmployee in objectMyDBContext.Employees.ToList<Employee>()
                from tmpShop in objectMyDBContext.Shops.ToList<Shop>()
                where tmpEmployee.shop == tmpShop
                select (
                new NewEmployee(tmpEmployee.ID, tmpEmployee.fName, tmpEmployee.name, tmpEmployee.lName, tmpEmployee.position,
                tmpEmployee.login, tmpEmployee.password, tmpEmployee.birth_date, tmpEmployee.address, tmpEmployee.phone,
                tmpEmployee.position_set_date, tmpEmployee.shop))).ToList();
        }
        public static IEnumerable<Shop> GetShops(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Shops.ToList();
        }
    }
}
