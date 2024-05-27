namespace EventsApp.Logic.Managers
{
    using System;
    using System.Data;
    using System.Reflection;
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Attributes;
    using EventsApp.Logic.Entities;
    using Microsoft.Data.SqlClient;

    public static class ManagersInitializer
    {
        public static string ConnectionString = string.Empty;

        public static void Initialize(bool testDB = false, bool regenerateDB = false)
        {
            SetupDB(regenerateDB, testDB);

            DataBaseAdapter<UserInfo> usersAdapter = new DataBaseAdapter<UserInfo>(ConnectionString);
            DataBaseAdapter<Entities.EventInfo> eventsAdapter = new DataBaseAdapter<Entities.EventInfo>(ConnectionString);
            DataBaseAdapter<ReportInfo> reportsAdapter = new DataBaseAdapter<ReportInfo>(ConnectionString);
            DataBaseAdapter<ReviewInfo> reviewsAdapter = new DataBaseAdapter<ReviewInfo>(ConnectionString);
            DataBaseAdapter<AdminInfo> adminsAdapter = new DataBaseAdapter<AdminInfo>(ConnectionString);
            DataBaseAdapter<UserEventRelationInfo> userEventRelationsAdapter = new DataBaseAdapter<UserEventRelationInfo>(ConnectionString);
            DataBaseAdapter<DonationInfo> donationsAdapter = new DataBaseAdapter<DonationInfo>(ConnectionString);

            UsersManager.Initialize(usersAdapter, adminsAdapter, userEventRelationsAdapter);
            EventsManager.Initialize(eventsAdapter, userEventRelationsAdapter);
            ReportsManager.Initialize(reportsAdapter);
            ReviewsManager.Initialize(reviewsAdapter);
            DonationsManager.Initialize(donationsAdapter);

            if (regenerateDB && !testDB)
            {
                Dummy.Populate();
            }
        }

        public static void SetUser(UserInfo user)
        {
            AppStateManager.SetCurrentUser(user.GUID, user.Name, user.Password);
        }

        public static void SetupDB(bool dropTables = false, bool testDB = false)
        {
            if (!testDB)
            {
                ConnectionString = AppDataInfo.DataBaseConnectionString;
            }
            else
            {
                ConnectionString = AppDataInfo.TestDataBaseConnectionString;
            }

            // ------------------- Create tables -------------------
            if (dropTables)
            {
                string namespaceName = "EventsApp.Logic.Entities";

                var assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes();

                var structsInNamespace = types
                    .Where(t => t.Namespace == namespaceName && t.IsValueType && !t.IsEnum)
                    .ToList();

                foreach (var structType in structsInNamespace)
                {
                    string tableName = structType.GetCustomAttribute<TableAttribute>().TableName;
                    // Drop table if exists
                    string dropTableQuery = $"DROP TABLE IF EXISTS {tableName}";

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(dropTableQuery, connection);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    List<string> columns = new List<string>();

                    // Get only structs with no consts or enums
                    foreach (var field in structType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        bool primaryKey = field.GetCustomAttribute<PrimaryKeyAttribute>() != null;
                        string columnName = field.Name;
                        string columnType = GetFieldType(field.FieldType);

                        string column = $"{columnName} {columnType}";

                        columns.Add(column);
                    }

                    // Add primery keys
                    string primaryKeyString = "PRIMARY KEY (";
                    foreach (var field in structType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (field.GetCustomAttribute<PrimaryKeyAttribute>() != null)
                        {
                            primaryKeyString += field.Name + ", ";
                        }
                    }

                    primaryKeyString = primaryKeyString.Remove(primaryKeyString.Length - 2) + ")";

                    columns.Add(primaryKeyString);

                    string createTableQuery = $"CREATE TABLE {tableName} ({string.Join(", ", columns)})";

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(createTableQuery, connection);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        private static string GetFieldType(Type fieldType)
        {
            if (fieldType == typeof(int))
            {
                return "INT";
            }
            else if (fieldType == typeof(string))
            {
                return "NVARCHAR(MAX)";
            }
            else if (fieldType == typeof(DateTime))
            {
                return "DATETIME";
            }
            else if (fieldType == typeof(float))
            {
                return "FLOAT";
            }
            else if (fieldType == typeof(bool))
            {
                return "BIT";
            }
            else
            {
                return "NVARCHAR(255)";
            }
        }
    }
}
