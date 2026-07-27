using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoApi.Entities
{
    [Index(nameof(SessionId), IsUnique = true)]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // e.g., "pending", "completed", "failed"
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        [MaxLength(100)]
        public string SessionId { get; set; }
    }
}
