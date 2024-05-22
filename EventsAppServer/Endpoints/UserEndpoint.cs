using EventsAppServer.Entities;

namespace EventsAppServer.Endpoints
{
    public class UserEndpoint
    {
        private readonly AppContext _context;

        public UserEndpoint(AppContext context)
        {
            _context = context;
        }

        public void CreateUser(UserInfo user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public List<UserInfo> ReadUsers()
        {
            return _context.Users.ToList();
        }
           
        public void UpdateUser(UserInfo newUser)
        {
            IEnumerable<UserInfo> items = 
                from user in _context.Users
                where user.GUID == newUser.GUID
                select user;

            UserInfo? item = items.FirstOrDefault();
            item.Name = newUser.Name;
            item.Password = newUser.Password;
            _context.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            IEnumerable<UserInfo> items =
                from user in _context.Users
                where user.GUID == id
                select user;

            UserInfo? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }


    }
}
