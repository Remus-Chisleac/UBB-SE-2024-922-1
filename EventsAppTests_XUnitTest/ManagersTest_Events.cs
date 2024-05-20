using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest.Managers
{
    using EventsApp.Logic.Managers;
    using EventsApp.Logic.Entities;
    using System.Diagnostics;
    using static EventsApp.Logic.Managers.EventsManager;

    public class ManagersTest_Events
    {
        [Fact]
        public void GetEvent_NormalRequest_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
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
            EventInfo Expect = eventInfo;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            EventInfo Actual = EventsManager.GetEvent(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void GetAllEvens_NormalRequest_ReturnsAllEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 10,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 10,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 0
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1, eventInfo2 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.GetAllEvents();

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void IsEventActive_ActiveEvent_ReturnsTrue()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            bool Expect = true;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            bool Actual = EventsManager.IsEventActive(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void IsEventActive_InactiveEvent_ReturnsFalse()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            bool Expect = false;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            bool Actual = EventsManager.IsEventActive(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void IsOrganizer_WithOrganizerOfEvent_ReturnsTrue()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            Guid organizerGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = organizerGUID,
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            bool Expect = true;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            bool Actual = EventsManager.IsOrganizer(organizerGUID, eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void IsOrganizer_WithNonOrganizerOfEvent_ReturnsFalse()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            Guid organizerGUID = Guid.NewGuid();
            Guid nonOrganizerGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = organizerGUID,
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            bool Expect = false;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            bool Actual = EventsManager.IsOrganizer(nonOrganizerGUID, eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void AddNewEvent_WithEventInfo_AddsEvent()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
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
            EventInfo Expect = eventInfo;

            // Act
            EventsManager.AddNewEvent(eventInfo);
            EventInfo Actual = EventsManager.GetEvent(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void UpdateEvent_WithEventInfo_UpdatesEvent()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
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
            EventInfo eventInfoUpdate = new EventInfo
            {
                GUID = eventInfo.GUID,
                OrganizerGUID = eventInfo.OrganizerGUID,
                EventName = "Test Event Update",
                Categories = "Test Category Update",
                Location = "Test Location Update",
                MaxParticipants = 20,
                Description = "Test Description Update",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner Update",
                LogoURL = "Test Logo Update",
                AgeLimit = 21,
                EntryFee = 10
            };
            EventInfo Expect = eventInfoUpdate;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            EventsManager.UpdateEvent(eventInfo.GUID, eventInfoUpdate);
            EventInfo Actual = EventsManager.GetEvent(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void DeleteEvent_NormalRequest_DeletesEvent()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
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
                GUID = Guid.Empty,
                OrganizerGUID = Guid.Empty,
                EventName = null,
                Categories = null,
                Location = null,
                MaxParticipants = 0,
                Description = null,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MinValue,
                BannerURL = null,
                LogoURL = null,
                AgeLimit = 0,
                EntryFee = 0
            };
            EventsManager.AddNewEvent(eventInfo);

            // Act
            EventsManager.DeleteEvent(eventInfo.GUID);
            EventInfo Actual = EventsManager.GetEvent(eventInfo.GUID);

            // Assert
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void IsEventOver_WithEventOver_ReturnsTrue()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(-1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            bool Expect = true;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            bool Actual = EventsManager.IsEventOver(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void IsEventOver_WithEventNotOver_ReturnsFalse()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            bool Expect = false;

            EventsManager.AddNewEvent(eventInfo);

            // Act
            bool Actual = EventsManager.IsEventOver(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void FilterEvents_WithName_ReturnEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 10,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 10,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 0
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1 };
            
            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            EventFilter eventFilter = new EventFilter
            {
                Name = "Test Event 1"
            };

            // Act
            List<EventInfo> Actual = EventsManager.FilterEvents(eventFilter);

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void FilterEvents_WithMaxFee_ReturnEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 10,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 10,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 10
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            EventFilter eventFilter = new EventFilter
            {
                MaxFee = 5
            };

            // Act
            List<EventInfo> Actual = EventsManager.FilterEvents(eventFilter);

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void FilterEvents_WithStartDate_ReturnEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 10,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/6/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/6/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 10,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/7/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/7/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 0
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo2 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            EventFilter eventFilter = new EventFilter
            {
                StartDate = DateTime.Parse("5/7/2024 7:39:42 PM"),
            };

            // Act
            List<EventInfo> Actual = EventsManager.FilterEvents(eventFilter);

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void FilterEvents_WithCategories_ReturnEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 10,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/6/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/6/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 0
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 10,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/7/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/7/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 0
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            EventFilter eventFilter = new EventFilter
            {
                Categories = new List<string> { "Test Category 1" }
            };

            EventFilter eventFilter1 = new EventFilter
            (
                string.Empty,
                0,
                DateTime.MinValue,
                DateTime.MinValue,
                new List<string> { "Test Category 2" }
            );

            // Act
            List<EventInfo> Actual = EventsManager.FilterEvents(eventFilter);

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void GetInterestedUsers_WithEvent_ReturnsUsers()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            Guid userGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo
            {
                GUID = userGUID,
                Name = "Test User",
                Password = "Test Password",
            };
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            List<UserInfo> Expect = new List<UserInfo> {userInfo};

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);
            UsersManager.SetInterestedStatus(userGUID, eventInfo.GUID);

            // Act
            List<UserInfo> Actual = EventsManager.GetInterestedUsers(eventInfo.GUID);

            // Assert
            foreach (UserInfo user in Expect)
            {
                Assert.Contains(user, Actual);
            }

            // Clean up
            UsersManager.DeleteUser(userInfo.GUID);
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void GetGoingUsers_WithEvent_ReturnsUsers()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            Guid userGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo
            {
                GUID = userGUID,
                Name = "Test User",
                Password = "Test Password",
            };
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            List<UserInfo> Expect = new List<UserInfo> { userInfo };

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);
            UsersManager.SetGoingStatus(userGUID, eventInfo.GUID);

            // Act
            List<UserInfo> Actual = EventsManager.GetGoingUsers(eventInfo.GUID);

            // Assert
            foreach (UserInfo user in Expect)
            {
                Assert.Contains(user, Actual);
            }

            // Clean up
            UsersManager.DeleteUser(userInfo.GUID);
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void GetEventOrganizer_WithEvent_ReturnsOrganizer()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            Guid organizerGUID = Guid.NewGuid();
            UserInfo organizerInfo = new UserInfo
            {
                GUID = organizerGUID,
                Name = "Test Organizer",
                Password = "Test Password",
            };
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = organizerGUID,
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 10,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            UserInfo Expect = organizerInfo;

            UsersManager.AddNewUser(organizerInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            UserInfo Actual = EventsManager.GetEventOrganizer(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            UsersManager.DeleteUser(organizerInfo.GUID);
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void GetNumberOfParticipants_WithEvent_ReturnsNumberOfParticipants()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            Guid userGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo
            {
                GUID = userGUID,
                Name = "Test User",
                Password = "Test Password",
            };
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 2,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 0
            };
            int Expect = 1;

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);
            UsersManager.SetGoingStatus(userGUID, eventInfo.GUID);

            // Act
            int Actual = EventsManager.GetNumberOfParticipants(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            UsersManager.DeleteUser(userInfo.GUID);
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void GetTotalDonationAmount_WithEvent_ReturnsTotalDonationAmount()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 2,
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BannerURL = "Test Banner",
                LogoURL = "Test Logo",
                AgeLimit = 18,
                EntryFee = 10
            };
            DonationInfo donationInfo = new DonationInfo
            {
                GUID = Guid.NewGuid(),
                EventGUID = eventInfo.GUID,
                UserGUID = Guid.NewGuid(),
                Amount = 10
            };
            float Expect = 10;

            EventsManager.AddNewEvent(eventInfo);
            DonationsManager.AddDonation(donationInfo);

            // Act
            float Actual = EventsManager.GetTotalDonationAmount(eventInfo.GUID);

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            DonationsManager.RemoveDonation(donationInfo.GUID);
            EventsManager.DeleteEvent(eventInfo.GUID);
        }

        [Fact]
        public void GetEventsOfUser_WithUser_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            Guid userGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo
            {
                GUID = userGUID,
                Name = "Test User",
                Password = "Test Password",
            };
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = userGUID,
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 10
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1};

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.GetEventsOfUser(userGUID);

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            UsersManager.DeleteUser(userInfo.GUID);
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void SortEventsByPopularityAscending_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 10
            };
            List<EventInfo> Expect = new List<EventInfo> {eventInfo2, eventInfo1 };

            UserInfo user1 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test User 1",
                Password = "Test Password 1",
            };
            UserInfo user2 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test User 2",
                Password = "Test Password 2",
            };
            UserInfo user3 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test User 3",
                Password = "Test Password 3",
            };

            UsersManager.AddNewUser(user1);
            UsersManager.AddNewUser(user2);
            UsersManager.AddNewUser(user3);

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            UsersManager.SetGoingStatus(user1.GUID, eventInfo1.GUID);
            UsersManager.SetGoingStatus(user2.GUID, eventInfo1.GUID);
            UsersManager.SetGoingStatus(user3.GUID, eventInfo1.GUID);

            // Act
            List<EventInfo> Actual = EventsManager.SortEventsByPopularityAscending();

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);

            UsersManager.DeleteUser(user1.GUID);
            UsersManager.DeleteUser(user2.GUID);
            UsersManager.DeleteUser(user3.GUID);
        }

        [Fact]
        public void SortEventsByPopularityDescending_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 10
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1, eventInfo2 };

            UserInfo user1 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test User 1",
                Password = "Test Password 1",
            };
            UserInfo user2 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test User 2",
                Password = "Test Password 2",
            };
            UserInfo user3 = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test User 3",
                Password = "Test Password 3",
            };

            UsersManager.AddNewUser(user1);
            UsersManager.AddNewUser(user2);
            UsersManager.AddNewUser(user3);

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            UsersManager.SetGoingStatus(user1.GUID, eventInfo1.GUID);
            UsersManager.SetGoingStatus(user2.GUID, eventInfo1.GUID);
            UsersManager.SetGoingStatus(user3.GUID, eventInfo1.GUID);

            // Act
            List<EventInfo> Actual = EventsManager.SortEventsByPopularityDescending();

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);

            UsersManager.DeleteUser(user1.GUID);
            UsersManager.DeleteUser(user2.GUID);
            UsersManager.DeleteUser(user3.GUID);
        }

        [Fact]
        public void SortEventsByDateAscending_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/3/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/3/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 10
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1, eventInfo2 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.SortEventsByDateAscending();

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void SortEventsByDateDescending_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/3/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/3/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 10
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo2, eventInfo1 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.SortEventsByDateDescending();

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void SortEventsByPriceAscending_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/3/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/3/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 20
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1, eventInfo2 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.SortEventsByPriceAscending();

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void SortEventsByPriceDescending_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/3/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/3/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 20
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo2, eventInfo1 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.SortEventsByPriceDescending();

            // Assert
            Assert.Equal(Expect, Actual);

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID); 
        }

        [Fact]
        public void SearchEventByName_WithEventName_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/3/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/3/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 20
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.SearchEventByName("Test Event 1");

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void SearchEventByLocation_WithEventLocation_ReturnsEvents()
        {
            // Arange
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            EventInfo eventInfo2 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 2",
                Categories = "Test Category 2",
                Location = "Test Location 2",
                MaxParticipants = 2,
                Description = "Test Description 2",
                StartDate = DateTime.Parse("5/3/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/3/2024 8:39:42 PM"),
                BannerURL = "Test Banner 2",
                LogoURL = "Test Logo 2",
                AgeLimit = 18,
                EntryFee = 20
            };
            List<EventInfo> Expect = new List<EventInfo> { eventInfo1 };

            EventsManager.AddNewEvent(eventInfo1);
            EventsManager.AddNewEvent(eventInfo2);

            // Act
            List<EventInfo> Actual = EventsManager.SearchEventByLocation("Test Location 1");

            // Assert
            foreach (EventInfo eventInfo in Expect)
            {
                Assert.Contains(eventInfo, Actual);
            }

            // Clean up
            EventsManager.DeleteEvent(eventInfo1.GUID);
            EventsManager.DeleteEvent(eventInfo2.GUID);
        }

        [Fact]
        public void BuyTicket_NormalRequest_SetsGoingStatusOfUser()
        {
            ManagersInitializer.Initialize(true, true);
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event 1",
                Categories = "Test Category 1",
                Location = "Test Location 1",
                MaxParticipants = 2,
                Description = "Test Description 1",
                StartDate = DateTime.Parse("5/2/2024 7:39:42 PM"),
                EndDate = DateTime.Parse("5/2/2024 8:39:42 PM"),
                BannerURL = "Test Banner 1",
                LogoURL = "Test Logo 1",
                AgeLimit = 18,
                EntryFee = 10
            };
            UserInfo userInfo = new UserInfo
            {
                GUID = Guid.NewGuid(),
                Name = "Test User",
                Password = "Test Password",
            };
            string cardHolderName = "Test Card Holder";
            string cardNumber = "1234567890123456";
            string cvv = "123";
            DateTime expirationDate = DateTime.Parse("5/8/2024 8:39:42 PM");

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo1);

            // Act
            EventsManager.BuyTicket(userInfo.GUID, eventInfo1.GUID, cardHolderName, cardNumber, cvv, expirationDate);
            
            // Assert
            Assert.True(UsersManager.IsGoing(userInfo.GUID, eventInfo1.GUID));

            // Clean up
            UsersManager.DeleteUser(userInfo.GUID);
            EventsManager.DeleteEvent(eventInfo1.GUID);
        }
    }
}
