using DocumentManagementApp.Models;
using System.Windows;

namespace DocumentManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AssignmentPrn221Context _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new AssignmentPrn221Context();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = TxUserName.Text;
            string password = TxPass.Password;

            var user = _context.Accounts.FirstOrDefault(u => u.Name == username);

            if (user != null && VerifyPassword(password, user.Password))
            {
                var role = _context.Groups.FirstOrDefault(g => g.Aids.Any(a => a.Id == user.Id));

                if (role != null)
                {
                    MessageBox.Show($"Login successful! Welcome, {user.Name}. Your role is {role.Name}.");

                    if (role.Name == "Administrator")
                    {
                        Managerment window1 = new Managerment(role.Name);
                        window1.Show();
                        Close();

                    }
                    else if (role.Name == "Editor")
                    {
                        Managerment window1 = new Managerment(role.Name);
                        window1.Show();

                        Close();

                    }
                    else
                    {
                        Managerment window1 = new Managerment(role.Name);
                        window1.Show();

                        Close();

                    }
                }
                else
                {
                    MessageBox.Show("Role not found for the user.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {

            return enteredPassword == hashedPassword;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Register registerForm = new Register();


            registerForm.ShowDialog();
        }
    }
}