using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thinh.Models
{
    public interface IProductRepository
    {
		IQueryable<Product> Products(int approve);
		Product GetProduct(int id);
		void SaveProduct(Product product);
		Product DeleteProduct(int productId);
		IQueryable<Product> Search(string search);
		IQueryable<Product> Category(string cat);

		IQueryable<Feedbacks> FeedbackList();
		void AddFeedback(Feedbacks fb);
	}
}
