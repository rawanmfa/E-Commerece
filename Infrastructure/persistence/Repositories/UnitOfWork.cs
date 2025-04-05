﻿using Domain.Contracts;
using Domain.Entities;
using persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _storeContext;
		private readonly ConcurrentDictionary<string, object> _repositories;

		public UnitOfWork(StoreContext storeContext)
		{
			_storeContext = storeContext;
			_repositories = new(); 
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _storeContext.SaveChangesAsync();
		}

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		=> (IGenericRepository<TEntity , TKey>)
			_repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity , TKey>(_storeContext));
	}
}
