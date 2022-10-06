using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankRoot.Models
{
    public class Account
    {
        [Key]
        public int Id_account { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string account_number { get; set; }

        [Column]
        public decimal amount { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string account_status { get; set; }

        [ForeignKey("App_user")]
        public int Id_app_user { get; set; }
        public App_user? App_user { get; set; }

        public virtual ICollection<Transaction?>? TransactionsD { get; set; }
        public virtual ICollection<Transaction?>? TransactionsC { get; set; }
    }
}
