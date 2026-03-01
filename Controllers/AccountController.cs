using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using LuminaPortal.Models;

namespace LuminaPortal.Controllers
{
    public class AccountController : Controller
    {
        string conn = ConfigurationManager.ConnectionStrings["LuminaCityDB"].ConnectionString;

        public ActionResult SignUp() => View();
        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Register(string username, string email, string password, string fullName)
        {
            using (SqlConnection sc = new SqlConnection(conn))
            {
                string sql = "INSERT INTO Users (Username, PasswordHash, Email, RoleID, FullName) VALUES (@u, @p, @e, 2, @f)";
                SqlCommand cmd = new SqlCommand(sql, sc);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@f", fullName);
                sc.Open(); cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using (SqlConnection sc = new SqlConnection(conn))
            {
                string sql = "SELECT UserID, RoleID, FullName FROM Users WHERE Username=@u AND PasswordHash=@p";
                SqlCommand cmd = new SqlCommand(sql, sc);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                sc.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    Session["UserID"] = rdr["UserID"];
                    Session["UserRole"] = (int)rdr["RoleID"];
                    Session["Username"] = username;
                    return (int)Session["UserRole"] == 1 ? RedirectToAction("Index", "Users") : RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}