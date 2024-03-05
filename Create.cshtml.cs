using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {

        public ClientInfo clientInfo =new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() { 
        
        clientInfo.Name = Request.Form["name"];
        clientInfo.Email = Request.Form["email"];
        clientInfo.Phone = Request.Form["phone"];
        clientInfo.Address = Request.Form["address"];
           
            if(clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0 ||
               clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0) 
            {  
                errorMessage = "All the fields are required";
                return;
            }

            //save data
            try
            {
                string connectionString = "Data Source=AMAN;Initial Catalog=mystore;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                { 
                    connection.Open();
                    String sql = "INSERT INTO clients" +
                        "(Name,Email,Phone,Address) VALUES" +
                        "(@Name,@Email, @phone,@Address);";

                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@Name", clientInfo.Name);
                        command.Parameters.AddWithValue("@Email", clientInfo.Email);
                        command.Parameters.AddWithValue("@Phone", clientInfo.Phone);
                        command.Parameters.AddWithValue("@Address", clientInfo.Address);

                        command.ExecuteNonQuery();
                    }
                }             
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            clientInfo.Name = "";
            clientInfo.Email="";
            clientInfo.Phone = "";
            clientInfo.Address = "";
            successMessage = "New Client added Sucessfully";

            Response.Redirect("/Clients/Index");
        }
    }
}
