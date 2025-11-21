using System;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
namespace CartProWebApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["msg"] == "registered")
            {
                lblMessage.Text = "Registration successfully. Please login.";
            }
        }
