﻿using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class JoinRequestAnswerForOneQuestionEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            string sql = "INSERT INTO JoinRequestMessage VALUES (@JoinRequestId,@[Key], @[Value])";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static List<JoinRequestAnswerToOneQuestion> ReadQuestion()
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return [];
            }
            List<JoinRequestAnswerToOneQuestion> allAnswersToAllQuestions = [];

            string sql = "SELECT * FROM JoinRequestMessage";
            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                JoinRequestAnswerToOneQuestion qAndA = new (reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                allAnswersToAllQuestions.Add(qAndA);
            }

            return allAnswersToAllQuestions;
        }
        public static void UpdateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            string sql = "UPDATE JoinRequestMessage SET [Value]=@[Value] WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static void DeleteQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            string sql = "DELETE FROM JoinRequestMessage WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.ExecuteNonQuery();
        }
    }
}
