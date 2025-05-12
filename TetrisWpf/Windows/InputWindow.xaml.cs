using System.Windows;

namespace TetrisWpf
{
    /// <summary>
    /// Логика взаимодействия для InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(PlayerName.Text);
            main.Show();
            Close();
        }
    }
}
