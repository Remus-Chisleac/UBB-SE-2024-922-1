using Moderation.Entities;
using Moderation.Repository;

namespace Moderation.Test
{
    public class UserRepositoryTests
    {
        private UserRepository repo;

        [SetUp]
        public void Setup()
        {
            repo = new UserRepository(new DbEndpoints.UserEndpoints("Server=tcp:localhost,1433;Initial Catalog=ISS_EventsApp_EF;User ID=ISS;Password=iss;TrustServerCertificate=True;MultiSubnetFailover=True"));
        }

        [Test]
        public void Add_AddUser_ReturnsTrue()
        {
            var user = new User("testUser");

            bool result = repo.Add(user.Id, user);

            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_ExistingUser_ReturnsTrue()
        {
            var user = new User("testUser");
            repo.Add(user.Id, user);

            bool result = repo.Contains(user.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_NonExistingUser_ReturnsFalse()
        {
            bool result = repo.Contains(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [Test]
        public void Get_ExistingUser_ReturnsUser()
        {
            var user = new User("testUser");
            repo.Add(user.Id, user);

            var retrievedUser = repo.Get(user.Id);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(user.Id, retrievedUser.Id);
        }

        [Test]
        public void Get_NonExistingUser_ReturnsNull()
        {
            var retrievedUser = repo.Get(Guid.NewGuid());

            Assert.IsNull(retrievedUser);
        }

        [Test]
        public void GetAll_ReturnsAllUsers()
        {
            var user1 = new User("user1");
            var user2 = new User("user2");
            repo.Add(user1.Id, user1);
            repo.Add(user2.Id, user2);
            int i = repo.GetAll().Count();
            var allUsers = repo.GetAll();

            Assert.AreEqual(i, allUsers.Count());
            Assert.IsTrue(allUsers.Contains(user1));
            Assert.IsTrue(allUsers.Contains(user2));
        }

        [Test]
        public void Remove_ExistingUser_ReturnsTrue()
        {
            var user = new User("testUser");
            repo.Add(user.Id, user);

            bool result = repo.Remove(user.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Update_ExistingUser_ReturnsTrue()
        {
            var user = new User("testUser");
            repo.Add(user.Id, user);
            var updatedUser = new User(user.Id, "updatedUser");

            bool result = repo.Update(user.Id, updatedUser);

            Assert.IsTrue(result);
            Assert.AreEqual("updatedUser", repo.Get(user.Id)?.Username);
        }

        [Test]
        public void GetGuidByName_NonExistingUsername_ReturnsNull()
        {
            var userId = repo.GetGuidByName("nonExistingUser");
            Assert.IsNull(userId);
        }
    }
}
