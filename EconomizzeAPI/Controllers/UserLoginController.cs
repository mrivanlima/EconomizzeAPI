using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/conta")]
	public class UserLoginController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUserLoginRepository _userLoginRepository;
		private readonly IConfiguration _config;
		public UserLoginController(IUserLoginRepository userLoginRepository, 
			                       IMapper mapper,
                                   IConfiguration config)
		{
			_config = config;
			_userLoginRepository = userLoginRepository ?? throw new ArgumentNullException(nameof(userLoginRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

        [HttpPost("criar")]
        public async Task<ActionResult<RegisterViewModel>> CreateUser(RegisterViewModel register)
		{
			register.UserUniqueId = Guid.NewGuid();
			var map = _mapper.Map<RegisterViewModel>(register);
			map.Password = BCrypt.Net.BCrypt.HashPassword
				(
					register.UserUniqueId.ToString() + 
				    map.Password + 
					register.UserUniqueId.ToString()
				);
			var RegisterViewModel = await _userLoginRepository.CreateAsync(map);
			if (RegisterViewModel.Item2.HasError)
			{
				return BadRequest(RegisterViewModel.Item2.ErrorMessage);
			}

			return CreatedAtRoute("usuario", new { UserId = RegisterViewModel.Item1.UserId }, _mapper.Map<RegisterViewModel>(map));
		}

		[HttpPost("autenticar")]
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
            //if (login.Password != userLogin.PasswordHash)
            if (!BCrypt.Net.BCrypt.Verify(userLogin.UserUniqueId.ToString() + 
				                          login.Password +
                                          userLogin.UserUniqueId.ToString(), userLogin.PasswordHash))
            {
				return Unauthorized("Senha incorreta.");
			}

			UserLoginViewModel toReturn = _mapper.Map<UserLoginViewModel>(userLogin);
			toReturn.UserToken = CreateToken(userLogin);

            return Ok(toReturn);
		}

		[HttpGet("{UserId}")]
		public async Task<ActionResult<RegisterViewModel>> GetById(short userId)
		{
			throw new NotImplementedException();
		}

		private string CreateToken(UserLogin userLogin)
		{
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["JwtSettings:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userLogin.UserId.ToString()),
                    new Claim("Username", userLogin.Username),
                    new Claim("UserUnique", userLogin.UserUniqueId.ToString()),

                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var sigIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _config["JwtSettings:Issuer"],
                    _config["JwtSettings:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: sigIn
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}