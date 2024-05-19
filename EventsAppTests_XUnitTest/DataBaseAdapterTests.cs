using EventsApp.Logic.Attributes;
using EventsApp.Logic.Adapters;
using EventsApp.Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventsApp.Logic.Managers;

namespace EventsAppTests_XUnitTest.DatabaseAdapters
{
    public class DataBaseAdapterTests
    {
        private readonly string connectionString = AppDataInfo.TestDataBaseConnectionString;

        [Fact]
        public void DataBaseAdapter_Constructor_SetsConnectionString()
        {
            DataBaseAdapter<int> dataBaseAdapter = new DataBaseAdapter<int>(connectionString);

            string actualConnectionString = dataBaseAdapter.ConnectionString();

            Assert.Equal(connectionString, actualConnectionString);
        }

        [Fact]
        public void Add_ValidAdd_CorrectlyAddsItem()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);
            dataBaseAdapter.Clear();
            UserInfo userInfo = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test Name",
                Password = "Test Password"
            };
            UserInfo Expected = userInfo;

            // Act
            dataBaseAdapter.Add(userInfo);


            // Assert
            UserInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "GUID", userInfo.GUID } }));
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Clear_ValidClear_ClearsAllItems()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);
            UserInfo userInfo = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test Name",
                Password = "Test Password"
            };

            // Act
            dataBaseAdapter.Add(userInfo);
            dataBaseAdapter.Clear();

            // Assert
            Assert.Empty(dataBaseAdapter.GetAll());

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Contains_ValidContains_ReturnsTrue()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);
            dataBaseAdapter.Clear();
            UserInfo userInfo = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test Name",
                Password = "Test Password"
            };

            // Act
            dataBaseAdapter.Add(userInfo);

            // Assert
            Assert.True(dataBaseAdapter.Contains(new Identifier(new Dictionary<string, object> { { "Name", userInfo.Name } })));

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Delete_ValidDelete_DeletesItem()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);
            dataBaseAdapter.Clear();
            UserInfo userInfo = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test Name",
                Password = "Test Password"
            };

            // Act
            dataBaseAdapter.Add(userInfo);
            dataBaseAdapter.Delete(new Identifier(new Dictionary<string, object> { { "GUID", userInfo.GUID } }));

            // Assert
            Assert.Empty(dataBaseAdapter.GetAll());

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Get_WithStringIdentifier_ReturnsItem()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);
            dataBaseAdapter.Clear();
            UserInfo userInfo = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test Name",
                Password = "Test Password"
            };
            UserInfo Expected = userInfo;

            // Act
            dataBaseAdapter.Add(userInfo);

            // Assert
            UserInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "name", userInfo.Name } }));
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Get_WithGuidIdentifier_ReturnsItem()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);
            dataBaseAdapter.Clear();
            UserInfo userInfo = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test Name",
                Password = "Test Password"
            };
            UserInfo Expected = userInfo;

            // Act
            dataBaseAdapter.Add(userInfo);

            // Assert
            UserInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "GUID", userInfo.GUID } }));
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Get_WithDateTimeIdentifier_ReturnsItem()
        {
            // Arrange
            DataBaseAdapter<EventInfo> dataBaseAdapter = new DataBaseAdapter<EventInfo>(connectionString);
            dataBaseAdapter.Clear();
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo Expected = eventInfo;

            // Act
            dataBaseAdapter.Add(eventInfo);

            // Assert
            EventInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "StartDate", eventInfo.StartDate } }));
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Get_WithIntIdentifier_ReturnsItem()
        {
            // Arrange
            DataBaseAdapter<EventInfo> dataBaseAdapter = new DataBaseAdapter<EventInfo>(connectionString);
            dataBaseAdapter.Clear();
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo Expected = eventInfo;

            // Act
            dataBaseAdapter.Add(eventInfo);

            // Assert
            EventInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "MaxParticipants", eventInfo.MaxParticipants } }));
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Get_WithFloatIdentifier_ReturnsItem()
        {
            // Arrange
            DataBaseAdapter<EventInfo> dataBaseAdapter = new DataBaseAdapter<EventInfo>(connectionString);
            dataBaseAdapter.Clear();
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo Expected = eventInfo;

            // Act
            dataBaseAdapter.Add(eventInfo);

            // Assert
            EventInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "EntryFee", eventInfo.EntryFee } }));
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void GetAll_ValidGetAll_ReturnsItems()
        {
            // Arrange
            DataBaseAdapter<EventInfo> dataBaseAdapter = new DataBaseAdapter<EventInfo>(connectionString);
            dataBaseAdapter.Clear();
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event1",
                Categories = "Test Category1",
                Location = "Test Location1",
                MaxParticipants = 10,
                Description = "Test Description1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner1",
                LogoURL = "Test Logo1",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event2",
                Categories = "Test Category2",
                Location = "Test Location2",
                MaxParticipants = 20,
                Description = "Test Description2",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner2",
                LogoURL = "Test Logo2",
                AgeLimit = 18,
                EntryFee = 0
            };

            // Act
            dataBaseAdapter.Add(eventInfo1);
            dataBaseAdapter.Add(eventInfo2);

            // Assert
            List<EventInfo> Expected = new List<EventInfo> { eventInfo1, eventInfo2 };
            List<EventInfo> Actual = dataBaseAdapter.GetAll();
            foreach (EventInfo eventInfo in Expected)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Update_ValidUpdate_UpdatesItem()
        {
            // Arrange
            DataBaseAdapter<EventInfo> dataBaseAdapter = new DataBaseAdapter<EventInfo>(connectionString);
            dataBaseAdapter.Clear();
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo Expected = new EventInfo
            {
                GUID = eventInfo.GUID,
                OrganizerGUID = eventInfo.OrganizerGUID,
                EventName = "Updated Event",
                Categories = "Updated Category",
                Location = "Updated Location",
                MaxParticipants = 20,
                Description = "Updated Description",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Updated Banner",
                LogoURL = "Updated Logo",
                AgeLimit = 21,
                EntryFee = 5
            };

            // Act
            dataBaseAdapter.Add(eventInfo);
            dataBaseAdapter.Update(new Identifier(new Dictionary<string, object> { { "GUID", eventInfo.GUID } }), Expected);

            // Assert
            EventInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "GUID", eventInfo.GUID } }));
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }

        [Fact]
        public void Add_WithNoDBConnection_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>("");

            // Act
            Action act = () => dataBaseAdapter.Add(new UserInfo());

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Clear_WithNoDBConnection_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>("");

            // Act
            Action act = () => dataBaseAdapter.Clear();

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Contains_WithNoDBConnection_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>("");

            // Act
            Action act = () => dataBaseAdapter.Contains(new Identifier(new Dictionary<string, object> { { "Name", "Test Name" } }));

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Delete_WithNoDBConnection_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>("");

            // Act
            Action act = () => dataBaseAdapter.Delete(new Identifier(new Dictionary<string, object> { { "GUID", Guid.NewGuid() } }));

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Get_WithNoDBConnection_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>("");

            // Act
            Action act = () => dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "Name", "Test Name" } }));

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void GetAll_WithNoDBConnection_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>("");

            // Act
            Action act = () => dataBaseAdapter.GetAll();

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Update_WithNoDBConnection_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>("");

            // Act
            Action act = () => dataBaseAdapter.Update(new Identifier(new Dictionary<string, object> { { "GUID", Guid.NewGuid() } }), new UserInfo());

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Get_WithInvalidIdentifier_ThrowsException()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);

            // Act
            Action act = () => dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "Invalid", "Invalid" } }));

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Get_WithNoItemInDataBase_RetrunsDefaultItem()
        {
            // Arrange
            DataBaseAdapter<UserInfo> dataBaseAdapter = new DataBaseAdapter<UserInfo>(connectionString);
            dataBaseAdapter.Clear();
            UserInfo Expected = new UserInfo(Guid.Empty,null,null);

            // Act
            UserInfo Actual = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "Name", "Test Name" } }));

            // Assert
            Assert.Equal(Expected, Actual);

            // Cleanup
            dataBaseAdapter.Clear();
        }
    }
}