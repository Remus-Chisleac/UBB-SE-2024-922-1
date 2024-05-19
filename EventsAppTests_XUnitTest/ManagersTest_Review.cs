using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest.Managers
{
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Managers;
    public class ManagersTest_Review
    {
        [Fact]
        public void GetReview_NormalRequest_ReturnsReview()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            ReviewInfo Expected = new ReviewInfo
            {
                UserGUID = reviewerGuid,
                EventGUID = eventGuid,
                Score = score,
                ReviewDescription = description,
            };

            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Act
            ReviewInfo Actual = ReviewsManager.GetReview(reviewerGuid, eventGuid);

            // Assert
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void GetAllReviews_NormalRequest_ReturnsAllReviews()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            ReviewInfo Expected = new ReviewInfo
            {
                UserGUID = reviewerGuid,
                EventGUID = eventGuid,
                Score = score,
                ReviewDescription = description,
            };

            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Act
            List<ReviewInfo> Actual = ReviewsManager.GetAllReviews();

            // Assert
            Assert.Single(Actual);
        }

        [Fact]
        public void GetAllReviewsOfReviewer_NormalRequest_ReturnsAllReviewsOfReviewer()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            ReviewInfo Expected = new ReviewInfo
            {
                UserGUID = reviewerGuid,
                EventGUID = eventGuid,
                Score = score,
                ReviewDescription = description,
            };

            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Act
            List<ReviewInfo> Actual = ReviewsManager.GetAllReviewsOfReviewer(reviewerGuid);

            // Assert
            Assert.Single(Actual);
        }

        [Fact]
        public void GetAllReviewsOfEvent_NormalRequest_ReturnsAllReviewsOfEvent()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            ReviewInfo Expected = new ReviewInfo
            {
                UserGUID = reviewerGuid,
                EventGUID = eventGuid,
                Score = score,
                ReviewDescription = description,
            };

            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Act
            List<ReviewInfo> Actual = ReviewsManager.GetAllReviewsOfEvent(eventGuid);

            // Assert
            Assert.Single(Actual);
        }

        [Fact]
        public void GetAllReviewsOfUser_NormalRequest_ReturnsAllReviewsOfUser()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            ReviewInfo Expected = new ReviewInfo
            {
                UserGUID = reviewerGuid,
                EventGUID = eventGuid,
                Score = score,
                ReviewDescription = description,
            };
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = eventGuid,
                OrganizerGUID = reviewerGuid,
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
            EventsManager.AddNewEvent(eventInfo1);
            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Act
            List<ReviewInfo> Actual = ReviewsManager.GetAllReviewsOfUser(reviewerGuid);

            // Assert
            Assert.Single(Actual);
        }

        [Fact]
        public void GetReviewsAverageScoreOfUser_NormalRequest_ReturnsReviewsAverageScoreOfUser()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            float Expected = 5.0f;
            EventInfo eventInfo1 = new EventInfo
            {
                GUID = eventGuid,
                OrganizerGUID = reviewerGuid,
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
            EventsManager.AddNewEvent(eventInfo1);
            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Act
            float Actual = ReviewsManager.GetReviewsAverageScoreOfUser(reviewerGuid);

            // Assert
            Assert.Equal(Expected, Actual);
        }
        [Fact]
        public void GetReviewsAverageScoreOfUser_NoUserReview_ReturnsFloatNaN()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            float Expected = float.NaN;
            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Act
            float Actual = ReviewsManager.GetReviewsAverageScoreOfUser(reviewerGuid);

            // Assert
            Assert.Equal(Expected, Actual);
        }

        [Fact]
        public void AddReview_NormalRequest_AddsReview()
        {
            // Arrange
            ManagersInitializer.Initialize(true, true);
            Guid reviewerGuid = Guid.NewGuid();
            Guid eventGuid = Guid.NewGuid();
            float score = 5;
            string description = "Great event!";
            ReviewInfo Expected = new ReviewInfo
            {
                UserGUID = reviewerGuid,
                EventGUID = eventGuid,
                Score = score,
                ReviewDescription = description,
            };

            // Act
            ReviewsManager.AddReview(reviewerGuid, eventGuid, score, description);

            // Assert
            Assert.Single(ReviewsManager.GetAllReviews());
        }
    }
}
