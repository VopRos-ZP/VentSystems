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

        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (ProductsTab.IsSelected)
            {
                if (!(ProductsList.SelectedItem is Products product)) return;
                Db.Entities.Products.Remove(product);
                UpdateProductsList(Db.Entities.Products.ToList());
            }
            else if (UsersTab.IsSelected)
            {
                if (!(UsersList.SelectedItem is Users user)) return;
                Db.Entities.Users.Remove(user);
                OnUsersUpdate();
            }
            else if (RolesTab.IsSelected)
            {
                if (!(RolesList.SelectedItem is Roles roles)) return;
                Db.Entities.Roles.Remove(roles);
                OnRolesUpdate();
            }
            else if (SuppliersTab.IsSelected)
            {
                if (!(SuppliersList.SelectedItem is Suppliers suppliers)) return;
                Db.Entities.Suppliers.Remove(suppliers);
                OnSuppliersUpdate();
            }
            else if (StoragesTab.IsSelected)
            {
                if (!(StoragesList.SelectedItem is Storages storages)) return;
                Db.Entities.Storages.Remove(storages);
                OnStoragesUpdate();
            }
            else if (CountriesTab.IsSelected)
            {
                if (!(CountriesList.SelectedItem is Countries countries)) return;
                Db.Entities.Countries.Remove(countries);
                OnCountriesUpdate();
            }
            Db.Entities.SaveChanges();
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