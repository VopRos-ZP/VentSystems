using System.Linq;
using System.Windows;
using VentSystems.Utils;

namespace VentSystems.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginBox.Text = "admin";
            PasswordBox.Password = "admin";
            OnLoginClick();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            OnLoginClick();
        }

        private void OnLoginClick()
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Password;

            var user = Db.Entities.Users.ToList().Find(u =>
                u.Login == login && u.Password == password
            );
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
            }
            else
            {
                switch (user.RoleId)
                {
                    case 1:
                        new UserWindow(user).Show();
                        break;
                    case 2:
                        new AdminWindow(user).Show();
                        break;
                    default:
                        new ManagerWindow(user).Show();
                        break;
                }
                Close();
            }
        }
        
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().ShowDialog();
        }
    }
}