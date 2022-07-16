using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SqlDb
    {
        private  readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AuthenticationDB"].ConnectionString;
        public  int ExecuteSqlTransaction(string cmd)
        {
            int modifier = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("SampleTransaction");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = cmd;
                    object result = command.ExecuteScalar();
                    if (result!=null && result.GetType() != typeof(DBNull))
                    {
                        modifier = (int?)result??0;
                    }


                    transaction.Commit();


                }
                catch (Exception ex)
                {


                
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception(ex2.Message);

                    }

                    throw new Exception(ex.Message);
                }
            }

            return modifier;

        }

        public DataTable ExecuteSqlQuery(string cmd)
        {
           
            SqlDataReader sqlreader;
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(cmd, connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                sqlreader= command.ExecuteReader();
              dt.Load(sqlreader);


            }

            return dt;
        }
    }
}
