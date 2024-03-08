using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace identity.Pages.Information
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=identity;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM information WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                clientInfo.id = reader.GetInt32(0).ToString();
                                clientInfo.tc_id = reader.GetString(1);
                                clientInfo.name = reader.GetString(2);
                                clientInfo.surname = reader.GetString(3);
                                clientInfo.gender = reader.GetString(4);
                                clientInfo.birth_date = reader.GetDateTime(5).ToString("yyyy-MM-dd");
                                clientInfo.birth_place = reader.GetString(6);
                                clientInfo.mother_name = reader.GetString(7);
                                clientInfo.father_name = reader.GetString(8);

                            }
                        }
                    }
                }
            }
            
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.tc_id = Request.Form["tc_id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.surname = Request.Form["surname"];
            clientInfo.gender= Request.Form["gender"];
            clientInfo.birth_date = Request.Form["birth_date"];
            clientInfo.birth_place = Request.Form["birth_place"];
            clientInfo.mother_name = Request.Form["mother_name"];
            clientInfo.father_name = Request.Form["father_name"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.tc_id.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=identity;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE information" +
                        "SET tc_id,name,surname,gender,birth_date,birth_place,mother_name,father_name" +
                        "WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@tc_id", clientInfo.tc_id);
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@surname", clientInfo.surname);
                        command.Parameters.AddWithValue("@gender", clientInfo.gender);
                        command.Parameters.AddWithValue("@birth_date", clientInfo.birth_date);
                        command.Parameters.AddWithValue("@birth_place", clientInfo.birth_place);
                        command.Parameters.AddWithValue("@mother_name", clientInfo.mother_name);
                        command.Parameters.AddWithValue("@father_name", clientInfo.father_name);

                        command.ExecuteNonQuery();

                    }
                }
            } 
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }
            Response.Redirect("/Information/Index");

        }
    }
}
