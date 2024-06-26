﻿using EventsAppServer.Entities;

namespace EventsAppServer.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        protected readonly Dictionary<Guid, Question> data;
        public QuestionRepository(Dictionary<Guid, Question> data)
        {
            this.data = data;
        }
        public QuestionRepository() : base()
        {
        }

        // public IEnumerable<JoinRequestAnswerToOneQuestion> GetQuestionsByGroup(Guid groupId)
        // {
        //    return data.Values.Where(q => q.GroupId == groupId);
        // }
    }
}
