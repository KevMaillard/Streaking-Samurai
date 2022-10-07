using BankRoot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace BankRoot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from ""Account"";";

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
        public JsonResult Post(Account acc)
        {
            string query = @"INSERT INTO ""Account""
                            (account_number, amount, account_status, ""Id_app_user"")
                            VALUES (@account_number, @amount, @account_status, @Id_app_user);";

            var uniqueValue = Guid.NewGuid();

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@account_number", uniqueValue);
                    myCommand.Parameters.AddWithValue("@amount", acc.amount);
                    myCommand.Parameters.AddWithValue("@account_status", acc.account_status);
                    myCommand.Parameters.AddWithValue("@Id_app_user", acc.Id_app_user);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Account acc)
        {
            string query = @"update ""Account"" set
                             account_number = @account_number, 
                             amount = @amount,
                             account_status = @account_status,
                             ""Id_app_user"" = @Id_app_user
                             where ""Id_account""=@Id_account;"
            ;

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@account_number", acc.account_number);
                    myCommand.Parameters.AddWithValue("@amount", acc.amount);
                    myCommand.Parameters.AddWithValue("@account_status", acc.account_status);
                    myCommand.Parameters.AddWithValue("@Id_app_user", acc.Id_app_user);
                    myCommand.Parameters.AddWithValue("@Id_account", acc.Id_account);
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
            string query = @"delete from ""Account""
                             where ""Id_account""=@Id_account;";

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id_account", id);
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
            string query = @"SELECT * FROM ""Account"" INNER JOIN ""App_user""
                            ON ""Account"".""Id_app_user"" = ""App_user"".""Id_app_user""
                            INNER JOIN ""Role"" ON ""App_user"".""Id_role"" = ""Role"".""Id_role""";

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
            string query = @"SELECT * FROM ""Account"" INNER JOIN ""App_user""
                            ON ""Account"".""Id_app_user"" = ""App_user"".""Id_app_user""
                            INNER JOIN ""Role"" ON ""App_user"".""Id_role"" = ""Role"".""Id_role""
                            WHERE ""Id_account"" = @Id_account";

            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id_account", id);
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
