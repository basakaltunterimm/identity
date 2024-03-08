using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace identity.Pages.Information
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> ListClient = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=identity;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM information";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = reader.GetInt32(0).ToString();
                                clientInfo.tc_id = reader.GetString(1);
                                clientInfo.name = reader.GetString(2);
                                clientInfo.surname = reader.GetString(3);
                                clientInfo.gender = reader.GetString(4);
                                clientInfo.birth_date = reader.GetDateTime(5).ToString("yyyy-MM-dd");
                                clientInfo.birth_place = reader.GetString(6);
                                clientInfo.mother_name = reader.GetString(7);
                                clientInfo.father_name = reader.GetString(8);

                                ListClient.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public string id;
        public string tc_id;
        public string name;
        public string surname;
        public string gender;
        public string birth_date;
        public string birth_place;
        public string mother_name;
        public string father_name;
    }
}

