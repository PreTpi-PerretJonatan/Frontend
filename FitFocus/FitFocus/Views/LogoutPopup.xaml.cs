using System;
using System.Collections.Generic;
using FitFocus.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace FitFocus.Views
{	
	public partial class LogoutPopup : Popup
	{
		LogoutPopupViewModel _viewModel;

		public LogoutPopup ()
		{
			BindingContext = _viewModel = new LogoutPopupViewModel();
            InitializeComponent ();
		}

        void LogoutButton_Clicked(System.Object sender, System.EventArgs e)
        {
			App.CurrentUser = new Models.User();
			Device.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

				// Close the popup
				Dismiss(null);
			});
        }

        void PreferencesButton_Clicked(System.Object sender, System.EventArgs e)
        {

        }
    }
}

