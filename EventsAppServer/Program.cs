using EventsAppServer.Adapters;
using EventsAppServer.Attributes;
using EventsAppServer.Entities;

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsAppServer.DbEndpoints;
using EventsAppServer.Endpoints;


var MyAllowSpecificOrigins = "randomStringTheySay";
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
// builder.Services.AddDbContext<AppContext>(options => options.UseSqlServer(connectionString));
//
if (connectionString == null)
{
    Console.WriteLine("No connection string found");
    return;
}
builder.Services.AddSingleton(new DataBaseAdapter<UserInfo>(connectionString));
builder.Services.AddSingleton(new DataBaseAdapter<EventInfo>(connectionString));
builder.Services.AddSingleton(new DataBaseAdapter<ReportInfo>(connectionString));
builder.Services.AddSingleton(new DataBaseAdapter<ReviewInfo>(connectionString));
builder.Services.AddSingleton(new DataBaseAdapter<AdminInfo>(connectionString));
builder.Services.AddSingleton(new DataBaseAdapter<UserEventRelationInfo>(connectionString));
builder.Services.AddSingleton(new DataBaseAdapter<DonationInfo>(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});


var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

DataBaseAdapter<UserInfo> UsersDataBaseAdapter = app.Services.GetRequiredService<DataBaseAdapter<UserInfo>>() ?? throw new Exception();
DataBaseAdapter<EventInfo> EventsDataBaseAdapter = app.Services.GetRequiredService<DataBaseAdapter<EventInfo>>() ?? throw new Exception();
DataBaseAdapter<ReportInfo> ReportsDataBaseAdapter = app.Services.GetRequiredService<DataBaseAdapter<ReportInfo>>() ?? throw new Exception();
DataBaseAdapter<ReviewInfo> ReviewsDataBaseAdapter = app.Services.GetRequiredService<DataBaseAdapter<ReviewInfo>>() ?? throw new Exception();
DataBaseAdapter<AdminInfo> AdminsDataBaseAdapter = app.Services.GetRequiredService<DataBaseAdapter<AdminInfo>>() ?? throw new Exception();
DataBaseAdapter<UserEventRelationInfo> UserEventRelationsDataBaseAdapter = app.Services.GetRequiredService<DataBaseAdapter<UserEventRelationInfo>>() ?? throw new Exception();
DataBaseAdapter<DonationInfo> DonationsDataBaseAdapter = app.Services.GetRequiredService<DataBaseAdapter<DonationInfo>>() ?? throw new Exception();


// AppContext appContrext = app.Services.GetService<AppContext>();
AppContext appContrext = new AppContext(new DbContextOptionsBuilder<AppContext>().UseSqlServer(connectionString).Options);
AwardEndpoint awardEndpoint = new AwardEndpoint(appContrext);
TextPostEndpoint textPostEndpoint = new TextPostEndpoint(appContrext);
UserEndpoint userEndpoint = new UserEndpoint(appContrext);
ReportEndpoint reportEndpoint = new ReportEndpoint(appContrext);

#region GetAll
app.MapGet("/GetAll/Users", () =>
{
    Console.WriteLine("GetAll/Users");
    List<UserInfo> list = new List<UserInfo>();

    return appContrext.Users.ToList();
});

app.MapGet("/GetAll/Events", () =>
{
    Console.WriteLine("GetAll/Events");
    List<EventInfo> list = new List<EventInfo>();

    return appContrext.Events.ToList();
});

app.MapGet("/GetAll/Reports", () =>
{
    Console.WriteLine("GetAll/Reports");
    List<ReportInfo> list = new List<ReportInfo>();

    return appContrext.Reports.ToList();
});

app.MapGet("/GetAll/Reviews", () =>
{
    Console.WriteLine("GetAll/Reviews");
    List<ReviewInfo> list = new List<ReviewInfo>();

    return appContrext.Reviews.ToList();
});

app.MapGet("/GetAll/Admins", () =>
{
    Console.WriteLine("GetAll/Admins");
    List<AdminInfo> list = new List<AdminInfo>();

    return appContrext.Admins.ToList();
});

app.MapGet("/GetAll/UserEventRelations", () =>
{
    Console.WriteLine("GetAll/UserEventRelations");
    List<UserEventRelationInfo> list = new List<UserEventRelationInfo>();

    return appContrext.UserEventRelations.ToList();
});

app.MapGet("/GetAll/Donations", () =>
{
    Console.WriteLine("GetAll/Donations");
    List<DonationInfo> list = new List<DonationInfo>();

    return appContrext.Donations.ToList();
});
#endregion

#region GetByGUID
app.MapGet("/Get/Users/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/User");
    return appContrext.Users.Where(x => x.GUID == GUID).FirstOrDefault();
});

