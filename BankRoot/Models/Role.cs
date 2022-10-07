using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankRoot.Models
{
    public class Role
    {

        [Key]
        public int Id_role { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string role_name { get; set; }

        public ICollection<App_user?>? App_users { get; set; }

    }
}
