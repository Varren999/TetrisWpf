using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TetrisWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int CubeSquareSize = 25;

        private const int TimerInterval = 5;
        private DispatcherTimer timer;
        Tetris tetris = new Tetris();
        public MainWindow()
        {
            InitializeComponent();
            InitialGame();
        }

        private void InitialGame()
        {
            //ConnectDB connect = new ConnectDB("Scope.db");
            //InputPlayer();
            tetris.score = 0;
            tetris.speed = 1;
            Score.Text = "0";
            Speed.Text = "1";
            GameCanvas.Children.Clear();
            tetris.Initialization();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(TimerInterval);
            timer.Start();
        }

        /// <summary>
        /// Обработка нажатий.
        /// </summary>
        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Движение фигуры влево.
                case Key.Left: tetris.MoveBlock(Tetris.Move.Left); break;
                case Key.A: tetris.MoveBlock(Tetris.Move.Left); break;

                // Движение фигуры вправо.
                case Key.Right: tetris.MoveBlock(Tetris.Move.Right); break;
                case Key.D: tetris.MoveBlock(Tetris.Move.Right); break;

                // Движение фигуры вниз.
                case Key.Down: tetris.MoveBlock(Tetris.Move.FastDown); break;
                case Key.S: tetris.MoveBlock(Tetris.Move.FastDown); break;

                // Ротация фигуры.
                case Key.Space: tetris.MoveBlock(Tetris.Move.Rotation); break;
                case Key.W: tetris.MoveBlock(Tetris.Move.Rotation); break;

                // Кнопка выхода.
                case Key.Escape: tetris.isExit = true; break;

                // Кнопка паузы.
                case Key.P: tetris.isPause = !tetris.isPause; break;
            }
        }

        //
        private void Timer_Tick(object sender, EventArgs e)
        {
            Rendering();

            tetris.Play();
            Score.Text = Convert.ToString(tetris.score);
            Speed.Text = Convert.ToString(tetris.speed);
            if (tetris.isExit)
            {
                GameOver();
                return;
            }
        }

        //
        private void GameOver()
        {
            timer.Stop();
            //connect.WriteDB(player, scope);
        }

        //
        private void Rendering()
        {
            GameCanvas.Children.Clear();
            Rectangle rectangle;
            int MarginSize = 1;
            for (int y = 0; y < tetris.Game_Fields.Length / (tetris.Game_Fields.GetUpperBound(0) + 1); y++)
            {
                for (int x = 0; x < (tetris.Game_Fields.GetUpperBound(0) + 1); x++)
                {
                    if (tetris.Game_Fields[x, y] == 0) continue;

                    rectangle = new Rectangle
                    {
                        Width = CubeSquareSize,
                        Height = CubeSquareSize,
                        Fill = Substitution(tetris.Game_Fields[x, y]),
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };

                    // Позиционируем прямоугольник в Canvas
                    Canvas.SetLeft(rectangle, x * CubeSquareSize + MarginSize);
                    Canvas.SetTop(rectangle, y * CubeSquareSize + MarginSize);

                    GameCanvas.Children.Add(rectangle);                   
                }
            }
        }

        //
        private static Brush Substitution(int value)
        {
            switch (value)
            {
                case 1: return Brushes.DarkGray;
                case 2: return Brushes.Gray;
                case 5: return Brushes.DarkGreen;
                case 6: return Brushes.DarkGreen;
                default: return Brushes.Transparent;
            }

        }
    }
}