app.MapGet("/Get/Events/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/Event");
    return appContrext.Events.Where(x => x.GUID == GUID).FirstOrDefault();
});

app.MapGet("/Get/Reports/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Get/Report");
    return appContrext.Reports.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
});

app.MapGet("/Get/Reviews/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Get/Review");
    return appContrext.Reviews.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
});

app.MapGet("/Get/Admins/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/Admin");
    return appContrext.Admins.Where(x => x.GUID == GUID).FirstOrDefault();
});

app.MapGet("/Get/UserEventRelations/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Get/UserEventRelation");
    return appContrext.UserEventRelations.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
});

app.MapGet("/Get/Donations/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/Donation");
    return appContrext.Donations.Where(x => x.GUID == GUID).FirstOrDefault();
});
#endregion

#region Add
app.MapPost("/Add/Users", (UserInfo user) =>
{
    Console.WriteLine("Add/User");
    appContrext.Users.Add(user);
    appContrext.SaveChanges();
    return user;
});

app.MapPost("/Add/Events", (EventInfo eventInfo) =>
{
    Console.WriteLine("Add/Event");
    appContrext.Events.Add(eventInfo);
    appContrext.SaveChanges();
    return eventInfo;
});

app.MapPost("/Add/Reports", (ReportInfo reportInfo) =>
{
    Console.WriteLine("Add/Report");
    appContrext.Reports.Add(reportInfo);
    appContrext.SaveChanges();
    return reportInfo;
});

app.MapPost("/Add/Reviews", (ReviewInfo reviewInfo) =>
{
    Console.WriteLine("Add/Review");
    appContrext.Reviews.Add(reviewInfo);
    appContrext.SaveChanges();
    return reviewInfo;
});

app.MapPost("/Add/Admins", (AdminInfo adminInfo) =>
{
    Console.WriteLine("Add/Admin");
    appContrext.Admins.Add(adminInfo);
    appContrext.SaveChanges();
    return adminInfo;
});

app.MapPost("/Add/UserEventRelations", (UserEventRelationInfo userEventRelationInfo) =>
{
    Console.WriteLine("Add/UserEventRelation");
    appContrext.UserEventRelations.Add(userEventRelationInfo);
    appContrext.SaveChanges();
    return userEventRelationInfo;
});

app.MapPost("/Add/Donations", (DonationInfo donationInfo) =>
{
    Console.WriteLine("Add/Donation");
    appContrext.Donations.Add(donationInfo);
    appContrext.SaveChanges();
    return donationInfo;
});
#endregion

#region Update
app.MapPut("/Update/Users", (UserInfo user) =>
{
    Console.WriteLine("Update/User");
    var item = appContrext.Users.Where(x => x.GUID == user.GUID).FirstOrDefault();
    item.GUID = user.GUID;
    item.Name = user.Name;
    item.Password = user.Password;
    appContrext.SaveChanges();
    return user;
});

app.MapPut("/Update/Events", (EventInfo eventInfo) =>
{
    Console.WriteLine("Update/Event");
    var item = appContrext.Events.Where(x => x.GUID == eventInfo.GUID).FirstOrDefault();
    item.BannerURL = eventInfo.BannerURL;
    item.Description = eventInfo.Description;
    item.EndDate = eventInfo.EndDate;
    item.Location = eventInfo.Location;
    item.EventName = eventInfo.EventName;
    item.StartDate = eventInfo.StartDate;
    item.EntryFee = eventInfo.EntryFee;
    item.MaxParticipants = eventInfo.MaxParticipants;
    appContrext.SaveChanges();
    return eventInfo;
});

app.MapPut("/Update/Reports", (ReportInfo reportInfo) =>
{
    Console.WriteLine("Update/Report");
    var item = appContrext.Reports.Where(x => x.EventGUID == reportInfo.EventGUID && x.UserGUID == reportInfo.UserGUID).FirstOrDefault();
    item.UserGUID = reportInfo.UserGUID;
    item.EventGUID = reportInfo.EventGUID;
    item.ReportTypeValue = reportInfo.ReportTypeValue;
    appContrext.SaveChanges();
    return reportInfo;
});

app.MapPut("/Update/Reviews", (ReviewInfo reviewInfo) =>
{
    Console.WriteLine("Update/Review");
    var item = appContrext.Reviews.Where(x => x.EventGUID == reviewInfo.EventGUID && x.UserGUID == reviewInfo.UserGUID).FirstOrDefault();
    item.UserGUID = reviewInfo.UserGUID;
    item.EventGUID = reviewInfo.EventGUID;
    item.ReviewDescription = reviewInfo.ReviewDescription;
    item.Score = reviewInfo.Score;
    appContrext.SaveChanges();
    return reviewInfo;
});

