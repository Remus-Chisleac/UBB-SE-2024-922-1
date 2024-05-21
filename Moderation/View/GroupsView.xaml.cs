using Moderation.CurrentSessionNamespace;
using Moderation.Model;
using Backend.Service;
using EventsApp;
using Moderation.Entities;

namespace Moderation;

public partial class GroupsView : ContentPage
{
    private IService service;

    public GroupsView(IService service)
    {
        this.service = service;
        Content = new StackLayout { HorizontalOptions = LayoutOptions.Fill };
        MakeKids();
    }

    private void MakeKids()
    {
        foreach (Group group in service.GetAllGroups())
        {
            ((StackLayout)Content).Children.Add(new View.SingleGroupView(service, group, CurrentSession.GetInstance().User));
        }

#pragma warning disable SA1000 // Keywords should be spaced correctly
        Button backButton = new()
        {
            Text = "Back",
            HorizontalOptions = LayoutOptions.Fill,
        };
#pragma warning restore SA1000 // Keywords should be spaced correctly
        backButton.Clicked += (s, e) =>
        {
            CurrentSession.GetInstance().LogOut();
            Navigation.PopAsync();
        };
        ((StackLayout)Content).Children.Add(backButton);

#pragma warning disable SA1000 // Keywords should be spaced correctly
        Button eventsButton = new()
        {
            Text = "Go to events",
            HorizontalOptions = LayoutOptions.Fill,
        };
#pragma warning restore SA1000 // Keywords should be spaced correctly
        eventsButton.Clicked += (s, e) =>
        {
            User user = CurrentSession.GetInstance().User;
            Navigation.PushAsync(new MainPage(user.Id, user.Username, user.Password));
        };
        ((StackLayout)Content).Children.Add(eventsButton);
    }
}