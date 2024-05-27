using System.Text.Json.Serialization;

namespace Moderation.Entities
{
    public class Award : IHasID
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        public enum AwardType
        {
            Bronze,
            Silver,
            Gold
        }
        [JsonPropertyName("awardTypeObj")]
        public AwardType AwardTypeObj { get; set; }

        public Award()
        {
        }

        public Award(Guid id, AwardType awardTypeObj)
        {
            Id = id;
            AwardTypeObj = awardTypeObj;
        }
    }
}
