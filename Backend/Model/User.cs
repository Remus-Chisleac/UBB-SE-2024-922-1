﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class User : IHasID
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = User.GetRandomlyGeneratedPassword();
        }
        public User(Guid id, string username)
        {
            Id = id;
            Username = username;
            Password = User.GetRandomlyGeneratedPassword();
        }
        public User(string username, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
        }
        public User(Guid id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
        private static string GetRandomlyGeneratedPassword()
        {
            Random random = new ();

            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";

            int length = random.Next(8, 24);

            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }
    }
}