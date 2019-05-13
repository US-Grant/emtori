using System;
using System.Collections.Generic;
using CocosSharp;
using emtori.Models;

namespace emtori.ViewModels
{
    public class GameFieldCellView : CCDrawNode
    {

        public enum CellState
        {
            BLACK,
            WHITE,
            NEUTRAL
        }

        private CellState state = CellState.NEUTRAL;

        public CellState State
        {
            get
            {
                return state;
            }
            set
            {
                this.state = value;
                switch (value)
                {
                    case CellState.BLACK:
                        fillColor = CCColor4B.Black;
                        textColor = CCColor3B.White;
                        break;
                    case CellState.WHITE:
                        fillColor = CCColor4B.White;
                        textColor = CCColor3B.DarkGray;
                        break;
                    case CellState.NEUTRAL:
                        fillColor = CCColor4B.LightGray;
                        textColor = CCColor3B.DarkGray;
                        break;
                }
            }
        }

        private CCColor4B fillColor;

        private CCPoint position;

        private CCColor3B textColor;

        private CCLabel label;

        private GameFieldCell cell;
        private int size;

        private CCEventListenerTouchAllAtOnce touchListener;


        public GameFieldCellView(GameFieldCell cell, int size, CCPoint position) : base()
        {
            this.cell = cell;
            this.size = size;
            this.position = position;
            this.State = CellState.NEUTRAL;
            label = new CCLabel(cell.Value.ToString(), "Arial", 100)
            {
                Position = new CCPoint(position.X + size / 2, position.Y + size / 2),
                Color = textColor,
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle,
                Dimensions = ContentSize
            };
            this.AddChild(label);
            this.touchListener = new CCEventListenerTouchAllAtOnce();
            this.touchListener.OnTouchesBegan += TouchListener_OnTouchesBegan;
            this.touchListener.OnTouchesEnded += TouchListener_OnTouchesEnded;
            this.touchListener.OnTouchesMoved += TouchListener_OnTouchesMoved;
            this.AddEventListener(this.touchListener);
        }

        void TouchListener_OnTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            this.State = CellState.NEUTRAL;
            DrawCell();
            if (new CCRect(position.X, position.Y, size, size).ContainsPoint(touches[0].Location))
            {
                this.State = CellState.WHITE;
                DrawCell();
            }
        }


        void TouchListener_OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            this.State = CellState.NEUTRAL;
            DrawCell();
        }


        void TouchListener_OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (new CCRect(position.X, position.Y, size, size).ContainsPoint(touches[0].Location))
            {
                this.State = CellState.WHITE;
                DrawCell();
            }
        }





        public void DrawCell()
        {
            this.DrawRect(new CCRect(position.X + 1, position.Y + 1, size - 2, size - 2), fillColor);
        }

    }
}
