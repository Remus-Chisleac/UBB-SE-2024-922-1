using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class JoinRequestAnswerForOneQuestionRepository : IJoinRequestAnswerForOneQuestionRepository
    {
        private JoinRequestAnswerForOneQuestionEndpoints joinRequestAnswerForOneQuestionEndpoints;

        public JoinRequestAnswerForOneQuestionRepository(JoinRequestAnswerForOneQuestionEndpoints joinRequestAnswerForOneQuestionEndpoints) : base()
        {
            this.joinRequestAnswerForOneQuestionEndpoints = joinRequestAnswerForOneQuestionEndpoints;
        }

        public bool Add(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            joinRequestAnswerForOneQuestionEndpoints.CreateQuestion(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return joinRequestAnswerForOneQuestionEndpoints.ReadQuestion().Exists(a => a.Id == key);
        }

        public JoinRequestAnswerToOneQuestion? Get(Guid key)
        {
            return joinRequestAnswerForOneQuestionEndpoints.ReadQuestion().Find(a => a.Id == key);
        }

        public IEnumerable<JoinRequestAnswerToOneQuestion> GetAll()
        {
            return joinRequestAnswerForOneQuestionEndpoints.ReadQuestion();
        }

        public bool Remove(Guid key)
        {
            return true;
            // throw new Exception("Remove needs more than just the id");
        }
        public bool Remove(JoinRequestAnswerToOneQuestion question)
        {
            joinRequestAnswerForOneQuestionEndpoints.DeleteQuestion(question);
            return true;
        }

        public bool Update(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            joinRequestAnswerForOneQuestionEndpoints.UpdateQuestion(value);
            return true;
        }
    }
}
