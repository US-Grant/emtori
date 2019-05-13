using System;
using CocosSharp;
using emtori.Models;

namespace emtori.ViewModels
{
    public class GameFieldView : CCLayer
    {

        private GameField field;

        public GameFieldView() : base()
        {
            this.field = new GameField(5);
            DrawLines();
        }

        private void DrawLines()
        {
            int y = 20;
            CCDrawNode node = new CCDrawNode();
            this.AddChild(node);
            for (int i = 0; i < field.Size + 1; i++)
            {
                node.DrawLine(new CCPointI(0, y), new CCPointI(100, y), 0.1f, CCColor4B.Gray, CCLineCap.Round);
                y += 10;
            }
            int x = 0;
            for (int i = 0; i < field.Size + 1; i++)
            {
                node.DrawLine(new CCPointI(x, 20), new CCPointI(x, 70), 0.1f, CCColor4B.Gray, CCLineCap.Round);
                x += 20;
            }
        }
    }
}
