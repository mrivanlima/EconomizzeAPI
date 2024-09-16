using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class UserLoginViewModel
    {
		public int UserId { get; set; }

		[Required(ErrorMessage = "Email necessario")]
		[EmailAddress(ErrorMessage = "Tem de ser um email")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Senha necessaria")]
		[DataType(DataType.Password)]
		//[RegularExpression(@"^[0-9]{10}$", Message = "Phone number must be 10 digits.")]
		public string Password { get; set; } = string.Empty;

		public string UserToken { get; set; } = string.Empty;
    }
}
