using System;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Db.Entities.Users.Add(new Users
                {
                    FirstName = FirstNameBox.Text,
                    LastName = LastNameBox.Text,
                    Patronymic = PatronymicBox.Text,
                    Login = LoginBox.Text,
                    Password = PasswordBox.Password,
                    RoleId = 1
                });
                Db.Entities.SaveChanges();
                Close();
            }
            catch (Exception)
            { 
                MessageBox.Show("Не удалось зарегистрироваться");
            }
        }
        
    }
}