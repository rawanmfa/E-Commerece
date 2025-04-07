using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
	public class ProductCountSpecifications : Specifications<Product>
	{
		public ProductCountSpecifications(ProductSpecificationParameters parameters) : base(product => (!parameters.BrandId.HasValue || product.ProductBrandId == parameters.BrandId)
		&& (!parameters.TypeId.HasValue || product.ProductTypeId == parameters.TypeId) && (string.IsNullOrWhiteSpace(parameters.Search)) || product.Name.Contains(parameters.Search.ToLower().Trim()))
		{ }
	}
}
