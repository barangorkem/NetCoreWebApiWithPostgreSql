using System.ComponentModel.DataAnnotations;


namespace NetCorePostgreSql.Service.DTO
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Gereklidir")]
        [MaxLength(20, ErrorMessage = "20 karakterden fazla olamaz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Gereklidir")]
        [MaxLength(20, ErrorMessage = "20 karakterden fazla olamaz.")]
        public string Password { get; set; }

        public string Email { get; set; }

        public string[] Roles { get; set; }
    }
}
