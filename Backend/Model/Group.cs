using System.Text.Json.Serialization;
using Moderation.Entities;
using Moderation.Repository;

namespace Moderation.Model
{
    public class Group : IHasID
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("creatorId")]
        public Guid CreatorId { get; set; }
        [JsonPropertyName("creator")]
        public User Creator { get; set; }
        [JsonPropertyName("groupEntryQuestions")]
        public QuestionRepository GroupEntryQuestions { get; }
        // public RoleRepository Roles { get; }
        [JsonPropertyName("groupMembers")]
        public Dictionary<User, Role> GroupMembers { get; }

        public Group()
        {
        }
        public Group(string name, string description, User creator)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Creator = creator;
            GroupEntryQuestions = new ();
            // Roles = new ();
            GroupMembers = [];
            var arrayOfAllPermissions = Enum.GetValues(typeof(Permission));
            var listOfAllPermissions = new List<Permission>(arrayOfAllPermissions.Cast<Permission>());
            Role creatorRole = new ("Creator", listOfAllPermissions);
            // Roles.Add(creatorRole.Id, creatorRole);
            GroupMembers.Add(creator, creatorRole);
        }
        public Group(Guid id, string name, string description, User creator)
        {
            Id = id;
            Name = name;
            Description = description;
            Creator = creator;
            GroupEntryQuestions = new ();
            // Roles = new ();
            GroupMembers = [];
            var arrayOfAllPermissions = Enum.GetValues(typeof(Permission));
            var listOfAllPermissions = new List<Permission>(arrayOfAllPermissions.Cast<Permission>());
            Role creatorRole = new ("Creator", listOfAllPermissions);
            // Roles.Add(creatorRole.Id, creatorRole);
            GroupMembers.Add(creator, creatorRole);
        }
    }
}