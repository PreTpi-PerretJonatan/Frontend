using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FitFocus.Models;
using FitFocus.Views;
using FitFocus.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms.PlatformConfiguration;
using ZXing;

namespace FitFocus.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.Refreshing();
            base.OnAppearing();
        }

        void RefreshView_Refreshing(System.Object sender, System.EventArgs e)
        {
            _viewModel.Refreshing();
        }

        void AccountButton_Clicked(Object sender, EventArgs e)
        {
            Navigation.ShowPopup(new LogoutPopup());
        }
    }
}
