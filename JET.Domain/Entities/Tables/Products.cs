using Microsoft.AspNetCore.Http;
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
        public DateTime DateCreation { get; set; }
        [MaxLength(100)]
        public string? Image { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
        public bool StatusPromo { get; set; }
        public double Price { get; set; }
        [NotMapped]
        public string FormattedPrice
        {
            get
            {
                return "R$ " + Price;
            }
        }

        [NotMapped]
        public string LabelPromo
        {
            get
            {
                if (StatusPromo)
                {
                    return "Promocional";
                }
                else
                {
                    return "Não Promocional";
                }

            }
        }
        [NotMapped]
        public string LabelStatus
        {
            get
            {
                if (Status)
                {
                    return "Ativo";
                }
                else
                {
                    return "Inativo";
                }

            }
        }

    }

    public class ProductCreateOrUpdate
    {
        [Required, MaxLength(150)]
        public string ProductName { get; set; }
        public string? Image { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
        public bool StatusPromo { get; set; }
        public double Price { get; set; }
    }

    public class ResponseImage
    {
        public string NameFile { get; set; }
        public string Response { get; set; }
    }
}
