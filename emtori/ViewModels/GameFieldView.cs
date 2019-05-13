using System;
using CocosSharp;
using emtori.Models;

namespace emtori.ViewModels
{
    public class GameFieldView : CCLayer
    {



        private GameField field;

        private GameFieldCellView[][] cells;

        private int cellSize;


        public GameFieldView(GameField field) : base()
        {
            this.field = field;
            cellSize = 1080 / field.Size;
            cells = new GameFieldCellView[field.Size][];
            for (int i = 0; i < field.Size; i++)
            {
                cells[i] = new GameFieldCellView[field.Size];
                for (int j = 0; j < field.Size; j++)
                {
                    cells[i][j] = new GameFieldCellView(field.Cells[i][j], cellSize, new CCPoint(i * cellSize, 1920 / 4 + j * cellSize));
                    this.AddChild(cells[i][j]);
                }
            }
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
    }
}
