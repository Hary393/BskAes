using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for FIXWindow.xaml
    /// </summary>
    public partial class FIXWindow : Window
    {
        public List<string> booklist;
        public List<KeyValuePair<string, int>> orderlist;
        public OrderWindow p;
        public FIXWindow(OrderWindow parent)
        {
            InitializeComponent();
            p = parent;
            booklist = p.booklist;
            orderlist = p.TestList;
            Books.ItemsSource = null;
            Books.ItemsSource = booklist;

            
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            orderlist.Add(new KeyValuePair<string, int>(Books.SelectedItem.ToString(), int.Parse(Amt.Text)));
            p.OrderGrid.ItemsSource = null;
            p.OrderGrid.ItemsSource = orderlist;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ValidateNumberText(TextBox txt)
        {
            txt.Text = Regex.Replace(txt.Text, @"[^\d-]", string.Empty);
            txt.SelectionStart = txt.Text.Length; // add some logic if length is 0
            txt.SelectionLength = 0;
        }

        private void Amt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumberText(Amt);
        }
    }
}
