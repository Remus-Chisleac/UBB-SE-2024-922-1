﻿namespace EventsApp.Logic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class AppStateManager
    {
        public static Guid CurrentUserGUID = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public static string Name = "Janina Gigel";
        public static string Password = "1234";

        // ToDo: add contructor to recive user info from the moderation side of the app
        public static void SetCurrentUser(Guid guid, string name, string password)
        {
            CurrentUserGUID = guid;
            Name = name;
            Password = password;
        }
    }
}
