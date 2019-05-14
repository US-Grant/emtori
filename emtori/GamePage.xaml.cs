using System;
using System.Collections.Generic;
using CocosSharp;
using Xamarin.Essentials;
using emtori.Models;

using Xamarin.Forms;

namespace emtori
{
    public partial class GamePage : ContentPage
    {

        private int screenWidth = 1080;
        private int screenHeight = 1920;

        private GameScene gameScene;

        private Difficulty difficulty;

        public GamePage(Difficulty difficulty)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            CocosView.ViewCreated += CocosView_ViewCreated;

        }

        private void CocosView_ViewCreated(object sender, EventArgs e)
        {
            if (sender is CCGameView gameView)
            {
                gameView.DesignResolution = new CCSizeI(screenWidth, screenHeight);
                gameView.ResolutionPolicy = CCViewResolutionPolicy.FixedHeight;
                gameScene = new GameScene(gameView, difficulty, Preferences.Get("emoji", false));
                gameView.RunWithScene(gameScene);
                gameScene.Done += OnGameDone;
            }
        }

        private async void OnGameDone(object sender, TimeSpan time)
        {
            await DisplayAlert("Puzzle done!", "Your time is: " + time.ToString(), "Ok");
            string key = string.Empty;
            switch (difficulty)
            {
                case Difficulty.EASY:
                    key = "easy";
                    break;
                case Difficulty.MEDIUM:
                    key = "medium";
                    break;
                case Difficulty.HARD:
                    key = "hard";
                    break;
            }
            TimeSpan val = TimeSpan.Zero;
            bool b = TimeSpan.TryParse(Preferences.Get(key, string.Empty), out val);
            if (time < val || b == false)
            {
                Preferences.Set(key, time.ToString());
            }
            await Navigation.PopAsync();
        }

    }
}
