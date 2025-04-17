using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerece.MiddleWare
{
	public class GlobalErrorHandlingMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;

		public GlobalErrorHandlingMiddleWare(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleWare> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
				if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
					await HandleNotFoundPointAsync(httpContext);
			}
			catch (Exception e)
			{
				_logger.LogError($"something went wrong {e}");
				await HandleExceptionAsync(httpContext, e);
			}
		}
		private async Task HandleNotFoundPointAsync(HttpContext httpContext)
		{
			httpContext.Response.ContentType = "application/json";
			var response = new ErrorDetails
			{
				StatusCode = (int)HttpStatusCode.NotFound,
				ErrorMessage = $"The End Point {httpContext.Request.Path}"
			}.ToString();
			await httpContext.Response.WriteAsync(response);
		}
		public async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
		{
			httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			httpContext.Response.ContentType = "application/json";
			var response = new ErrorDetails
			{
				ErrorMessage = exception.Message,
			};
			httpContext.Response.StatusCode = exception switch
			{
				NotFoundException => (int)HttpStatusCode.NotFound,
				UnAuthorisedException => (int)HttpStatusCode.Unauthorized,
				ValidationException validationException => HandleValidationExcepthion(validationException, response),
				_ => (int)HttpStatusCode.InternalServerError
			};
			response.StatusCode = httpContext.Response.StatusCode;
			//var response = new ErrorDetails
			//{
			//	StatusCode = httpContext.Response.StatusCode,
			//	ErrorMessage = exception.Message
			//}.ToString();
			await httpContext.Response.WriteAsync(response.ToString());
		}
		private int HandleValidationExcepthion(ValidationException validationException, ErrorDetails response)
		{
			response.Errors = validationException.Errors;
			return (int)HttpStatusCode.BadRequest;
		}
	}
}