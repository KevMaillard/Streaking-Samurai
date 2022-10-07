using BankRoot.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BankRoot.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();


            modelBuilder.Entity<Transaction>()
                .HasOne(e => e.AccountD)
                .WithMany(e => e.TransactionsD)
                .HasForeignKey(e => e.Dtransaction);

            modelBuilder.Entity<Transaction>()
                .HasOne(e => e.AccountC)
                .WithMany(e => e.TransactionsC)
                .HasForeignKey(e => e.Ctransaction);
        } 

        public DbSet<App_user> App_user { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}

//modelBuilder.Entity<Transaction>()
//    .HasKey(e => new { e.Dtransaction, e.Ctransaction });