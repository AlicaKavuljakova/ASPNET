using Dapper;
using System.Collections.Generic;
using System.Data;
using Testing.Models;

namespace Testing
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public Product AssignCategory()
        {
           var categoryList=GetCategories();
            Product product = new Product();
            product.Categories=categoryList;
            return product;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _conn.Query<Category>("Select * from categories");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("Select * from products");
        }

        public Product GetProduct(int id)
        {
           return _conn.QuerySingle<Product>("Select * from products where ProductID=@id",new {id=id});
        }

        public void InsertProduct(Product productToInsert)
        {
            _conn.Execute("Insert into products (name,price,categoryID) values (@name,@price,@categoryid)",
            new { name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID });
        }

        public void UpdateProduct(Product product)
        {
             _conn.Execute("Update products set Name=@name,Price=@price,CategoryID=@categoryId where ProductID=@id", new { name = product.Name, price = product.Price, categoryid = product.CategoryID , id=product.ProductID});
        }
    }
}
