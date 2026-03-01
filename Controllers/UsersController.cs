using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using LuminaPortal.Models;

namespace LuminaPortal.Controllers
{
    public class UsersController : Controller
    {
        string conn = ConfigurationManager.ConnectionStrings["LuminaCityDB"].ConnectionString;

        public ActionResult Index()
        {
            if (Session["UserRole"] == null || (int)Session["UserRole"] != 1) return RedirectToAction("Login", "Account");

            List<UserAccount> users = new List<UserAccount>();
            using (SqlConnection sc = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", sc);
                sc.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    users.Add(new UserAccount { UserID = (int)r["UserID"], Username = r["Username"].ToString(), Email = r["Email"].ToString(), RoleID = (int)r["RoleID"] });
                }
            }
            return View(users);
        }

        public ActionResult Delete(int id)
        {
            if (Session["UserRole"] == null || (int)Session["UserRole"] != 1) return RedirectToAction("Login", "Account");
            using (SqlConnection sc = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID=@id", sc);
                cmd.Parameters.AddWithValue("@id", id);
                sc.Open(); cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}