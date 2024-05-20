using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.GroupFeed;
using Moderation.Model;
using Backend.Service;

namespace Moderation.View;

public class SingleGroupView : ContentView
{
    private IService service;

    public SingleGroupView(IService service, Group group, User? user)
    {
        this.service = service;

        if (user == null)
        {
            return;
        }

        var userIsInGroup = group.Creator.Id == user.Id;
        var label = new Label
        {
            Margin = 5,
            Padding = 5,
            VerticalTextAlignment = TextAlignment.Start,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = group.Name
        };
        var viewOrJoinButton = new Button
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = userIsInGroup ? "View" : "Join",
        };
        viewOrJoinButton.Clicked += (s, e) =>
        {
            if (userIsInGroup)
            {
                CurrentSession.GetInstance().LookIntoGroup(group);
                List<TextPost> posts = service.GetPostsOfAuthorsInGivenGroup(group);
                GroupFeedView nextPage = new (posts);
                Navigation.PushAsync(nextPage);
            }
            else
            {
                Navigation.PushAsync(new GroupEntryForm.GroupEntryForm(
                    [
                    new TextQuestion("What would you like to be when yoyu grow up?"),
                    new SliderQuestion("How much do you want it?", 0, 100),
                    new RadioQuestion("Pick your favourite farmyard animal:", ["duck", "cow", "pig"])
                    ]));
            }
        };
        var reportButton = new Button
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Text = "reports",
        };
        reportButton.Clicked += (s, e) =>
        {
            CurrentSession.GetInstance().LookIntoGroup(group);
            Navigation.PushAsync(new ReportListView.ReportListView(service, group));
        };
        var joinRequestButton = new Button
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Text = "requests",
        };
        joinRequestButton.Clicked += (s, e) =>
        {
            CurrentSession.GetInstance().LookIntoGroup(group);
            Navigation.PushAsync(new JoinRequestView.JoinRequestListView(service, group));
        };
        if (userIsInGroup)
        {
            Content = new HorizontalStackLayout
            {
                Margin = 5,
                Padding = 5,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                label,
                viewOrJoinButton,
                reportButton,
                joinRequestButton
            }
            };
        }
        else
        {
            Content = new HorizontalStackLayout
            {
                Margin = 5,
                Padding = 5,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                label,
                viewOrJoinButton,
            }
            };
        }
    }
}