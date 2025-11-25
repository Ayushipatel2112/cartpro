using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CartProWebApp.admin
{
    public partial class product : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["CartProConnection"].ConnectionString;
        int PageSize = 10;

        // Properties to store state in ViewState
        public int CurrentPage
        {
            get { return ViewState["Page"] != null ? (int)ViewState["Page"] : 1; }
            set { ViewState["Page"] = value; }
        }

        public string SortColumn
        {
            get { return ViewState["SortColumn"] != null ? (string)ViewState["SortColumn"] : "id"; }
            set { ViewState["SortColumn"] = value; }
        }

        public string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? (string)ViewState["SortDirection"] : "DESC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminEmail"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                // Check for URL messages
                if (Request.QueryString["msg"] != null)
                {
                    pnlMessage.Visible = true;
                    litMessage.Text = Request.QueryString["msg"];
                }

                BindCategories();
                BindProducts();
            }
        }

        private void BindCategories()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sql = "SELECT id, category_name FROM categories ORDER BY category_name ASC";
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataTextField = "category_name";
                    ddlCategory.DataValueField = "id";
                    ddlCategory.DataBind();
                }
            }
        }

        private void BindProducts()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // 1. Build Base Query and Filter Logic
                string whereClause = "WHERE 1=1";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    whereClause += " AND (p.productname LIKE @Search OR p.productdescription LIKE @Search OR c.category_name LIKE @Search)";
                    parameters.Add(new SqlParameter("@Search", "%" + txtSearch.Text.Trim() + "%"));
                }

                if (ddlCategory.SelectedValue != "0")
                {
                    whereClause += " AND p.catid = @CatId";
                    parameters.Add(new SqlParameter("@CatId", ddlCategory.SelectedValue));
                }

                if (!string.IsNullOrEmpty(ddlStock.SelectedValue))
                {
                    whereClause += " AND p.stock_status = @Stock";
                    parameters.Add(new SqlParameter("@Stock", ddlStock.SelectedValue));
                }

                // 2. Count Total Records (For Pagination)
                string countSql = "SELECT COUNT(*) FROM products p LEFT JOIN categories c ON p.catid = c.id " + whereClause;
                SqlCommand cmdCount = new SqlCommand(countSql, conn);
                cmdCount.Parameters.AddRange(parameters.ToArray()); // Add filters to count query
                int totalRecords = (int)cmdCount.ExecuteScalar();

                // 3. Setup Pagination Display
                int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);

                // Safety check if current page is out of bounds (e.g. after deleting items)
                if (CurrentPage > totalPages && totalPages > 0) CurrentPage = totalPages;
                if (CurrentPage < 1) CurrentPage = 1;

                int offset = (CurrentPage - 1) * PageSize;

                litStart.Text = (totalRecords > 0) ? (offset + 1).ToString() : "0";
                litEnd.Text = Math.Min(offset + PageSize, totalRecords).ToString();
                litTotal.Text = totalRecords.ToString();

                // 4. Fetch Data with Sorting and Paging
                // Note: We clone parameters because we used them in the count query already
                SqlParameter[] dataParams = new SqlParameter[parameters.Count];
                for (int i = 0; i < parameters.Count; i++) dataParams[i] = (SqlParameter)((ICloneable)parameters[i]).Clone();

                string sortSql = SortColumn == "category_name" ? "c.category_name" : "p." + SortColumn;

                string dataSql = $@"
                    SELECT p.id, p.catid, p.productname, p.productdescription, p.productprice, 
                           p.image, p.stock_status, c.category_name 
                    FROM products p 
                    LEFT JOIN categories c ON p.catid = c.id 
                    {whereClause}
                    ORDER BY {sortSql} {SortDirection}
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                SqlCommand cmdData = new SqlCommand(dataSql, conn);
                cmdData.Parameters.AddRange(dataParams);
                cmdData.Parameters.AddWithValue("@Offset", offset);
                cmdData.Parameters.AddWithValue("@PageSize", PageSize);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmdData))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }

                // 5. Generate Page Numbers
                BindPaginationButtons(totalPages);
                divPagination.Visible = totalRecords > 0;

                // 6. Update Sort Indicators
                UpdateSortIndicators();
            }
        }

        private void BindPaginationButtons(int totalPages)
        {
            List<int> pages = new List<int>();
            int start = Math.Max(1, CurrentPage - 2);
            int end = Math.Min(totalPages, CurrentPage + 2);

            for (int i = start; i <= end; i++) pages.Add(i);

            rptPages.DataSource = pages;
            rptPages.DataBind();

            btnFirst.Enabled = btnPrev.Enabled = (CurrentPage > 1);
            btnNext.Enabled = btnLast.Enabled = (CurrentPage < totalPages);

            // Store total pages for First/Last logic logic
            ViewState["TotalPages"] = totalPages;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CurrentPage = 1; // Reset to page 1 on search
            BindProducts();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlCategory.SelectedIndex = 0;
            ddlStock.SelectedIndex = 0;
            CurrentPage = 1;
            SortColumn = "id";
            SortDirection = "DESC";
            BindProducts();
        }

        protected void Sort_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string argument = btn.CommandArgument;

            if (SortColumn == argument)
            {
                // Toggle direction
                SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            }
            else
            {
                // New column, default to ASC
                SortColumn = argument;
                SortDirection = "ASC";
            }

            BindProducts();
        }

        private void UpdateSortIndicators()
        {
            // Reset all labels
            lblSortId.Text = lblSortCat.Text = lblSortName.Text = lblSortPrice.Text = lblSortStock.Text = "";

            // Set arrow for current column
            string arrow = (SortDirection == "ASC") ? "▲" : "▼";
            switch (SortColumn)
            {
                case "id": lblSortId.Text = arrow; break;
                case "category_name": lblSortCat.Text = arrow; break;
                case "productname": lblSortName.Text = arrow; break;
                case "productprice": lblSortPrice.Text = arrow; break;
                case "stock_status": lblSortStock.Text = arrow; break;
            }
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string arg = btn.CommandArgument;
            int totalPages = (int)ViewState["TotalPages"];

            if (arg == "prev") CurrentPage--;
            else if (arg == "next") CurrentPage++;
            else if (arg == "first") CurrentPage = 1;
            else if (arg == "last") CurrentPage = totalPages;
            else CurrentPage = Convert.ToInt32(arg);

            BindProducts();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Optional: Delete image from folder before deleting DB record
                // (Requires fetching image path first, omitted for brevity but recommended)

                string sql = "DELETE FROM products WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Refresh grid
            BindProducts();
            pnlMessage.Visible = true;
            litMessage.Text = "Product deleted successfully.";
        }

        // Helper for Badge CSS
        public string GetStockBadgeClass(string status)
        {
            status = status.ToLower();
            if (status == "in stock") return "badge badge-success";
            if (status == "low stock") return "badge badge-warning";
            return "badge badge-danger";
        }
    }
}