using MinistryTask.Domain;
using System.ComponentModel.DataAnnotations;

namespace MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels
{
    public class AuthorDisplayModel
    {
        public int Id { get; set; }
        [MinLength(2), MaxLength(50)]
        public string Name { get; set; }
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [MinLength(11), MaxLength(11)]
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [MinLength(4), MaxLength(50)]
        public string Phone { get; set; }
        public string Mail { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
