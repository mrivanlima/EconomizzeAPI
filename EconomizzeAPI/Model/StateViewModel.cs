using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
	public class StateViewModel
	{
		public byte StateId { get; set; }

		[Required(ErrorMessage = "Nome do Estado e necessario")]
		[MinLength(4, ErrorMessage = "Palavra precisa ser no minimo 4 caracteres.")]
        [MaxLength(20, ErrorMessage = "Palavra precisa ser de no maximo 20 caracteres.")]
        public string StateName { get; set; } = string.Empty;

        [MinLength(2, ErrorMessage = "Palavra precisa ser no minimo 2 caracteres.")]
        [MaxLength(2, ErrorMessage = "Palavra precisa ser de no maximo 2 caracteres.")]
        public string StateUf { get; set; } = string.Empty;
		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }
	}
}
