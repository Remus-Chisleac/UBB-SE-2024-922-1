using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Model;

namespace Moderation.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private GroupEndpoints groupEndpoints;

        public GroupRepository(GroupEndpoints endpoints) : base()
        {
            groupEndpoints = endpoints;
        }

        public bool Add(Guid key, Group value)
        {
            groupEndpoints.CreateGroup(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return groupEndpoints.ReadAllGroups().Exists(group => group.Id == key);
        }

        public Group? Get(Guid key)
        {
            return groupEndpoints.ReadAllGroups().Find(group => group.Id == key);
        }

        public IEnumerable<Group> GetAll()
        {
            return groupEndpoints.ReadAllGroups();
        }

        public bool Remove(Guid key)
        {
            groupEndpoints.DeleteGroup(key);
            return true;
        }

        public bool Update(Guid key, Group value)
        {
            groupEndpoints.UpdateGroup(value);
            return true;
        }
        public Guid? GetGuidByName(string name)
        {
            return groupEndpoints.ReadAllGroups().Find(group => group.Name == name)?.Id;
        }
    }
}
