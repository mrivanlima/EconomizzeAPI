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
		public async Task<ActionResult<RegisterViewModel>> AuthUser(RegisterViewModel register)
		{
			register.UserUniqueId = Guid.NewGuid();
			var map = _mapper.Map<RegisterViewModel>(register);
			var RegisterViewModel = await _userLoginRepository.AuthorizeAsync(map);
			if (RegisterViewModel.Item2.HasError)
			{
				return BadRequest(RegisterViewModel.Item2.ErrorMessage);
			}

			return CreatedAtRoute("usuario", new { UserId = RegisterViewModel.Item1.UserId }, _mapper.Map<RegisterViewModel>(map));
		}

		[HttpGet("{UserId}", Name = "register")]
		public async Task<ActionResult<RegisterViewModel>> GetById(short userId)
		{
			throw new NotImplementedException();
		}
	}
}