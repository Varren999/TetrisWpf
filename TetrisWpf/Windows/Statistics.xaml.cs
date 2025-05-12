using System.Windows;

namespace TetrisWpf
{
    /// <summary>
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();
            List.ItemsSource = ConnectDB.Instance.GetAll();
        }

        private void Escape_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            this.Close();
        }
    }
}
