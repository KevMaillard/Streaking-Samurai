using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankRoot.Models
{
    public class App_user
    {
        [Key]
        public int Id_app_user { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string app_user_number { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string first_name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string last_name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string password { get; set; }

        [ForeignKey("Role")]
        public int Id_role { get; set; }
        public Role? Role { get; set; }

        public ICollection<Account?>? Accounts { get; set; }

    }


}
