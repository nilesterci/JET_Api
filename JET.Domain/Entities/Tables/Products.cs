using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JET.Domain.Entities.Tables
{
    [Table(name: "products")]
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string ProductName { get; set; }
        [Timestamp]
        public DateTime DateCreation { get; set; }
        public byte[]? Image { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }
    }
}
