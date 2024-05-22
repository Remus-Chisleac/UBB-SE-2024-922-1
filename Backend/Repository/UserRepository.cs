using Moderation.Entities;
using Moderation.DbEndpoints;
using Backend.Repository.Interfaces;

namespace Moderation.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserEndpoints userEndpoints;

        public UserRepository(UserEndpoints userEndpoints) : base()
        {
            this.userEndpoints = userEndpoints;
        }

        public bool Add(Guid key, User value)
        {
            userEndpoints.CreateUser(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return userEndpoints.ReadAllUsers().Exists(u => u.Id == key);
        }

        public User? Get(Guid key)
        {
            return userEndpoints.ReadAllUsers().Find(u => u.Id == key);
        }

        public IEnumerable<User> GetAll()
        {
            return userEndpoints.ReadAllUsers();
        }

        public bool Remove(Guid key)
        {
            userEndpoints.DeleteUser(key);
            return true;
        }

        public bool Update(Guid key, User value)
        {
            userEndpoints.UpdateUser(value);
            return true;
        }
        public Guid? GetGuidByName(string name)
        {
            return userEndpoints.ReadAllUsers().Find(u => u.Username == name)?.Id;
        }
    }
}