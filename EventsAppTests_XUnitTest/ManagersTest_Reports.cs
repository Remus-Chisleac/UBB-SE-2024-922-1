using EventsApp.Logic.Entities;
using EventsApp.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest.Managers
{
    public class ManagersTest_Reports
    {
        [Fact]
        public void GetAllReports_NormalRequest_ReturnsAllReports()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            ReportInfo.ReportType reportType = ReportInfo.ReportType.Spam;

            ReportsManager.AddReport(userGuid, eventGuid, reportType);

            // Act
            List<ReportInfo> Actual = ReportsManager.GetAllReports();

            // Assert
            Assert.Single(Actual);
        }

        [Fact]
        public void GetReport_NormalRequest_ReturnsReport()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            ReportInfo.ReportType reportType = ReportInfo.ReportType.Spam;
            ReportInfo Expected = new ReportInfo
            {
                UserGUID = userGuid,
                EventGUID = eventGuid,
                ReportTypeValue = ReportInfo.ReportType.Harassment,
            };

            ReportsManager.AddReport(userGuid, eventGuid, reportType);

            // Act
            ReportInfo Actual = ReportsManager.GetReport(userGuid, eventGuid);

            // Assert
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void RemoveReport_NormalRequest_RemovesReport()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            ReportInfo.ReportType reportType = ReportInfo.ReportType.Spam;

            ReportsManager.AddReport(userGuid, eventGuid, reportType);

            // Act
            ReportsManager.RemoveReport(userGuid, eventGuid);

            // Assert
            Assert.Empty(ReportsManager.GetAllReports());
        }

        [Fact]
        public void RemoveAllReportsForEvent_NormalRequest_RemovesAllReportsForEvent()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            ReportInfo.ReportType reportType = ReportInfo.ReportType.Spam;

            ReportsManager.AddReport(userGuid, eventGuid, reportType);

            // Act
            ReportsManager.RemoveAllReportsForEvent(eventGuid);

            // Assert
            Assert.Empty(ReportsManager.GetAllReports());
        }

        [Fact]
        public void RemoveAllReportsFromUser_NormalRequest_RemovesAllReportsFromUser()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            ReportInfo.ReportType reportType = ReportInfo.ReportType.Spam;

            ReportsManager.AddReport(userGuid, eventGuid, reportType);

            // Act
            ReportsManager.RemoveAllReportsFromUser(userGuid);

            // Assert
            Assert.Empty(ReportsManager.GetAllReports());
        }

        [Fact]
        public void RemoveAllReportsForEventAndUser_NormalRequest_RemovesAllReportsForEventAndUser()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            ReportInfo.ReportType reportType = ReportInfo.ReportType.Spam;

            ReportsManager.AddReport(userGuid, eventGuid, reportType);

            // Act
            ReportsManager.RemoveAllReportsForEventAndUser(userGuid, eventGuid);

            // Assert
            Assert.Empty(ReportsManager.GetAllReports());
        }

        [Fact]
        public void RemoveAllReports_NormalRequest_RemovesAllReports()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid userGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            ReportInfo.ReportType reportType = ReportInfo.ReportType.Spam;

            ReportsManager.AddReport(userGuid, eventGuid, reportType);

            // Act
            ReportsManager.RemoveAllReports();

            // Assert
            Assert.Empty(ReportsManager.GetAllReports());
        }

    }
}
