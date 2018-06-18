using Infrastructure.Dapper.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.Tests.Repository
{
    [TestClass]
    public class DapperAsyncRepositoryShould
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class ProductAsyncRepository : DapperAsyncRepository<Product>
        {
            public ProductAsyncRepository(IUnitOfWork unitOfWork)
                : base(unitOfWork)
            {
            }

            protected override ParametrizedSqlQuery SaveCommand(Product aggregateRoot)
            {
                return new ParametrizedSqlQuery(
                    "INSERT INTO Product(id, Name, Description) VALUES (@Id, @Name, @Description)",
                    new { aggregateRoot.Id, aggregateRoot.Name, aggregateRoot.Description });
            }

            protected override ParametrizedSqlQuery UpdateCommand(Product aggregateRoot)
            {
                return new ParametrizedSqlQuery(
                    "UPDATE Product SET Name = @Name, Description = @Description WHERE Id = @Id ",
                    new { aggregateRoot.Id, aggregateRoot.Name, aggregateRoot.Description });
            }
        }

        [TestMethod]
        public async Task SaveProduct_When_NewProductInserted()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id=1, Name = "Product1", Description = "This is the first product" },
                new Product { Id=2, Name = "Product2", Description = "This is the second product" }
            };
            var db = new InMemoryDatabase();
            db.Insert(products);

            using (var connection = db.OpenConnection())
            {
                var unitOfWork = new UnitOfWork.UnitOfWork(connection);
                ProductAsyncRepository productAsyncRepository = new ProductAsyncRepository(unitOfWork);
                var product = new Product { Id = 3, Name = "Product3", Description = "This is the third product" };

                // Act
                await productAsyncRepository.SaveAsync(product);

                // Assert
                var allProducts = db.GetAll<Product>();
                Assert.AreEqual(3, allProducts.Count());

                var thirdProduct = allProducts.ToList()[2];
                Assert.IsNotNull(thirdProduct);

                Assert.AreEqual(3, thirdProduct.Id);
                Assert.AreEqual("Product3", thirdProduct.Name);
                Assert.AreEqual("This is the third product", thirdProduct.Description);
            }
        }

        [TestMethod]
        public async Task UpdateProduct_When_ProductIsModified()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id=1, Name = "Product1", Description = "This is the first product" },
                new Product { Id=2, Name = "Product2", Description = "This is the second product" }
            };
            var db = new InMemoryDatabase();
            db.Insert(products);

            using (var connection = db.OpenConnection())
            {
                var unitOfWork = new UnitOfWork.UnitOfWork(connection);
                ProductAsyncRepository productAsyncRepository = new ProductAsyncRepository(unitOfWork);
                var product = new Product { Id = 1, Name = "Product1 Updated", Description = "This is the first updated product" };

                // Act
                await productAsyncRepository.UpdateAsync(product);

                // Assert
                var allProducts = db.GetAll<Product>();
                Assert.AreEqual(2, allProducts.Count());

                var firstProduct = allProducts.ToList()[0];
                Assert.IsNotNull(firstProduct);

                Assert.AreEqual(1, firstProduct.Id);
                Assert.AreEqual("Product1 Updated", firstProduct.Name);
                Assert.AreEqual("This is the first updated product", firstProduct.Description);
            }
        }
    }
}
