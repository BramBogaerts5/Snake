using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ITalentSnake.Objects
{
    class Playground
    {
        private const int fieldSizeX = 32;
        private const int fieldSizeY = 18;
        private Cube[,] playField = new Cube[fieldSizeX, fieldSizeY];
        private Canvas playCanvas;
        private int xActiveBlock = 16;
        private int yActiveBlock = 9;
        private int score = 0;
        private Random random;
        private bool bitePresent = false;

        public Playground(Canvas c)
        {
            playCanvas = c;
            random = new Random();
            BuildBoard();
            SnakeHead(xActiveBlock, yActiveBlock);
        }

        public int GetxActiveBlock { get { return xActiveBlock; } }
        public int GetyActiveBlock { get { return yActiveBlock; } }
        public Cube[,] GetPlayField { get { return playField; } }
        public int GetScore { get { return score; } set { score = value; } }
        public bool GetBitePresent { get { return bitePresent; } }

        public void BuildBoard()
        {
            for (int x = 0; x < fieldSizeX; x++)
            {
                for (int y = 0; y < fieldSizeY; y++)
                {
                    playField[x, y] = new Cube(x, y, Colors.White);
                    playField[x, y].DrawRectangle(playCanvas);
                }
            }
        }

        public void SnakeHead(int xActiveBlock, int yActiveBlock)
        {
            playField[xActiveBlock, yActiveBlock] = new Cube(xActiveBlock, yActiveBlock, Colors.Black);
            playField[xActiveBlock, yActiveBlock].DrawRectangle(playCanvas);
        }

        public void MoveSnake(String direction)
        {

            if (direction.Equals("up"))
            {
                yActiveBlock--;
            }
            else if (direction.Equals("down"))
            {
                yActiveBlock++;
            }
            else if (direction.Equals("left"))
            {
                xActiveBlock--;
            }
            else
            {
                xActiveBlock++;
            }
        }

        public void ColorSnake(int xActiveBlock, int yActiveBlock)
        {
            if (playField[xActiveBlock, yActiveBlock].ColorOfRectangle == Colors.Green)
            {
                TakeBite(xActiveBlock, yActiveBlock);
            }
            playField[xActiveBlock, yActiveBlock] = new Cube(xActiveBlock, yActiveBlock, Colors.Black);
            playField[xActiveBlock, yActiveBlock].DrawRectangle(playCanvas);
        }

        public void DeleteTail(int xCoordinates, int yCoordinates)
        {
            playField[xCoordinates, yCoordinates] = new Cube(xCoordinates, yCoordinates, Colors.White);
            playField[xCoordinates, yCoordinates].DrawRectangle(playCanvas);
        }

        public void MakeBite()
        {
            bool found = false;
            while (found == false)
            {
                int randomX = random.Next(0, fieldSizeX);
                int randomY = random.Next(0, fieldSizeY);
                if (playField[randomX, randomY].ColorOfRectangle == Colors.White)
                {
                    playField[randomX, randomY] = new Cube(randomX, randomY, Colors.Green);
                    playField[randomX, randomY].DrawRectangle(playCanvas);
                    found = true;
                    bitePresent = true;
                }
            }
        }

        public void TakeBite(int xCoordinates, int yCoordinates)
        {
            score = score + 100;
            DeleteTail(xCoordinates, yCoordinates);
            bitePresent = false;
        }

        public bool AreBlocksFree()
        {
            if (xActiveBlock > fieldSizeX - 1 || yActiveBlock > fieldSizeY - 1 || xActiveBlock < 0 || yActiveBlock < 0)
            {
                return false;
            }
            else
            {
                if (xActiveBlock <= fieldSizeX - 1 && yActiveBlock <= fieldSizeY - 1 && xActiveBlock >= 0 && yActiveBlock >= 0)
                {
                    if (playField[xActiveBlock, yActiveBlock].ColorOfRectangle == Colors.Black)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
