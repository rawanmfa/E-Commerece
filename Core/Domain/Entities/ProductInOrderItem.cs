﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ProductInOrderItem
	{
		public ProductInOrderItem() { }
		public ProductInOrderItem(int _ProductId,string _ProductName, string _PictureUrl) 
		{
			ProductId = _ProductId;
			ProductName = _ProductName;
			PictureUrl = _PictureUrl;
		}
		public int ProductId {  get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
	}
}
