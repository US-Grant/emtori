using System;
using CocosSharp;
using emtori.Models;
using emtori.ViewModels;

namespace emtori
{
    public class GameScene : CCScene
    {

        private GameFieldView gameFieldView;

        public GameScene(CCGameView gameView, bool isEmoji) : base(gameView)
        {
            gameFieldView = new GameFieldView(new GameField(5), isEmoji);
            this.AddChild(gameFieldView);
        }

    }
}
