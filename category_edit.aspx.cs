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
        private void LoadCategoryData(int id)
{
    using (SqlConnection conn = new SqlConnection(connString))
    {
        string sql = "SELECT category_name, category_description, image FROM categories WHERE id = @id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            txtName.Text = reader["category_name"].ToString();
            txtDescription.Text = reader["category_description"].ToString();

            string imgPath = reader["image"].ToString();

            // Store the current path so we can use it if the user DOESN'T upload a new one
            if (hfOldImagePath != null) hfOldImagePath.Value = imgPath;

            if (!string.IsNullOrEmpty(imgPath))
            {
                imgPreview.ImageUrl = "~/" + imgPath;
                divCurrentImage.Visible = true;
            }
        }
        else
        {
            Response.Redirect("category.aspx?msg=Category not found");
        }
    }
}

