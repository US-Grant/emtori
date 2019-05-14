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
            NEUTRAL,
            EMOJI_BLACK,
            EMOJI_WHITE,
            EMOJI_NEUTRAL
        }

        private Dictionary<int, string> emojiDict = new Dictionary<int, string> 
        {
            { 1, "\ud83d\ude03" },
            { 2, "\ud83d\ude43" },
            { 3, "\ud83e\udd14" },
            { 4, "\ud83d\ude02" },
            { 5, "\ud83d\ude0d" },
            { 6, "\ud83e\udd2a" },
            { 7, "\ud83d\ude42" },
            { 8, "\ud83e\udd2d" },
            { 9, "\ud83d\ude1a" }
        };

        private CellState state = CellState.NEUTRAL;

        public CellState State
        {
            get
            {
                return state;
            }
            private set
            {
                this.state = value;
                switch (value)
                {
                    case CellState.BLACK:
                        isEmoji = false;
                        fillColor = CCColor4B.Black;
                        textColor = CCColor3B.DarkGray;
                        cell.Freedom = -1;
                        break;
                    case CellState.WHITE:
                        isEmoji = false;
                        fillColor = CCColor4B.White;
                        textColor = CCColor3B.DarkGray;
                        cell.Freedom = 1;
                        break;
                    case CellState.NEUTRAL:
                        isEmoji = false;
                        fillColor = CCColor4B.LightGray;
                        textColor = CCColor3B.DarkGray;
                        cell.Freedom = 0;
                        break;
                    case CellState.EMOJI_BLACK:
                        isEmoji = true;
                        fillColor = CCColor4B.Black;
                        textColor = CCColor3B.DarkGray;
                        cell.Freedom = -1;
                        break;
                    case CellState.EMOJI_WHITE:
                        isEmoji = true;
                        fillColor = CCColor4B.White;
                        textColor = CCColor3B.Yellow;
                        cell.Freedom = 1;
                        break;
                    case CellState.EMOJI_NEUTRAL:
                        isEmoji = true;
                        fillColor = CCColor4B.LightGray;
                        textColor = CCColor3B.Yellow;
                        cell.Freedom = 0;
                        break;
                }
            }
        }

        private CCColor4B fillColor;

        private CCPoint position;

        private CCColor3B textColor;

        private CCLabel label;

        private bool isEmoji;

        public bool IsEmoji
        {
            get { return isEmoji; }
            set
            {
                isEmoji = value;
                switch (state)
                {
                    case CellState.BLACK:
                        State = value ? CellState.EMOJI_BLACK : State;
                        break;
                    case CellState.WHITE:
                        State = value ? CellState.EMOJI_WHITE : State;
                        break;
                    case CellState.NEUTRAL:
                        State = value ? CellState.EMOJI_NEUTRAL : State;
                        break;
                    case CellState.EMOJI_BLACK:
                        State = value ? CellState.BLACK : State;
                        break;
                    case CellState.EMOJI_WHITE:
                        State = value ? CellState.WHITE : State;
                        break;
                    case CellState.EMOJI_NEUTRAL:
                        State = value ? CellState.NEUTRAL : State;
                        break;
                }
            }
        }

        private GameFieldCell cell;

        private int size;

        private CCEventListenerTouchAllAtOnce touchListener;

        public event EventHandler<EventArgs> OnTouched;


        public GameFieldCellView(ref GameFieldCell cell, int size, CCPoint position) : base()
        {
            this.cell = cell;
            this.size = size;
            this.position = position;
            this.isEmoji = false;
            this.State = CellState.NEUTRAL;
            label = new CCLabel(cell.Value.ToString(), "San Fransisco", size / 2)
            {
                Position = new CCPoint(position.X + size / 2, position.Y + size / 2),
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle
            };
            this.AddChild(label);
            this.touchListener = new CCEventListenerTouchAllAtOnce();
            this.touchListener.OnTouchesBegan += TouchListener_OnTouchesBegan;
            this.AddEventListener(this.touchListener);
        }

        void TouchListener_OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (new CCRect(position.X, position.Y, size, size).ContainsPoint(touches[0].Location))
            {
                OnTouched?.Invoke(this, new EventArgs());
                if (isEmoji)
                {
                    if (this.State == CellState.EMOJI_NEUTRAL)
                        this.State = CellState.EMOJI_WHITE;
                    else if (this.State == CellState.EMOJI_WHITE)
                        this.State = CellState.EMOJI_BLACK;
                    else if (this.State == CellState.EMOJI_BLACK)
                        this.State = CellState.EMOJI_NEUTRAL;
                }
                else
                {
                    if (this.State == CellState.NEUTRAL)
                        this.State = CellState.WHITE;
                    else if (this.State == CellState.WHITE)
                        this.State = CellState.BLACK;
                    else if (this.State == CellState.BLACK)
                        this.State = CellState.NEUTRAL;
                }
                DrawCell();
            }
        }


        public void DrawCell()
        {
            this.Clear();
            this.DrawRect(new CCRect(position.X + 1, position.Y + 1, size - 2, size - 2), fillColor);
            if (isEmoji)
            {
                label.Text = emojiDict[cell.Value];
                label.Color = this.textColor;
            }
            else
            {
                label.Text = cell.Value.ToString();
                label.Color = this.textColor;
            }

        }

    }
}
