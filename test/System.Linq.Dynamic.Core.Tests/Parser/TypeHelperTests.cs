using System.Collections;
using NFluent;
using System.Linq.Dynamic.Core.Parser;
using FluentAssertions;
using Xunit;

namespace System.Linq.Dynamic.Core.Tests.Parser
{
    public class TypeHelperTests
    {
        enum TestEnum
        {
            x = 1
        }

        [Fact]
        public void TypeHelper_ParseEnum_Valid()
        {
            // Assign + Act
            var result = TypeHelper.ParseEnum("x", typeof(TestEnum));

            // Assert
            Check.That(result).Equals(TestEnum.x);
        }

        [Fact]
        public void TypeHelper_ParseEnum_Invalid()
        {
            // Assign + Act
            var result = TypeHelper.ParseEnum("test", typeof(TestEnum));

            // Assert
            Check.That(result).IsNull();
        }

        [Fact]
        public void TypeHelper_IsCompatibleWith_SameTypes_True()
        {
            // Assign + Act
            var result = TypeHelper.IsCompatibleWith(typeof(int), typeof(int));

            // Assert
            Check.That(result).IsTrue();
        }

        [Fact]
        public void TypeHelper_IsCompatibleWith_True()
        {
            // Assign + Act
            var result = TypeHelper.IsCompatibleWith(typeof(int), typeof(long));

            // Assert
            Check.That(result).IsTrue();
        }

        [Fact]
        public void TypeHelper_IsCompatibleWith_False()
        {
            // Assign + Act
            var result = TypeHelper.IsCompatibleWith(typeof(long), typeof(int));

            // Assert
            Check.That(result).IsFalse();
        }

        [Theory]
        [InlineData(typeof(IEnumerable), typeof(ICollection), true)]
        public void TypeHelper_IsCompatibleWith_Collections(Type source, Type target, bool isCompatible)
        {
            // Assign + Act
            var result = TypeHelper.IsCompatibleWith(source, target);

            // Assert
            result.Should().Be(isCompatible);
        }
    }
}
