using BankRoot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace BankRoot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from ""Transaction"";";

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
        public JsonResult Post(Transaction transa)
        {
            string query = @"INSERT INTO ""Transaction""
                            (""Dtransaction"", ""Ctransaction"", date_transaction, amount, status)
                            VALUES (@Dtransaction, @Ctransaction, @date_transaction, @amount, @status);";

            string queryUpdate = @"UPDATE ""Account""
                                 SET amount = ""Account"".amount + @amount2
                                 WHERE ""Id_account"" = @Ctransaction2;";

            string queryDowngrade = @"UPDATE ""Account""
                                 SET amount = ""Account"".amount - @amount3
                                 WHERE ""Id_account"" = @Dtransaction3;";

            DateTime date = DateTime.Now;

            DataTable table = new DataTable();
            DataTable tableUpdate = new DataTable();
            DataTable tableDowngrade = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("MvcDemoConnectionString");
            NpgsqlDataReader myReader;
            NpgsqlDataReader myReaderUpdate;
            NpgsqlDataReader myReaderDowngrade;
            using (NpgsqlConnection myCon = new NpgsqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Dtransaction", transa.Dtransaction);
                    myCommand.Parameters.AddWithValue("@Ctransaction", transa.Ctransaction);
                    myCommand.Parameters.AddWithValue("@date_transaction", date);
                    myCommand.Parameters.AddWithValue("@amount", transa.amount);
                    myCommand.Parameters.AddWithValue("@status", transa.status);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }

                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(queryUpdate, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Ctransaction2", transa.Ctransaction);
                        myCommand.Parameters.AddWithValue("@amount2", transa.amount);
                        myReaderUpdate = myCommand.ExecuteReader();
                        tableUpdate.Load(myReaderUpdate);

                        myReaderUpdate.Close();
                        myCon.Close();
                    }

                        myCon.Open();
                        using (NpgsqlCommand myCommand = new NpgsqlCommand(queryDowngrade, myCon))
                        {
                            myCommand.Parameters.AddWithValue("@Dtransaction3", transa.Dtransaction);
                            myCommand.Parameters.AddWithValue("@amount3", transa.amount);
                            myReaderDowngrade = myCommand.ExecuteReader();
                            tableDowngrade.Load(myReaderDowngrade);

                            myReaderDowngrade.Close();
                            myCon.Close();
                        }

            }
                return new JsonResult("Added Successfully");
        }

        [HttpGet("Details")]
        public JsonResult Details()
        {
            string query = @"SELECT * FROM ""Transaction""
                            INNER JOIN ""Account"" ON ""Account"".""Id_account"" = ""Transaction"".""Ctransaction""
                            INNER JOIN ""App_user"" ON ""Account"".""Id_app_user"" = ""App_user"".""Id_app_user""
                            INNER JOIN ""Role"" ON ""App_user"".""Id_role"" = ""Role"".""Id_role""
                            INNER JOIN ""Account"" AS Acc ON Acc.""Id_account"" = ""Transaction"".""Dtransaction""
                            INNER JOIN ""App_user"" AS App ON Acc.""Id_app_user"" = App.""Id_app_user""
                            INNER JOIN ""Role"" AS Rol ON App.""Id_role"" = Rol.""Id_role""";

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
    }
}
