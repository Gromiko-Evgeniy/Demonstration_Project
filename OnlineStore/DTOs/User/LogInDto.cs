using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTOs.User
{
    public class LogInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
