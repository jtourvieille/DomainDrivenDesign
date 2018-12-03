using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class EntityTests
    {
        private class MyLongEntity : Entity<long>
        {
            public MyLongEntity(long id)
            {
                this.Id = id;
            }
        }

        private class MyGuidEntity : Entity<Guid>
        {
            public MyGuidEntity(Guid id)
            {
                this.Id = id;
            }
        }

        [TestMethod]
        public void Should_BeTheSame_When_IdAreEqualForLong()
        {
            // Arrange
            var myFirstEntity = new MyLongEntity(12);
            var mySecondEntity = new MyLongEntity(12);

            // Act
            var areEqual = myFirstEntity == mySecondEntity;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_NotBeTheSame_When_IdAreDifferentForLong()
        {
            // Arrange
            var myFirstEntity = new MyLongEntity(12);
            var mySecondEntity = new MyLongEntity(13);

            // Act
            var areEqual = myFirstEntity == mySecondEntity;

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_InstanceIsTheSameForLong()
        {
            // Arrange
            var myFirstEntity = new MyLongEntity(12);

            // Act
            var areEqual = myFirstEntity == myFirstEntity;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_IdAreEqualForGuid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var myFirstEntity = new MyGuidEntity(id);
            var mySecondEntity = new MyGuidEntity(id);

            // Act
            var areEqual = myFirstEntity == mySecondEntity;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_NotBeTheSame_When_IdAreDifferentForGuid()
        {
            // Arrange
            var myFirstEntity = new MyGuidEntity(Guid.NewGuid());
            var mySecondEntity = new MyGuidEntity(Guid.NewGuid());

            // Act
            var areEqual = myFirstEntity == mySecondEntity;

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_InstanceIsTheSameForGuid()
        {
            // Arrange
            var myFirstEntity = new MyGuidEntity(Guid.NewGuid());

            // Act
            var areEqual = myFirstEntity == myFirstEntity;

            // Assert
            Assert.IsTrue(areEqual);
        }
    }
}
