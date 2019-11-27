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
        public IQueryable<Product> Products => context.Products;
            //.Include(rw => rw.Reviews);


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
                }
            }
            context.SaveChanges();
        }
    }
}
