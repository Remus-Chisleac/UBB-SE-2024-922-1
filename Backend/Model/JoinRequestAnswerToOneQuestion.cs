﻿namespace Moderation.Entities
{
    public class JoinRequestAnswerToOneQuestion : IHasID
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string QuestionText { get; set; }

        public string QuestionAnswer { get; set; }

        public JoinRequestAnswerToOneQuestion(Guid requestId, string text, string answer)
        {
            Id = Guid.NewGuid();
            RequestId = requestId;
            QuestionText = text;
            QuestionAnswer = answer;
        }
        public JoinRequestAnswerToOneQuestion(Guid id, Guid requestId, string text, string answer)
        {
            Id = id;
            RequestId = requestId;
            QuestionText = text;
            QuestionAnswer = answer;
        }
    }
}
