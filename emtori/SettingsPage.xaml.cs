using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace emtori
{
    public partial class SettingsPage : ContentPage
    {
   
        public bool Emoji 
        {
            get { return Preferences.Get("emoji", false); }
        }

        public SettingsPage()
        {
            InitializeComponent();
            this.BindingContext = this;

        }

        private void Handle_OnChanged(object sender, ToggledEventArgs e)
        {
            Preferences.Set("emoji", e.Value);
        }

        private async void Handle_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RecordsPage());
        }
    }
}
