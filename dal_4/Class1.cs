using Microsoft.Data.SqlClient;
using System.Data;

namespace dal_4
{
    public class product_dto
    {
        public int id      {get;set;} 
        public string type {get;set;}
        public string name {get;set;}
        public int amount  {get;set;}
        public int price { get; set; }

        public product_dto(int id, string name, string type, int amount, int price)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.price = price;
            this.amount = amount;
        }
    }
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
                    var outputid = new SqlParameter("@id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputid);
                    cnct.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static product_dto list_info(string type, string name)
        {
            using (SqlConnection cnct = new SqlConnection(connection_string))
            {
                using (SqlCommand cmd = new SqlCommand("sp_list_info", cnct))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", type);
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


    }
}

