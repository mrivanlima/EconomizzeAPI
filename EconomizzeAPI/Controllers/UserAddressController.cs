using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
    [Route("api/usuarioendereco")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserAddressRepository _userAddressRepository;
        public UserAddressController(IUserAddressRepository userAddressRepository, IMapper mapper)
        {
            _userAddressRepository = userAddressRepository ?? throw new ArgumentNullException(nameof(userAddressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpPost]
        public async Task<ActionResult<UserAddressViewModel>> CreateUserAddress(UserAddressViewModel userAddress)
        {
            var map = _mapper.Map<UserAddress>(userAddress);
            var model = await _userAddressRepository.CreateAsync(map);
            if (model.Item2.HasError)
            {
                return BadRequest(model.Item2.Message);
            }

            return CreatedAtRoute("usuarioendereco", userAddress, _mapper.Map<UserAddressViewModel>(map));
        }

        [HttpGet("{userId}", Name = "usuarioendereco")]
        public async Task<ActionResult<UserAddressViewModel>> GetById(UserAddressViewModel userAddress)
        {
            var user = await _userAddressRepository.ReadByIdAsync(userAddress);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserAddressViewModel>(userAddress));
        }

    }
}
