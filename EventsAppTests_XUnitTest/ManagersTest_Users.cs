using EventsApp.Logic.Entities;
using EventsApp.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest.Managers
{
    public class ManagersTest_Users
    {
        [Fact]
        public void GetUser_NormalRequest_ReturnsUser()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            string username = "JohnDoe";
            string password = "password";

            UserInfo Expected = new UserInfo
            {
                Name = username,
                Password = password,
            };

            Expected.GUID = UsersManager.AddNewUser(username, password);

            // Act
            UserInfo Actual = UsersManager.GetUser(Expected.GUID);

            // Assert
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void GetAllUsers_NormalRequest_ReturnsAllUsers()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);

            UserInfo user1 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "JohnDoe",
                Password = "password",
            };
            UserInfo user2 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "JaneDoe2",
                Password = "password2",
            };
            List<UserInfo> Expected = new List<UserInfo>
            {
                user1,
                user2,
            };

            UsersManager.AddNewUser(user1);
            UsersManager.AddNewUser(user2);
            

            // Act
            List<UserInfo> Actual = UsersManager.GetAllUsers();

            // Assert
            foreach (UserInfo user in Expected)
            {
                Assert.Contains(user, Actual);
            }
        }

        [Fact]
        public void IsAdmin_NotImplemented_ReturnsTrue()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            AdminInfo admin = new AdminInfo
            {
                GUID = userGuid,
            };

            // Act
            bool Actual = UsersManager.IsAdmin(userGuid);

            // Assert
            Assert.False(Actual);
        }

        [Fact]
        public void HasPermission_NotImplemented_ReturnsZero()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            AdminInfo admin = new AdminInfo
            {
                GUID = userGuid,
            };

            // Act
            float Actual = UsersManager.HasPermission(userGuid);

            // Assert
            Assert.Equal(0, Actual);
        }

        [Fact]
        public void SearchUsersByUsername_NormalRequest_ReturnsAllUsersWithUsername()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            UserInfo user1 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "JohnDoe",
                Password = "password",
            };
            UserInfo user2 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "JaneDoe2",
                Password = "password2",
            };
            List<UserInfo> Expected = new List<UserInfo>
            {
                user1,
            };

            UsersManager.AddNewUser(user1);
            UsersManager.AddNewUser(user2);

            // Act
            List<UserInfo> Actual = UsersManager.SearchUsersByUsername("John");

            // Assert
            foreach (UserInfo user in Expected)
            {
                Assert.Contains(user, Actual);
            }
        }

        [Fact]
        public void SendNotificationToUser_NotImplemented_ReturnsVoid()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            string message = "Hello!";

            // Act
            UsersManager.SendNotificationToUser(userGuid, message);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void AddNewUser_NormalRequest_AddsUser()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            string username = "JohnDoe";
            string password = "password";
            UserInfo user = new UserInfo
            {
                Name = username,
                Password = password,
            };
            UserInfo Expected = new UserInfo
            {
                Name = username,
                Password = password,
            };

            // Act
            user.GUID = UsersManager.AddNewUser(username, password);
            Expected.GUID = user.GUID;

            // Assert
            UserInfo Actual = UsersManager.GetUser(user.GUID);
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void AddNewUser_NormalRequest_ReturnsUserGuid()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            string username = "JohnDoe";
            string password = "password";
            UserInfo user = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = username,
                Password = password,
            };
            UserInfo Expected = user;

            // Act
            UsersManager.AddNewUser(user);

            // Assert
            UserInfo Actual = UsersManager.GetUser(user.GUID);
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void InviteUser_NotImplemented_ReturnsVoid()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            Guid userInvitedGuid = Guid.NewGuid();

            // Act
            UsersManager.InviteUser(userGuid, eventGuid, userInvitedGuid);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void SetInterestedStatus_FromNotInterested_SetsStatus()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();

            // Act
            UsersManager.SetInterestedStatus(userGuid, eventGuid);

            // Assert
            Assert.True(UsersManager.IsInterested(userGuid, eventGuid));
        }

        [Fact]
        public void SetGoingStatus_FromNotGoing_SetsStatus()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();

            // Act
            UsersManager.SetGoingStatus(userGuid, eventGuid);

            // Assert
            Assert.True(UsersManager.IsGoing(userGuid, eventGuid));
        }

        [Fact]
        public void RemoveInterestedStatus_FromInterested_RemovesStatus()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();

            UsersManager.SetInterestedStatus(userGuid, eventGuid);

            // Act
            UsersManager.RemoveInterestedStatus(userGuid, eventGuid);

            // Assert
            Assert.False(UsersManager.IsInterested(userGuid, eventGuid));
        }

        [Fact]
        public void IsInterested_FromInterested_ReturnsTrue()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();

            UsersManager.SetInterestedStatus(userGuid, eventGuid);

            // Act
            bool Actual = UsersManager.IsInterested(userGuid, eventGuid);

            // Assert
            Assert.True(Actual);
        }

        [Fact]
        public void IsGoing_FromGoing_ReturnsTrue()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();

            UsersManager.SetGoingStatus(userGuid, eventGuid);
            UsersManager.SetGoingStatus(userGuid, eventGuid);

            // Act
            bool Actual = UsersManager.IsGoing(userGuid, eventGuid);

            // Assert
            Assert.True(Actual);
        }

        [Fact]
        public void IsGoing_FromInterested_ReturnsFalse()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();

            UsersManager.SetInterestedStatus(userGuid, eventGuid);
            UsersManager.SetInterestedStatus(userGuid, eventGuid);

            // Act
            bool Actual = UsersManager.IsGoing(userGuid, eventGuid);

            // Assert
            Assert.False(Actual);
        }

        [Fact]
        public void IsGoing_FromNotGoing_ReturnsFalse()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();

            // Act
            bool Actual = UsersManager.IsGoing(userGuid, eventGuid);

            // Assert
            Assert.False(Actual);
        }

        [Fact]
        public void DeleteUser_NormalRequest_DeletesUser()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            UserInfo user = new UserInfo
            {
                GUID = userGuid,
                Name = "JohnDoe",
                Password = "password",
            };
            UserInfo Expected = new UserInfo
            {
                GUID = Guid.Empty,
                Name = null,
                Password = null,
            };

            UsersManager.AddNewUser(user);

            // Act
            UsersManager.DeleteUser(userGuid);

            // Assert
            UserInfo Actual = UsersManager.GetUser(userGuid);
            Assert.Equal(Expected, Actual);
        }

    }
}
