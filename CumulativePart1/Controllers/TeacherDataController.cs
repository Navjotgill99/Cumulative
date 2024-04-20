using System;
using System.Collections;
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
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];

    

                //create a new teacher
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeachaerFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.TeacherSalary = TeacherSalary;

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
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeachaerFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
            }
            ResultSet.Close();

            MySqlCommand classCmd = Conn.CreateCommand();
            cmd.CommandText = "Select * from classes where teacherid = " + id;

            MySqlDataReader ClassResultSet = cmd.ExecuteReader();

            List<Class> Classes = new List<Class>();

            while (ClassResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ClassResultSet["ClassId"]);
                string ClassCode = ClassResultSet["ClassCode"].ToString();
                string ClassName = ClassResultSet["ClassName"].ToString();


                Class NewClass = new Class
                {
                    ClassId = ClassId,
                    ClassCode = ClassCode,
                    ClassName = ClassName
                };
                Classes.Add(NewClass);
            }
            NewTeacher.Classes = Classes;

            Conn.Close();

            return NewTeacher;
        }

        /// <summary>
        /// This method will delete a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <example>
        /// POST: /api/TeacherData/DeleteTeacher/3
        /// </example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Teacherdata.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Delete from teachers where teacherid=@id";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection between MYSQL database and web server
            Conn.Close();
        }


        /// <summary>
        /// Receives teacher information and enters it into the database
        /// </summary>
        /// <param name="NewTeacher"></param>
        /// <example>
        /// POST: /api/TeacherData/AddTeacher
        /// </example>
        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Teacherdata.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary)" +
                "values(@TeacherFname,@TeacherLname,@EmployeeNum, @HireDate, @TeacherSalary)";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNum", NewTeacher.EmployeeNum);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@TeacherSalary", NewTeacher.TeacherSalary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection between MYSQL database and web server
            Conn.Close();
        }


        /// <summary>
        /// Updates a teacher on the MySql Database. Non-deterministic.
        /// </summary>
        /// <param name="id"> The Id of the teacher that we want to update</param>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table</param>
        /// <example>
        /// POST : api/TeacherData/UpdateTeacher/{id}
        /// FROM DATA / POST DATA / REQUEST BODY
        /// {
        /// "TeacherFname" : "John"
        /// "TeacherLname" : "Doe",
        /// "EmployeeNum" : "T123",
        /// "TeacherSalary" : $50
        /// }
        /// </example>
        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Teacherdata.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNum, salary=@TeacherSalary where teacherid = @TeacherId";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNum", TeacherInfo.EmployeeNum);
            cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@TeacherId", id );

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection between MYSQL database and web server
            Conn.Close();

        }

    }
}
