using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thinh.Data;

namespace Thinh.Models
{
	public class EFProductRepository : IProductRepository
	{
		private ProductDbContext context;

		public EFProductRepository(ProductDbContext ctx)
		{
			context = ctx;
		}
		public IQueryable<Product> Products(int approve) => context.Products.Where(x => x.IsApproved == approve);


		public Product DeleteProduct(int productId)
		{
			Product dbEntry = context.Products
				.FirstOrDefault(r => r.productId == productId);
			if (dbEntry != null)
			{
				context.Products.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}

		public Product GetProduct(int id)
		{
			return context.Products.Find(id);
		}

		public void SaveProduct(Product product)
		{
			if (product.productId == 0)
			{
				context.Products.Add(product);
			}
			else
			{
				Product dbEntry = context.Products
				.FirstOrDefault(r => r.productId == product.productId);
				if (dbEntry != null)
				{
					dbEntry.productName = product.productName;
					dbEntry.productUrl = product.productUrl;
					dbEntry.productCategory = product.productCategory;
					dbEntry.productDescription = product.productDescription;
					dbEntry.productImgUrl = product.productImgUrl;
					dbEntry.Price = product.Price;
					dbEntry.IsApproved = product.IsApproved;
					dbEntry.DateAdded = product.DateAdded;
				}
			}
			context.SaveChanges();
		}


		public IQueryable<Product> Search(string search) => context.Products.Where(x => x.productName.Contains(search) && x.IsApproved == 1);
		public IQueryable<Product> Category(string cat) => context.Products.Where(x => x.productCategory.Contains(cat) && x.IsApproved == 1);

		public IQueryable<Feedbacks> FeedbackList()
		{
			return context.Feedbacks.OrderByDescending(x => x.FeedbackId);
		}

		public void AddFeedback(Feedbacks fb)
		{
			context.Feedbacks.Add(fb);
			context.SaveChanges();
		}
	}
}
