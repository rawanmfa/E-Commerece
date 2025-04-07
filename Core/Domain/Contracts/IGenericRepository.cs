using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
	public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
	{
		Task<TEntity> GetAsync(TKey id);
		Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
		#region For Specification
		Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(Specifications<TEntity> specifications);
		Task<TEntity?> GetByIdWithSpecificationAsync(Specifications<TEntity> specifications);
		#endregion
		Task AddAsync(TEntity entity);
		void Delete(TEntity entity);
		void Update(TEntity entity);
		Task<int> CountAsync(Specifications<TEntity?> specifications);
	}
}
