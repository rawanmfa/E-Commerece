using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class ProductService(IUnitOfWork unitOfWork ,IMapper mapper) : IProductService
	{
		public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
		{
			var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			var brandsResult = mapper.Map<IEnumerable<BrandResultDTO>>(brands);
			return brandsResult;
		}

		public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters)
		{
			var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationAsync(new ProductWithBrandAndTypeSpecifications( parameters));
			var productsResult = mapper.Map<IEnumerable<ProductResultDTO>>(products);
			var count = productsResult.Count();
			var totalCount = await unitOfWork.GetRepository<Product,int>()
				.CountAsync(new ProductCountSpecifications( parameters));
			var result = new PaginatedResult<ProductResultDTO>(parameters.PageIndex,count,count,productsResult);
			return result;
		}

		public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
		{
			var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
			var TypesResult = mapper.Map<IEnumerable<TypeResultDTO>>(types);
			return TypesResult;
		}

		public async Task<ProductResultDTO?> GetProductByIdAsync(int id)
		{
			var product = await unitOfWork.GetRepository<Product, int>().GetByIdWithSpecificationAsync(new ProductWithBrandAndTypeSpecifications(id));
			return product is null? throw new ProductNotFoundException(id):
				mapper.Map<ProductResultDTO>(product);
		}
	}
}
