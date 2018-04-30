using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class ValueObjectTests
    {
        private abstract class MyValueObject : ValueObject<MyValueObject>
        {
            public string MyStringProperty { get; set; }
            public short MyShortProperty { get; set; }
            public long MyLongProperty { get; set; }
            public int MyIntProperty { get; set; }
            public float MyFloatProperty { get; set; }
            public double MyDoubleProperty { get; set; }
            public bool MyBoolProperty { get; set; }
            public char MyCharProperty { get; set; }
            public byte MyByteProperty { get; set; }
        }

        private class MyStringValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyStringProperty;
            }
        }

        private class MyShortValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyShortProperty;
            }
        }

        private class MyLongValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyLongProperty;
            }
        }

        private class MyIntValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyIntProperty;
            }
        }

        private class MyFloatValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyFloatProperty;
            }
        }

        private class MyDoubleValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyDoubleProperty;
            }
        }

        private class MyBoolValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyBoolProperty;
            }
        }

        private class MyCharValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyCharProperty;
            }
        }

        private class MyByteValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyByteProperty;
            }
        }

        private class MyMixedValueObject : MyValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return MyByteProperty;
                yield return MyBoolProperty;
                yield return MyStringProperty;
                yield return MyCharProperty;
                yield return MyDoubleProperty;
                yield return MyFloatProperty;
                yield return MyIntProperty;
                yield return MyLongProperty;
                yield return MyShortProperty;
            }
        }

        [TestMethod]
        public void Should_BeTheSame_When_StringPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyStringValueObject { MyStringProperty = "10" };
            var mySecondValueObject = new MyStringValueObject { MyStringProperty = "10" };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_ShortPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyShortValueObject { MyShortProperty = 10 };
            var mySecondValueObject = new MyShortValueObject { MyShortProperty = 10 };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_LongPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyLongValueObject { MyLongProperty = 10 };
            var mySecondValueObject = new MyLongValueObject { MyLongProperty = 10 };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_IntPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyIntValueObject { MyIntProperty = 10 };
            var mySecondValueObject = new MyIntValueObject { MyIntProperty = 10 };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_FloatPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyFloatValueObject { MyFloatProperty = 10 };
            var mySecondValueObject = new MyFloatValueObject { MyFloatProperty = 10 };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_DoublePropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyDoubleValueObject { MyDoubleProperty = 10 };
            var mySecondValueObject = new MyDoubleValueObject { MyDoubleProperty = 10 };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_BoolPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyBoolValueObject { MyBoolProperty = true };
            var mySecondValueObject = new MyBoolValueObject { MyBoolProperty = true };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_CharPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyCharValueObject { MyCharProperty = 'd' };
            var mySecondValueObject = new MyCharValueObject { MyCharProperty = 'd' };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_BytePropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyByteValueObject { MyByteProperty = 10 };
            var mySecondValueObject = new MyByteValueObject { MyByteProperty = 10 };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_BeTheSame_When_MixedPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyMixedValueObject
            {
                MyStringProperty = "10",
                MyBoolProperty = true,
                MyByteProperty = 10,
                MyCharProperty = 'd',
                MyDoubleProperty = 10,
                MyFloatProperty = 10,
                MyIntProperty = 10,
                MyLongProperty = 10,
                MyShortProperty = 10
            };
            var mySecondValueObject = new MyMixedValueObject
            {
                MyStringProperty = "10",
                MyBoolProperty = true,
                MyByteProperty = 10,
                MyCharProperty = 'd',
                MyDoubleProperty = 10,
                MyFloatProperty = 10,
                MyIntProperty = 10,
                MyLongProperty = 10,
                MyShortProperty = 10
            };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Should_NotBeTheSame_When_OneOfMixedPropertiesAreNotEqual()
        {
            // Arrange
            var myFirstValueObject = new MyMixedValueObject
            {
                MyStringProperty = "10",
                MyBoolProperty = false,
                MyByteProperty = 10,
                MyCharProperty = 'd',
                MyDoubleProperty = 10,
                MyFloatProperty = 10,
                MyIntProperty = 10,
                MyLongProperty = 10,
                MyShortProperty = 10
            };
            var mySecondValueObject = new MyMixedValueObject
            {
                MyStringProperty = "10",
                MyBoolProperty = true,
                MyByteProperty = 10,
                MyCharProperty = 'd',
                MyDoubleProperty = 10,
                MyFloatProperty = 10,
                MyIntProperty = 10,
                MyLongProperty = 10,
                MyShortProperty = 10
            };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Should_NotBeTheSame_When_NoMixedPropertiesAreEqual()
        {
            // Arrange
            var myFirstValueObject = new MyMixedValueObject
            {
                MyStringProperty = "09",
                MyBoolProperty = false,
                MyByteProperty = 9,
                MyCharProperty = 'e',
                MyDoubleProperty = 9,
                MyFloatProperty = 9,
                MyIntProperty = 9,
                MyLongProperty = 9,
                MyShortProperty = 9
            };
            var mySecondValueObject = new MyMixedValueObject
            {
                MyStringProperty = "10",
                MyBoolProperty = true,
                MyByteProperty = 10,
                MyCharProperty = 'd',
                MyDoubleProperty = 10,
                MyFloatProperty = 10,
                MyIntProperty = 10,
                MyLongProperty = 10,
                MyShortProperty = 10
            };

            // Act
            var areEqual = myFirstValueObject == mySecondValueObject;

            // Assert
            Assert.IsFalse(areEqual);
        }
    }
}
