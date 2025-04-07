using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
	public class SpecificationEvaluator
	{
		public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, Specifications<T> specifications) where T : class
		{
			var query = inputQuery;
            if (specifications.Criteria is not null) query = query.Where(specifications.Criteria);
			// another way for the code below it  
			//foreach (var item in specifications.IncludeExpression)
			//{
			//    query = query.Include(item);
			//}
			query = specifications.IncludeExpression.Aggregate(query, (CurrentQuery, IncludeExpresion) => CurrentQuery.Include(IncludeExpresion));
			if (specifications.OrderBy is not null)
				query = query.OrderBy(specifications.OrderBy);
			else if (specifications.OrderByDescending is not null)
				query = query.OrderByDescending(specifications.OrderByDescending);

			return query;
		}
	}
}
