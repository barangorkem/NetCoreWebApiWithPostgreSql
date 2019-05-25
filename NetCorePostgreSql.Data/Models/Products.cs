using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NetCorePostgreSql.Data.Models
{
    public class Products
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage = "Gereklidir.")]
        [MaxLength(50, ErrorMessage = "50 karakterden fazla ürün adı olamaz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gereklidir.")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Price { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}
