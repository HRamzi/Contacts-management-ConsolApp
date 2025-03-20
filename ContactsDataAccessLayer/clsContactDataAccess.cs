using System;
using System.Data;
using System.Data.SqlClient;


namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        private static string ConnectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=123456";

        public static int AddNewContact(string firstName, string lastName, string email, string phone,
                                        string address, DateTime dateOfBirth, int countryID, string imagePath)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddNewContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                cmd.Parameters.AddWithValue("@ImagePath", imagePath);

                SqlParameter outputIdParam = new SqlParameter("@NewID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputIdParam);

                conn.Open();
                int v = cmd.ExecuteNonQuery();
                conn.Close();

                return (int)outputIdParam.Value;
            }
        }

        public static bool UpdateContact(int id, string firstName, string lastName, string email, string phone,
                                         string address, DateTime dateOfBirth, int countryID, string imagePath)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                cmd.Parameters.AddWithValue("@ImagePath", imagePath);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }

        public static bool DeleteContact(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }

        public static bool GetContactInfoByID(int Id, ref string firstName, ref string lastName, ref string email,
                                              ref string phone, ref string address, ref DateTime dateOfBirth,
                                              ref int countryID, ref string imagePath)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetContactByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", Id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    firstName = reader["FirstName"].ToString();
                    lastName = reader["LastName"].ToString();
                    email = reader["Email"].ToString();
                    phone = reader["Phone"].ToString();
                    address = reader["Address"].ToString();
                    dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    countryID = Convert.ToInt32(reader["CountryID"]);
                    imagePath = reader["ImagePath"].ToString();

                    conn.Close();
                    return true;
                }

                conn.Close();
                return false;
            }
        }

        public static DataTable GetAllContacts()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllContacts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }

        public static bool IsContactExist(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_IsContactExist", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                SqlParameter outputExists = new SqlParameter("@Exists", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputExists);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return (bool)outputExists.Value;
            }
        }
    }
}
