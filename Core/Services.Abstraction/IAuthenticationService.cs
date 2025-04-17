﻿using Shared;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IAuthenticationService
	{
		public Task<UserResultDto> LoginAsync(LoginDto loginDto);
		public Task<UserResultDto> RegisterAsync(UserRegisterDto registerDto);

	}
}
