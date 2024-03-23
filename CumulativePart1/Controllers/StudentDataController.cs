using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativePart1.Models;
using MySql.Data.MySqlClient;

namespace CumulativePart1.Controllers
{
    public class StudentDataController : ApiController
    {
        //Create context to access database
        private SchoolDbContext Studentdata = new SchoolDbContext();

        /// <summary>
        /// Access information about students and returns a list of students
        /// </summary>
        /// <returns>
        /// A list of students objects with all the information about students from database column.
        /// </returns>
        /// <example>
        /// GET: api/Studentdata/Liststudents -> {student object, Student Object....}
        /// </example>
        [HttpGet]
        [Route("api/studentdata/liststudents")]

        public IEnumerable<Student> ListStudents()
        {
            //create an instance of a connection
            MySqlConnection Conn = Studentdata.AccessDatabase();

            //open the connection between web server and database
            Conn.Open();

            //SQL query
            string query = "select * from students";

            //a new command for our databse
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            //get results of the query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //create an empty list of students
            List<Student> Students = new List<Student>{};

            //loop through each row of the result set
            while (ResultSet.Read())
            {
                //Access column info by database column name
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNum = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);



                //create a new student
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNum = StudentNum;
                NewStudent.EnrolDate = EnrolDate;

                //add student name to the list
                Students.Add(NewStudent);
            }

            //close the connection between MYSQL database and web server
            Conn.Close();

            //return the list of student names
            return Students;
        }

        /// <summary>
        /// Find a student by id
        /// </summary>
        /// <param name="id">The student primary key</param>
        /// <returns>A student object</returns>

        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //create an instance of a connection
            MySqlConnection Conn = Studentdata.AccessDatabase();

            //open the connection between webserver and database
            Conn.Open();

            //a new command (query) for our databse
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Students where studentid = " + id;

            //get results of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access column info by database column name
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNum = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);



                //create a new student
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNum = StudentNum;
                NewStudent.EnrolDate = EnrolDate;
            }

            return NewStudent;
        }
    }
}
