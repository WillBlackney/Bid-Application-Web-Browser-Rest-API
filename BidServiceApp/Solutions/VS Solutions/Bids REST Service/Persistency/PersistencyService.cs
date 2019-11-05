using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Bid_REST_Service.Persistency
{
    public static class PersistencyService
    {
        public const string GET_ALL = "Select * from Bids";
        // public const string ConnectionString = "Server=tcp:mysqlserver20199.database.windows.net,1433;Initial Catalog=BookDB;Persist Security Info=False;User ID=willblackney;Password=Chocolate321;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Get books from azure DB methods
        public static Bid ReadNextElement(SqlDataReader reader)
        {
            Bid bid = new Bid(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
            return bid;
        }

        // Get All
        public static IEnumerable<Bid> Get()
        {
            List<Bid> bids = new List<Bid>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = GET_ALL;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bids.Add(ReadNextElement(reader));
                            }
                        }
                    }
                }

                return bids;
            }
        }

        // Get by ISBN Code reference
        public static Bid Get(string bidID)
        {
            //return books.Find(i => i.IsbnCode == id);
            Bid bookReturned = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = GET_ALL;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (ReadNextElement(reader).Id == bidID)
                                {
                                    bookReturned = ReadNextElement(reader);
                                }
                            }
                        }
                    }
                }

                return bookReturned; 
            }
        }

        // Add a book to the azure DB
        public static void Post(Bid bidAdded)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Insert into Bids Values (@Param1, @Param2, @Param3, @Param4)";
                        cmd.Parameters.AddWithValue(parameterName: "@param1", bidAdded.Id);
                        cmd.Parameters.AddWithValue(parameterName: "@param2", bidAdded.Item);
                        cmd.Parameters.AddWithValue(parameterName: "@param3", bidAdded.BidAmount);
                        cmd.Parameters.AddWithValue(parameterName: "@param4", bidAdded.Name);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Update a book's info in the azure DB
        public static void Put(string bidID, Bid newBidData)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open && newBidData != null)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            "UPDATE Bids SET ID = @param1, Item = @param2, BidAmount = @param3, UserName = @param4 WHERE ID = @param5";

                        cmd.Parameters.AddWithValue(parameterName: "@param1", newBidData.Id);
                        cmd.Parameters.AddWithValue(parameterName: "@param2", newBidData.Item);
                        cmd.Parameters.AddWithValue(parameterName: "@param3", newBidData.BidAmount);
                        cmd.Parameters.AddWithValue(parameterName: "@param4", newBidData.Name);

                        cmd.Parameters.AddWithValue(parameterName: "@param5", bidID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Update a book's info in the azure DB
        public static void Delete(string bidID)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Bids WHERE ID = @param1";

                        cmd.Parameters.AddWithValue(parameterName: "@param1", bidID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
