using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest.Managers
{
    using EventsApp.Logic.Managers;
    public class ManagersTest_Dummy
    {
        [Fact]
        public void Populate_AddsCorrectAmountOfUsers()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);

            // Act
            Dummy.Populate();

            // Assert
            Assert.Equal(11, UsersManager.GetAllUsers().Count);

            // Clean up
            ManagersInitializer.Initialize(true, true);
        }
    }
}
