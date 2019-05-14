using System;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace emtori
{
    public partial class RecordsPage : ContentPage
    {
        public RecordsPage()
        {
            InitializeComponent();
        }

        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    SegContent.Children.Clear();
                    SegContent.Children.Add(new Label()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 36,
                        TextColor = Color.Gray,
                        Margin = new Thickness(0, 30, 0, 0),
                        Text = Preferences.Get("easy", "No record") 
                    });
                    break;
                case 1:
                    SegContent.Children.Clear();
                    SegContent.Children.Add(new Label()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 36,
                        TextColor = Color.Gray,
                        Margin = new Thickness(0, 30, 0, 0),
                        Text = Preferences.Get("medium", "No record")
                    });
                    break;
                case 2:
                    SegContent.Children.Clear();
                    SegContent.Children.Add(new Label()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 36,
                        TextColor = Color.Gray,
                        Margin = new Thickness(0, 30, 0, 0),
                        Text = Preferences.Get("hard", "No record")
                    });
                    break;
            }
        }
    }
}
