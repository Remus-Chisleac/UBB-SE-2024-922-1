using EventsAppServer.Entities;

namespace EventsAppServer.DbEndpoints
{
    public class JoinRequestAnswerForOneQuestionEndpoints
    {
        private readonly AppContext _context;

        public JoinRequestAnswerForOneQuestionEndpoints(AppContext context)
        {
            _context = context;
        }
        public void CreateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            _context.Add(question);
            _context.SaveChanges();
        }
        public List<JoinRequestAnswerToOneQuestion> ReadQuestion()
        {
            return _context.JoinRequestAnswerToOneQuestion.ToList();
        }
        public void UpdateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            IEnumerable<JoinRequestAnswerToOneQuestion> items =
                from it in _context.JoinRequestAnswerToOneQuestion
                where it.Id == question.Id
                select it;
            JoinRequestAnswerToOneQuestion? item = items.FirstOrDefault();
            item.QuestionText = question.QuestionText;
            item.QuestionAnswer = question.QuestionAnswer;
            _context.SaveChanges();
        }
        public void DeleteQuestion(JoinRequestAnswerToOneQuestion question)
        {
            IEnumerable<JoinRequestAnswerToOneQuestion> items =
                from it in _context.JoinRequestAnswerToOneQuestion
                where it.Id == question.Id
                select it;
            JoinRequestAnswerToOneQuestion? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}
