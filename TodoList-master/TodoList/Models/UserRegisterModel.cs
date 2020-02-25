using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TodoList.Models
{
    public class UserRegisterModel
    {
        [DisplayName("User name")]
        [Required(ErrorMessage = "User name is required")]
        [Remote("IsUserNameNotExists", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists in database")]
        [MinLength(8, ErrorMessage = "User name should have atleast 8 characters")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="Password should have atleast 8 characters")]
        public string Password { get; set; }

        [DisplayName("Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string Name { get; set; }

        [DisplayName("Email id")]
        [Required(ErrorMessage = "Email id is required")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [DisplayName("Contact number")]
        [Required(ErrorMessage = "Contact number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Invalid phone number")]
        public string ContactNo { get; set; }
    }
}
