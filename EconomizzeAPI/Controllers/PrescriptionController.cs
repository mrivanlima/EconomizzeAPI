using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using EconomizzeHybrid.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/prescricao")]
	public class PrescriptionController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IPrescriptionRepository _prescriptionRepository;
		public PrescriptionController(IPrescriptionRepository prescriptionRepository, IMapper mapper)
		{
			_prescriptionRepository = prescriptionRepository ?? throw new ArgumentNullException(nameof(prescriptionRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		#region CREATE PRESCRIPTION
		[HttpPost("criar")]
		public async Task<ActionResult<Prescription>> CreatePrescription(PrescriptionViewModel prescription)
		{
			var map = _mapper.Map<Prescription>(prescription);

			var PrescriptionViewModel = await _prescriptionRepository.CreatePrescriptionAsync(map);
			if (PrescriptionViewModel.Item2.HasError)
			{
				return BadRequest(PrescriptionViewModel.Item2.Message);
			}

			return CreatedAtRoute("prescricao", new { PrescriptionId = PrescriptionViewModel.Item1.PrescriptionId }, _mapper.Map<PrescriptionViewModel>(map));
		}
		#endregion

		#region CREATE PRESCRIPTION
		[HttpPost("criar/orcamentoprescricao")]
		public async Task<ActionResult<QuotePrescription>> CreateQuotePrescription(QuotePrescriptionViewModel quotePrescription)
		{
			var map = _mapper.Map<QuotePrescription>(quotePrescription);

			var QuotePrescriptionViewModel = await _prescriptionRepository.CreateQuotePrescriptionAsync(map);
			if (QuotePrescriptionViewModel.Item2.HasError)
			{
				return BadRequest(QuotePrescriptionViewModel.Item2.Message);
			}

			return CreatedAtRoute("prescricao", new { PrescriptionId = QuotePrescriptionViewModel.Item1.PrescriptionId }, _mapper.Map<QuotePrescriptionViewModel>(map));
		}
		#endregion

		#region GET PRESCRIPTION ID
		[HttpGet("{PrescriptionId}", Name = "prescricao")]
		public async Task<ActionResult<PrescriptionViewModel>> GetPrescriptionById(int PrescriptionId)
		{
			var prescription = "hi";
			if (prescription == null)
			{
				return NotFound();
			}

			return Ok(prescription);
		}
		#endregion
	}
}
