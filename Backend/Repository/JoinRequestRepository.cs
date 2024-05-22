using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class JoinRequestRepository : IJoinRequestRepository
    {
        private JoinRequestEndpoints joinRequestEndpoints;

        public JoinRequestRepository(JoinRequestEndpoints joinRequestEndpoints) : base()
        {
            this.joinRequestEndpoints = joinRequestEndpoints;
        }

        public bool Add(Guid key, JoinRequest value)
        {
            joinRequestEndpoints.CreateJoinRequest(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return joinRequestEndpoints.ReadAllJoinRequests().Exists(a => a.Id == key);
        }

        public JoinRequest? Get(Guid key)
        {
            return joinRequestEndpoints.ReadAllJoinRequests().Find(a => a.Id == key);
        }

        public IEnumerable<JoinRequest> GetAll()
        {
            return joinRequestEndpoints.ReadAllJoinRequests();
        }

        public bool Remove(Guid key)
        {
            joinRequestEndpoints.DeleteJoinRequest(key);
            return true;
        }

        public bool Update(Guid key, JoinRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
