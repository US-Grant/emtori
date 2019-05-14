using System;
using CocosSharp;
using emtori.Models;
using emtori.ViewModels;
using Xamarin.Forms;

namespace emtori
{
    public class GameScene : CCScene
    {

        private GameFieldView gameFieldView;

        private Difficulty difficulty;

        private bool done = false;

        public event EventHandler<TimeSpan> Done;

        private TimeSpan time = TimeSpan.Zero;

        private CCLabel timeLabel;

        public GameScene(CCGameView gameView, Difficulty difficulty, bool isEmoji) : base(gameView)
        {
            this.difficulty = difficulty;
            gameFieldView = new GameFieldView(new GameField(difficulty), isEmoji);
            this.AddChild(gameFieldView);
            timeLabel = new CCLabel(time.ToString(), "San Fransisco", 80)
            {
                Position = new CCPoint(20, 1820),
                Color = CCColor3B.Gray,
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddleLeft
            };
            gameFieldView.AddChild(timeLabel);
            gameFieldView.Done += (sender, e) => 
            {
                done = true;
                Done?.Invoke(this, time);
            };
            Device.StartTimer(TimeSpan.FromSeconds(1), () => 
            {
                time = time.Add(TimeSpan.FromSeconds(1));
                timeLabel.Text = time.ToString();
                if (done) return false;
                return true;
            });
        }

    }
}
