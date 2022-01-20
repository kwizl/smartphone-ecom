using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Queries all Products
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        // Queries Product by ID
        public async Task<IEnumerable<Product>> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.ID == id).ToListAsync();
        }

        // Queries Product by category
        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context.Products.Find(filter).ToListAsync();
        }

        // Queries Product by name
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context.Products.Find(filter).ToListAsync();
        }

        // Creates Product
        public async Task CreateProducts(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        // Updates Product
        public async Task<bool> UpdateProduct(Product product)
        {
            // Replaces Product with the same ID with the new Product
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.ID == product.ID, replacement: product);

            if (updateResult.IsAcknowledged == false)
            {
                throw new Exception("Product update failed!");
            }
            else
            {
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
        }

        // Deletes Product
        public async Task<bool> DeleteProducts(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.ID, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            if (deleteResult.IsAcknowledged == false)
            {
                throw new Exception("Product delete failed!");
            }
            else
            {
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
        }
    }
}
