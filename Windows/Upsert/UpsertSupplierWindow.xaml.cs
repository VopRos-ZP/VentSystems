using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows.Upsert
{
    public partial class UpsertSupplierWindow : Window
    {
        
        private readonly Action _onAction;
        
        public UpsertSupplierWindow(Suppliers supplier, Action onAction)
        {
            InitializeComponent();
            DataContext = supplier;
            CountriesBox.ItemsSource = Db.Entities.Countries.ToList();
            _onAction = onAction;
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Db.Entities.Suppliers.AddOrUpdate((Suppliers)DataContext);
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