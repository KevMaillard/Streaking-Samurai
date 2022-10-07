using BankRoot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace BankRoot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AppUserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from ""App_user"";";

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(App_user user)
        {
            string query = @"INSERT INTO ""App_user""
                            (app_user_number,first_name,last_name,email,password,""Id_role"")
                            VALUES (@app_user_number,@first_name,@last_name,@email,@password,@Id_role);";

            //if (user.password != "123")
            //{
            //    return new JsonResult("There was a problem...");
            //}

            byte[] encData_byte = new byte[user.password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(user.password);
            string encodedData = Convert.ToBase64String(encData_byte);


            var uniqueValue = Guid.NewGuid();

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@app_user_number", uniqueValue);
                    myCommand.Parameters.AddWithValue("@first_name", user.first_name);
                    myCommand.Parameters.AddWithValue("@last_name", user.last_name);
                    myCommand.Parameters.AddWithValue("@email", user.email);
                    myCommand.Parameters.AddWithValue("@password", encodedData);
                    myCommand.Parameters.AddWithValue("@Id_role", user.Id_role);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(App_user user)
        {
            string query = @"update ""App_user"" set
                             app_user_number = @app_user_number, 
                             first_name = @first_name,
                             last_name = @last_name,
                             email = @email,
                             password = @password,
                             ""Id_role"" = @Id_role
                             where ""Id_app_user""=@Id_app_user;"
            ;

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@app_user_number", user.app_user_number);
                    myCommand.Parameters.AddWithValue("@first_name", user.first_name);
                    myCommand.Parameters.AddWithValue("@last_name", user.last_name);
                    myCommand.Parameters.AddWithValue("@email", user.email);
                    myCommand.Parameters.AddWithValue("@password", user.password);
                    myCommand.Parameters.AddWithValue("@Id_role", user.Id_role);
                    myCommand.Parameters.AddWithValue("@Id_app_user", user.Id_app_user);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from ""App_user""
                             where ""Id_app_user""=@Id_app_user;";

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id_app_user", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
        [HttpGet("Details")]
        public JsonResult Details()
        {
            string query = @"SELECT * FROM ""App_user"" INNER JOIN ""Role""
                            ON ""App_user"".""Id_role"" = ""Role"".""Id_role""";

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("Details/{id}")]
        public JsonResult DetailsId(int id)
        {
            string query = @"SELECT * FROM ""App_user"" INNER JOIN ""Role""
                            ON ""App_user"".""Id_role"" = ""Role"".""Id_role""
                            WHERE ""Id_app_user"" = @Id_user";

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id_user", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
