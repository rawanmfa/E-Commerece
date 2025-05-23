﻿using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
	public class GenericRepository <TEntity, TKey> : IGenericRepository <TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		private readonly StoreContext _storeContext;

		public GenericRepository(StoreContext storeContext)
		{
			_storeContext = storeContext;
		}

		public async Task<TEntity> GetAsync(TKey id)
		{
			return await _storeContext.Set<TEntity>().FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
		{
            return trackChanges ? await _storeContext.Set<TEntity>().ToListAsync() 
				: await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();
		}

		public async Task AddAsync(TEntity entity)
		{
			await _storeContext.Set<TEntity>().AddAsync(entity);
		}

		public void Delete(TEntity entity)
		{
			_storeContext.Set<TEntity>().Remove(entity);
		}

		public void Update(TEntity entity)
		{
			_storeContext.Set<TEntity>().Update(entity);
		}
		#region For Specification
		public async Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(Specifications<TEntity> specifications)
		=> await ApplySpecification(specifications).ToListAsync();
		
		public async Task<TEntity?> GetByIdWithSpecificationAsync(Specifications<TEntity> specifications)
		=> await ApplySpecification(specifications).FirstOrDefaultAsync();

		private IQueryable<TEntity> ApplySpecification(Specifications<TEntity> specifications)
		=> SpecificationEvaluator.GetQuery<TEntity>(_storeContext.Set<TEntity>(), specifications);

		#endregion
		public async Task<int> CountAsync(Specifications<TEntity?> specifications)
        => await SpecificationEvaluator.GetQuery(_storeContext.Set<TEntity>(), specifications).CountAsync();

	}
}
