using System;
using System.ComponentModel;
using Xamarin.Forms;
using emtori.Models;

namespace emtori
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
    

        private async void Start_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Select difficulty", "Cansel", null, "Easy", "Medium", "Hard");
            switch (action)
            {
                case "Easy":
                    await Navigation.PushAsync(new GamePage(Difficulty.EASY));
                    break;
                case "Medium":
                    await Navigation.PushAsync(new GamePage(Difficulty.MEDIUM));
                    break;
                case "Hard":
                    await Navigation.PushAsync(new GamePage(Difficulty.HARD));
                    break;
            }
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
