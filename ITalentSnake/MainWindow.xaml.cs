using ITalentSnake.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ITalentSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Playground board;
        private static Hashtable keytable = new Hashtable();
        private bool gameStarted = false;
        private DispatcherTimer timer = new DispatcherTimer();
        private String direction = "right";
        private int tickTimer = 0;
        private int snakeLenght = 2;
        private IList<int> list = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            board = new Playground(playCanvas);
            timer.Interval = TimeSpan.FromMilliseconds(400);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            list.Add(board.GetxActiveBlock);
            list.Add(board.GetyActiveBlock);
            if (tickTimer % 20 == 0 && tickTimer > 50 && board.GetBitePresent == false)
            {
                board.MakeBite();
            }
            board.MoveSnake(direction);
            board.GetScore = board.GetScore + 5;
            scoreLabel.Content = Convert.ToString(board.GetScore);
            tickTimer++;
            if (tickTimer > snakeLenght && tickTimer % 5 != 0)
            {
                board.DeleteTail(list[0], list[1]);
                list.RemoveAt(0);
                list.RemoveAt(0);
            }
            if (board.AreBlocksFree() == true)
            {
                board.ColorSnake(board.GetxActiveBlock, board.GetyActiveBlock);
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Oh, too bad! Game Over! Your score is " + board.GetScore);
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            list.Clear();
            board = new Playground(playCanvas);
            gameStarted = true;
            direction = "right";
            tickTimer = 0;
            timer.Start();
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            list.Clear();
            Environment.Exit(0);
        }

        private void Key_KeyDown(object sender, KeyEventArgs e)
        {
            KeyEventArgs button = e;

            if (button.Key == Key.Up)
            {
                if (gameStarted == true)
                {
                    direction = "up";
                }
            }
            if (button.Key == Key.Down)
            {
                if (gameStarted == true)
                {
                    direction = "down";
                }
            }
            if (button.Key == Key.Right)
            {
                if (gameStarted == true)
                {
                    direction = "right";
                }
            }
            if (button.Key == Key.Left)
            {
                if (gameStarted == true)
                {
                    direction = "left";
                }
            }
        }
    }
}
