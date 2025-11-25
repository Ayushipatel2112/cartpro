using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace CartProWebApp.admin
{
    public partial class edit_category : System.Web.UI.Page
    {
        // UPDATED: Now matches the name in your Web.config ("CartProConnection")
        string connString = ConfigurationManager.ConnectionStrings["CartProConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
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
                    hfOldImagePath.Value = imgPath;

                    if (!string.IsNullOrEmpty(imgPath))
                    {
                        // Add tilde (~) to ensure it points to root
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string imagePath = hfOldImagePath.Value;

            if (string.IsNullOrEmpty(name))
            {
                ShowError("Category name is required.");
                return;
            }

            // Handle Image Upload
            if (fileImage.HasFile)
            {
                string fileExt = Path.GetExtension(fileImage.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

                bool isValidExt = false;
                foreach (string ext in allowedExtensions) { if (fileExt == ext) isValidExt = true; }

                if (isValidExt)
                {
                    string uploadFolder = Server.MapPath("~/images/categories/");

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Delete Old Image
                    if (!string.IsNullOrEmpty(hfOldImagePath.Value))
                    {
                        try
                        {
                            string oldFilePhysicalPath = Server.MapPath("~/" + hfOldImagePath.Value);
                            if (File.Exists(oldFilePhysicalPath))
                            {
                                File.Delete(oldFilePhysicalPath);
                            }
                        }
                        catch { }
                    }

                    // Save New Image
                    string newFileName = "cat_" + DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString().Substring(0, 4) + fileExt;
                    string savePath = Path.Combine(uploadFolder, newFileName);

                    fileImage.SaveAs(savePath);

                    imagePath = "images/categories/" + newFileName;
                }
                else
                {
                    ShowError("Invalid image type. Allowed: JPG, PNG, GIF, WEBP.");
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    string sqlUpdate = "UPDATE categories SET category_name = @name, category_description = @desc, image = @img WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sqlUpdate, conn);

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@desc", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                    cmd.Parameters.AddWithValue("@img", imagePath);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    Response.Redirect("category.aspx?msg=" + Server.UrlEncode("Category updated successfully."));
                }
                catch (Exception ex)
                {
                    ShowError("Database Error: " + ex.Message);
                }
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            pnlError.Visible = true;
        }
    }
}