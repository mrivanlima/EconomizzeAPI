using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
	public class StateViewModel
	{
		public byte StateId { get; set; }

		[StringLength(20, MinimumLength = 4, ErrorMessage = "Palavra precisa ser de no maximo 20 caracteres.")]
		[Required(ErrorMessage = "Nome do Estado e necessario")]
		public string StateName { get; set; } = string.Empty;

		[StringLength(2, MinimumLength = 2, ErrorMessage = "Palavra precisa ser de 2 caracteres.")]
		public string StateUf { get; set; } = string.Empty;
		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }
	}
}
