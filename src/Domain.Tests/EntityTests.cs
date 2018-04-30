using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class EntityTests
    {
        private class MyEntity : Entity
        {
            public MyEntity(long id)
            {
                this.Id = id;
            }
        }

        [TestMethod]
        public void Should_BeTheSame_When_IdAreEqual()
        {
            // Arrange
            var myFirstEntity = new MyEntity(12);
            var mySecondEntity = new MyEntity(12);

            // Act
            var areEqual = myFirstEntity == mySecondEntity;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_NotBeTheSame_When_IdAreDifferent()
        {
            // Arrange
            var myFirstEntity = new MyEntity(12);
            var mySecondEntity = new MyEntity(13);

            // Act
            var areEqual = myFirstEntity == mySecondEntity;

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_InstanceIsTheSame()
        {
            // Arrange
            var myFirstEntity = new MyEntity(12);

            // Act
            var areEqual = myFirstEntity == myFirstEntity;

            // Assert
            Assert.IsTrue(areEqual);
        }
    }
}
