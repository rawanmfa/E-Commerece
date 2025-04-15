using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	public record UserRegisterDto
	{
		[Required(ErrorMessage ="Display Name is required !!!")]
		public string DisplayName { get; init; }
		[EmailAddress]
		public string Email { get; init; }
		[Required(ErrorMessage = "Password is required !!!")]
		public string Password { get; init; }
		[Required(ErrorMessage = "User Nmae is required !!!")]
		public string UserName { get; init; }
		public string PhoneNumber { get; init; }
	}
}
