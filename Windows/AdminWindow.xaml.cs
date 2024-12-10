using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VentSystems.Model;
using VentSystems.Utils;
using VentSystems.Windows.Upsert;

namespace VentSystems.Windows
{
    public partial class AdminWindow : Window
    {
        private readonly List<string> _excludeNames = new List<string>();

        public AdminWindow(Users user)
        {
            InitializeComponent();

            InitExcludeNames();

            InitDataGrid(UsersList,Db.Entities.Users.ToList());
            InitDataGrid(RolesList,Db.Entities.Roles.ToList());
            InitDataGrid(SuppliersList,Db.Entities.Suppliers.ToList());
            InitDataGrid(StoragesList,Db.Entities.Storages.ToList());
            InitDataGrid(CountriesList,Db.Entities.Countries.ToList());
            
            UpdateProductsList(Db.Entities.Products.ToList());

            FindBox.TextChanged += (s, e) =>
            {
                var filter = Db.Entities.Products.ToList()
                    .FindAll(p => p.Name.ToLower().Contains(FindBox.Text.ToLower()));
                UpdateProductsList(filter);
            };
        }

        private void InitExcludeNames()
        {
            _excludeNames.Add("Users");
            _excludeNames.Add("Roles");
            _excludeNames.Add("Products");
            _excludeNames.Add("Storages");
            _excludeNames.Add("Suppliers");
            _excludeNames.Add("Countries");
        }

        private void InitDataGrid<T>(DataGrid dataGrid, List<T> source)
        {
            dataGrid.CanUserSortColumns = true;
            dataGrid.ItemsSource = source;
        }

        private void UpdateProductsList(List<Products> products)
        {
            ProductsList.ItemsSource = products;
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

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (_excludeNames.Contains(propertyDescriptor.DisplayName))
            {
                e.Cancel = true;
            }
        }
    }
}