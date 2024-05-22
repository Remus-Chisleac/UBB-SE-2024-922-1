using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class AwardRepository : IAwardRepository
    {
        private AwardEndpoint awardEndpoint;

        public AwardRepository(AwardEndpoint awardEndpoint) : base()
        {
            this.awardEndpoint = awardEndpoint;
        }

        public bool Add(Guid key, Award value)
        {
            awardEndpoint.CreateAward(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return awardEndpoint.ReadAwards().Exists(a => a.Id == key);
        }

        public Award? Get(Guid key)
        {
            return awardEndpoint.ReadAwards().Find(a => a.Id == key);
        }

        public IEnumerable<Award> GetAll()
        {
            return awardEndpoint.ReadAwards();
        }

        public bool Remove(Guid key)
        {
            awardEndpoint.DeleteAward(key);
            return true;
        }

        public bool Update(Guid key, Award award)
        {
            awardEndpoint.UpdateAward(award);
            return true;
        }
    }
}
