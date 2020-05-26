using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AzureExercisesWithMVC.Controllers
{
    public class AzureSqlDBController : Controller
    {
        SqlConnection con = new SqlConnection();

        // GET: AzureSqlDB
        public ActionResult AzureSQLDB()
        {
            //Azure SQL Connection string.
            string connectionString = "Server=tcp:devserverpraveen.database.windows.net,1433;Initial Catalog=DevCustomerDB;Persist Security Info=False;User ID=devadmin;Password=Password@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            con.ConnectionString = connectionString;
            con.Open();

            //Create new record.
            //CreateRow();

            //Update record
           //UpdateRow();

            //Delete record
            DeleteRow();

            //Read all records.
            ReadRows();

            return View();
        }
        public void CreateRow()
        {
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("Insert into Customer(customerId,customerName) values(@id,@name)", con);
            cmd.Parameters.AddWithValue("@id", "4");
            cmd.Parameters.AddWithValue("@name", "Visam");
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Message = "Record created successfully";
        }

        public void ReadRows()
        {
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("Select * from Customer", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
        }

        public void UpdateRow()
        {
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("Update Customer set customerName=@name where customerID=@id", con);
            cmd.Parameters.AddWithValue("@id", "3");
            cmd.Parameters.AddWithValue("@name", "Maruthia");
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Message = "Record updated successfully";
        }

        public void DeleteRow()
        {
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("Delete customer where  customerID=@id", con);
            cmd.Parameters.AddWithValue("@id", "4");
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Message = "Record deleted successfully"; 
        }
    }
}