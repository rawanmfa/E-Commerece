using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class CashService(ICashRepository cashRepository) : ICashService
	{
		public async Task<string> GetCashValueAsync(string cashKey)
		=> await cashRepository.GetAsync(cashKey);

		public Task SetCashValueAsync(string cashKey, object value, TimeSpan duration)
		=> cashRepository.SetAsync(cashKey, value, duration);
	}
}
