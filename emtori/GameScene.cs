using System;
using CocosSharp;
using emtori.Models;
using emtori.ViewModels;

namespace emtori
{
    public class GameScene : CCScene
    {

        private GameFieldView gameFieldView;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            gameFieldView = new GameFieldView(new GameField(5));
            this.AddChild(gameFieldView);
            
        }
    }
}
