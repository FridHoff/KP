//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace WpfApplicationEntity.API
//{
//    class DatabaseRequest
//    {
//            public struct NewEmployee
//            {
//            public int ID { get; set; }
//            public string fName { get; set; }
//            public string name { get; set; }
//            public string lName { get; set; }
//            public string position { get; set; }
//            public string login { get; set; }
//            public string password { get; set; }
//            public string birth_date { get; set; }
//            public string address { get; set; }
//            public string phone { get; set; }
//            public string pisitipn_set_date { get; set; }
//            public virtual ICollection<Shop> shop { get; set; }
//            public NewCustomer(int Id, string Name, string address, string phone)
//                {
//                    this.Id = Id;
//                    this.Name = Name;
//                    this.address = address;
//                    this.phone= phone;
//                }
//            }
//            static DatabaseRequest()
//            {
//            }
//            public static bool IsUser(MyDBContext objectMyDBContext, string login, string password)
//            {
//                var tmp = (
//                    from tmpUser in objectMyDBContext.Users.ToList<User>()
//                    where tmpUser.Login.CompareTo(login) == 0 && tmpUser.Password.CompareTo(password) == 0
//                    select tmpUser
//                          ).ToList();
//                if (tmp.Count == 1)
//                    return true;
//                return false;
//            }
//            public static IEnumerable<NewStudent> GetStudentsWithGroups(MyDBContext objectMyDBContext)
//            {
//                return (
//                    from tmpStudent in objectMyDBContext.Students.ToList<Student>()
//                    from tmpGroup in objectMyDBContext.Groups.ToList<Group>()
//                    where tmpStudent.Id == tmpGroup.Id
//                    select (
//                    new NewStudent(tmpStudent.Id, tmpStudent.Name, tmpStudent.Surname, tmpStudent.Patronymic, tmpGroup.Id, tmpGroup.Name)
//                    )
//                           ).ToList();
//            }
//            public static IEnumerable<Group> GetGroups(MyDBContext objectMyDBContext)
//            {
//                return objectMyDBContext.Groups.ToList();
//            }
//        }
//    }
