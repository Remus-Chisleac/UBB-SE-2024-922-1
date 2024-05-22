using EventsAppServer.Entities;

namespace EventsAppServer.DbEndpoints
{
    internal class GroupEndpoints
    {
        private readonly AppContext _context;
        public GroupEndpoints(AppContext context)
        {
            _context = context;
        }

        public void CreateGroup(Group group)
        {
           _context.Add(group);
           _context.SaveChanges();
        }
        public List<Group> ReadAllGroups()
        {
            return _context.Groups.ToList();
        }
        public void UpdateGroup(Group newGroup)
        {
            IEnumerable<Group> items = 
                from g in _context.Groups
                where g.Id == newGroup.Id
                select g;
            Group ? item = items.FirstOrDefault();
            item.Name = newGroup.Name;
            item.Description = newGroup.Description;    
        }
        public void DeleteGroup(Guid id)
        {
            IEnumerable<Group> items = 
                from g in _context.Groups
                where g.Id == id
                select g;
            Group? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}
