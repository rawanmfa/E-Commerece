﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
	public interface ICashRepository
	{
		public Task SetAsync (string key , object value , TimeSpan duration);
		public Task<string?> GetAsync (string key);
	}
}
