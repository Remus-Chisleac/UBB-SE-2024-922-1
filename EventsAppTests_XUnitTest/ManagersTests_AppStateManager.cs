using EventsApp.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    public class ManagersTests_AppStateManager
    {
        public static Guid CurrentUserGUID = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public static string Name = "Janina Gigel";
        public static string Password = "1234";

        [Fact]
        public static void Contructor_HardCodedData()
        {
            Assert.Equal(CurrentUserGUID, AppStateManager.CurrentUserGUID);
            Assert.Equal(Name, AppStateManager.Name);
            Assert.Equal(Password, AppStateManager.Password);
        }
    }
}
