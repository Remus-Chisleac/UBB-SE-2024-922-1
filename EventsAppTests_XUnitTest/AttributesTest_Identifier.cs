using EventsApp.Logic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest.Attributes
{
    public class AttributesTest_Identifier
    {
        [Fact]
        public void Identifier_Constructor()
        {
            Dictionary<string, object> primaryKeys = new Dictionary<string, object> { { "Id", 0 } };

            Identifier identifier = new Identifier(primaryKeys);

            Assert.Equal(primaryKeys, identifier.PrimaryKeys);
        }

        [Fact]
        public void Identifier_Equals_ReturnsTrue()
        {
            Dictionary<string, object> primaryKeys = new Dictionary<string, object> { { "Id", 0 } };
            Identifier identifier = new Identifier(primaryKeys);
            Identifier identifier2 = new Identifier(primaryKeys);

            Assert.True(identifier.Equals(identifier2));
        }

        [Fact]
        public void Identifier_Equals_ReturnsFalse()
        {
            Dictionary<string, object> primaryKeys = new Dictionary<string, object> { { "Id", 0 } };
            Dictionary<string, object> primaryKeys2 = new Dictionary<string, object> { { "Id", 1 } };
            Identifier identifier = new Identifier(primaryKeys);
            Identifier identifier2 = new Identifier(primaryKeys2);

            Assert.False(identifier.Equals(identifier2));
        }

        [Fact]
        public void Identifier_Equals_ReturnsFalse2()
        {
            Dictionary<string, object> primaryKeys = new Dictionary<string, object> { { "Id", 0 } };
            Identifier identifier = new Identifier(primaryKeys);

            Assert.False(identifier.Equals(null));
        }

        [Fact]
        public void Identifier_GetHashCode_ThrowsNotImplementedException()
        {
            Dictionary<string, object> primaryKeys = new Dictionary<string, object> { { "Id", 0 } };
            Identifier identifier = new Identifier(primaryKeys);

            Assert.Throws<NotImplementedException>(() => identifier.GetHashCode());
        }
    }
}