app.MapPut("/Update/Admins", (AdminInfo adminInfo) =>
{
    Console.WriteLine("Update/Admin");
    var item = appContrext.Admins.Where(x => x.GUID == adminInfo.GUID).FirstOrDefault();
    item.GUID = adminInfo.GUID;
    appContrext.SaveChanges();
    return adminInfo;
});

app.MapPut("/Update/UserEventRelations", (UserEventRelationInfo userEventRelationInfo) =>
{
    Console.WriteLine("Update/UserEventRelation");
    var item = appContrext.UserEventRelations.Where(x => x.EventGUID == userEventRelationInfo.EventGUID && x.UserGUID == userEventRelationInfo.UserGUID).FirstOrDefault();
    item.Status = userEventRelationInfo.Status;
    item.UserGUID = userEventRelationInfo.UserGUID;
    item.EventGUID = userEventRelationInfo.EventGUID;
    appContrext.SaveChanges();
    return userEventRelationInfo;
});

app.MapPut("/Update/Donations", (DonationInfo donationInfo) =>
{
    Console.WriteLine("Update/Donation");
    var item = appContrext.Donations.Where(x => x.GUID == donationInfo.GUID).FirstOrDefault();
    item.GUID = donationInfo.GUID;
    item.Amount = donationInfo.Amount;
    item.UserGUID = donationInfo.UserGUID;
    item.EventGUID = donationInfo.EventGUID;
    appContrext.SaveChanges();
    return donationInfo;
});
#endregion

#region Delete
app.MapDelete("/Delete/Users/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/User");
    var user = appContrext.Users.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Users.Remove(user);
    appContrext.SaveChanges();
    return user;
});

app.MapDelete("/Delete/Events/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/Event");
    var eventInfo = appContrext.Events.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Events.Remove(eventInfo);
    appContrext.SaveChanges();
    return eventInfo;
});

app.MapDelete("/Delete/Reports/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Delete/Report");
    var reportInfo = appContrext.Reports.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
    appContrext.Reports.Remove(reportInfo);
    appContrext.SaveChanges();
    return reportInfo;
});

app.MapDelete("/Delete/Reviews/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Delete/Review");
    var reviewInfo = appContrext.Reviews.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
    appContrext.Reviews.Remove(reviewInfo);
    appContrext.SaveChanges();
    return reviewInfo;
});

app.MapDelete("/Delete/Admins/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/Admin");
    var adminInfo = appContrext.Admins.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Admins.Remove(adminInfo);
    appContrext.SaveChanges();
    return adminInfo;
});

app.MapDelete("/Delete/UserEventRelations/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Delete/UserEventRelation");
    var userEventRelationInfo = appContrext.UserEventRelations.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
    appContrext.UserEventRelations.Remove(userEventRelationInfo);
    appContrext.SaveChanges();
    return userEventRelationInfo;
});

app.MapDelete("/Delete/Donations/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/Donation");
    var donationInfo = appContrext.Donations.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Donations.Remove(donationInfo);
    appContrext.SaveChanges();
    return donationInfo;
});

#endregion

#region Clear
//Full Delete
app.MapDelete("/Clear/Users", () =>
{
    Console.WriteLine("Clear/Users");
    appContrext.Users.RemoveRange(appContrext.Users);
    appContrext.SaveChanges();
    return appContrext.Users.ToList();
});

app.MapDelete("/Clear/Events", () =>
{
    Console.WriteLine("Clear/Events");
    appContrext.Events.RemoveRange(appContrext.Events);
    appContrext.SaveChanges();
    return appContrext.Events.ToList();
});

app.MapDelete("/Clear/Reports", () =>
{
    Console.WriteLine("Clear/Reports");
    appContrext.Reports.RemoveRange(appContrext.Reports);
    appContrext.SaveChanges();
    return appContrext.Reports.ToList();
});

app.MapDelete("/Clear/Reviews", () =>
{
    Console.WriteLine("Clear/Reviews");
    appContrext.Reviews.RemoveRange(appContrext.Reviews);
    appContrext.SaveChanges();
    return appContrext.Reviews.ToList();
});

app.MapDelete("/Clear/Admins", () =>
{
    Console.WriteLine("Clear/Admins");
    appContrext.Admins.RemoveRange(appContrext.Admins);
    appContrext.SaveChanges();
    return appContrext.Admins.ToList();
});

app.MapDelete("/Clear/UserEventRelations", () =>
{
    Console.WriteLine("Clear/UserEventRelations");
    appContrext.UserEventRelations.RemoveRange(appContrext.UserEventRelations);
    appContrext.SaveChanges();
    return appContrext.UserEventRelations.ToList();
});

app.MapDelete("/Clear/Donations", () =>
{
    Console.WriteLine("Clear/Donations");
    appContrext.Donations.RemoveRange(appContrext.Donations);
    appContrext.SaveChanges();
    return appContrext.Donations.ToList();
});

#endregion

