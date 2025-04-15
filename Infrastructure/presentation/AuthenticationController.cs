using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation
{
	public class AuthenticationController(IServicesManger servicesManger): ApiController
	{
		#region login
		[HttpPost("Login")]
		public async Task<ActionResult<UserRegisterDto>> Login(LoginDto loginDto)
		{
			var result = await servicesManger.AuthenticationService.LoginAsync(loginDto);
			return Ok(result);
		}
		#endregion
		#region register
		[HttpPost("Register")]
		public async Task<ActionResult<UserRegisterDto>> Register(UserRegisterDto userRegisterDto)
		{
			var result = await servicesManger.AuthenticationService.RegisterAsync(userRegisterDto);
			return Ok(result);
		}
		#endregion
	}
}
