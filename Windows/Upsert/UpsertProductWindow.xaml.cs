using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows.Upsert
{
    public partial class UpsertProductWindow : Window
    {
        public UpsertProductWindow(Products product)
        {
            InitializeComponent();
            
            DataContext = product;
            
            StoragesBox.ItemsSource = Db.Entities.Storages.ToList();
            SuppliersBox.ItemsSource = Db.Entities.Suppliers.ToList();
        }
        
        private void SetImageBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                DefaultExt = ".png",
                FileName="MyImage.png",
                Filter="Рисунки|*.jpg;*.jpeg;*.png;.*bmp;"
            };
            var result = dialog.ShowDialog();
            if (result != true) return;
            if (!(DataContext is Products ctx)) return;
            ctx.Image = File.ReadAllBytes(dialog.FileName);
        }
        
        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Db.Entities.Products.AddOrUpdate(DataContext as Products);
                Db.Entities.SaveChanges();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить продукт");
            }
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void CountTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!CountTextBox.Text.All(char.IsDigit))
            {
                CountTextBox.Text = CountTextBox.Text.Take(CountTextBox.Text.Length - 1).ToString();
            }
        }
    }
}