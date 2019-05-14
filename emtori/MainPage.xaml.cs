using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace emtori
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            string action = await DisplayActionSheet("Select difficulty", "Cansel", null, "Easy", "Medium", "Hard");
            switch (action)
            {
                case "Easy":
                    await Navigation.PushAsync(new GamePage(4));
                    break;
                case "Medium":
                    await Navigation.PushAsync(new GamePage(5));
                    break;
                case "Hard":
                    await Navigation.PushAsync(new GamePage(6));
                    break;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
