namespace EventsApp.Logic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using EventsApp.Logic.Attributes;

    [Table("Users")]
    [System.Serializable]
    public struct UserInfo
    {
        public const float MINSCORE = 4.0f;

        [PrimaryKey]
        [JsonPropertyName("guid")]
        public Guid GUID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public UserInfo(Guid guid, string name, string password)
        {
            this.GUID = guid;
            this.Name = name;
            this.Password = password;
        }

        public UserInfo(string name, string password)
        {
            this.GUID = Guid.NewGuid();
            this.Name = name;
            this.Password = password;
        }

        public UserInfo(Guid guid)
        {
            this.GUID = guid;
            this.Name = string.Empty;
            this.Password = string.Empty;
        }

        public UserInfo()
        {
            this.GUID = Guid.NewGuid();
            this.Name = string.Empty;
            this.Password = string.Empty;
        }
    }
    [Table("Events")]
    [System.Serializable]
    public struct EventInfo
    {
        [PrimaryKey]
        [JsonPropertyName("guid")]
        public Guid GUID { get; set; }
        [JsonPropertyName("organizerGUID")]
        public Guid OrganizerGUID { get; set; }
        [JsonPropertyName("eventName")]
        public string EventName { get; set; }
        [JsonPropertyName("categories")]
        public string Categories { get; set; } // "music, sports, etc."
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("maxParticipants")]
        public int MaxParticipants { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("bannerURL")]
        public string BannerURL { get; set; }
        [JsonPropertyName("logoURL")]
        public string LogoURL { get; set; }
        [JsonPropertyName("ageLimit")]
        public int AgeLimit { get; set; }
        [JsonPropertyName("entryFee")]
        public float EntryFee { get; set; }

        public EventInfo(Guid guid, Guid userGUID, string eventName, string categories, string location, int maxParticipants, string description, DateTime startDate, DateTime endDate, string bannerURL, string logoURL, int ageLimit, float entryFee)
        {
            this.GUID = guid;
            this.OrganizerGUID = userGUID;
            this.EventName = eventName;
            this.Categories = categories;
            this.Location = location;
            this.MaxParticipants = maxParticipants;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BannerURL = bannerURL;
            this.LogoURL = logoURL;
            this.AgeLimit = ageLimit;
            this.EntryFee = entryFee;
        }

        public EventInfo(Guid userGUID, string eventName, string categories, string location, int maxParticipants, string description, DateTime startDate, DateTime endDate, string bannerURL, string logoURL, int ageLimit, float entryFee)
        {
            this.GUID = Guid.NewGuid();
            this.OrganizerGUID = userGUID;
            this.EventName = eventName;
            this.Categories = categories;
            this.Location = location;
            this.MaxParticipants = maxParticipants;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BannerURL = bannerURL;
            this.LogoURL = logoURL;
            this.AgeLimit = ageLimit;
            this.EntryFee = entryFee;
        }

        public EventInfo(Guid guid)
        {
            this.GUID = guid;
            this.OrganizerGUID = Guid.Empty;
            this.EventName = string.Empty;
            this.Categories = string.Empty;
            this.Location = string.Empty;
            this.MaxParticipants = 0;
            this.Description = string.Empty;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.BannerURL = string.Empty;
            this.LogoURL = string.Empty;
            this.AgeLimit = 0;
            this.EntryFee = 0;
        }

        public EventInfo()
        {
            this.GUID = Guid.Empty;
            this.OrganizerGUID = Guid.Empty;
            this.EventName = string.Empty;
            this.Categories = string.Empty;
            this.Location = string.Empty;
            this.MaxParticipants = 0;
            this.Description = string.Empty;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.BannerURL = string.Empty;
            this.LogoURL = string.Empty;
            this.AgeLimit = 0;
            this.EntryFee = 0;
        }
    }

    [Table("UserEventRelations")]
    [System.Serializable]

    public struct UserEventRelationInfo
    {
        [PrimaryKey]
        [JsonPropertyName("userGUID")]
        public Guid UserGUID { get; set; }
        [PrimaryKey]
        [JsonPropertyName("eventGUID")]
        public Guid EventGUID { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }

        public UserEventRelationInfo(Guid userGUID, Guid eventGUID, string status)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Status = status;
        }

        public UserEventRelationInfo(Guid userGUID, Guid eventGUID)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Status = string.Empty;
        }

        public UserEventRelationInfo()
        {
            this.UserGUID = Guid.Empty;
            this.EventGUID = Guid.Empty;
            this.Status = string.Empty;
        }
    }

    [Table("Reports")]
    [System.Serializable]
    public struct ReportInfo
    {
        [PrimaryKey]
        [JsonPropertyName("userGUID")]
        public Guid UserGUID;
        [PrimaryKey]
        [JsonPropertyName("eventGUID")]
        public Guid EventGUID;
        [JsonPropertyName("reportTypeValue")]
        public ReportType ReportTypeValue;

        public ReportInfo(Guid userGUID, Guid eventGUID, ReportType reportType)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.ReportTypeValue = reportType;
        }

        public ReportInfo(Guid userGUID, Guid eventGUID)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.ReportTypeValue = ReportType.Harassment;
        }

        public ReportInfo()
        {
            this.UserGUID = Guid.Empty;
            this.EventGUID = Guid.Empty;
            this.ReportTypeValue = ReportType.Harassment;
        }

        public enum ReportType
        {
            /// <summary>
            /// Harassment
            /// </summary>
            Harassment,

            /// <summary>
            /// Nudity
            /// </summary>
            Nudity,

            /// <summary>
            /// Spam
            /// </summary>
            Spam,

            /// <summary>
            /// Violence
            /// </summary>
            Violence,

            /// <summary>
            /// None
            /// </summary>
            None,

            /// <summary>
            /// Fraud
            /// </summary>
            Fraud,

            /// <summary>
            /// Offensive
            /// </summary>
            Offensive,

            /// <summary>
            /// GuidelinesViolations
            /// </summary>
            GuidelinesViolations,
        }
    }

    [Table("Reviews")]
    [System.Serializable]
    public struct ReviewInfo
    {
        [PrimaryKey]
        [JsonPropertyName("userGUID")]
        public Guid UserGUID { get; set; }
        [PrimaryKey]
        [JsonPropertyName("eventGUID")]
        public Guid EventGUID { get; set; }
        [JsonPropertyName("score")]
        public float Score { get; set; }
        [JsonPropertyName("reviewDescription")]
        public string ReviewDescription { get; set; }

        public ReviewInfo(Guid userGUID, Guid eventGUID, float score, string reviewDescription)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Score = score;
            this.ReviewDescription = reviewDescription;
        }

        public ReviewInfo(Guid userGUID, Guid eventGUID)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Score = 0;
            this.ReviewDescription = string.Empty;
        }

        public ReviewInfo()
        {
            this.UserGUID = Guid.Empty;
            this.EventGUID = Guid.Empty;
            this.Score = 0;
            this.ReviewDescription = string.Empty;
        }
    }

    [Table("Expenses")]
    [System.Serializable]
    public struct ExpenseInfo
    {
        [PrimaryKey]
        [JsonPropertyName("guid")]
        public Guid GUID { get; set; }
        [JsonPropertyName("eventGUID")]
        public Guid EventGUID { get; set; }
        [JsonPropertyName("expenseName")]
        public string ExpenseName { get; set; }
        [JsonPropertyName("cost")]
        public float Cost { get; set; }

        public ExpenseInfo(Guid guid, Guid eventGUID, string expenseName, float cost)
        {
            this.GUID = guid;
            this.EventGUID = eventGUID;
            this.ExpenseName = expenseName;
        }

        public ExpenseInfo()
        {
            this.GUID = Guid.Empty;
            this.EventGUID = Guid.Empty;
            this.ExpenseName = string.Empty;
            this.Cost = 0;
        }
    }

    [Table("Donations")]
    [System.Serializable]
    public struct DonationInfo
    {
        [PrimaryKey]
        [JsonPropertyName("guid")]
        public Guid GUID { get; set; }
        [JsonPropertyName("eventGUID")]
        public Guid EventGUID { get; set; }
        [JsonPropertyName("userGUID")]
        public Guid UserGUID { get; set; }
        [JsonPropertyName("amount")]
        public float Amount { get; set; }

        public DonationInfo(Guid guid, Guid eventGUID, Guid userGUID, float amount)
        {
            this.GUID = guid;
            this.EventGUID = eventGUID;
            this.UserGUID = userGUID;
            this.Amount = amount;
        }

        public DonationInfo(Guid eventGUID, Guid userGUID, float amount)
        {
            this.EventGUID = eventGUID;
            this.UserGUID = userGUID;
            this.Amount = amount;
            this.GUID = Guid.NewGuid();
        }

        public DonationInfo(Guid guid)
        {
            this.GUID = guid;
            this.EventGUID = Guid.Empty;
            this.UserGUID = Guid.Empty;
            this.Amount = 0;
        }
        public DonationInfo()
        {
            this.GUID = Guid.NewGuid();
            this.EventGUID = Guid.Empty;
            this.UserGUID = Guid.Empty;
            this.Amount = 0;
        }
    }

    [Table("Admins")]
    [System.Serializable]
    public struct AdminInfo
    {
        [PrimaryKey]
        [JsonPropertyName("guid")]
        public Guid GUID { get; set; }
        public AdminInfo(Guid guid)
        {
            this.GUID = guid;
        }
        public AdminInfo()
        {
            this.GUID = Guid.NewGuid();
        }
    }
}
