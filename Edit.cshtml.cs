using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly string connectionString = "Data Source=AMAN;Initial Catalog=mystore;Integrated Security=True";

        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["Id"];
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients WHERE Id=@Id";
                    

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Example: Assuming CreatedAt is a DateTime column
                              
                                {
                                    clientInfo.Id = reader.GetInt32(0).ToString();
                                    clientInfo.Name = reader.GetString(1);
                                    clientInfo.Email = reader.GetString(2);
                                    clientInfo. Phone = reader.GetString(3);
                                    clientInfo. Address = reader.GetString(4);
                                    clientInfo.CreatedAt = reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm:ss"); // Adjust format as needed
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            
        }
        public void OnPost()
        {
            clientInfo.Name = Request.Form["name"];
            clientInfo.Email = Request.Form["email"];
            clientInfo.Phone = Request.Form["phone"];
            clientInfo.Address = Request.Form["address"];
            clientInfo.Id = Request.Form["id"];


            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0 ||
             clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                string connectionString = "Data Source=AMAN;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    

                    String sql = "UPDATE clients " +
             "SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address " +
             "WHERE Id=@Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", clientInfo.Name);
                        command.Parameters.AddWithValue("@Email", clientInfo.Email);
                        command.Parameters.AddWithValue("@Phone", clientInfo.Phone);
                        command.Parameters.AddWithValue("@Address", clientInfo.Address);
                        command.Parameters.AddWithValue("@Id", clientInfo.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
