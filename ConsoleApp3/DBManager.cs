using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class DBManager
    {
        SqlConnection conn = null;
          public DBManager()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"data source=(localdb)\mssqllocaldb; initial catalog=test; integrated security=true";
        }

        public void addtotatble(string title, int amount)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = $"insert into products values('{title}',{amount})";
            command.ExecuteNonQuery();
        }
        public void selectAll()
        {
            int line = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from products";
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                if(line==0)
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.Write(rdr.GetName(i).ToString()+'\t');
                    }
                Console.WriteLine();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    Console.Write(rdr[i].ToString()+'\t');
                }
                line++;
            }
        }
        public void selectAllTables()
        {
            int line = 0;
            int x = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from products; select * from customers";
            SqlDataReader rdr = command.ExecuteReader();
            do
            {
                line = 0;
                while (rdr.Read())
                {
                    if (line == 0)
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr.GetName(i).ToString() + '\t');
                        }
                    Console.WriteLine();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.Write(rdr[i].ToString() + '\t');
                    }
                    line++;
                }
                Console.WriteLine();
            } while (rdr.NextResult());
           
        }
        public void selectAvg()
        {
            int line = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select avg(amount) from products";
            var rdr = command.ExecuteScalar();
            Console.Write("{0:f3}",Convert.ToDouble( rdr) );
           
        }
        public void connect()
        {
            conn.Open();
            Console.WriteLine("Ok");

        }
        public void close()
        {
            if(conn!=null)
            conn.Close();
        }
    }
}
