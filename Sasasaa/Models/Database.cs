using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Sasasaa.Models
{
    public static class Database
    {
       public static int veteranid = 0 ;
        public static int volunteerid = 0;
        public static int orderno = 10000;
        public static List<Order> orders = new List<Order>();
        public static bool AddVeteran(Veteran p)
        {
            veteranid++;
            string connetionString;
            SqlConnection cnn;
            var name = p.name;
            var mail = p.mail;
            var age =   p.age;
            var pass = p.pass;
            var lat = p.location.latlng.lat;
            var lng = p.location.latlng.lng;
            connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query1 = "SELECT PASSWORD FROM Veteran WHERE MAIL = @mail";
            SqlCommand myCommand1 = new SqlCommand(query1, cnn);
            myCommand1.Parameters.AddWithValue("@mail", p.mail);
            using (SqlDataReader reader = myCommand1.ExecuteReader())
                if (reader.Read())
                {
                        return false;
                }
            pass = pass.GetHashCode().ToString();
            string query = "INSERT INTO Veteran(Name,Age,mail,password,lat,long)";
            query += " VALUES (@name, @age, @mail, @pass, @lat, @long)";
            SqlCommand myCommand = new SqlCommand(query, cnn);
            myCommand.Parameters.AddWithValue("@name", name);
            myCommand.Parameters.AddWithValue("@age", age);
            myCommand.Parameters.AddWithValue("@mail", mail);
            myCommand.Parameters.AddWithValue("@pass", pass);
            myCommand.Parameters.AddWithValue("@lat", lat);
            myCommand.Parameters.AddWithValue("@long", lng);
            myCommand.ExecuteNonQuery();
            Console.WriteLine("Connection Open  !");
            cnn.Close();
            return true;
        }

        internal static object getOrderLIST()
        {
            List<OrderListContent> list = new List<OrderListContent>();
            string connetionString;
            SqlConnection cnn;
            connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query1 = "SELECT * FROM ORDER_TABLE_VETERAN";
            SqlCommand myCommand1 = new SqlCommand(query1, cnn);
            using (SqlDataReader reader = myCommand1.ExecuteReader())
                while (reader.Read())
                {
                    OrderListContent or = new OrderListContent();
                    var orderid = (int)reader["ORDER_ID"];
                    var veteran_mail = (string)reader["VETERAN_MAIL"];
                    var status = (string)reader["STATUS"];
                    var volunteer_mail = "";
                    try
                    {
                        volunteer_mail = (string)reader["VOLUNTEER"];
                    }
                    catch { }
                    var price = (double)reader["PRICE"];
                    var items = (string)reader["ITEMS"];
                    or.orderid = orderid;
                    or.items = items;
                    or.price = price;
                    or.volunteer_mail = volunteer_mail;
                    or.veteran_mail = veteran_mail;
                    or.Status = status;
                    list.Add(or);
                }
            cnn.Close();
            return list;
        }

        internal static void saveorder(Accepted p)
        {
            string connetionString;
            SqlConnection cnn;
            var orderno = p.orderno;
            var name = p.name;
            connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query = "UPDATE ORDER_TABLE_VETERAN set STATUS=@status ,VOLUNTEER=@name ";
            query += "where ORDER_ID=@orderno";
            SqlCommand myCommand = new SqlCommand(query, cnn);
            myCommand.Parameters.AddWithValue("@status", "ACCEPTED");
            myCommand.Parameters.AddWithValue("@name", name);
            myCommand.Parameters.AddWithValue("@orderno", orderno);
            myCommand.ExecuteNonQuery();
            cnn.Close();
        }

        internal static List<Order> getOrder()
        {
            orders.Clear();
            string connetionString;
            SqlConnection cnn;
            connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query1 = "SELECT ORDER_ID,VETERAN_MAIL,ITEMS,PRICE FROM ORDER_TABLE_VETERAN where status!= @accepted";
            SqlCommand myCommand1 = new SqlCommand(query1, cnn);
            myCommand1.Parameters.AddWithValue("@accepted","ACCEPTED");
            using (SqlDataReader reader = myCommand1.ExecuteReader())
                while (reader.Read())
                {
                    Order or = new Order();
                    var orderid = (int)reader["ORDER_ID"];
                    var mail = (string)reader["VETERAN_MAIL"];
                    var items = (string)reader["ITEMS"];
                    var price = (double)reader["PRICE"];
                    or.orderno = orderid;
                    or.items = items;
                    or.price = price;
                    or.mail = mail;
                    getlatlong(or);
                   }
                  cnn.Close();
                    return orders;
        }

        private static void getlatlong(Order or)
        {
    string connetionString;
    SqlConnection cnn2;
    connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
    cnn2 = new SqlConnection(connetionString);
            cnn2.Open();
    string query2 = "SELECT lat,long,Name FROM Veteran where mail=@findmail";
    SqlCommand myCommand2 = new SqlCommand(query2, cnn2);
    myCommand2.Parameters.AddWithValue("@findmail", or.mail);
            var latitude = "";
            var longitude = "";
            var name = "";
            using (SqlDataReader reader1 = myCommand2.ExecuteReader())
        if (reader1.Read())
        {
            latitude = (string)reader1["lat"];
            longitude = (string)reader1["long"];
            name = (string)reader1["Name"];
        }
    or.name = name;

    or.lat = Double.Parse(latitude);
    or.longitude = Double.Parse(longitude);

    orders.Add(or);

    cnn2.Close();
}

        internal static double[] AddOrder(Items p)
        {
            string a="";
            double price = 0;
            string connetionString;
            SqlConnection cnn;
            double[] t = new double[3];
            connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query1 = "SELECT ORDER_ID FROM ORDER_TABLE_VETERAN WHERE VETERAN_MAIL = @mail and status='NOT ACCEPTED'";
            SqlCommand myCommand1 = new SqlCommand(query1, cnn);
            myCommand1.Parameters.AddWithValue("@mail", p.mail);
            using (SqlDataReader reader = myCommand1.ExecuteReader())
                if (reader.Read())
                {
                    t[0] = 0;
                    return t;
                }
            orderno++;
            for (int i = 0; i < 30; i++)
                    {
                        if (p.itemlist[i] == 1)
                        {
                            a = a + Pricelist.pricelist.ElementAt(i).Key.ToString() + "-" + p.quantity[i] + ",";

                            price = price + p.quantity[i] * Pricelist.pricelist[Pricelist.pricelist.ElementAt(i).Key.ToString()];
                        }
                    }
                    if (!a.Equals(""))
                        a = a.Remove(a.Length - 1);

                    string query = "INSERT INTO ORDER_TABLE_VETERAN(ITEMS,PRICE,VETERAN_MAIL,Order_ID,STATUS)";
                    query += " VALUES (@ITEMS, @PRICE, @VETERAN_MAIL, @Order_ID , @STATUS_ID)";
                    SqlCommand myCommand = new SqlCommand(query, cnn);
                    myCommand.Parameters.AddWithValue("@ITEMS", a);
                    myCommand.Parameters.AddWithValue("@PRICE", price);
                    myCommand.Parameters.AddWithValue("@VETERAN_MAIL", p.mail);
                    myCommand.Parameters.AddWithValue("@Order_ID", orderno);
                    myCommand.Parameters.AddWithValue("@STATUS_ID", "NOT ACCEPTED");
                    myCommand.ExecuteNonQuery();
                    Console.WriteLine("Connection Open  !");
                    cnn.Close();
                    t[0] = 1;
                    t[1] = Database.orderno;
                    t[2] = price;
                    return t;
        }

        internal static bool CheckpasswordVolunteer(Password p)
        {
            string connetionString;
            SqlConnection cnn;
            var mail = p.mail;
           var pass = p.pass;
            var boolean = false;
           connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
           cnn = new SqlConnection(connetionString);
           cnn.Open();
            pass = pass.GetHashCode().ToString();
            string query = "SELECT PASSWORD FROM Volunteer WHERE MAIL = @mail";
           SqlCommand myCommand = new SqlCommand(query, cnn);
           myCommand.Parameters.AddWithValue("@mail", mail);
            using (SqlDataReader reader = myCommand.ExecuteReader())
                if (reader.Read())
                {
                    if (pass == (string)reader["password"])
                        boolean =  true;
                }
            return boolean;
        }

        internal static bool CheckpasswordVeteran(Password p)
        {
            string connetionString;
            SqlConnection cnn;
            var mail = p.mail;
            var pass = p.pass;
            var boolean = false;
            connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query = "SELECT PASSWORD FROM Veteran WHERE MAIL = @mail";
            SqlCommand myCommand = new SqlCommand(query, cnn);
            myCommand.Parameters.AddWithValue("@mail", mail);
            var md5 = new MD5CryptoServiceProvider();
            pass = pass.GetHashCode().ToString();
            using (SqlDataReader reader = myCommand.ExecuteReader())
                if (reader.Read())
                {
                    if (pass == (string)reader["password"])
                        boolean = true;
                }
            return boolean;
        }


        public static bool AddVolunteer(Volunteer p)
        {
            volunteerid++;
            var name = p.name;
            var mail = p.mail;
            var age = p.age;
            var pass = p.pass;
            var lat = p.location.latlng.lat;
            var lng = p.location.latlng.lng;
            string connetionString;
            SqlConnection cnn;
            connetionString = "Server=localhost;Database=master;Trusted_Connection=True; Initial Catalog=Veteranhelp;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query1 = "SELECT PASSWORD FROM Volunteer WHERE MAIL = @mail";
            SqlCommand myCommand1 = new SqlCommand(query1, cnn);
            myCommand1.Parameters.AddWithValue("@mail", p.mail);
            using (SqlDataReader reader = myCommand1.ExecuteReader())
                if (reader.Read())
                {
                    return false;
                }
            pass = pass.GetHashCode().ToString();
            string query = "INSERT INTO Volunteer(Name,Age,mail,password)";
            query += " VALUES (@name, @age, @mail, @pass)";
            SqlCommand myCommand = new SqlCommand(query, cnn);
            myCommand.Parameters.AddWithValue("@name", name);
            myCommand.Parameters.AddWithValue("@age", age);
            myCommand.Parameters.AddWithValue("@mail", mail);
            myCommand.Parameters.AddWithValue("@pass", pass);
            myCommand.ExecuteNonQuery();
            Console.WriteLine("Connection Open  !");
            cnn.Close();
            return true;
        }

    }
}