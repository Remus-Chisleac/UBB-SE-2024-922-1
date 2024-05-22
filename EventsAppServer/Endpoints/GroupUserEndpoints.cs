using System.Configuration;
using Microsoft.Data.SqlClient;
using EventsAppServer.Entities;

namespace EventsAppServer.DbEndpoints
{
    public class GroupUserEndpoints
    {
        private readonly AppContext _context;
        public GroupUserEndpoints(AppContext context)
        {
            _context = context;
        }
        public void CreateGroupUser(GroupUser groupUser)
        {
            _context.Add(groupUser);
            _context.SaveChanges();
        }
        public List<GroupUser> ReadAllGroupUsers()
        {
            return _context.GroupUsers.ToList();
        }
        public void UpdateGroupUser(GroupUser groupUser)
        {
            IEnumerable<GroupUser> items =
                from g in _context.GroupUsers
                where g.Id == groupUser.Id
                select groupUser;
            GroupUser? item = items.FirstOrDefault();
            item.UserId = groupUser.Id;
            item.GroupId = groupUser.GroupId;
            _context.SaveChanges();
            
        }
        public void DeleteGroupUser(Guid id)
        {
            IEnumerable<GroupUser> items =
                from g in _context.GroupUsers
                where g.Id == id
                select g;
            GroupUser? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}