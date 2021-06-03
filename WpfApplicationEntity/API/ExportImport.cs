using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace WpfApplicationEntity.API
{
    static class ExportImport
    {
        private static readonly string ConnectionString =  ConfigurationManager.ConnectionStrings["DbConnectString"].ConnectionString;
        private static StreamWriter File;
        //static string path;
        public static void ExportDataBase()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    File = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Confectionery.csv", false, Encoding.Unicode);
                    SelectTable("SELECT * FROM Employees", connection);//0
                    SelectTable("SELECT * FROM Customers", connection);//1
                    SelectTable("SELECT * FROM Orders", connection);//2
                    SelectTable("SELECT * FROM Products", connection);//3
                    SelectTable("SELECT * FROM Product_in_stock", connection);//4
                    SelectTable("SELECT * FROM Product_Type", connection);//5
                    SelectTable("SELECT * FROM Production_plan", connection);//6
                    SelectTable("SELECT * FROM Shipments", connection);//7
                    SelectTable("SELECT * FROM Shops", connection);       //8             
                    File.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    connection.Dispose();
                    MessageBox.Show("База данных успешно сохранена", "Успех");
                }
            }
        }
        private static void SelectTable(string query, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader SQLreader = command.ExecuteReader();
            if (SQLreader.HasRows)
            {
                File.WriteLine("#");
                while (SQLreader.Read())
                {
                    for (int i = 1; i < SQLreader.FieldCount; i++)
                        File.Write(SQLreader.GetValue(i).ToString() + ";");
                    File.WriteLine("$");
                }
            }
            connection.Close();
        }
        private static void InsertTable(string query, string[] values, int count, SqlConnection connection)
        {
            query += $" values (";
            for (int i = 0; i < count; i++)
            {           
                query += $"'{values[i]}', ";
            }
            query = query.Substring(0, query.Length - 2);
            query += ")";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public static void ImportDataBase() // если не работает попробуй убрать опцию разделения
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                //try 
                //{
                    using (StreamReader streamReader = new StreamReader("Confectionery.csv"))
                    {
                        string[] tables = streamReader.ReadToEnd().Replace(Environment.NewLine, "").Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tables.Length == 9)
                        {
                            for (int i = 0; i < tables[8].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[8].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Shops(number)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 1, connection);
                            }
                            for (int i = 0; i < tables[0].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[0].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Employees(fName,name,lName,position, login,password,birth_date, address, phone,position_set_date, shop_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 11, connection);
                            }
                            for (int i = 0; i < tables[5].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[5].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Product_Type(name, shop_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 2, connection);
                            }
                            for (int i = 0; i < tables[6].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[6].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Production_Plan(date, count,shop_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 3, connection);
                            }
                            for (int i = 0; i < tables[3].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[3].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Products(name,price,shelf_life, type_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 4, connection);
                            }
                            for (int i = 0; i < tables[4].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[4].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Product_in_stock(count,manufacture_date, employee_ID, shop_ID,product_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 5, connection);
                            }
                            for (int i = 0; i < tables[1].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[1].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Customers(Name,address,phone)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 3, connection);
                            }
                            for (int i = 0; i < tables[2].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[2].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Orders(date,status,count,customer_ID,employee_ID, product_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 6, connection);
                            }
                            for (int i = 0; i < tables[7].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[7].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Shipments(departure_date,receiving_date,count,Order_ID,product_in_stock_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 5, connection);
                            }                          
                        }
                        else MessageBox.Show("Количество таблиц не соответсвует таблицам базы данных", "Ошибка");
                        streamReader.Close();
                    }
                    connection.Close();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //finally
                //{
                //    if (connection.State == ConnectionState.Open)
                //        connection.Close();
                //    connection.Dispose();
                //}
            }
        }
    }
}
