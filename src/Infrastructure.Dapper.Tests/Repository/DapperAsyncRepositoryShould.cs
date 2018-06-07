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

            protected override ParametrizedQuery GetAllCommand()
            {
                return new ParametrizedQuery("SELECT * FROM Product");
            }

            protected override ParametrizedQuery GetByIdCommand(long id)
            {
                return new ParametrizedQuery(
                    "SELECT * FROM Product WHERE Id = @Id",
                    new { Id = id });
            }

            protected override ParametrizedQuery SaveCommand(Product aggregateRoot)
            {
                return new ParametrizedQuery(
                    "INSERT INTO Product(id, Name, Description) VALUES (@Id, @Name, @Description)",
                    new { aggregateRoot.Id, aggregateRoot.Name, aggregateRoot.Description });
            }
        }

        [TestMethod]
        public async Task RetrieveProduct_When_ClaimingProduct()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id=1,Name="Product1", Description="This is the first product" },
                new Product { Id=2,Name="Product2", Description="This is the second product" }
            };
            var db = new InMemoryDatabase();
            db.Insert(products);

            using (var connection = db.OpenConnection())
            {
                var unitOfWork = new UnitOfWork.UnitOfWork(connection);
                ProductAsyncRepository productAsyncRepository = new ProductAsyncRepository(unitOfWork);

                // Act
                var result = await productAsyncRepository.GetByIdAsync(1);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("Product1", result.Name);
                Assert.AreEqual("This is the first product", result.Description);
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
        public async Task ListAllProducts_When_ClaimingAllProducts()
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

                // Act
                var allProducts = await productAsyncRepository.GetAllAsync();

                // Assert
                Assert.AreEqual(2, allProducts.Count());
            }
        }
    }
}
