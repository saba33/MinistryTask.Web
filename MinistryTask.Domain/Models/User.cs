using MinistryTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MinistryTask.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        [MinLength(2), MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [MinLength(11), MaxLength(11)]
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; } = UserRoles.Operator.ToString();
    }
}
