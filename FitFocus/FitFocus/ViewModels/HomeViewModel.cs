using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FitFocus.Models;
using FitFocus.Views;
using FitFocus.Services;
using System.Collections.Generic;

namespace FitFocus.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Workout> Workouts { get; }

        public string HomePageImage
        {
            get
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    // iPad
                    return App.ApiServer + "/Ressources/Images/Tablet/HomePageImage.png";
                }

                if (Device.Idiom == TargetIdiom.Phone)
                {
                    // iPhone
                    return App.ApiServer + "/Ressources/Images/Phones/HomePageImage.png";
                }
                return "";
            }
        }

        public HomeViewModel()
        {
            Workouts = new ObservableCollection<Workout>();
            Title = "HomePage";
        }

        public async void Refreshing()
        {
            IsBusy = true;
            try
            {
                Workouts.Clear();
                List<Workout> list = await WorkoutsManager.GetHomeWorkouts();
                foreach (Workout workout in list)
                {
                    Workouts.Add(workout);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            IsBusy = false;
        }
    }
}
