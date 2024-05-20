using EventsAppServer.Adapters;
using EventsAppServer.Attributes;
using EventsAppServer.Entities;

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var MyAllowSpecificOrigins = "randomStringTheySay";
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

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
                          policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
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


AppContrext appContrext = new AppContrext();

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
app.MapGet("/Get/User/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/User");
    return appContrext.Users.Where(x => x.GUID == GUID).FirstOrDefault();
});

app.MapGet("/Get/Event/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/Event");
    return appContrext.Events.Where(x => x.GUID == GUID).FirstOrDefault();
});

app.MapGet("/Get/Report/{EventGUID}/{UserGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Get/Report");
    return appContrext.Reports.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
});

app.MapGet("/Get/Review/{EventGUID}/{UserGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Get/Review");
    return appContrext.Reviews.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
});

app.MapGet("/Get/Admin/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/Admin");
    return appContrext.Admins.Where(x => x.GUID == GUID).FirstOrDefault();
});

app.MapGet("/Get/UserEventRelation/{EventGUID}/{UserGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Get/UserEventRelation");
    return appContrext.UserEventRelations.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
});

app.MapGet("/Get/Donation/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Get/Donation");
    return appContrext.Donations.Where(x => x.GUID == GUID).FirstOrDefault();
});
#endregion

#region Add
app.MapPost("/Add/User", (UserInfo user) =>
{
    Console.WriteLine("Add/User");
    appContrext.Users.Add(user);
    appContrext.SaveChanges();
    return user;
});

app.MapPost("/Add/Event", (EventInfo eventInfo) =>
{
    Console.WriteLine("Add/Event");
    appContrext.Events.Add(eventInfo);
    appContrext.SaveChanges();
    return eventInfo;
});

app.MapPost("/Add/Report", (ReportInfo reportInfo) =>
{
    Console.WriteLine("Add/Report");
    appContrext.Reports.Add(reportInfo);
    appContrext.SaveChanges();
    return reportInfo;
});

app.MapPost("/Add/Review", (ReviewInfo reviewInfo) =>
{
    Console.WriteLine("Add/Review");
    appContrext.Reviews.Add(reviewInfo);
    appContrext.SaveChanges();
    return reviewInfo;
});

app.MapPost("/Add/Admin", (AdminInfo adminInfo) =>
{
    Console.WriteLine("Add/Admin");
    appContrext.Admins.Add(adminInfo);
    appContrext.SaveChanges();
    return adminInfo;
});

app.MapPost("/Add/UserEventRelation", (UserEventRelationInfo userEventRelationInfo) =>
{
    Console.WriteLine("Add/UserEventRelation");
    appContrext.UserEventRelations.Add(userEventRelationInfo);
    appContrext.SaveChanges();
    return userEventRelationInfo;
});

app.MapPost("/Add/Donation", (DonationInfo donationInfo) =>
{
    Console.WriteLine("Add/Donation");
    appContrext.Donations.Add(donationInfo);
    appContrext.SaveChanges();
    return donationInfo;
});
#endregion

#region Update
app.MapPut("/Update/User", (UserInfo user) =>
{
    Console.WriteLine("Update/User");
    appContrext.Users.Update(user);
    appContrext.SaveChanges();
    return user;
});

app.MapPut("/Update/Event", (EventInfo eventInfo) =>
{
    Console.WriteLine("Update/Event");
    appContrext.Events.Update(eventInfo);
    appContrext.SaveChanges();
    return eventInfo;
});

app.MapPut("/Update/Report", (ReportInfo reportInfo) =>
{
    Console.WriteLine("Update/Report");
    appContrext.Reports.Update(reportInfo);
    appContrext.SaveChanges();
    return reportInfo;
});

app.MapPut("/Update/Review", (ReviewInfo reviewInfo) =>
{
    Console.WriteLine("Update/Review");
    appContrext.Reviews.Update(reviewInfo);
    appContrext.SaveChanges();
    return reviewInfo;
});

app.MapPut("/Update/Admin", (AdminInfo adminInfo) =>
{
    Console.WriteLine("Update/Admin");
    appContrext.Admins.Update(adminInfo);
    appContrext.SaveChanges();
    return adminInfo;
});

app.MapPut("/Update/UserEventRelation", (UserEventRelationInfo userEventRelationInfo) =>
{
    Console.WriteLine("Update/UserEventRelation");
    appContrext.UserEventRelations.Update(userEventRelationInfo);
    appContrext.SaveChanges();
    return userEventRelationInfo;
});

app.MapPut("/Update/Donation", (DonationInfo donationInfo) =>
{
    Console.WriteLine("Update/Donation");
    appContrext.Donations.Update(donationInfo);
    appContrext.SaveChanges();
    return donationInfo;
});
#endregion

#region Delete
app.MapDelete("/Delete/User/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/User");
    var user = appContrext.Users.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Users.Remove(user);
    appContrext.SaveChanges();
    return user;
});

app.MapDelete("/Delete/Event/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/Event");
    var eventInfo = appContrext.Events.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Events.Remove(eventInfo);
    appContrext.SaveChanges();
    return eventInfo;
});

app.MapDelete("/Delete/Report/{EventGUID}/{UserGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Delete/Report");
    var reportInfo = appContrext.Reports.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
    appContrext.Reports.Remove(reportInfo);
    appContrext.SaveChanges();
    return reportInfo;
});

app.MapDelete("/Delete/Review/{EventGUID}/{UserGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Delete/Review");
    var reviewInfo = appContrext.Reviews.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
    appContrext.Reviews.Remove(reviewInfo);
    appContrext.SaveChanges();
    return reviewInfo;
});

app.MapDelete("/Delete/Admin/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/Admin");
    var adminInfo = appContrext.Admins.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Admins.Remove(adminInfo);
    appContrext.SaveChanges();
    return adminInfo;
});

app.MapDelete("/Delete/UserEventRelation/{EventGUID}/{UserGUID}", (Guid EventGUID, Guid UserGUID) =>
{
    Console.WriteLine("Delete/UserEventRelation");
    var userEventRelationInfo = appContrext.UserEventRelations.Where(x => x.EventGUID == EventGUID && x.UserGUID == UserGUID).FirstOrDefault();
    appContrext.UserEventRelations.Remove(userEventRelationInfo);
    appContrext.SaveChanges();
    return userEventRelationInfo;
});

app.MapDelete("/Delete/Donation/{GUID}", (Guid GUID) =>
{
    Console.WriteLine("Delete/Donation");
    var donationInfo = appContrext.Donations.Where(x => x.GUID == GUID).FirstOrDefault();
    appContrext.Donations.Remove(donationInfo);
    appContrext.SaveChanges();
    return donationInfo;
});

#endregion



app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class AppContrext : DbContext
{
    public DbSet<UserInfo> Users { get; set; }
    public DbSet<EventInfo> Events { get; set; }
    public DbSet<ReportInfo> Reports { get; set; }
    public DbSet<ReviewInfo> Reviews { get; set; }
    public DbSet<AdminInfo> Admins { get; set; }
    public DbSet<UserEventRelationInfo> UserEventRelations { get; set; }
    public DbSet<DonationInfo> Donations { get; set; }
    public AppContrext() : base()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:localhost,1433;Initial Catalog=ISS_EventsApp_EF;User ID=ISS;Password=iss;TrustServerCertificate=True;MultiSubnetFailover=True");
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
