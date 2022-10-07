using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankRoot.Data;
using BankRoot.Models;

namespace BankRoot.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Account.Include(a => a.App_user);
            return View(await dataContext.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.App_user)
                .FirstOrDefaultAsync(m => m.Id_account == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            ViewData["Id_app_user"] = new SelectList(_context.App_user, "Id_app_user", "Id_app_user");
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_account,account_number,amount,account_status,Id_app_user")] Account account)
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
            ViewData["Id_app_user"] = new SelectList(_context.App_user, "Id_app_user", "Id_app_user", account.Id_app_user);
            return View(account);
        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["Id_app_user"] = new SelectList(_context.App_user, "Id_app_user", "Id_app_user", account.Id_app_user);
            return View(account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_account,account_number,amount,account_status,Id_app_user")] Account account)
        {
            if (id != account.Id_account)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id_account))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_app_user"] = new SelectList(_context.App_user, "Id_app_user", "Id_app_user", account.Id_app_user);
            return View(account);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.App_user)
                .FirstOrDefaultAsync(m => m.Id_account == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Account == null)
            {
                return Problem("Entity set 'DataContext.Account'  is null.");
            }
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                _context.Account.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return _context.Account.Any(e => e.Id_account == id);
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
