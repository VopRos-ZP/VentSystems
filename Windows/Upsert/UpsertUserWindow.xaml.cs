using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows.Upsert
{
    public partial class UpsertUserWindow : Window
    {
        
        private readonly Action _onAction;
        
        public UpsertUserWindow(Users user, Action onAction)
        {
            InitializeComponent();
            _onAction = onAction;
            DataContext = user;
            RolesBox.ItemsSource = Db.Entities.Roles.ToList();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Db.Entities.Users.AddOrUpdate((Users)DataContext);
            Db.Entities.SaveChanges();
            _onAction();
            Close();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}