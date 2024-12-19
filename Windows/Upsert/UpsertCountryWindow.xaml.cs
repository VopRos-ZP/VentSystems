using System;
using System.Data.Entity.Migrations;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows.Upsert
{
    public partial class UpsertCountryWindow : Window
    {
        
        private readonly Action _onAction;
        
        public UpsertCountryWindow(Countries country, Action onAction)
        {
            InitializeComponent();
            DataContext = country;
            _onAction = onAction;
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Db.Entities.Countries.AddOrUpdate((Countries)DataContext);
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