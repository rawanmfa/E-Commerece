using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IAuthenticationService = Services.Abstractions.IAuthenticationService;

namespace Services
{
	public class AuthenticationService(UserManager<User> userManager, IConfiguration configuration, IOptions<JwtOptions> options) : IAuthenticationService
	{
		public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
		{
			var user = await userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
			{
				throw new UnAuthorisedException("email doesn't exist");
			}
			var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
			if (!result) throw new UnAuthorisedException("password is incorrect");
			return new UserResultDto
				(
				user.DisplayName,
				user.Email,
				await CreateTokenAsync(user)
				);
		}

		public async Task<UserResultDto> RegisterAsync(UserRegisterDto registerDto)
		{
			var user = new User()
			{
				Email = registerDto.Email,
				DisplayName = registerDto.DisplayName,
				PhoneNumber = registerDto.PhoneNumber,
				UserName = registerDto.UserName,
			};
			var result = await userManager.CreateAsync(user, registerDto.Password);
			if (result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new ValidationException(errors);
			}
			return new UserResultDto(user.DisplayName, user.Email, await CreateTokenAsync(user));
		}

		private async Task<string> CreateTokenAsync(User user)
		{
			var JwtOptions = options.Value;
			var authenticationClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Name,user.UserName!),
				new Claim(ClaimTypes.Email,user.Email!)
			};
			var roles = await userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				authenticationClaims.Add(new Claim(ClaimTypes.Role, role));
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
			var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var Token = new JwtSecurityToken(
				audience: JwtOptions.audience,
				issuer: JwtOptions.issuer,
				expires: DateTime.UtcNow.AddDays(JwtOptions.DurationINDays),
				claims: authenticationClaims,
				signingCredentials: signingCreds
				);
			return new JwtSecurityTokenHandler().WriteToken(Token);
		}
	}
}
