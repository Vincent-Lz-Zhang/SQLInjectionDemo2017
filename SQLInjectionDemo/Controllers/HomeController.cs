/*
  Vincent Vincent 20/May/2017 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using SQLInjectionDemo.Models;
using System.Text.RegularExpressions;

namespace SQLInjectionDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Home");
            // return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            ViewBag.LoginFailed = true;

            string useremail = form["email"].ToString();
            string userpwd = form["password"].ToString();

            if (string.IsNullOrWhiteSpace(useremail) || string.IsNullOrWhiteSpace(userpwd))
            {
                return View();
            }
            else
            {

                #region Approach 1: direct ADO.NET


                #region Problematic sample code 
                /*
                 * The sample of the old-fashioned way to access data from DB through ADO.NET directly,
                 * which exposes SQL injection vulnerablity 
                 */

                /**/

                string cmdStr = "SELECT * FROM Admin WHERE Email = '" + useremail + "' AND Password = '" + userpwd + "'";
                string dbConnStr = "Data Source=ANDROID-STUDIO; Initial Catalog=SQLInjectionDemo;User ID=MIT502712;Password=[Freud 1900]";
                SqlConnection conn = new SqlConnection(dbConnStr);
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    TempData["UserName"] = dt.Rows[0]["Email"].ToString();
                    return RedirectToAction("Dashboard", "Home");
                }
                

                #endregion



                #region Solution 1: Sanitize input data for direct-ADO.NET approach

                /*
                 * The sample code of the solution which filters out the dangerous SQL key words with regular expression,
                 * and it should NOT be considered as a good-enough workaround, however, in real-world practice, it is
                 * still widely used for other purposes, such as capturing users' malicious behavior
                 */

                /*
                
                useremail = Regex.Replace(useremail, "-{2,}", string.Empty);
                useremail = Regex.Replace(useremail, @"[(\')*]+", string.Empty);
                useremail = Regex.Replace(useremail, @"(;|\s)(exec | execute | select | insert | update | delete | create | alter | drop | rename | truncate | backup | restore)\s", string.Empty, RegexOptions.IgnoreCase);

                userpwd = Regex.Replace(userpwd, "-{2,}", string.Empty);
                userpwd = Regex.Replace(userpwd, @"[(\')*]+", string.Empty);
                userpwd = Regex.Replace(userpwd, @"(;|\s)(exec | execute | select | insert | update | delete | create | alter | drop | rename | truncate | backup | restore)\s", string.Empty, RegexOptions.IgnoreCase);

                string cmdStr = "SELECT * FROM Admin WHERE Email = '" + useremail + "' AND Password = '" + userpwd + "'";
                string dbConnStr = "Data Source=ANDROID-STUDIO; Initial Catalog=SQLInjectionDemo;User ID=MIT502712;Password=[Freud 1900]";
                SqlConnection conn = new SqlConnection(dbConnStr);
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    TempData["UserName"] = dt.Rows[0]["Email"].ToString();
                    return RedirectToAction("Dashboard", "Home");
                }
                */


                #endregion



                #region Solution 2: Parameterized query for direct-ADO.NET approach

                /*
                 * The sample code of the solution which execute parameterized query,
                 * this is practically the best solution
                 */


                /*
                string cmdStr = "SELECT * FROM Admin WHERE Email = @email AND Password = @password";
                string dbConnStr = "Data Source=ANDROID-STUDIO; Initial Catalog=SQLInjectionDemo;User ID=MIT502712;Password=[Freud 1900]";
                SqlConnection conn = new SqlConnection(dbConnStr);
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@email", useremail);
                cmd.Parameters.AddWithValue("@password", userpwd);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    TempData["UserName"] = dt.Rows[0]["Email"].ToString();
                    return RedirectToAction("Dashboard", "Home");
                }
                */

                #endregion



                #endregion



                #region Approach 2: ORM framework


                #region Problematic sample code 
                /*
                 * Entity Framework approach to access data,
                 * which exposes SQL injection vulnerablity as well 
                 */

                /*
                SQLInjectionDemoEntities dbCnt = new SQLInjectionDemoEntities();
                string cmdStr = "SELECT * FROM Admin WHERE Email = '" + useremail + "' AND Password = '" + userpwd + "'";

                List<Admin> res = dbCnt.Database.SqlQuery<Admin>(cmdStr).ToList();
                if (res.Count > 0)
                {
                    TempData["UserName"] = res[0].Email;
                    return RedirectToAction("Dashboard", "Home");
                }
                */
                #endregion



                #region Solution 1: LINQ query aginst entity service 

                /*
                 * This approach is the most broadly used one these days,
                 * it's natively immune to SQL injection, nevertheless, as LINQ comes with a lot drawbacks,
                 * depending on the scale and complexity of your application, 
                 * you might need to explore other data access approaches one day 
                 */

                /*
                SQLInjectionDemoEntities dbCnt = new SQLInjectionDemoEntities();

                try
                {
                    Admin adm = dbCnt.Admins.Where(x => x.Email == useremail && x.Password == userpwd).Single();
                    TempData["UserName"] = adm.Email;
                    return RedirectToAction("Dashboard", "Home");
                }
                catch (InvalidOperationException ex)
                {
                    //ViewBag.ExpMsg = ex.Message ?? ex.InnerException.Message ?? "None or more than one records found";
                    // you may log it down somewhere
                }
                catch (Exception ex)
                {
                    // you may log it down somewhere
                }
                */

                #endregion



                #region Solution 2: parameterized query with Entity Framework 

                /*
                SQLInjectionDemoEntities dbCnt = new SQLInjectionDemoEntities();

                string cmdStr2 = @"SELECT * FROM Admin WHERE Email = @email AND Password = @password";

                List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(new SqlParameter("@email", useremail));
                parameterList.Add(new SqlParameter("@password", userpwd));
                SqlParameter[] parameters = parameterList.ToArray();

                List<Admin> res = dbCnt.Database.SqlQuery<Admin>(cmdStr2, parameters).ToList();
                if (res.Count > 0)
                {
                    TempData["UserName"] = res[0].Email;
                    return RedirectToAction("Dashboard", "Home");
                }
                */

                #endregion



                #endregion


            }

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.UserName = TempData["UserName"].ToString();
            return View();
        }

    }
}