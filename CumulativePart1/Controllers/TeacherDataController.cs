using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativePart1.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace CumulativePart1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Create context to access database
        private SchoolDbContext Teacherdata = new SchoolDbContext();

        /// <summary>
        /// Access information about teachers and returns a list of teachers
        /// </summary>
        /// <returns>
        /// A list of teacher objects with all the information about teachers from database column.
        /// </returns>
        /// <example>
        /// GET: api/Teacherdata/Listteachers -> {Teacher object, Teacher Object....}
        /// </example>
        [HttpGet]
        [Route("api/teacherdata/listteachers/{SearchKey?}")]

        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //create an instance of a connection
            MySqlConnection Conn = Teacherdata.AccessDatabase();

            //open the connection between web server and database
            Conn.Open();

            //SQL query
            string query = "select * from teachers where lower(teacherfname) like lower('%"+SearchKey+"%') or lower(teacherlname) like lower('%"+SearchKey+ "%') or hiredate like('%"+SearchKey+ "%') or salary like('%"+SearchKey+"%')";

            //a new command for our databse
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            //get results of the query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //create an empty list of teachers
            List<Teacher> Teachers = new List<Teacher>{};

            //loop through each row of the result set
            while (ResultSet.Read())
            {
                //Access column info by database column name
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeachaerFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNum = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                decimal TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);

    

                //create a new teacher
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeachaerFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.TeacherSalary = (decimal)TeacherSalary;

                //add teacher name to the list
                Teachers.Add(NewTeacher);
            }

            //close the connection between MYSQL database and web server
            Conn.Close();

            //return the list of teacher names
            return Teachers;
        }

        /// <summary>
        /// Find a teacher by id
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A teacher object</returns>
        
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();
            
            //create an instance of a connection
            MySqlConnection Conn = Teacherdata.AccessDatabase();

            //open the connection between webserver and database
            Conn.Open();

            //a new command (query) for our databse
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;

            //get results of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access column info by database column name
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeachaerFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNum = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                decimal TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeachaerFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.TeacherSalary = (decimal)TeacherSalary;
            }

            return NewTeacher;
        }
    }
}
