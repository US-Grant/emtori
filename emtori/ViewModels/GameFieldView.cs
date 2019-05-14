using System;
using System.Threading.Tasks;
using CocosSharp;
using emtori.Models;

namespace emtori.ViewModels
{
    public class GameFieldView : CCLayer
    {

        private bool isEmoji = false;

        private CCLabel messageLabel;

        public bool IsEmoji
        {
            get
            {
                return isEmoji;
            }
            set
            {
                isEmoji = value;
                for (int i = 0; i < cells.Length; i++)
                {
                    for (int j = 0; j < cells[i].Length; j++)
                    {
                        cells[i][j].IsEmoji = value;
                    }
                }
                DrawCells();
            }
        }

        private GameField field;

        private GameFieldCellView[][] cells;

        private int cellSize;


        public GameFieldView(GameField field, bool isEmoji) : base()
        {
            this.field = field;
            cellSize = 1080 / field.Size;
            cells = new GameFieldCellView[field.Size][];
            for (int i = 0; i < field.Size; i++)
            {
                cells[i] = new GameFieldCellView[field.Size];
                for (int j = 0; j < field.Size; j++)
                {
                    cells[i][j] = new GameFieldCellView(ref field.Cells[i][j], cellSize, new CCPoint(i * cellSize, 1920 / 4 + j * cellSize));
                    cells[i][j].OnTouched += OnCellTouched;
                    this.AddChild(cells[i][j]);
                }
            }
            IsEmoji = isEmoji;
            messageLabel = new CCLabel(string.Empty, "San Fransisco", 50)
            {
                Position = new CCPoint(540, 200),
                Color = CCColor3B.Gray,
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle
            };
            this.AddChild(messageLabel);
            DrawLines();
            DrawCells();
        }

        private void DrawLines()
        {
            int y = 1920 / 4;
            CCDrawNode node = new CCDrawNode();
            this.AddChild(node);
            for (int i = 0; i < field.Size + 1; i++)
            {
                node.DrawLine(new CCPointI(0, y), new CCPointI(1080, y), 1f, CCColor4B.Gray, CCLineCap.Round);
                y += this.cellSize;
            }
            int x = 0;
            for (int i = 0; i < field.Size + 1; i++)
            {
                node.DrawLine(new CCPointI(x, 1920 / 4), new CCPointI(x, 1920 / 4 + cellSize * field.Size), 1f, CCColor4B.Gray, CCLineCap.Round);
                x += this.cellSize;
            }

        }

        private void DrawCells()
        {
            for (int i = 0; i < field.Size; i++)
            {
                for (int j = 0; j < field.Size; j++)
                {
                    cells[i][j].DrawCell();
                }
            }
        }

        private async void OnCellTouched(object sender, EventArgs e)
        {
            await CheckField();
        }

        private async Task<bool> CheckField() 
        {
            return await Task.Run(() => 
            { 
                for (int i = 0; i < field.Size; i++)
                {
                    for (int j = 0; j < field.Size; j++)
                    {
                        if (field.Cells[i][j].Freedom == 0)
                        {
                            messageLabel.Text = string.Empty;
                            return false;
                        }
                    }
                }
                //checking black cells, freedom = -1
                for (int i = 0; i < field.Size; i++)
                {
                    for (int j = 0; j < field.Size; j++)
                    {
                        if (field.Cells[i][j].Freedom == -1)
                        {
                            if (i > 0 && field.Cells[i-1][j].Freedom == -1 ||
                                i < field.Size - 1 && field.Cells[i + 1][j].Freedom == -1 ||
                                j > 0 && field.Cells[i][j - 1].Freedom == -1 ||
                                j < field.Size - 1 && field.Cells[i][j + 1].Freedom == -1)
                            {
                                messageLabel.Text = "Two black cells can't be beside each other!";
                                return false;
                            }
                        }
                    }
                }
                //checking white cells for same values, freedom = 1
                GameFieldCell same;
                //in columns
                for (int i = 0; i < field.Size; i++)
                {
                    for (int j = 0; j < field.Size; j++)
                    {
                        same = field.Cells[i][j];
                        if (same.Freedom == -1 || j == field.Size - 1) continue;
                        for (int k = j + 1; k < field.Size; k++)
                        {
                            if (field.Cells[i][k].Freedom == -1) continue;
                            if (same.Value == field.Cells[i][k].Value)
                            {
                                messageLabel.Text = "Two same values can't be inside one column!";
                               return false;
                            }
                        }
                    }
                }
                //in rows
                for (int i = 0; i < field.Size; i++)
                {
                    for (int j = 0; j < field.Size; j++)
                    {
                        same = field.Cells[j][i];
                        if (same.Freedom == -1 || j == field.Size - 1) continue;
                        for (int k = j + 1; k < field.Size; k++)
                        {
                            if (field.Cells[k][i].Freedom == -1) continue;
                            if (same.Value == field.Cells[k][i].Value)
                            {
                                messageLabel.Text = "Two same values can't be inside one row!";
                                return false;
                            }
                        }
                    }
                }
                //checking white cells, freedom = 1
                for (int i = 0; i < field.Size; i++)
                {
                    for (int j = 0; j < field.Size; j++)
                    {
                        if (field.Cells[i][j].Freedom != 1)
                        {
                            continue;
                        }
                        int freedom = 4;
                        if (i > 0 && field.Cells[i - 1][j].Freedom == -1) freedom--;
                        if (i < field.Size - 1 && field.Cells[i + 1][j].Freedom == -1) freedom--;
                        if (j > 0 && field.Cells[i][j - 1].Freedom == -1) freedom--;
                        if (j < field.Size - 1 && field.Cells[i][j + 1].Freedom == -1) freedom--;
                        if (i == 0 || i == field.Size - 1) freedom--;
                        if (j == 0 || j == field.Size - 1) freedom--;
                        if (freedom <= 0)
                        {
                            messageLabel.Text = "Each white cell must have at least \none white neighbour cell!";
                            return false;
                        }
                    }
                }
                messageLabel.Text = "Done!";
                return true;
            });
        }
    }
}
