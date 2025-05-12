using System.Windows;

namespace TetrisWpf
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            InputWindow input = new InputWindow();
            input.Show();
            Close();
        }

        private void Exit_Click( object sender, RoutedEventArgs e ) => Application.Current.Shutdown();

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для управления фигурой используйте кнопки A, D или курсор Вправо, курсор Влево.\n" +
                "Для быстрого движения фигуры вниз используйте кнопки S или курсор Вниз.\n" +
                "Для ротации фигуры используйте кнопки W, Пробел или курсор Вверх.\n" +
                "Чтобы поставить игру на паузу нажмите кнопку P.\n" +
                "Чтобы вернутся в меню нажмите кнопку ESCAPE.");
        }

        private void DB_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics();
            statistics.Show();
            this.Close();
        }
    }
}
