using System;
using System.Collections.Generic;
using CocosSharp;

using Xamarin.Forms;

namespace emtori
{
    public partial class GamePage : ContentPage
    {

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
                gameView.DesignResolution = new CCSizeI(100, 100);
                gameScene = new GameScene(gameView);
                gameView.RunWithScene(gameScene);
            }
        }

    }
}
