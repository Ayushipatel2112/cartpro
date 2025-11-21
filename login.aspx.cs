using System;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;

namespace CartProWebApp.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminEmail"] != null)
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // --- OPTION 1: Hardcoded Master Admin (No Database Needed) ---
            if (username == "admin" && password == "admin123")
            {
                Session["AdminEmail"] = "admin"; // Set session
                Response.Redirect("Dashboard.aspx"); // Login successful
                return; // Stop here, do not check database
            }

            // --- OPTION 2: Check Database for other admins ---
            string cs = ConfigurationManager.ConnectionStrings["CartProConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                // Checking [admin] table
                string query = "SELECT COUNT(1) FROM [user] WHERE (name = @User OR email = @User) AND password = @Pass";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@Pass", password);

                    try
                    {
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count == 1)
                        {
                            Session["AdminEmail"] = username;
                            Response.Redirect("index.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid Username or Password.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Database Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}