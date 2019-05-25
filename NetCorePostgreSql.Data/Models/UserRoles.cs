using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NetCorePostgreSql.Data.Models
{
    public class UserRoles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
    }
}
