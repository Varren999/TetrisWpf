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
            ConnectDB connect = new ConnectDB("Scope.db");
            InitializeComponent();
            connect.ReadDB();
            List.ItemsSource = connect.collections;
        }

        private void Escape_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            Close();
        }
    }
}
