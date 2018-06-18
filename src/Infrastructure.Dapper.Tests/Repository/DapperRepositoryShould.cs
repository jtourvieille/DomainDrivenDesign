using Infrastructure.Dapper.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Dapper.Tests.Repository
{
    [TestClass]
    public class DapperRepositoryShould
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class ProductRepository : DapperRepository<Product>
        {
            public ProductRepository(IUnitOfWork unitOfWork)
                : base (unitOfWork)
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
        public void SaveProduct_When_NewProductInserted()
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
                ProductRepository productRepository = new ProductRepository(unitOfWork);
                var product = new Product { Id = 3, Name="Product3", Description = "This is the third product" };

                // Act
                productRepository.Save(product);

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
        public void UpdateProduct_When_ProductIsModified()
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
                ProductRepository productRepository = new ProductRepository(unitOfWork);
                var product = new Product { Id = 1, Name = "Product1 Updated", Description = "This is the first updated product" };

                // Act
                productRepository.Update(product);

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
