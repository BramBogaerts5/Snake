using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ITalentSnake.Objects
{
    class Cube
    {
        private Rectangle rect;
        private SolidColorBrush colorOfSquare = new SolidColorBrush();
        private Thickness position;

        private const int size = 20;

        public Cube(int positionX, int positionY, Color color)
        {
            rect = new Rectangle();
            position = new Thickness(positionX * size, positionY * size, 0, 0);
            colorOfSquare = new SolidColorBrush(color);
            rect.Margin = position;
            rect.Fill = colorOfSquare;
            rect.Height = size;
            rect.Width = size;
        }

        public Rectangle Rectangle
        {
            get
            {
                return rect;
            }
        }

        public Color ColorOfRectangle
        {
            get
            {
                return colorOfSquare.Color;
            }
            set
            {
                colorOfSquare.Color = value;
            }
        }

        public void DrawRectangle(Canvas playBoard)
        {
            playBoard.Children.Add(rect);
        }
    }
}
