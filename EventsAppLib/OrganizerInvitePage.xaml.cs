/*namespace EventsApp;

public partial class OrganizerInvitePage : ContentPage
{
	public OrganizerInvitePage()
	{
		InitializeComponent();
	}
    private void CloseButton_Clicked(object sender, EventArgs e)
    {
    }
}	*/

namespace EventsApp
{
    using System;
    using System.Collections.ObjectModel;
    using Microsoft.Maui.Controls;

    public partial class OrganizerInvitePage : ContentPage
    {
        public ObservableCollection<UserUi> Users { get; set; }

        public OrganizerInvitePage()
        {
            this.InitializeComponent();

            // Initialize the Users collection with some sample data
            this.Users = new ObservableCollection<UserUi>
            {
                new UserUi("User1", "circle1.jpeg"),
                new UserUi("User2", "circle1.jpeg"),
                new UserUi("User3", "circle1.jpeg"),
            };

            // Set the BindingContext of the page to itself
            this.BindingContext = this;
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            // Add your code here to handle the button click event
            // For example, you can close the page:
            this.Navigation.PopModalAsync(); // If this page is displayed modally
            // Or
            // Navigation.PopAsync(); // If this page is pushed onto a navigation stack
        }

        private void InviteButton_Clicked(object sender, EventArgs e)
        {
        }
    }
}
