using System.Collections.ObjectModel;
using EventsApp.Logic.Entities;
using EventsApp.Logic.Managers;

namespace EventsApp;

public partial class UserSharePage : ContentPage
{
    private Guid userGuid;
    private Guid eventGuid;

    public ObservableCollection<UserUi> Users { get; set; }

    public UserSharePage(Guid userGUID, Guid eventGUID)
    {
        this.InitializeComponent();
        this.userGuid = userGUID;

        this.Users = this.GetUsers();
        this.BindingContext = this;
    }

    private ObservableCollection<UserUi> GetUsers()
    {
        List<UserInfo> users = UsersManager.GetAllUsers();
        ObservableCollection<UserUi> usersList = new ObservableCollection<UserUi>();
        foreach (UserInfo user in users)
        {
            usersList.Add(new UserUi(user.Name, "circle1.jpeg"));
        }

        return usersList;
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PopAsync();
    }

    private void InviteButton_Clicked(object sender, EventArgs e)
    {
    }
}
