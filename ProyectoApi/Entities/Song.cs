using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoApi.Entities
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Album { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Artist { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }
        
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
