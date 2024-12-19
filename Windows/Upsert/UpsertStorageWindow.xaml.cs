using System;
using System.Data.Entity.Migrations;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows.Upsert
{
    public partial class UpsertStorageWindow : Window
    {

        private readonly Action _onAction;
        
        public UpsertStorageWindow(Storages storages, Action onAction)
        {
            InitializeComponent();
            DataContext = storages;
            _onAction = onAction;
        }


        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Db.Entities.Storages.AddOrUpdate((Storages)DataContext);
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