using System;
using System.Data.Entity.Migrations;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows.Upsert
{
    public partial class UpsertRoleWindow : Window
    {

        private readonly Action _onAction;
        
        public UpsertRoleWindow(Roles roles, Action onAction)
        {
            InitializeComponent();
            DataContext = roles;
            _onAction = onAction;
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Db.Entities.Roles.AddOrUpdate((Roles)DataContext);
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