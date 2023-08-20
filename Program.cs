using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderdbDatabaseFirst
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //To connect with SQL Server database we need to fetch connection string
            string connectionstring = "data source=LAPTOP-E1HH8098;initial catalog=OrdersDB;integrated security=True";
            
            //Create object for SQL connection class
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();




            //1. READ (Fetch the Product table)

            string query = "SELECT * FROM PRODUCT";

            //To execute the query we need to create SQL command class
            SqlCommand cmd = new SqlCommand(query, connection);


            //To execute the query we need to open the connection
            connection.Open();

            //To read the data from database we use sql datareader 
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.HasRows)          //HasRows property will check that the tables has any rows/data. It return true/false.
            { 
                while(reader.Read())
                {
                    Console.WriteLine("{0}  {1}  {2} ", reader[0], reader[1], reader[2]);
                }
            }

            var dbcontext = new OrdersDBEntities1();        //Instance of DbContext Class

            //2. Insert


            //Manuallu insert a record into the table
            var product = new Product()                     
            {
                Id = 106,
                Product_Name = "Conditioner",
                Product_Description = "Sunsilk Conditioner"


            };
            dbcontext.Products.Add(product);               
            dbcontext.SaveChanges();                      

            
           
           
            // User Input function to insert record into the table
             Console.WriteLine("Enter product id : ");
             int id_p = Convert.ToInt32(Console.ReadLine());

             Console.WriteLine("Enter product name : ");
             string p_name = Console.ReadLine();

             Console.WriteLine("Enter product description : ");
             string p_desc = Console.ReadLine();

             string insertquery = "insert into product(Id, Product_Name, Product_Description) values ('"+id_p+ "','"+p_name+"','"+p_desc+"')";
             SqlCommand insertcommand = new SqlCommand(insertquery, connection);
             insertcommand.ExecuteNonQuery();

             Console.WriteLine("Data Insterted ");
        
         


            //3. Update 

            Console.WriteLine("Enter product id : ");
                int id_p1 = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter product name : ");
                string p_name1 = Console.ReadLine();


             string updatequery = "UPDATE Product SET Product_Name = '"+p_name1+"' WHERE Id = '"+id_p1+"'";
             SqlCommand updatecommand = new SqlCommand(updatequery, connection);
             updatecommand.ExecuteNonQuery();

             Console.WriteLine("Data Updated ");
          



            //4. Delete

            Console.WriteLine("Enter product id : ");
            int id_p2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter product name : ");
            string p_name2 = Console.ReadLine();


            string deletequery = "DELETE FROM Product WHERE Product_Name = '"+p_name2+"' OR Id = '"+id_p2+"'";
            SqlCommand deletecommand = new SqlCommand(deletequery, connection);
            deletecommand.ExecuteNonQuery();

            Console.WriteLine("Data Deleted ");


            connection.Close();


        }
    }
}
