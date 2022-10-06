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
                            (""Dtransaction"", ""Ctransaction"", amount, status)
                            VALUES (@Dtransaction, @Ctransaction, @amount, @status);";

            string queryUpdate = @"UPDATE ""Account""
                                 SET amount = ""Account"".amount + @amount2
                                 WHERE ""Id_account"" = @Ctransaction2;";

            string queryDowngrade = @"UPDATE ""Account""
                                 SET amount = ""Account"".amount - @amount3
                                 WHERE ""Id_account"" = @Dtransaction3;";

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
    }
}
