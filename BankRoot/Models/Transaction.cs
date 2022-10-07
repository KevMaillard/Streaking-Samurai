using BankRoot.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankRoot.Models
{
    public class Transaction
    {

        [Key]
        public int Id_transaction { get; set; }

        public int Dtransaction { get; set; }
        public int Ctransaction { get; set; }

        public virtual Account? AccountD { get; set; }
        public virtual Account? AccountC { get; set; }






        [Column]
        public DateTime? date_transaction { get; set; } = DateTime.Now;

        [Column]
        public decimal amount { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string status { get; set; }

    }
}