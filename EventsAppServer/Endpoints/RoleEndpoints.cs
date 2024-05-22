using System.Configuration;

using EventsAppServer.Entities;


namespace EventsAppServer.DbEndpoints
{
    public class RoleEndpoints
    {
        private readonly AppContext _context;

        public RoleEndpoints(AppContext context)
        {
            _context = context;
        }
        public void CreateRole(Role role)
        {
            _context.Add(role);
            _context.SaveChanges();
        }
        public List<Role> ReadRole()
        {
            return _context.Roles.ToList();
        }
        public void UpdateRoleName(Guid roleId, string newName)
        {
           IEnumerable<Role> items =
                from role in _context.Roles
                where roleId == role.Id
                select role;
            Role? item = items.FirstOrDefault();
            item.Name = newName;
            _context.SaveChanges();
        }
        public void UpdateRolePermissions(Guid roleId, List<Permission> newPermissions)
        {
           IEnumerable<Role> items =
                from role in _context.Roles
                where roleId == role.Id
                select role;
            Role? item = items.FirstOrDefault();
            item.Permissions = newPermissions;
            _context.SaveChanges();
        }

        public void DeleteRole(Guid roleId)
        {
            IEnumerable<Role> items =
                from role in _context.Roles
                where role.Id == roleId 
                select role;
            Role? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}