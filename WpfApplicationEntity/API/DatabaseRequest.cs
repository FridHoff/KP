 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplicationEntity.API
{
    class DatabaseRequest
    {
        static DatabaseRequest()
        {
        }
        #region Сотрудник
        public static List<Employee> GetEmployees(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Employees.ToList();
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
        public static Employee GetEmployeeById(MyDBContext objectMyDBContext, int Id)
        {
            List<Employee> employee = objectMyDBContext.Employees.ToList();
            Employee employe = new Employee();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        #region Заказ
        public static List<Order> GetOrders(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Orders.ToList();
        }
        public static Order GetOrdersById(MyDBContext objectMyDBContext, int Id)
        {
            List<Order> employee = objectMyDBContext.Orders.ToList();
            Order employe = new Order();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        #region Цех
        public static List<Shop> GetShops(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Shops.ToList();
        }
        public static Shop GetShopsById(MyDBContext objectMyDBContext, int Id)
        {
            List<Shop> employee = objectMyDBContext.Shops.ToList();
            Shop employe = new Shop();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        public static string GetShopNum(MyDBContext objectMyDBContext, Shop shop)
        {            
            return shop.number.ToString();
        }
        #endregion
        #region Заказчик
        public static IEnumerable<Customer> GetCustomer(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Customers.ToList();
        }
        public static Customer GetCustomerById(MyDBContext objectMyDBContext, int Id)
        {
            List<Customer> employee = objectMyDBContext.Customers.ToList();
            Customer employe = new Customer();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        #region Отгрузка
        public static IEnumerable<Shipment> GetShipment(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Shipments.ToList();
        }
        public static Shipment GetShipmentById(MyDBContext objectMyDBContext, int Id)
        {
            List<Shipment> employee = objectMyDBContext.Shipments.ToList();
            Shipment employe = new Shipment();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        #region Продукция
        public static IEnumerable<Product> GetProduct(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Products.ToList();
        }
        public static Product GetProductById(MyDBContext objectMyDBContext, int Id)
        {
            List<Product> employee = objectMyDBContext.Products.ToList();
            Product employe = new Product();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        #region Продукция на складе
        public static IEnumerable<Product_in_stock> GetProductInStock(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Product_in_stocks.ToList();
        }
        public static Product_in_stock GetProductInStockById(MyDBContext objectMyDBContext, int Id)
        {
            List<Product_in_stock> employee = objectMyDBContext.Product_in_stocks.ToList();
            Product_in_stock employe = new Product_in_stock();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        #region План
        public static IEnumerable<Production_plan> GetPlan(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Production_plans.ToList();
        }
        public static Production_plan GetPlanById(MyDBContext objectMyDBContext, int Id)
        {
            List<Production_plan> employee = objectMyDBContext.Production_plans.ToList();
            Production_plan employe = new Production_plan();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        #region Вид продукции
        public static IEnumerable<Product_Type> GetType(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Product_types.ToList();
        }
        public static Product_Type GetTypeById(MyDBContext objectMyDBContext, int Id)
        {
            List<Product_Type> employee = objectMyDBContext.Product_types.ToList();
            Product_Type employe = new Product_Type();
            foreach (var item in employee)
            {
                if (item.ID == Id)
                    employe = item;
            }
            return employe;
        }
        #endregion
        //public static IEnumerable<>
    }
}
