using System;
using CocosSharp;
using emtori.ViewModels;

namespace emtori
{
    public class GameScene : CCScene
    {

        private GameFieldView gameFieldView;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            gameFieldView = new GameFieldView();
            this.AddChild(gameFieldView);
        }
    }
}
