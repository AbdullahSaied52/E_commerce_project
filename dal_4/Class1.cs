using Microsoft.Data.SqlClient;
using System.Data;
using class_1_DTO;

namespace dal_4
{

    public class dal
    {
        static string connection_string = "Server=localhost;Database=My_Products;Integrated Security=True;TrustServerCertificate=True";

        public static void insert(product_dto p)
        {
            using (SqlConnection cnct = new SqlConnection(connection_string))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insert_item", cnct))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", p.type);
                    cmd.Parameters.AddWithValue("@name", p.name);
                    cmd.Parameters.AddWithValue("@amount", p.amount);
                    cmd.Parameters.AddWithValue("@price", p.price);
                    var outputid = new SqlParameter("@id_for_products", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputid);
                    cnct.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static product_dto list_info(string name)
        {
            using (SqlConnection cnct = new SqlConnection(connection_string))
            {
                using (SqlCommand cmd = new SqlCommand("sp_list_info", cnct))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name);
                    cnct.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new product_dto
                            (reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetString(reader.GetOrdinal("Type")),
                            reader.GetInt32(reader.GetOrdinal("Amount")),
                            reader.GetInt32(reader.GetOrdinal("Price"))
                            );
                        }
                        else
                            return null;
                    }
                }
            }
        }

        public static int sell(string name, int amount)
        {
            int result=0;
            using (SqlConnection cnct=new SqlConnection(connection_string))
            {
                using(SqlCommand cmd=new SqlCommand("sp_sell",cnct))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cnct.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            result= reader.GetInt32(reader.GetOrdinal("result"));
                        }
                        return result;
                    }
                }
            }
        }

        public static int login(string username,string pass)
        {
            using (SqlConnection cnct = new SqlConnection(connection_string))
            {
                using(SqlCommand cmd=new SqlCommand("sp_login",cnct))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", pass);
                    cnct.Open();
                    return  (int)cmd.ExecuteScalar();

                }
            }
        }

    }
}

