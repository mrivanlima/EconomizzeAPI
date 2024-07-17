using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
	public class RegisterViewModel
	{
		public int UserId { get; set; }
		public Guid UserUniqueId { get; set; }

		[Required(ErrorMessage = "Email necessario")]
		[EmailAddress(ErrorMessage = "Tem de ser um email")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Senha necessaria")]
		[DataType(DataType.Password)]
		//[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits.")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Comfirmacao necessaria")]
		[DataType(DataType.Password)]
		//[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits.")]
		[Compare("Password")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
