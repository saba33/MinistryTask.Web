using MinistryTask.Domain.Enums;
using MinistryTask.Serivices.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel
{
    public class AuthorDto
    {

        [MinLength(2), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [MinLength(11), MaxLength(11)]
        public string PrivateNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Country Country { get; set; }

        public City City { get; set; }

        [MinLength(4), MaxLength(50)]
        public string Phone { get; set; }

        [EmailValidator(ErrorMessage = "Please enter a valid email address")]
        public string Mail { get; set; }
    }
}
