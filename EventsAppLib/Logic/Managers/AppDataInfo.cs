namespace EventsApp.Logic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class AppDataInfo
    {
        // public const string AppFolder = "EventsApp";
        // public const string UsersCSV = "UsersCSV.csv";
        // public const string EventsCSV = "EventsCSV.csv";
        // public const string DataBase = "Events.mdf";
        // private static string id = "CloudSAd4cccee0";
        // private static string password = "issevents_123";
        private static string ip = "localhost";
        private static string port = "1433";
        private static string database = "ISS_EventsApp";
        private static string testDataBase = "ISS_EventsApp_tests";
        private static string user = "ISS";
        private static string password = "iss";
        private static string otherConfig = "TrustServerCertificate=True;MultiSubnetFailover=True";

        public static string DataBaseConnectionString = "Server=" + ip + "," + port + ";Database=" + database + ";User Id=" + user + ";Password=" + password + ";" + otherConfig;
        public static string TestDataBaseConnectionString = "Server=" + ip + "," + port + ";Database=" + testDataBase + ";User Id=" + user + ";Password=" + password + ";" + otherConfig;
        // public static string PersistentDataPath => FileSystem.Current.AppDataDirectory;

        // public static string AzureConnectionString => $"Server=tcp:iss-events.database.windows.net,1433;Initial Catalog=EventsDB;Persist Security Info=False;User ID={id};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
