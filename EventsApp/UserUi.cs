namespace EventsApp
{
    public class UserUi(string username, string profilePicture)
    {
        public string Username { get; set; } = username;

        public string ProfilePicture { get; set; } = profilePicture;
    }
}