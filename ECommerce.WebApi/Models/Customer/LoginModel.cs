using System.ComponentModel.DataAnnotations;

namespace ECommerce.WebApi.Models.Customer
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username Is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Username Is Required")]
        public string Password { get; set; }
    }
}
