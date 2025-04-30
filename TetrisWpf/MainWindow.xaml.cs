using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private const int CubeSquareSize = 20;
        private readonly SolidColorBrush figureColor = Brushes.White;
        private readonly SolidColorBrush wallColor = Brushes.DarkGreen;
        private readonly SolidColorBrush fieldColor = Brushes.DarkGray;

        private const int TimerInterval = 200;
        private DispatcherTimer timer;
        Tetris tetris = new Tetris();
        public MainWindow()
        {
            InitializeComponent();
            InitialGame();
        }

        private void InitialGame()
        {
            tetris.score = 0;
            //ScoreTextBlock.Text = "Score: 0";
            GameCanvas.Children.Clear();
            //snake.Clear();
            //direction = Direction.Right;
            //snakeHead = CreateSnakeSegment(new Point(5, 5));
            //snake.Add(snakeHead);
            //GameCanvas.Children.Add(snakeHead);
            //PlaceFood();
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
                case Key.Left:
                    tetris.MoveBlock(Tetris.Move.Left); break;
                case Key.A:
                    tetris.MoveBlock(Tetris.Move.Left); break;

                // Движение фигуры вправо.
                case Key.Right:
                    tetris.MoveBlock(Tetris.Move.Right); break;
                case Key.D:
                    tetris.MoveBlock(Tetris.Move.Right); break;

                // Движение фигуры вниз.
                case Key.Down:
                    tetris.MoveBlock(Tetris.Move.FastDown); break;
                case Key.S:
                    tetris.MoveBlock(Tetris.Move.FastDown); break;

                // Ротация фигуры.
                case Key.Space:
                    tetris.MoveBlock(Tetris.Move.Rotation); break;
                case Key.W:
                    tetris.MoveBlock(Tetris.Move.Rotation); break;

                // Кнопка выхода.
                case Key.Escape:
                    tetris.isExit = true; break;

                // Кнопка паузы.
                case Key.P:
                    tetris.isPause = !tetris.isPause; break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //ConnectDB connect = new ConnectDB("Scope.db");
            //InputPlayer();

            //tetris.Play();

            Rendering();

            //if (!tetris.isExit)
            //{
            //    GameOver();
            //    return;
            //}

            //connect.WriteDB(player, scope);
        }

        private void GameOver()
        {
            timer.Stop();
        }

        private void Rendering()
        {
            Rectangle rectangle;

            for (int y = 0; y < tetris.Game_Fields.Length / (tetris.Game_Fields.GetUpperBound(0) + 1); y++)
            {
                for (int x = 0; x < (tetris.Game_Fields.GetUpperBound(0) + 1); x++)
                {
                    if(tetris.Game_Fields[x, y] == 1)
                    {
                        //rectangle = new Rectangle { (y * CubeSquareSize), x * CubeSquareSize, fieldColor };
                        //GameCanvas.Children.Add(rectangle);
                    }
                }
            }

            for (int i = 0; i < tetris.block.Length; i++)
            {
                rectangle = CreateRectangle(tetris.block[i]);
                GameCanvas.Children.Add(rectangle);
            }
            //point = new Point(10, 10);
            //rectangle = CreateRectangle(point);
            //GameCanvas.Children.Add(rectangle);
        }

        private Rectangle CreateRectangle(Point position)
        {
            Rectangle rectangle = new Rectangle { Width = CubeSquareSize, Height = CubeSquareSize, Fill = wallColor };

            Canvas.SetLeft(rectangle, position.X = CubeSquareSize);
            Canvas.SetTop(rectangle, position.Y = CubeSquareSize);

            return rectangle;
        }
    }
}
