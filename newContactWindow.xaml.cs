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
using System.Windows.Shapes;
using System.IO;
using SQLite;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for newContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        private SqlService SqlService = null;

        public NewContactWindow(SqlService sqlService)
        {
            InitializeComponent();

            SqlService = sqlService;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SqlService != null)
            {
                SqlService.CommitContact(nameTextBox.Text, emailTextBox.Text, phoneTextBox.Text); 
            }

            this.Close();
        }       
    }
}
