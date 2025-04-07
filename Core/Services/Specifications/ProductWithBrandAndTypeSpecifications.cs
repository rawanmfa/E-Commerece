using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.ProductSpecificationParameters;

namespace Services.Specifications
{
	public class ProductWithBrandAndTypeSpecifications: Specifications<Product>
	{
		public ProductWithBrandAndTypeSpecifications(int id):base(product=>product.Id == id) 
		{
			AddInclude(product => product.ProductBrand);
			AddInclude(product => product.ProductType);
		}
		public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters) :base(product=>(!parameters.BrandId.HasValue || product.ProductBrandId == parameters.BrandId)
		&&(!parameters.TypeId.HasValue || product.ProductTypeId == parameters.TypeId)&& (string.IsNullOrWhiteSpace(parameters.Search)) || product.Name.Contains(parameters.Search.ToLower().Trim()))
		{
			AddInclude(product => product.ProductBrand);
			AddInclude(product => product.ProductType);
			#region Sort
			ApplyPagination(parameters.PageIndex, parameters.PageSize);
			switch (parameters.Sort)
			{
				case ProductSortingOptions.PriceDesc:
					SetOrderByDescending(p => p.Price);
					break;
				case ProductSortingOptions.PriceAsc:
					SetOrderBy(p => p.Price);
					break;
				case ProductSortingOptions.NameDesc:
					SetOrderByDescending(p => p.Name);
					break;
				case ProductSortingOptions.NameAsc:
					SetOrderBy(p => p.Name);
					break;
			}
			#endregion

		}
	}
}
