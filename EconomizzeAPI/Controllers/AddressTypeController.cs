using AutoMapper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
    [ApiController]
    [Route("api/tipodeendereco")]
    public class AddressTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAddressTypeRepository _addressTypeRepository;
        public AddressTypeController(IAddressTypeRepository addressTypeRepository, IMapper mapper)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressTypeViewModel>>> GetAddressTypeAll()
        {
            var addressType = await _addressTypeRepository.ReadAllAsync();
            if (addressType.Count() < 1)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AddressTypeViewModel>>(addressType));
        }


    }
}
