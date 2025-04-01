﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Product:BaseEntity<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }
		#region Product Brand
		public int ProductBrandId { get; set; }
		public ProductBrand ProductBrand { get; set; }
		#endregion
		#region Product Type
		public int ProductTypeId { get; set; }
		public ProductType ProductType { get; set; }
		#endregion

	}
}
