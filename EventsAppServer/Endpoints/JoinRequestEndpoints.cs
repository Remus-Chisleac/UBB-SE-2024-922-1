using EventsAppServer.Entities;

namespace EventsAppServer.DbEndpoints
{
    public class JoinRequestEndpoints
    {
        private readonly AppContext _context;

        public JoinRequestEndpoints(AppContext context)
        {
            _context = context;
        }
        public void CreateJoinRequest(JoinRequest joinRequest)
        {
            _context.Add(joinRequest);
            _context.SaveChanges();
        }
        public List<JoinRequest> ReadAllJoinRequests()
        {
            return _context.JoinRequests.ToList();
        }

        public void DeleteJoinRequest(Guid joinRequestId)
        {
           IEnumerable<JoinRequest> items =
                from joinRequest in _context.JoinRequests
                where joinRequest.Id == joinRequestId
                select joinRequest;
            JoinRequest? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}
