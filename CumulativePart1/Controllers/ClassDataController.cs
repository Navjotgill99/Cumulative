using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativePart1.Models;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;

namespace CumulativePart1.Controllers
{
    public class ClassDataController : ApiController
    {
        //Create context to access database
        private SchoolDbContext Classdata = new SchoolDbContext();

        /// <summary>
        /// Access information about classes and returns a list of classes
        /// </summary>
        /// <returns>
        /// A list of classes objects with all the information about classes from database column.
        /// </returns>
        /// <example>
        /// GET: api/Classdata/Listclasses -> {class object, class Object....}
        /// </example>
        [HttpGet]
        [Route("api/classdata/listclasses")]

        public IEnumerable<Class> ListClasses()
        {
            //create an instance of a connection
            MySqlConnection Conn = Classdata.AccessDatabase();

            //open the connection between web server and database
            Conn.Open();

            //SQL query
            string query = "select * from classes";

            //a new command for our databse
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            //get results of the query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //create an empty list of classes
            List<Class> classes = new List<Class> { };

            //loop through each row of the result set
            while (ResultSet.Read())
            {
                //Access column info by database column name
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);



                //create a new class
                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;
                NewClass.TeacherId = TeacherId;

                //add class name to the list
                classes.Add(NewClass);
            }

            //close the connection between MYSQL database and web server
            Conn.Close();

            //return the list of class names
            return classes;
        }

        /// <summary>
        /// Find a class by id
        /// </summary>
        /// <param name="id">The class primary key</param>
        /// <returns>A class object</returns>

        [HttpGet]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            //create an instance of a connection
            MySqlConnection Conn = Classdata.AccessDatabase();

            //open the connection between webserver and database
            Conn.Open();

            //a new command (query) for our databse
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from classes where classid = " + id;

            //get results of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access column info by database column name
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);



                //create a new class
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;
                NewClass.TeacherId = TeacherId;
            }
            //return newclass
            return NewClass;
        }
    }
}
