using System;
using CocosSharp;
using emtori.Models;
using emtori.ViewModels;

namespace emtori
{
    public class GameScene : CCScene
    {

        private GameFieldView gameFieldView;

        private int difficulty;

        public GameScene(CCGameView gameView, int difficulty, bool isEmoji) : base(gameView)
        {
            this.difficulty = difficulty;
            gameFieldView = new GameFieldView(new GameField(difficulty), isEmoji);
            this.AddChild(gameFieldView);
        }

        public void SaveGame()
        {

        }

        public void LoadGame()
        {

        }

    }
}
