using System.Windows;
using System.Windows.Input;

namespace DocumentManagementApp
{
    /// <summary>
    /// Interaction logic for Managerment.xaml
    /// </summary>
    public partial class Managerment : Window
    {
        public Managerment()
        {
            InitializeComponent();
        }
        private string username;

        public Managerment(string username)
        {
            InitializeComponent();
            this.username = username;
            Loaded += Managerment_Load;
        }

        private void Managerment_Load(object sender, RoutedEventArgs e)
        {
            bool isViewer = (username != "Viewers");

            button1.Visibility = isViewer ? Visibility.Visible : Visibility.Hidden;
            button2.Visibility = isViewer ? Visibility.Visible : Visibility.Hidden;
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

            }

        }
        private bool IsMaximized = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;

                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }


            }

        }
    }
}
