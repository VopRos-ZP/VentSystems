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
    public partial class ManagerWindow : Window
    {
        
        private readonly List<string> _excludeNames = new List<string>();
        
        public ManagerWindow(Users user)
        {
            InitializeComponent();

            InitExcludeNames();

            InitDataGrid(UsersList, Db.Entities.Users.ToList());
            InitDataGrid(RolesList, Db.Entities.Roles.ToList());
            InitDataGrid(SuppliersList, Db.Entities.Suppliers.ToList());
            InitDataGrid(StoragesList, Db.Entities.Storages.ToList());
            InitDataGrid(CountriesList, Db.Entities.Countries.ToList());

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
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.ItemsSource = source;
        }

        private void UpdateProductsList(List<Products> products)
        {
            ProductsList.ItemsSource = products;
        }
        
        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (ProductsTab.IsSelected)
            {
                UpsertProduct(new Products());
            }
            else if (UsersTab.IsSelected)
            {
                var user = new Users();
                if (UsersList.SelectedItem != null && ((Button)sender).Name != "AddBtn" )
                {
                    user = (Users)UsersList.SelectedItem;
                }
                new UpsertUserWindow(user, OnUsersUpdate).ShowDialog();
            }
            else if (RolesTab.IsSelected)
            {
                var roles = new Roles();
                if (RolesList.SelectedItem != null && ((Button)sender).Name != "AddBtn" )
                {
                    roles = (Roles)RolesList.SelectedItem;
                }
                new UpsertRoleWindow(roles, OnRolesUpdate).ShowDialog();
            }
            else if (SuppliersTab.IsSelected)
            {
                var suppliers = new Suppliers();
                if (SuppliersList.SelectedItem != null && ((Button)sender).Name != "AddBtn" )
                {
                    suppliers = (Suppliers)SuppliersList.SelectedItem;
                }
                new UpsertSupplierWindow(suppliers, OnSuppliersUpdate).ShowDialog();
            }
            else if (StoragesTab.IsSelected)
            {
                var storages = new Storages();
                if (StoragesList.SelectedItem != null && ((Button)sender).Name != "AddBtn" )
                {
                    storages = (Storages)StoragesList.SelectedItem;
                }
                new UpsertStorageWindow(storages, OnStoragesUpdate).ShowDialog();
            }
            else if (CountriesTab.IsSelected)
            {
                var countries = new Countries();
                if (CountriesList.SelectedItem != null && ((Button)sender).Name != "AddBtn" )
                {
                    countries = (Countries)CountriesList.SelectedItem;
                }
                new UpsertCountryWindow(countries, OnCountriesUpdate).ShowDialog();
            }
        }
        
        private void UpsertProduct(Products product)
        {
            new UpsertProductWindow(product).ShowDialog();
        }

        private void OnUsersUpdate() => UsersList.ItemsSource = Db.Entities.Users.ToList();
        private void OnRolesUpdate() => RolesList.ItemsSource = Db.Entities.Roles.ToList();
        private void OnSuppliersUpdate() => SuppliersList.ItemsSource = Db.Entities.Suppliers.ToList();
        private void OnStoragesUpdate() => StoragesList.ItemsSource = Db.Entities.Storages.ToList();
        private void OnCountriesUpdate() => CountriesList.ItemsSource = Db.Entities.Countries.ToList();
        
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