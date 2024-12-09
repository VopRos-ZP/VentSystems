using System.Linq;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;
using VentSystems.Windows.Upsert;

namespace VentSystems.Windows
{
    public partial class AdminWindow : Window
    {
        public AdminWindow(Users user)
        {
            InitializeComponent();
            
            ProductsList.ItemsSource = Db.Entities.Products.ToList();
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            UpsertProduct(new Products());
        }

        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ProductsList.SelectedItem is Products product)) return;
            UpsertProduct(product);
        }

        private void UpsertProduct(Products product)
        {
            new UpsertProductWindow(product).ShowDialog();
        }
        
        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ProductsList.SelectedItem is Products product)) return;
            Db.Entities.Products.Remove(product);
            Db.Entities.SaveChanges();
        }
    }
}