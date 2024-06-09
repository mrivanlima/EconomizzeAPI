using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<UserSetUp>> CreateUser(UserSetUp user)
        {
            var map = _mapper.Map<UserSetUp>(user);
            var userSetUpViewModel = await _userRepository.CreateAsync(map);
            if (userSetUpViewModel.Item2.HasError)
            {
                return BadRequest(userSetUpViewModel.Item2.ErrorMessage);
            }

            return CreatedAtRoute("usuario", new { UserId = userSetUpViewModel.Item1.UserId }, _mapper.Map<UserSetUpViewModel>(map));
        }

        [HttpGet("{userId}", Name = "usuario")]
        public async Task<ActionResult<UserSetUpViewModel>> GetById(int userId)
        {
            var user = await _userRepository.ReadByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StateViewModel>(user));
        }
    }
}