#region Contains
app.MapGet("/Contains/Users/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Contains/User");
    return appContrext.Users.Where(x => x.GUID == GUID).Any();
});

app.MapGet("/Contains/Events/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Contains/Event");
    return appContrext.Events.Where(x => x.GUID == GUID).Any();
});

app.MapGet("/Contains/Reports/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Contains/Report");
    return appContrext.Reports.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).Any();
});

app.MapGet("/Contains/Reviews/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Contains/Review");
    return appContrext.Reviews.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).Any();
});

app.MapGet("/Contains/Admins/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Contains/Admin");
    return appContrext.Admins.Where(x => x.GUID == GUID).Any();
});

app.MapGet("/Contains/UserEventRelations/{UserGUID}/{EventGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Contains/UserEventRelation");
    return appContrext.UserEventRelations.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).Any();
});

app.MapGet("/Contains/Donations/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Contains/Donation");
    return appContrext.Donations.Where(x => x.GUID == GUID).Any();
});

#endregion

#region Award
app.MapGet("/award", () =>
{
    Console.WriteLine("GetAll/Awards");

    return awardEndpoint.ReadAwards();
});

app.MapPost("/award/add", (Award award) =>
{
    Console.WriteLine("Add/Award");
    awardEndpoint.CreateAward(award);
});

app.MapPut("/award/update", (Award award) =>
{
    Console.WriteLine("Update/Award");
    awardEndpoint.UpdateAward(award);
});

app.MapDelete("/award/delete/{id}", (Guid GUID) =>
{
    Console.WriteLine("Delete/Award");
    awardEndpoint.DeleteAward(GUID);
});
#endregion

#region TextPost
app.MapGet("/textPost", () =>
{
    Console.WriteLine("GetAll/TextPosts");
    textPostEndpoint.ReadTextPosts();
});

app.MapPost("/textPost/add", (TextPost textPost) =>
{
    Console.WriteLine("Add/TextPost");
    textPostEndpoint.CreateTextPost(textPost);
});

app.MapPut("/textPost/update", (TextPost textPost) =>
{
    Console.WriteLine("Update/TextPost");
    textPostEndpoint.UpdateTextPost(textPost);
});

app.MapDelete("/textPost/delete/{id}", (Guid GUID) =>
{
    Console.WriteLine("Delete/TextPost");
    textPostEndpoint.DeleteTextPost(GUID);
});
#endregion

#region UserInfo
app.MapGet("/user", () =>
{
    Console.WriteLine("GetAll/UserInfo");
    return userEndpoint.ReadUsers();
});

app.MapPost("/user/add", (UserInfo user) =>
{
    Console.WriteLine("Add/UserInfo");
    userEndpoint.CreateUser(user);
});

app.MapPut("/user/update", (UserInfo user) =>
{
    Console.WriteLine("Update/UserInfo");
    userEndpoint.UpdateUser(user);
});

app.MapDelete("/user/delete/{id}", (Guid GUID) =>
{
    Console.WriteLine("Delete/UserInfo");
    userEndpoint.DeleteUser(GUID);
})


#endregion

#region PostReport
app.MapGet("/postReport", () =>
{
    Console.WriteLine("GetAll/PostReports");
    return reportEndpoint.ReadReports();
});

app.MapPost("/postReport/add", (PostReport postReport) =>
{
    Console.WriteLine("Add/PostReport");
    reportEndpoint.CreateReport(postReport);
});

app.MapPut("/postReport/update", (PostReport postReport) =>
{
    Console.WriteLine("Update/PostReport");
    reportEndpoint.UpdateReport(postReport);
});

app.MapDelete("/postReport/delete/{id}", (Guid GUID) =>
{
    Console.WriteLine("Delete/PostReport");
    reportEndpoint.DeleteReport(GUID);
});

#endregion


app.Run();


public class AppContext : DbContext
{
    public DbSet<UserInfo> Users { get; set; }
    public DbSet<EventInfo> Events { get; set; }
    public DbSet<ReportInfo> Reports { get; set; }
    public DbSet<ReviewInfo> Reviews { get; set; }
    public DbSet<AdminInfo> Admins { get; set; }
    public DbSet<UserEventRelationInfo> UserEventRelations { get; set; }
    public DbSet<DonationInfo> Donations { get; set; }
    public DbSet<TextPost> TextPosts { get; set; }
    public DbSet<PostReport> PostReports { get; set; }

    public DbSet<Award> Awards { get; set; }

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReportInfo>()
              .HasKey(m => new { m.EventGUID, m.UserGUID });
        modelBuilder.Entity<ReviewInfo>()
              .HasKey(m => new { m.EventGUID, m.UserGUID });
        modelBuilder.Entity<UserEventRelationInfo>()
              .HasKey(m => new { m.EventGUID, m.UserGUID });
    }
}
