using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerece.Factories
{
	public class ApiResponseFactory
	{
		public static IActionResult customValidationErrorResponse(ActionContext context)
		{
			var error = context.ModelState.Where(error => error.Value.Errors.Any()).Select(error => new ValidationError
			{
				Filed = error.Key,
				Errors = error.Value.Errors.Select(e => e.ErrorMessage)
			});
			var response = new ValidationErrorResponce
			{
				StatusCode = (int)HttpStatusCode.BadRequest,
				ErrorMessage = "Validation Failed",
				Errors = error
			};
			return new BadRequestObjectResult(response);
		}
	}
}
