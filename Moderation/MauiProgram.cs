using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moderation.Repository;
using Backend.Repository.Interfaces;
using Backend.Repository;
using Moderation.Serivce;
using Backend.Service;
using EventsApp.Logic.Managers;
using Moderation.DbEndpoints;

namespace Moderation
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("BAHNSCHRIFT.TTF", "Bahnschrift");
                });

            string serverAddress = "http://localhost:5043";

            builder.Services.AddSingleton(new AwardEndpoint(serverAddress));
            builder.Services.AddSingleton(new GroupEndpoints(serverAddress));
            builder.Services.AddSingleton(new GroupUserEndpoints(serverAddress));
            builder.Services.AddSingleton(new JoinRequestAnswerForOneQuestionEndpoints(serverAddress));
            builder.Services.AddSingleton(new JoinRequestEndpoints(serverAddress));
            builder.Services.AddSingleton(new ReportEndpoint(serverAddress));
            builder.Services.AddSingleton(new RoleEndpoints(serverAddress));
            builder.Services.AddSingleton(new TextPostEndpoints(serverAddress));
            builder.Services.AddSingleton(new UserEndpoints(serverAddress));
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IAwardRepository, AwardRepository>();
            builder.Services.AddScoped<IGroupRules, GroupRules>();
            builder.Services.AddScoped<IGroupUserRepository, GroupUserRepository>();
            builder.Services.AddScoped<IJoinRequestAnswerForOneQuestionRepository, JoinRequestAnswerForOneQuestionRepository>();
            builder.Services.AddScoped<IJoinRequestRepository, JoinRequestRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ITextPostRepository, TextPostRepository>();
            builder.Services.AddSingleton<ApplicationState>();
            builder.Services.AddSingleton<IService, Service>();
            builder.Services.AddSingleton<LoginPage>();

            // builder.Services.AddSingleton<MainPage>();
            // builder.Services.AddSingleton<EventPageUser>();
            ManagersInitializer.Initialize();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
