using Microsoft.AspNetCore.Mvc;
using Economizze.Library;
using AutoMapper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Interfaces;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/usuariorole")]
	public class UserRoleController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUserRoleRepository _userRoleRepository;
		public UserRoleController(IUserRoleRepository userRoleRepository, IMapper mapper)
		{
			_userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpPost]
		public async Task<ActionResult<UserRole>> CreateUserRole(UserRoleViewModel userRole)
		{
			var map = _mapper.Map<UserRole>(userRole);
			var userRoleViewModel = await _userRoleRepository.CreateUserRoleAsync(map);
			if (userRoleViewModel.Item2.HasError)
			{
				return BadRequest();
			}

			return CreatedAtRoute("userRole", new { RoleId = userRoleViewModel.Item1.RoleId }, _mapper.Map<UserRoleViewModel>(map));
		}

		[HttpPost("userRoleRead")]
		public async Task<ActionResult<IEnumerable<RoleViewModel>>> ReadUserRolesById(UserRoleViewModel userRole)
		{
			var map = _mapper.Map<UserRoleViewModel>(userRole);
			var roles = await _userRoleRepository.ReadAllUserRolesAsync(map);
			if (roles.Count() < 1)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<IEnumerable<RoleViewModel>>(roles));
		}

		[HttpGet("{userRoleId}", Name = "userRole")]
		public async Task<ActionResult<RoleViewModel>> GetById(short roleId)
		{
			throw new NotImplementedException();
		}
	}
}
