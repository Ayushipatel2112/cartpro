using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace CartProWebApp.admin
{
    public partial class category_edit : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["CartProConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Auth Check (matches category.aspx)
            if (Session["AdminEmail"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id;
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        // 2. Store ID in ViewState to persist across button clicks
                        ViewState["SelectedCatId"] = id;
                        LoadCategoryData(id);
                    }
                    else
                    {
                        Response.Redirect("category.aspx?msg=Invalid ID");
                    }
                }
                else
                {
                    Response.Redirect("category.aspx?msg=No ID Provided");
                }
            }
        }
