using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VentSystems.Model;
using VentSystems.Utils;

namespace VentSystems.Windows
{
    public partial class UserWindow : Window
    {
        public UserWindow(Users user)
        {
            InitializeComponent();
            
            Title = $"{user.LastName} {user.FirstName} {user.LastName}";
            
            UpdateProductsList(Db.Entities.Products.ToList());
            
            FindBox.TextChanged += (s, e) =>
            {
                var filter = Db.Entities.Products.ToList().FindAll(p => p.Name.ToLower().Contains(FindBox.Text.ToLower()));
                UpdateProductsList(filter);
            };
        }

        private void UpdateProductsList(List<Products> products)
        {
            ProductsList.ItemsSource = products;
        }
        
    }
}