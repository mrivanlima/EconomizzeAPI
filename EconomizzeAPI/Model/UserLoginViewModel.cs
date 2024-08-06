using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class UserLoginViewModel
    {
		[Required(ErrorMessage = "Email necessario")]
		[EmailAddress(ErrorMessage = "Tem de ser um email")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Senha necessaria")]
		[DataType(DataType.Password)]
		//[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits.")]
		public string Password { get; set; } = string.Empty;
    }
}
