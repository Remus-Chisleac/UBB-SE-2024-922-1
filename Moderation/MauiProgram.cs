﻿using Microsoft.Extensions.Logging;
using Moderation.Repository;
using Backend.Repository.Interfaces;
using Backend.Repository;
using Moderation.Serivce;
using Backend.Service;
using EventsApp;
using EventsApp.Logic.Managers;

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
