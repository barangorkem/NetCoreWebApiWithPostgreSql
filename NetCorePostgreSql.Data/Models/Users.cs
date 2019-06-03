using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NetCorePostgreSql.Data.Models
{
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Gereklidir")]
        [MaxLength(20, ErrorMessage = "20 karakterden fazla olamaz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Gereklidir")]
        [MaxLength(20, ErrorMessage = "20 karakterden fazla olamaz.")]
        public string Password { get; set; }
        public string Salt { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<Products> Products { get; set; }

    }
}
