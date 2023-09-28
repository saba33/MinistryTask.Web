using MinistryTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MinistryTask.Serivices.Models.RequestModels.ProductRequestModel
{
    public class ProductDto
    {
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
    }
}
