using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class UserModel
    {
        [DisplayName("User name")]
        [Required(ErrorMessage = "User name required")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}