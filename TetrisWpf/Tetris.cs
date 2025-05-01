//////////////////////////////////////////////////////////////////////////////////
// Autor: Vatslav Varren
//////////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.Windows;
using Log;

namespace ConsoleApp
{
    internal class Tetris
    {
        private Random random;
        private readonly Stopwatch timer = new Stopwatch();
        private long lastTimer;

        private const int WIDTH = 12, HEIGHT = 18;

        public int speed = 1;
        public int score = 0;
        private int thisFigure = 0;
        private int nextFigure = 0;

        public Point[] block;
        private Blocks next_block;
        private bool isBlock_Live = false;

        public bool isPause = false;
        public bool isExit = false;
        public enum Move { Down, FastDown, Left, Right, Rotation};

        public int[,] Game_Fields = new int[WIDTH, HEIGHT];

        /// <summary>
        /// Создаем новый блок.
        /// </summary>
        private void Born_Block()
        {
            block = next_block.Block;
            if (Collision())
                isExit = true;
            thisFigure = nextFigure;
            nextFigure = random.Next(0, 7);
            next_block = new Blocks(nextFigure);
            isBlock_Live = true;
        }

        /// <summary>
        /// Проверка столкновений.
        /// </summary>
        /// <returns></returns>
        private bool Collision()
        {
            try
            {
                for (int i = 0; i < block.Length; i++)
                {
                    // Проверяем столкновение со стенами и дном.
                    if (block[i].X <= 0 || block[i].X >= WIDTH - 1 || block[i].Y >= HEIGHT - 1 || block[i].Y < 0)
                        return true;

                    // Проверка столкновения с другими фигурами.
                    if (block[i].Y >= 0 && Game_Fields[(int)block[i].X, (int)block[i].Y] == 2)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Метод движения блока.
        /// </summary>
        /// <param name="move"></param>
        public void MoveBlock(Move move)
        {
            try
            {
                if (!isPause) // Если игра на паузе блоки не двигаются)).
                {
                    // Сохраняем текущее положение на случай отката.
                    Point[] oldPosition = new Point[block.Length];
                    Array.Copy(block, oldPosition, block.Length);

                    switch (move)
                    {
                        case Move.Down:
                            for (int i = 0; i < block.Length; i++)
                            {
                                Game_Fields[(int)block[i].X, (int)block[i].Y] = 0;
                                block[i].Y++;
                            } break;

                        case Move.FastDown:
                            while (!Collision())
                            {
                                // Сохраняем текущее положение на случай отката.
                                oldPosition = new Point[block.Length];
                                Array.Copy(block, oldPosition, block.Length);

                                for (int i = 0; i < block.Length; i++)
                                {
                                    Game_Fields[(int)block[i].X, (int)block[i].Y] = 0;
                                    block[i].Y++;
                                }
                            } break;


                        case Move.Left:
                            for (int i = 0; i < block.Length; i++)
                            {
                                Game_Fields[(int)block[i].X, (int)block[i].Y] = 0;
                                block[i].X--;
                            } break;

                        case Move.Right:
                            for (int i = 0; i < block.Length; i++)
                            {
                                Game_Fields[(int)block[i].X, (int)block[i].Y] = 0;
                                block[i].X++;
                            } break;

                        case Move.Rotation:
                            if (thisFigure == 0) return; // Если фигура квадрат его вращать не нужно.
                            Point center;
                            if (thisFigure == 6) // Если фигура T то центр вращения 3 точка массива у остальных фигур вторая.
                                center = block[2];
                            else
                                center = block[1];
                            Point[] newPositions = new Point[4];

                            for (int i = 0; i < 4; i++)
                            {
                                // Вычисляем новые координаты после поворота
                                int newX = (int)(center.X - (block[i].Y - center.Y));
                                int newY = (int)(center.Y + (block[i].X - center.X));
                                newPositions[i] = new Point(newX, newY);
                            }

                            // Применяем новые позиции
                            for (int i = 0; i < 4; i++)
                            {
                                Game_Fields[(int)block[i].X, (int)block[i].Y] = 0;
                                block[i] = newPositions[i];
                            }
                            break;
                    }

                    // Если после движения произошло столкновение - возвращаем старое положение
                    if (Collision())
                    {
                        Array.Copy(oldPosition, block, block.Length);
                        if (move == Move.Down || move == Move.FastDown)
                        {
                            FixBlock();
                            isBlock_Live = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
        }

        /// <summary>
        /// Метод фиксирует блок на игровом поле.
        /// </summary>
        private void FixBlock()
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i].Y >= 0 || block[i].X <= WIDTH - 1) // Проверяем, что блок в пределах видимой области
                    Game_Fields[(int)block[i].X, (int)block[i].Y] = 2; // 2 - зафиксированный блок
            }
            CheckCompletedLines();
        }

        /// <summary>
        /// Метод проверяет заполненные линий.
        /// </summary>
        private void CheckCompletedLines()
        {
            for (int y = HEIGHT - 2; y >= 0; y--) // Идем снизу вверх
            {
                bool lineComplete = true;

                // Проверяем всю строку кроме границ
                for (int x = 1; x < WIDTH - 1; x++)
                {
                    if (Game_Fields[x, y] != 2)
                    {
                        lineComplete = false;
                        break;
                    }
                }

                if (lineComplete)
                {
                    RemoveLine(y);
                    y++;
                }
            }
        }

        /// <summary>
        /// Метод удаляет заполненные линии и смещяет вышележащие строки вниз.
        /// </summary>
        /// <param name="lineToRemove"></param>
        private void RemoveLine(int lineToRemove)
        {
            // Смещаем все строки выше удаляемой вниз
            for (int y = lineToRemove; y > 0; y--)
            {
                for (int x = 1; x < WIDTH - 1; x++)
                {
                    Game_Fields[x, y] = Game_Fields[x, y - 1];
                }
            }

            // Очищаем верхнюю строку
            for (int x = 1; x < WIDTH - 1; x++)
            {
                Game_Fields[x, 0] = 0;
            }

            score += 100; 
        }

        /// <summary>
        /// Метод рисует фигуру на игровом поле.
        /// </summary>
        private void DrawFigure()
        {
            try
            {
                for (int i = 0; i < block.Length; i++)
                {
                    Game_Fields[(int)block[i].X, (int)block[i].Y] = 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
        }

        // Метод рисует игровое поле.
        private void DrawMap()
        {
            try 
            {
                // Рисуем дно.
                for (int x = 0; x < WIDTH; x++)
                {
                    Game_Fields[x, HEIGHT - 1] = 6;
                }

                // Рисуем стены.
                for (int y = 0; y < HEIGHT; y++)
                {
                    Game_Fields[0, y] = 5;
                    Game_Fields[WIDTH - 1, y] = 5;
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
        }

        /// <summary>
        /// Метод проверяет счет и ускоряет игру.
        /// </summary>
        /// <param name="Score"></param>
        private void SpeedTest(int Score)
        {
            switch(Score)
            {
                case int n when n >= 0 && n < 2000: speed = 1; break;

                case int n when n >= 2000 && n < 4000: speed = 2; break;

                case int n when n >= 4000 && n < 8000: speed = 3; break;
            }
        }

        //
        public void Initialization()
        {
            timer.Start();
            lastTimer = timer.ElapsedMilliseconds;
            random = new Random(DateTime.Now.Millisecond);
            nextFigure = random.Next(0, 7);
            next_block = new Blocks(nextFigure);
            DrawMap();
        }

        //
        public void Play()
        {
            try
            {
                if (!isBlock_Live)
                {
                    Born_Block();
                }

                DrawMap();
                DrawFigure();

                SpeedTest(score);

                if (timer.ElapsedMilliseconds - lastTimer >= (1000 / speed) && !isPause)
                {
                    lastTimer = timer.ElapsedMilliseconds;

                    MoveBlock(Move.Down);
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }
    }
}

