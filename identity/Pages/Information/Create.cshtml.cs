using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace identity.Pages.Information
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.tc_id = Request.Form["tc_id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.surname = Request.Form["surname"];
            clientInfo.gender = Request.Form["gender"];
            clientInfo.birth_date = Request.Form["birth_date"];
            clientInfo.birth_place = Request.Form["birth_place"];
            clientInfo.mother_name = Request.Form["mother_name"];
            clientInfo.father_name = Request.Form["fatger_name"];

            if(clientInfo.name.Length==0||clientInfo.tc_id.Length<11||clientInfo.surname.Length==0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=identity;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    String sql = "INSERT INTO information" +
                        "(tc_id,name,surname,gender,birth_date,birth_place,mother_name,father_name) VALUES" +
                        "(@tc_id,@name,@surname,@gender,@birth_date,@birth_place,@mother_name,@father_name);";
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

            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            clientInfo.tc_id = ""; 
            clientInfo.name = "";
            clientInfo.surname = "";
            clientInfo.gender = "";
            clientInfo.birth_date = "";
            clientInfo.birth_place = "";
            clientInfo.mother_name = "";
            clientInfo.father_name = "";

            successMessage = "New information aded correctly";
            Response.Redirect("/Information/Index");

        }
    }
}
