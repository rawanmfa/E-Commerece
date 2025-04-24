using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace presentation
{
	public class RedisCashAttribute(int durationInsec):ActionFilterAttribute
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cashService = context.HttpContext.RequestServices
			   .GetRequiredService<IServicesManger>().CashService;
			string cashKey = GenerateCashKey(context.HttpContext.Request);
			var result = await cashService.GetCashValueAsync(cashKey);
			if (result != null)
			{
				context.Result = new ContentResult
				{
					Content = result,
					ContentType = "application/json",
					StatusCode = (int)HttpStatusCode.OK,
				};
				return;
			}
			var contextresult = await next.Invoke();
			if (contextresult.Result is OkObjectResult okObject)
			{
				await cashService.SetCashValueAsync(cashKey, okObject, TimeSpan.FromSeconds(durationInsec));
			}

		}
		private string GenerateCashKey(HttpRequest request)
		{
			var keybuilder = new StringBuilder();
			keybuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q=>q.Key))
            {
				keybuilder.Append($"{item.Key}--{item.Value}");
            }
			return keybuilder.ToString();
        }
	}
}
