using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
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

		public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync()
		{
			var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();
			var productsResult = mapper.Map<IEnumerable<ProductResultDTO>>(products);
			return productsResult;
		}

		public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
		{
			var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
			var TypesResult = mapper.Map<IEnumerable<TypeResultDTO>>(types);
			return TypesResult;
		}

		public async Task<ProductResultDTO?> GetProductByIdAsync(int id)
		{
			var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
			var productResult = mapper.Map<ProductResultDTO>(product);
			return productResult;
		}
	}
}
