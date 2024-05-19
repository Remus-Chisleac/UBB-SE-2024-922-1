using EventsApp.Logic.Adapters;
using EventsApp.Logic.Entities;
using EventsApp.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest.Managers
{
    public class ManagersTest_Donations
    {
        [Fact]
        public void GetDonation_NormalRequest_ReturnsDonation()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo donationInfo = new DonationInfo(EventGUID, UserGUID, Amount);
            DonationInfo Expected = donationInfo;

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            DonationsManager.AddDonation(donationInfo);
            DonationInfo Actual = DonationsManager.GetDonation(donationInfo.GUID);

            // Assert
            Assert.Equal(Expected, Actual);

            // Clean up
            DonationsManager.RemoveDonation(donationInfo.GUID);
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void GetAllDonations_NormalRequest_ReturnsDonations()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo donationInfo = new DonationInfo(EventGUID, UserGUID, Amount);
            List<DonationInfo> Expected = new List<DonationInfo> { donationInfo };

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            DonationsManager.AddDonation(donationInfo);
            List<DonationInfo> Actual = DonationsManager.GetAllDonations();

            // Assert
            foreach (DonationInfo donation in Expected)
            {
                Assert.Contains(donation, Actual);
            }

            // Clean up
            DonationsManager.RemoveDonation(donationInfo.GUID);
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void GetAllDonationsForEvents_NormalRequest_ReturnsDonations()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo donationInfo = new DonationInfo(EventGUID, UserGUID, Amount);
            List<DonationInfo> Expected = new List<DonationInfo> { donationInfo };

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            DonationsManager.AddDonation(donationInfo);
            List<DonationInfo> Actual = DonationsManager.GetAllDonationsForEvent(EventGUID);

            // Assert
            foreach (DonationInfo donation in Expected)
            {
                Assert.Contains(donation, Actual);
            }

            // Clean up
            DonationsManager.RemoveDonation(donationInfo.GUID);
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void AddDonation_WithUserIDEventIDAmount_AddsDonationAndReturnsItsGuid()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo Expected = new DonationInfo(EventGUID, UserGUID, Amount);

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            Expected.GUID = DonationsManager.AddDonation(UserGUID, EventGUID, Amount);
            DonationInfo Actual = DonationsManager.GetDonation(Expected.GUID);

            // Assert
            Assert.Equal(Expected, Actual);

            // Clean up
            DonationsManager.RemoveDonation(Actual.GUID);
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void AddDonation_withDonationInfo_AddsDonation()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo Expected = new DonationInfo(EventGUID, UserGUID, Amount);
            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            DonationsManager.AddDonation(Expected);
            DonationInfo Actual = DonationsManager.GetDonation(Expected.GUID);

            // Assert
            Assert.Equal(Expected, Actual);

            // Clean up
            DonationsManager.RemoveDonation(Actual.GUID);
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void GetTotalDonationsforEvent_NormalRequest_ReturnsTotalDonations()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo donationInfo = new DonationInfo(EventGUID, UserGUID, Amount);
            float Expected = Amount;

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            DonationsManager.AddDonation(donationInfo);
            float Actual = DonationsManager.GetTotalDonationsForEvent(EventGUID);

            // Assert
            Assert.Equal(Expected, Actual);

            // Clean up
            DonationsManager.RemoveDonation(donationInfo.GUID);
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void GetDonationsFromUser_NormalRequest_ReturnsDonations()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo donationInfo = new DonationInfo(EventGUID, UserGUID, Amount);
            List<DonationInfo> Expected = new List<DonationInfo> { donationInfo };

            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);

            // Act
            DonationsManager.AddDonation(donationInfo);
            List<DonationInfo> Actual = DonationsManager.GetDonationsFromUser(UserGUID);

            // Assert
            foreach (DonationInfo donation in Expected)
            {
                Assert.Contains(donation, Actual);
            }

            // Clean up
            DonationsManager.RemoveDonation(donationInfo.GUID);
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void RemoveDonation_NormalRequest_RemovesDonation()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo donationInfo = new DonationInfo(EventGUID, UserGUID, Amount);
            DonationInfo Expected = new DonationInfo
            {
                GUID = Guid.Empty,
                EventGUID = Guid.Empty,
                UserGUID = Guid.Empty,
                Amount = 0
            };
            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);
            DonationsManager.AddDonation(donationInfo);

            // Act
            DonationsManager.RemoveDonation(donationInfo.GUID);
            DonationInfo Actual = DonationsManager.GetDonation(donationInfo.GUID);

            // Assert
            Assert.Equal(Expected, Actual);

            // Clean up
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }

        [Fact]
        public void RemoveAllDonationsForEvent_NormalRequest_RemovesAllDonations()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid UserGUID = Guid.NewGuid();
            UserInfo userInfo = new UserInfo(UserGUID);
            Guid EventGUID = Guid.NewGuid();
            EventInfo eventInfo = new EventInfo(EventGUID);
            float Amount = 100;
            DonationInfo donationInfo = new DonationInfo(EventGUID, UserGUID, Amount);
            List<DonationInfo> Expected = new List<DonationInfo>();
            UsersManager.AddNewUser(userInfo);
            EventsManager.AddNewEvent(eventInfo);
            DonationsManager.AddDonation(donationInfo);

            // Act
            DonationsManager.RemoveAllDonationsForEvent(EventGUID);
            List<DonationInfo> Actual = DonationsManager.GetAllDonationsForEvent(EventGUID);

            // Assert
            Assert.Equal(Expected, Actual);

            // Clean up
            EventsManager.DeleteEvent(EventGUID);
            UsersManager.DeleteUser(UserGUID);
        }


    }
}
