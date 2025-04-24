using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface ICashService
	{
		public Task<string> GetCashValueAsync(string cashKey);
		public Task SetCashValueAsync(string cashKey, object value, TimeSpan duration);
	}
}
