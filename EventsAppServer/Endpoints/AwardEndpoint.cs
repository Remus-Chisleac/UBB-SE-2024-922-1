using EventsAppServer.Entities;

namespace EventsAppServer.DbEndpoints
{
    public class AwardEndpoint
    {
        private readonly AppContext _context;

        public AwardEndpoint(AppContext context)
        {
            _context = context;
        }

        public void CreateAward(Award award)
        {
            _context.Add(award);
            _context.SaveChanges();
        }
        public List<Award> ReadAwards()
        {
            return _context.Awards.ToList();
        }

        public void UpdateAward(Award newAward)
        {
            IEnumerable<Award> items = 
                from award in _context.Awards
                where award.Id == newAward.Id
                select award;
            Award? item = items.FirstOrDefault();
            item.AwardTypeObj = newAward.AwardTypeObj;
            _context.SaveChanges();
        }
        public void DeleteAward(Guid id)
        {
            IEnumerable<Award> items =
                from award in _context.Awards
                where award.Id == id
                select award;
            Award item = items.FirstOrDefault() ?? new Award();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}
