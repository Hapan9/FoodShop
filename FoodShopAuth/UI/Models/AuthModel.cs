using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class AuthModel
    {
        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
    }
}