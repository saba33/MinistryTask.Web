using System.ComponentModel.DataAnnotations;

namespace MinistryTask.Serivices.Models.ResposeModels.UserRequestModels
{
    public class LoginRequestDto
    {
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
