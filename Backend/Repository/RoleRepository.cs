using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private RoleEndpoints roleEndpoints;

        public RoleRepository(RoleEndpoints roleEndpoints) : base()
        {
            this.roleEndpoints = roleEndpoints;
        }

        public bool Add(Guid key, Role role)
        {
            roleEndpoints.CreateRole(role);
            return true;
        }

        public bool Remove(Guid key)
        {
            roleEndpoints.DeleteRole(key);
            return true;
        }

        public Role? Get(Guid key)
        {
            return roleEndpoints.ReadRole().Find((role) => role.Id == key);
        }

        public IEnumerable<Role> GetAll()
        {
            return roleEndpoints.ReadRole();
        }

        public bool Contains(Guid key)
        {
            return roleEndpoints.ReadRole().Exists((role) => role.Id == key);
        }

        public bool Update(Guid key, Role value)
        {
            roleEndpoints.UpdateRole(key, value);
            return true;
        }
    }
}