using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class GroupUserRepository : IGroupUserRepository
    {
        private GroupUserEndpoints groupUserEndpoints;

        public GroupUserRepository(GroupUserEndpoints groupUserEndpoints) : base()
        {
            this.groupUserEndpoints = groupUserEndpoints;
        }

        public bool Add(Guid key, GroupUser value)
        {
            groupUserEndpoints.CreateGroupUser(value);
            return true;
        }
        public bool Contains(Guid key)
        {
            return groupUserEndpoints.ReadAllGroupUsers().Exists(groupUser => groupUser.Id == key);
        }

        public GroupUser? Get(Guid key)
        {
            return groupUserEndpoints.ReadAllGroupUsers().Find(groupUser => groupUser.Id == key);
        }

        public GroupUser? GetByUserIdAndGroupId(Guid userId, Guid groupId)
        {
            return groupUserEndpoints.ReadAllGroupUsers().Find(groupUser => groupUser.UserId == userId && groupUser.GroupId == groupId);
        }

        public IEnumerable<GroupUser> GetAll()
        {
            return groupUserEndpoints.ReadAllGroupUsers();
        }

        public bool Remove(Guid key)
        {
            groupUserEndpoints.DeleteGroupUser(key);
            return true;
        }
        public bool Update(Guid key, GroupUser value)
        {
            groupUserEndpoints.UpdateGroupUser(value);
            return true;
        }
    }
}
