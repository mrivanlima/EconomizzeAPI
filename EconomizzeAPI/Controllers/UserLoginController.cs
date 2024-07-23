using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/register")]
	public class UserLoginController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUserLoginRepository _userLoginRepository;
		public UserLoginController(IUserLoginRepository userLoginRepository, IMapper mapper)
		{
			_userLoginRepository = userLoginRepository ?? throw new ArgumentNullException(nameof(userLoginRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpPost]
		public async Task<ActionResult<RegisterViewModel>> CreateUser(RegisterViewModel register)
		{
			register.UserUniqueId = Guid.NewGuid();
			var map = _mapper.Map<RegisterViewModel>(register);
			var RegisterViewModel = await _userLoginRepository.CreateAsync(map);
			if (RegisterViewModel.Item2.HasError)
			{
				return BadRequest(RegisterViewModel.Item2.ErrorMessage);
			}

			return CreatedAtRoute("usuario", new { UserId = RegisterViewModel.Item1.UserId }, _mapper.Map<RegisterViewModel>(map));
		}

		[HttpPost("Auth")]
		public async Task<ActionResult<UserLoginViewModel>> AuthUser(UserLoginViewModel login)
		{
			var map = _mapper.Map<UserLoginViewModel>(login);
			var userLogin = await _userLoginRepository.AuthorizeAsync(map);
			if (userLogin.UserId == 0)
			{
				return NotFound("Usuario nao encontrado");
			}
			if (!userLogin.IsActive)
			{
				return Unauthorized("Conta nao ativada.");
			}
			if (!userLogin.IsVerified)
			{
				return Unauthorized("Verifique link no email.");
			}
			if (userLogin.IsLocked)
			{
				return Unauthorized("Conta suspensa.");
			}
			if (login.Password != userLogin.PasswordHash)
			{
				return Unauthorized("Senha incorreta.");
			}

			return Ok(_mapper.Map<UserLoginViewModel>(userLogin));
		}

		[HttpGet("{UserId}", Name = "register")]
		public async Task<ActionResult<RegisterViewModel>> GetById(short userId)
		{
			throw new NotImplementedException();
		}
	}
}