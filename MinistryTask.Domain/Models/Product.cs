using MinistryTask.Domain.Enums;
using MinistryTask.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MinistryTask.Domain
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(250), MinLength(2)]
        public string Name { get; set; }
        [MaxLength(500), MinLength(100)]
        public string Annotation { get; set; }
        public ProductType ProductType { get; set; }
        [MinLength(13), MaxLength(13)]
        public string ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Publisher Publisher { get; set; }
        public int NumberOfPages { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public int ProductStatusId { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
