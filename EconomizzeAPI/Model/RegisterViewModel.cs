using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
	public class RegisterViewModel
	{
		//unique IDs, one ordered, the other random
		public int UserId { get; set; }
		public Guid UserUniqueId { get; set; }

		//required Username (Email)
		[Required(ErrorMessage = "Email necessario")]
		[EmailAddress(ErrorMessage = "Tem de ser um email")]
		public string Username { get; set; } = string.Empty;

		//required Password
		[Required(ErrorMessage = "Senha necessaria")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		//required Password Confirmation (Must be same as password)
		[Required(ErrorMessage = "Comfirmacao necessaria")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Comfirmacao necessaria, senhas diferentes")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
