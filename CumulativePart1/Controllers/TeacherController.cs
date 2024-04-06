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
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
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
            if(string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNum) || HireDate == null || TeacherSalary == null)
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
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNum) || HireDate > DateTime.Now|| HireDate == null || TeacherSalary == null || TeacherSalary < 0)
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



    }

}