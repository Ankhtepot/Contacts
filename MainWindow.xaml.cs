using Contacts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Contacts.Classes.SqlService;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SqlService SqlService = new SqlService(SqlDaoType.SQLiteDao);

        List<Contact> contacts = null;

        private bool searchUsed = false;

        public MainWindow()
        {
            InitializeComponent();            

            ReadDatabase();

            if(contacts != null)
            {
                sqliteConnectionButton.Style = FindResource(App.SUCCESS_BUTTON_STYLE) as Style;
            }
        }

        private void newContactButton_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow(SqlService);
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            if (SqlService != null)
            {
                contacts = SqlService.ReadAllContacts();

                if (contacts != null)
                {
                    contacts.OrderBy(contact => contact.Name).ToList();
                }

                contactsListView.ItemsSource = contacts;
            }

            if(!string.IsNullOrEmpty(App.SqlError))
            {
                ErrorTextBlock.Text = App.SqlError;
                ErrorTextBlock.Style = FindResource(App.ERROR_TEXTBLOCK_STYLE) as Style;
                ErrorBorder.Style = FindResource(App.ERROR_BORDER_STYLE) as Style;
                App.SqlError = "";
            }
            else
            {
                ErrorTextBlock.Style = FindResource(App.BASE_TEXTBLOCK_STYLE) as Style;
                ErrorBorder.Style = FindResource(App.BASE_BORDER_STYLE) as Style;
                ErrorTextBlock.Text = "";
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (contacts != null && contacts.Count > 0)
            {
                TextBox searchTexBox = (TextBox)sender; // alternatively = sender as textBox

                var filteredList = contacts
                    .Where(contact => contact.Name.ToLower()
                    .Contains(searchTexBox.Text.ToLower()))
                    .ToList();

                contactsListView.ItemsSource = filteredList;
            }
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = contactsListView.SelectedItem as Contact;

            if(selectedContact != null)
            {
                new ContactDetailWindow(selectedContact, SqlService).ShowDialog();

                ReadDatabase();
            }
        }

        private void searchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!searchUsed)
            {
                Style normalStyle = FindResource(App.BASE_TEXTBOX_STYLE) as Style;
                searchTextBox.Style = normalStyle;
                searchTextBox.Text = "";
                searchUsed = true;
            }
        }

        private void sqliteConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            contacts = null;
            SqlService = new SqlService(SqlDaoType.SQLiteDao);

            ReadDatabase();

            if(contacts != null)
            {
                sqliteConnectionButton.Style = FindResource(App.SUCCESS_BUTTON_STYLE) as Style;
                oracleConnectionButton.Style = FindResource(App.BASE_BUTTON_STYLE) as Style;
            }
        }

        private void oracleConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            contacts = null;
            SqlService = new SqlService(SqlDaoType.SQLOracle);

            ReadDatabase();

            if (contacts != null)
            {
                sqliteConnectionButton.Style = FindResource(App.BASE_BUTTON_STYLE) as Style;
                oracleConnectionButton.Style = FindResource(App.SUCCESS_BUTTON_STYLE) as Style;
            }
        }
    }
}
