using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace минимагазин
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();

            products.Add(new Product { Name = "Яблоко", Price = 50, InStock = true });
            products.Add(new Product { Name = "Банан", Price = 70, InStock = false });

            ProductsGrid.ItemsSource = products;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text) || string.IsNullOrWhiteSpace(PriceInput.Text))
            {
                MessageBox.Show("Заполните поля!");
                return;
            }

            int price = 0;
            try
            {
                price = Convert.ToInt32(PriceInput.Text);
            }
            catch
            {
                MessageBox.Show("Цена введена неверно");
                return;
            }

            if (price < 0)
            {
                MessageBox.Show("Цена не может быть отрицательной!");
                return;
            }

            products.Add(new Product
            {
                Name = NameInput.Text,
                Price = price,
                InStock = true
            });

            NameInput.Clear();
            PriceInput.Clear();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product p)
            {
                MessageBoxResult q = MessageBox.Show("Вы точно хотите удалить",
                    "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (q == MessageBoxResult.Yes)
                    products.Remove(p);
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?",
                "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}