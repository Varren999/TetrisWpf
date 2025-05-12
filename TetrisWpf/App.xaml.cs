using System.Diagnostics;
using System.Windows;

namespace TetrisWpf
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        ConnectDB connect = new ConnectDB( "Score.db" );

        protected override void OnStartup( StartupEventArgs e )
        {
            // Настройка записи логов в файл
            Trace.Listeners.Add( new TextWriterTraceListener( "log.txt" ) );
            Trace.AutoFlush = true;

            base.OnStartup( e );
        }
    }
}
