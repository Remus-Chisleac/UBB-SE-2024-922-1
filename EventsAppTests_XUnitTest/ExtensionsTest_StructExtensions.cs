using Xunit;
using EventsApp.Logic.Attributes;
using EventsApp.Logic.Extensions;
using System;
using System.Collections.Generic;
using EventsApp.Logic.Entities;

namespace EventsAppTests_XUnitTest.StructExtensions
{
    public class ExtensionsTest_StructExtensions
    {

        [Table("TestTable")]
        [System.Serializable]
        public struct TestStruct
        {
            [PrimaryKey]
            public Guid Id { get; set; }
            public string Name { get; set; }

            public TestStruct(Guid id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public TestStruct()
            {
                this.Id = Guid.NewGuid();
                this.Name = string.Empty;
            }
        }

        [Fact]
        public void GetIdentifier_NoDBConnection_ReturnsCorrectIdentifier()
        {
            // Arrange
            UserInfo testObj = new UserInfo {};
            Identifier Expected = new Identifier(new Dictionary<string, object> { {"GUID", testObj.GUID }});

            // Act
            Identifier Actual = testObj.GetIdentifier();

            // Assert
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void GetTableName_NoDBConnection_ReturnsNull()
        {
            // Arrange
            TestStruct testObj = new TestStruct();
            string Expected = "TestTable";

            // Act
            string Actual = testObj.GetTableName();

            // Assert
            Assert.Equal(Expected, Actual);

        }
    }
}
