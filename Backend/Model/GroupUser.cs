using Moderation.Model;
using System.Text.Json.Serialization;

namespace Moderation.Entities
{
    public class GroupUser : IHasID
    {
        [JsonPropertyName("description")]
        public Guid Id { get; set; }
        [JsonPropertyName("description")]
        public Guid UserId { get; set; }
        [JsonPropertyName("description")]
        public Guid GroupId { get; set; }
        [JsonPropertyName("description")]
        public int PostScore { get; set; }
        [JsonPropertyName("description")]
        public int MarketplaceScore { get; set; }
        public UserStatus Status { get; set; }
        [JsonPropertyName("description")]
        public Guid RoleId { get; set; }
        public GroupUser(Guid userId, Guid groupId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            GroupId = groupId;
            PostScore = 1;
            MarketplaceScore = 1;
            Status = new (UserRestriction.None, DateTime.Now);
        }
        public GroupUser(Guid id, Guid userId, Guid groupId, int postScore, int marketplaceScore, UserStatus userStatus)
        {
            Id = id;
            UserId = userId;
            GroupId = groupId;
            PostScore = postScore;
            MarketplaceScore = marketplaceScore;
            Status = userStatus;
        }
        public GroupUser(Guid id, Guid userId, Guid groupId, int postScore, int marketplaceScore, UserStatus userStatus, Guid roleId)
        {
            Id = id;
            UserId = userId;
            GroupId = groupId;
            PostScore = postScore;
            MarketplaceScore = marketplaceScore;
            Status = userStatus;
            RoleId = roleId;
        }

        public GroupUser(Guid id, Guid userId, Guid groupID)
        {
            Id = id;
            UserId = userId;
            GroupId = groupID;
            Status = new (UserRestriction.None, DateTime.Now);
        }
    }
}