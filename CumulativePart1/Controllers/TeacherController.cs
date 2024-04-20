using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativePart1.Models;

namespace CumulativePart1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //GET: /Teacher/DeleteConfirmAjax/{id}
        public ActionResult DeleteConfirmAjax(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }


        //GET: /Teacher/AjaxNew
        public ActionResult AjaxNew()
        {
            return View();
        }



        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNum, DateTime? HireDate, decimal? TeacherSalary)
        {
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNum) || HireDate == null || TeacherSalary == null || TeacherSalary < 0)
            {
                ViewBag.Error = "All fields are required.";
                return View("New");
            }

            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNum);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(TeacherSalary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNum = EmployeeNum;
            NewTeacher.HireDate = (DateTime)HireDate;
            NewTeacher.TeacherSalary = (decimal)TeacherSalary;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //POST: /Teacher/CreateAjax
        [HttpPost]
        public ActionResult CreateAjax(string TeacherFname, string TeacherLname, string EmployeeNum, DateTime? HireDate, decimal? TeacherSalary)
        {
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNum) || HireDate > DateTime.Now || HireDate == null || TeacherSalary == null || TeacherSalary < 0)
            {
                Response.StatusCode = 400;
                return Content("Missing or incorrect information when adding a teacher", "text/plain");
            }

            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNum);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(TeacherSalary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNum = EmployeeNum;
            NewTeacher.HireDate = (DateTime)HireDate;
            NewTeacher.TeacherSalary = (decimal)TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            Response.StatusCode = 200;
            return Content("Teacher added successfully", "text/plain");
        }

        //GET: /Teacher/Update/{id}
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>a dynamic "Update Teacher" webpage which provides the current information of the author and asks the user for new information as a part of a form</returns>
        /// <example>GET : /Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }


        public ActionResult Ajax_update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a post request containing information about an existing teacher in the 
        /// system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNum">The updated employee number of the teacher</param>
        /// <param name="TeacherSalary">The upadated salary of the teacher</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>POST : /Teacher/Update/2
        /// FORM DATA / POST DATA/ REQUEST BODY
        /// {
        /// "TeacherFname" : "John"
        /// "TeacherLname" : "Doe",
        /// "EmployeeNum" : "T123",
        /// "TeacherSalary" : $50
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNum, decimal? TeacherSalary)
        {
            TeacherDataController controller = new TeacherDataController();
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNum) || TeacherSalary == null || TeacherSalary < 0)
            {
                ViewBag.Error = "All fields are required.";
                Teacher SelectedTeacher = controller.FindTeacher(id);
                return View("Update", SelectedTeacher);
            }

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNum = EmployeeNum;
            TeacherInfo.TeacherSalary = (decimal)TeacherSalary;

            controller.UpdateTeacher( id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }

}