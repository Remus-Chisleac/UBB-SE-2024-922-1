using System.Data;

namespace EventsAppServer.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EventsAppServer.Attributes;

    [Table("Users")]
    [System.Serializable]
    public class UserInfo
    {
        public const float MINSCORE = 4.0f;

        [Key]
        public Guid GUID { get; set; }
        public string Name { get; set; }
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
    public class EventInfo
    {
        [Key]
        public Guid GUID { get; set; }
        public Guid OrganizerGUID { get; set; }
        public string EventName { get; set; }
        public string Categories { get; set; } // "music, sports, etc."
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BannerURL { get; set; }
        public string LogoURL { get; set; }
        public int AgeLimit { get; set; }
        public float EntryFee { get; set; }
        public EventInfo(DataRow row)
        {
            GUID = (Guid)row["GUID"];
            OrganizerGUID = (Guid)row["OrganizerGUID"];
            EventName = (string)row["EventName"];
            Categories = (string)row["Categories"];
            Location = (string)row["Location"];
            MaxParticipants = (int)row["MaxParticipants"];
            Description = (string)row["Description"];
            StartDate = (DateTime)row["StartDate"];
            EndDate = (DateTime)row["EndDate"];
            BannerURL = (string)row["BannerURL"];
            LogoURL = (string)row["LogoURL"];
            AgeLimit = (int)row["AgeLimit"];
            EntryFee = (float)row["EntryFee"];
        }

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
            this.GUID = Guid.NewGuid();
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
    public class UserEventRelationInfo
    {
        public Guid UserGUID { get; set; }
        public Guid EventGUID { get; set; }
        public string Status { get; set; }

        public UserEventRelationInfo(DataRow row)
        {
            UserGUID = (Guid)row["UserGUID"];
            EventGUID = (Guid)row["EventGUID"];
            Status = (string)row["Status"];
        }

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
    public class ReportInfo
    {
        public Guid UserGUID { get; set; }
        public Guid EventGUID { get; set; }

        public ReportType ReportTypeValue { get; set; }

        public ReportInfo(DataRow row)
        {
            UserGUID = (Guid)row["UserGUID"];
            EventGUID = (Guid)row["EventGUID"];
            ReportTypeValue = (ReportType)row["ReportTypeValue"];
        }

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
    public class ReviewInfo
    {
        public Guid UserGUID { get; set; }
        public Guid EventGUID { get; set; }
        public float Score { get; set; }
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
    public class ExpenseInfo(Guid guid, Guid eventGUID, string expenseName, float cost)
    {
        [Key]
        public Guid GUID = guid;
        public Guid EventGUID = eventGUID;
        public string ExpenseName = expenseName;
        public float Cost = cost;
    }

    [Table("Donations")]
    [System.Serializable]
    public class DonationInfo
    {
        [Key]
        public Guid GUID { get; set; }
        public Guid EventGUID { get; set; }
        public Guid UserGUID { get; set; }
        public float Amount { get; set; }

        public DonationInfo(DataRow row)
        {
            GUID = (Guid)row["GUID"];
            EventGUID = (Guid)row["EventGUID"];
            UserGUID = (Guid)row["UserGUID"];
            Amount = (float)row["Amount"];
        }

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

    public class AdminInfo
    {
        [Key]
        public Guid GUID { get; set; }

        public AdminInfo()
        {
        }
        public AdminInfo(Guid guid)
        {
            this.GUID = guid;
        }
    }
}
