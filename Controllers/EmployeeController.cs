using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VCSSystem_Task1.Models;

namespace VCSSystem_Task1.Controllers
{
    public class EmployeeController : Controller
    {
        string constr = "Data Source =VENKAT\\SQLEXPRESS01 ;Initial Catalog=task1;Integrated Security=true";
        public ActionResult Index()
        {
            List<EmployeeModel> Employees = new List<EmployeeModel>();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Employee_select_all";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
                Employees.Add(new EmployeeModel()
                {
                    Id = Convert.ToInt32(sdr[0]),
                    Name = (sdr[1]).ToString(),
                    DateOfBirth = Convert.ToString(sdr[2]),
                    Email = Convert.ToString(sdr[3]),
                    profile = (sdr[4]).ToString()
                });
            return View(Employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            EmployeeModel Employees = new EmployeeModel();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Employee_select "+ id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
                Employees = new EmployeeModel
                {
                    Id = Convert.ToInt32(sdr[0]),
                    Name = (sdr[1]).ToString(),
                    DateOfBirth = Convert.ToString(sdr[2]),
                    Email = Convert.ToString(sdr[3]),
                    profile = (sdr[4]).ToString()
                };
            return View(Employees);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new EmployeeModel());

        }

        // POST: Employee/Create
        [HttpPost]
       
        public ActionResult Create(EmployeeModel employee)
        {
            try
            {
                // If model is valid
                if (ModelState.IsValid)
                {
                    // Handle the file upload logic (if necessary)
                    HttpPostedFileBase upload = employee.TEMP_PROFILE;
                    if (upload != null)
                    {
                        string extension = Path.GetExtension(upload.FileName);
                        string imageName = employee.Name + extension;
                        string imagePath = Path.Combine(Server.MapPath("~/Content/Profile/" + imageName));
                        upload.SaveAs(imagePath);
                        employee.profile = imageName; // Save the image name to the profile property
                    }

                    // Validate DOB
                    if (string.IsNullOrEmpty(employee.DateOfBirth) || Convert.ToDateTime(employee.DateOfBirth) >= DateTime.Today)
                    {
                        ModelState.AddModelError("DateOfBirth", "Please enter a valid date of birth.");
                        return View(employee);
                    }

                    // Insert the new employee data into the database
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        string query = "Employee_add @Name, @DateOfBirth, @Email, @profile";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Email", employee.Email);
                        cmd.Parameters.AddWithValue("@profile", employee.profile);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Redirect to Index page after success
                    return RedirectToAction("Index");
                }

                // If model is invalid, re-render the form
                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred: " + ex.Message);
                return View(employee);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeModel Employees = new EmployeeModel();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Employee_select " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
                Employees = new EmployeeModel
                {
                    Id = Convert.ToInt32(sdr[0]),
                    Name = (sdr[1]).ToString(),
                    DateOfBirth = Convert.ToString(sdr[2]),
                    Email = Convert.ToString(sdr[3]),
                    profile = (sdr[4]).ToString()
                };
            return View(Employees);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeModel Employees)
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                string query = "Employee_edit " + Employees.Id + ",'" + Employees.Name + "','"
                    + Employees.DateOfBirth + "','" +
                    Employees.Email + "','" + Employees.profile + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                int i = cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            EmployeeModel Employees = new EmployeeModel();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Employee_select " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
                Employees = new EmployeeModel
                {
                    Id = Convert.ToInt32(sdr[0]),
                    Name = (sdr[1]).ToString(),
                    DateOfBirth = Convert.ToString(sdr[2]),
                    Email = Convert.ToString(sdr[3]),
                    profile = (sdr[4]).ToString()
                };
            return View(Employees);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EmployeeModel Employees)
        {
            
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                string query = "Employee_delete "+ Employees.Id;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Index1()
        {
            List<EmployeeModel> Employees = new List<EmployeeModel>();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Employee_select_all";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Employees.Add(new EmployeeModel()
                {
                    Id = Convert.ToInt32(sdr[0]),
                    Name = (sdr[1]).ToString(),
                    DateOfBirth = Convert.ToString(sdr[2]),
                    Email = Convert.ToString(sdr[3]),
                    profile = (sdr[4]).ToString()
                });
            }
            con.Close();

            var viewModel = new EmployeeViewModel
            {
                Employees = Employees,
                NewEmployee = new EmployeeModel() // Empty employee for the form
            };
            return View(viewModel);
        }





    }
}
