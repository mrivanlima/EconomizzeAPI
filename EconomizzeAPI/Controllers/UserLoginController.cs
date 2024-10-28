using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
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
        #region INTERFACE INTEGRATION
        private readonly IMapper _mapper;
		private readonly IUserLoginRepository _userLoginRepository;
		private readonly IConfiguration _config;
        #endregion

        #region CONSTRUCTOR
        public UserLoginController(IUserLoginRepository userLoginRepository, 
			                       IMapper mapper,
                                   IConfiguration config)
		{
			_config = config;
			_userLoginRepository = userLoginRepository ?? throw new ArgumentNullException(nameof(userLoginRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
        #endregion

        #region VERIFY USER
        [HttpGet("verificar/{userId}")]
        public async Task<ActionResult<RegisterViewModel>> VerifyUser(int userId, [FromQuery] Guid userUniqueId)
        {
			var results = await _userLoginRepository.UserVerifyAsync(userId, userUniqueId);
			if(results.HasError)
			{
				return BadRequest(results.Message);
			}
			return Ok();
        }
        #endregion

        #region CREATE USER
        [HttpPost("criar")]
        public async Task<ActionResult<RegisterViewModel>> CreateUser(RegisterViewModel register)
		{
			register.UserUniqueId = Guid.NewGuid();
			var map = _mapper.Map<RegisterViewModel>(register);
            map.Password = EncryptPassword(register.UserUniqueId, map.Password);

            var RegisterViewModel = await _userLoginRepository.CreateUserLoginAsync(map);
			if (RegisterViewModel.Item2.HasError)
			{
				return BadRequest(RegisterViewModel.Item2.Message);
			}

			return CreatedAtRoute("usuario", new { UserId = RegisterViewModel.Item1.UserId }, _mapper.Map<RegisterViewModel>(map));
		}
        #endregion

        #region CHANGE PASSWORD FOR LOGGED IN USER
        [HttpPut("trocarSenha")]
        public async Task<ActionResult<LoggedInPasswordViewModel>> PasswordChange(LoggedInPasswordViewModel password)
        {
            var userLogin = await _userLoginRepository.ReadByIdAsync(password.UserId);
            if (userLogin == null)
            {
                return NotFound();
            }
            if (!BCrypt.Net.BCrypt.Verify(userLogin.UserUniqueId.ToString() +
                                          password.CurrentPassword +
                                          userLogin.UserUniqueId.ToString(), userLogin.PasswordHash))
            {
                return Unauthorized("Senha incorreta.");
            }

            var map = _mapper.Map<LoggedInPasswordViewModel>(password);
            map.NewPassword = EncryptPassword(userLogin.UserUniqueId, map.NewPassword);
            var LoggedInPasswordViewModel = await _userLoginRepository.ChangeUserPassword(map);

            if (LoggedInPasswordViewModel.Item2.HasError)
            {
                return BadRequest(LoggedInPasswordViewModel.Item2.Message);
            }

            return Ok(LoggedInPasswordViewModel);
        }
        #endregion

        #region CHANGE PASSWORD FOR NOT LOGGED IN USER
        [HttpPut("trocarSenha/esqueceu")]
        public async Task<ActionResult<ForgotPasswordViewModel>> ForgotPasswordChange(ForgotPasswordViewModel password)
        {
            var userLogin = await _userLoginRepository.ReadByIdAsync(password.UserId);
            if (userLogin == null)
            {
                return NotFound();
            }
            if (BCrypt.Net.BCrypt.Verify(userLogin.UserUniqueId.ToString() +
                                          password.NewPassword +
                                          userLogin.UserUniqueId.ToString(), userLogin.PasswordHash))
            {
                return BadRequest("Senha nao pode ser a mesma!");
            }

            var map = _mapper.Map<ForgotPasswordViewModel>(password);
            map.NewPassword = EncryptPassword(userLogin.UserUniqueId, map.NewPassword);
            var ForgotPasswordViewModel = await _userLoginRepository.ChangeUserForgotPassword(map);

            if (ForgotPasswordViewModel.Item2.HasError)
            {
                return BadRequest(ForgotPasswordViewModel.Item2.Message);
            }

            return Ok(ForgotPasswordViewModel);
        }
        #endregion

        #region AUTHENTICATE USER
        [HttpPost("autenticar")]
		public async Task<ActionResult<UserLoginViewModel>> AuthUser(UserLoginViewModel login)
		{
			var map = _mapper.Map<UserLoginViewModel>(login);
			var userLogin = await _userLoginRepository.ReadUserLoginByUserName(map);
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
            string passwordHash =   userLogin.UserUniqueId.ToString() +
                                    login.Password +
                                    userLogin.UserUniqueId.ToString();
            //if (login.Password != userLogin.PasswordHash)
            if (!BCrypt.Net.BCrypt.Verify(passwordHash, userLogin.PasswordHash))
            {
				return Unauthorized("Senha incorreta.");
			}

			UserLoginViewModel toReturn = _mapper.Map<UserLoginViewModel>(userLogin);
			toReturn.UserToken = CreateToken(userLogin);

            return Ok(toReturn);
		}
        #endregion

        #region COLLECT USER ID AND UUID
        [HttpPost("leer")]
        public async Task<ActionResult<ForgotPasswordViewModel>> ReadIdUuid(ForgotPasswordViewModel forgotPassword)
        {
            var map = _mapper.Map<ForgotPasswordViewModel>(forgotPassword);
            var idUuid = await _userLoginRepository.ReadIdUuid(map);

            var toReturn = _mapper.Map<ForgotPasswordViewModel>(idUuid);

            return Ok(toReturn);
        }
        #endregion

        #region SEARCH FOR USER
        [HttpPost("buscar")]
        public async Task<ActionResult<UserLoginViewModel>> SearchUser(UserLoginViewModel login)
        {
            var map = _mapper.Map<UserLoginViewModel>(login);
            var userLogin = await _userLoginRepository.ReadUserLoginByUserName(map);
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

            return Ok();
        }
        #endregion

        #region GET USER BY ID
        [HttpGet("{UserId}")]
		public async Task<ActionResult<UserLoginViewModel>> GetById(int userId)
		{
            var userLogin = await _userLoginRepository.ReadByIdAsync(userId);
            if (userLogin == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserLoginViewModel>(userLogin));
        }
        #endregion

        #region CREATE TOKEN
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
        #endregion

        #region ENCRYPT
        private string EncryptPassword(Guid uuid, string password)
		{
            string encryption = BCrypt.Net.BCrypt.HashPassword
                (
                    uuid.ToString() +
                    password +
                    uuid.ToString()
                );

            return encryption;
        }
        #endregion
    }
}