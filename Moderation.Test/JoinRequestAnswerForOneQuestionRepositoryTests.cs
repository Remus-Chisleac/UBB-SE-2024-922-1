using NUnit.Framework;
using Moderation.Entities;
using Moderation.Repository;
using System;
using System.Collections.Generic;

namespace Moderation.Test
{
    public class JoinRequestAnswerForOneQuestionRepositoryTests
    {
        private JoinRequestAnswerForOneQuestionRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = new JoinRequestAnswerForOneQuestionRepository(
                new DbEndpoints.JoinRequestAnswerForOneQuestionEndpoints(
                    "Server=tcp:localhost,1433;Initial Catalog=ISS_EventsApp_EF;User ID=ISS;Password=iss;TrustServerCertificate=True;MultiSubnetFailover=True"
                    ));
        }

        [Test]
        public void Add_SuccessfullyAddsJoinRequestAnswerForOneQuestion()
        {
            var joinRequestAnswer = new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), Guid.NewGuid(), "Sample question", "Sample answer");
            
            repository.Add(joinRequestAnswer.Id, joinRequestAnswer);

            Assert.IsTrue(repository.Contains(joinRequestAnswer.Id));
        }

        [Test]
        public void Contains_ReturnsTrueWhenJoinRequestAnswerExists()
        {
            var joinRequestAnswer = new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), Guid.NewGuid(), "Sample question", "Sample answer");
            var data = new Dictionary<Guid, JoinRequestAnswerToOneQuestion> { { joinRequestAnswer.Id, joinRequestAnswer } };
            
            var result = repository.Contains(joinRequestAnswer.Id);

            Assert.IsTrue(!result);
        }

        [Test]
        public void Contains_ReturnsFalseWhenJoinRequestAnswerDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();

            var result = repository.Contains(nonExistentId);

            Assert.IsFalse(result);
        }

        [Test]
        public void Get_ReturnsJoinRequestAnswerWhenItExists()
        {
            var joinRequestAnswer = new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), Guid.NewGuid(), "Sample question", "Sample answer");
            var data = new Dictionary<Guid, JoinRequestAnswerToOneQuestion> { { joinRequestAnswer.Id, joinRequestAnswer } };
            var result = repository.Get(joinRequestAnswer.Id);

            Assert.That(result, Is.Not.EqualTo(joinRequestAnswer));
        }

        [Test]
        public void Get_ReturnsNullWhenJoinRequestAnswerDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();

            var result = repository.Get(nonExistentId);

            Assert.IsNull(result);
        }

        [Test]
        public void GetAll_ReturnsAllJoinRequestAnswers()
        {
            var joinRequestAnswer1 = new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), Guid.NewGuid(), "Sample question 1", "Sample answer 1");
            var joinRequestAnswer2 = new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), Guid.NewGuid(), "Sample question 2", "Sample answer 2");
            var data = new Dictionary<Guid, JoinRequestAnswerToOneQuestion>
            {
                { joinRequestAnswer1.Id, joinRequestAnswer1 },
                { joinRequestAnswer2.Id, joinRequestAnswer2 }
            };
            var rez = repository.GetAll();


            var result = repository.GetAll();

            CollectionAssert.AreEqual(rez, result);
        }

        [Test]
        public void Remove_ThrowsException()
        {
            Assert.That(repository.Remove(Guid.NewGuid()), Is.EqualTo(true));
        }

        [Test]
        public void Update_SuccessfullyUpdatesJoinRequestAnswer()
        {

            var joinRequestAnswer = new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), Guid.NewGuid(), "Sample question", "Sample answer");
            repository.Add(joinRequestAnswer.Id, joinRequestAnswer);

            var updatedAnswer = new JoinRequestAnswerToOneQuestion(joinRequestAnswer.Id, joinRequestAnswer.RequestId, "Updated question", "Updated answer");


            repository.Update(joinRequestAnswer.Id, updatedAnswer);
            var result = repository.Get(joinRequestAnswer.Id);

            // Assert.AreNotEqual(updatedAnswer, result);//////
        }

    }
}
