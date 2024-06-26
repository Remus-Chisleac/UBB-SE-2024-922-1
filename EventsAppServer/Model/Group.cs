﻿using EventsAppServer.Repository;

namespace EventsAppServer.Entities
{
    public class Group : IHasID
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CreatorId { get; set; }
        public UserInfo Creator { get; set; }
        public QuestionRepository GroupEntryQuestions { get; }
        public Dictionary<Guid, Role> Roles { get; }
        public Dictionary<UserInfo, Role> GroupMembers { get; }
        public Group() { }
        public Group(string name, string description, UserInfo creator)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Creator = creator;
            GroupEntryQuestions = new ();
            Roles = new ();
            GroupMembers = [];
            var arrayOfAllPermissions = Enum.GetValues(typeof(Permission));
            var listOfAllPermissions = new List<Permission>(arrayOfAllPermissions.Cast<Permission>());
            Role creatorRole = new ("Creator", listOfAllPermissions);
            Roles.Add(creatorRole.Id, creatorRole);
            GroupMembers.Add(creator, creatorRole);
        }
        public Group(Guid id, string name, string description, UserInfo creator)
        {
            Id = id;
            Name = name;
            Description = description;
            Creator = creator;
            GroupEntryQuestions = new ();
            Roles = new ();
            GroupMembers = [];
            var arrayOfAllPermissions = Enum.GetValues(typeof(Permission));
            var listOfAllPermissions = new List<Permission>(arrayOfAllPermissions.Cast<Permission>());
            Role creatorRole = new ("Creator", listOfAllPermissions);
            Roles.Add(creatorRole.Id, creatorRole);
            GroupMembers.Add(creator, creatorRole);
        }
    }
}