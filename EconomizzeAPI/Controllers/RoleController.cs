using Microsoft.AspNetCore.Mvc;
using Economizze.Library;
using AutoMapper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Interfaces;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/role")]
	public class RoleController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IRoleRepository _roleRepository;
		public RoleController(IRoleRepository roleRepository, IMapper mapper)
		{
			_roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpPost]
		public async Task<ActionResult<Role>> CreateRole(RoleViewModel role)
		{
			var map = _mapper.Map<Role>(role);
			var roleViewModel = await _roleRepository.CreateRoleAsync(map);
			if (roleViewModel.Item2.HasError)
			{
				return BadRequest();
			}

			return CreatedAtRoute("role", new { RoleId = roleViewModel.Item1.RoleId }, _mapper.Map<RoleViewModel>(map));
		}

		[HttpGet("{roleId}", Name = "role")]
		public async Task<ActionResult<RoleViewModel>> GetById(short roleId)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<RoleViewModel>>> ReadAllRoles()
		{
			var roles = await _roleRepository.ReadAllRolesAsync();
			if (roles.Count() < 1)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<IEnumerable<RoleViewModel>>(roles));
		}
	}
}
