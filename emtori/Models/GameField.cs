using System;

namespace emtori.Models
{
    public sealed class GameField
    {

        private readonly int size;

        public int Size { get { return size; } }

        private readonly int black = 2;

        private GameFieldCell[][] field;

        public GameFieldCell [][] Cells 
        {
            get { return field; }
        }

        public GameField(int size)
        {
            this.size = size;
            field = new GameFieldCell[size][];
            Init();
            Generate();
        }

        private void Init()
        {
            for (int i = 0; i < size; i++)
            {
                field[i] = new GameFieldCell[size];
                for (int j = 0; j < size; j++)
                {
                    field[i][j] = new GameFieldCell();
                    if (i == 0 || i == size - 1)
                    {
                        if (j <= 1 || j >= size - 2)
                        {
                            field[i][j].Freedom = 2;
                        }
                        else field[i][j].Freedom = 2;
                    }
                    else if (j == 0 || j == size - 1)
                    {
                        field[i][j].Freedom = 3;
                    }
                    else field[i][j].Freedom = 3;
                }
            }
        }

        private void Generate()
        {
            bool generate = true;
            while (generate)
            {
                Init();

                if (!GenerateBlackCells()) continue;
                if (!GenerateWhiteCells()) continue;
                GenerateBlackValues();
                generate = false;
            }
            ResetFreedomValues();
        }

        private bool GenerateBlackCells()
        {
            bool error = false;
            for (int i = 0; i < size; i++)
            {
                Random rand = new Random();
                int count = rand.Next(black) + 1;
                for (int j = 0; j < count; j++)
                {
                    if (i == 0)
                    {
                        bool b = true;
                        int attempts = 10;
                        while (b)
                        {
                            attempts--;
                            if (attempts == 0)
                            {
                                error = true;
                                break;
                            }
                            int column = rand.Next(size);
                            if (column > 0 && field[i][column - 1].Value == -1)
                            {
                                continue;
                            }
                            if (column < size - 1 && field[i][column + 1].Value == -1)
                            {
                                continue;
                            }
                            if (column > 0 && field[i][column - 1].Freedom - 1 == 0) continue;
                            if (column < size - 1 && field[i][column + 1].Freedom - 1 == 0) continue;
                            if (i < size - 1 && field[i + 1][column].Freedom - 1 == 0) continue;
                            b = false;
                            field[i][column].Value = -1;
                            field[i][column].Freedom = -1;
                            if (column > 0)
                                field[i][column - 1].Freedom--;
                            if (column < size - 1)
                                field[i][column + 1].Freedom--;
                            if (i < size - 1)
                                field[i + 1][column].Freedom--;
                        }
                    }
                    else
                    {
                        bool b = true;
                        int attempts = 10;
                        while (b)
                        {
                            attempts--;
                            if (attempts == 0)
                            {
                                error = true;
                                break;
                            }
                            int column = rand.Next(size);
                            if (field[i - 1][column].Value == 0)
                            {
                                if (column > 0 && field[i][column - 1].Value == -1)
                                {
                                    continue;
                                }
                                if (column < size - 1 && field[i][column + 1].Value == -1)
                                {
                                    continue;
                                }
                                if (field[i - 1][column].Freedom - 1 == 0) continue;
                                if (i < size - 1 && field[i + 1][column].Freedom - 1 == 0) continue;
                                if (column > 0 && field[i][column - 1].Freedom - 1 == 0) continue;
                                if (column < size - 1 && field[i][column + 1].Freedom - 1 == 0) continue;
                                field[i][column].Value = -1;
                                field[i][column].Freedom = -1;
                                b = false;
                                if (field[i - 1][column].Freedom - 1 >= 0)
                                    field[i - 1][column].Freedom--;
                                if (i < size - 1 && field[i + 1][column].Freedom - 1 >= 0)
                                    field[i + 1][column].Freedom--;
                                if (column > 0 && field[i][column - 1].Freedom - 1 >= 0)
                                    field[i][column - 1].Freedom--;
                                if (column < size - 1 && field[i][column + 1].Freedom - 1 >= 0)
                                    field[i][column + 1].Freedom--;
                            }
                        }
                    }
                }
            }
            return !error;
        }

        private bool GenerateWhiteCells() 
        {
            bool error = false;
            for (int i = 0; i < size; i++)
            {
                Random rand = new Random();
                for (int j = 0; j < size; j++)
                {
                    if (field[i][j].Value == -1) continue;
                    bool b = true;
                    int attempts = 10;
                    while (b)
                    {
                        attempts--;
                        if (attempts == 0)
                        {
                            error = true;
                            break;
                        }
                        int value = rand.Next(size) + 1;
                        int compare = 0;
                        for (int k = 0; k < size; k++)
                        {
                            if (field[k][j].Value != value && field[i][k].Value != value)
                                compare++;
                        }
                        if (compare == size)
                        {
                            field[i][j].Value = value;
                            b = false;
                        }
                    }
                    if (error) break;
                }
                if (error) break;
            }
            return !error;
        }

        private void GenerateBlackValues()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i][j].Value == -1)
                    {
                        Random rand = new Random();
                        int hv = rand.Next(2);
                        if (hv == 0)
                        {
                            bool b = true;
                            while (b)
                            {
                                int value = rand.Next(size) + 1;
                                for (int k = 0; k < size; k++)
                                {
                                    if (value == field[i][k].Value)
                                    {
                                        field[i][j].Value = value;
                                        b = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (hv == 1)
                        {
                            bool b = true;
                            while (b)
                            {
                                int value = rand.Next(size) + 1;
                                for (int k = 0; k < size; k++)
                                {
                                    if (value == field[k][j].Value)
                                    {
                                        field[i][j].Value = value;
                                        b = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ResetFreedomValues()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    field[i][j].Freedom = 0;
                }
            }
        }

    }
}
