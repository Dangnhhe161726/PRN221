using DocumentManagementApp.Models;
using System.Windows;

namespace DocumentManagementApp
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly AssignmentPrn221Context _context;
        public Register()
        {
            InitializeComponent();
            _context = new AssignmentPrn221Context();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string password = txtPassword.Password;
            string displayName = txtDisplayName.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(displayName))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            try
            {
                Account newUser = new Account
                {
                    Name = name,
                    Password = password,
                    DisplayName = displayName,
                    Status = true,
                };


                _context.Accounts.Add(newUser);


                _context.SaveChanges();


                MessageBox.Show($"Registration Successful!\nName: {name}\nDisplay Name: {displayName}");

                ClearFields();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Registration failed: {ex.Message}");
            }
        }
        private void ClearFields()
        {

            txtName.Text = "";
            txtPassword.Password = "";
            txtDisplayName.Text = "";
        }
    }
}
