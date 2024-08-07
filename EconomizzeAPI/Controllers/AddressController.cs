using AutoMapper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
    [ApiController]
    [Route("api/endereco")]
    public class AddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{zipCode}", Name = "endereco")]
        public async Task<ActionResult<AddressDetailViewModel>> GetById(string zipCode)
        {
            var addressDetail = await _addressRepository.ReadByZipCodeAsync(zipCode);
            if (addressDetail is null)
            {
                return NotFound("Cep nao encontrado!");
            }

            return Ok(_mapper.Map<AddressDetailViewModel>(addressDetail));
        }
    }
}
