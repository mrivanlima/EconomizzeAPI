using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        ///[Authorize]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserViewModel userViewModel)
        {
            var map = _mapper.Map<User>(userViewModel);
            var user = await _userRepository.CreateAsync(map);
            if (user.Item2.HasError)
            {
                return BadRequest( user.Item2.Message);
            }

            return CreatedAtRoute("usuario",

                new { UserId = user.Item1.UserId },
                _mapper.Map<UserViewModel>(map));
        }

        [HttpGet("{userId}", Name = "usuario")]
        public async Task<ActionResult<UserViewModel>> GetById(int userId)
        {
            var user = await _userRepository.ReadByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost("{username}")]
        public async Task<ActionResult<UserDetailViewModel>> GetByUsername(UserLoginViewModel userlogin)
        {
            var user = await _userRepository.ReadUserByUsername(userlogin.Username);
            if (user == null)
            {
                return NotFound();
            }

            if (user.PasswordHash != userlogin.Password)
            {
                return Unauthorized("Senha incorreta!");
            }

            if (!user.IsVerified)
            {
                return Unauthorized("Confirme seu email!");
            }

            if (!user.IsActive)
            {
                return Unauthorized("Usuario inativo!");
            }

            if (user.IsLocked)
            {
                return Unauthorized("Usuario blockeado!");
            }

            if (!user.ChangedInitialPassword)
            {
                return Unauthorized("Mude senha!");
            }

            if (user.PasswordAttempts > 2)
            {
                return Unauthorized("Espere uma hora!");
            }

            return Ok(_mapper.Map<UserDetailViewModel>(user));
        }
    }
}
