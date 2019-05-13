using System;
using System.Collections.Generic;
using CocosSharp;
using CocosDenshion;

using Xamarin.Forms;

namespace emtori
{
    public partial class GamePage : ContentPage
    {

        private int screenWidth = 1080;
        private int screenHeight = 1920;

        private GameScene gameScene;

        public GamePage()
        {
            InitializeComponent();
            CocosView.ViewCreated += CocosView_ViewCreated;

        }

        void CocosView_ViewCreated(object sender, EventArgs e)
        {
            if (sender is CCGameView gameView)
            {
                gameView.DesignResolution = new CCSizeI(screenWidth, screenHeight);
                gameView.ResolutionPolicy = CCViewResolutionPolicy.FixedHeight;
                gameScene = new GameScene(gameView);
                gameView.RunWithScene(gameScene);
            }
        }

    }
}
