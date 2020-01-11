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

namespace Contacts
{
    /// <summary>
    /// Interaction logic for ContactDetailWindow.xaml
    /// </summary>
    public partial class ContactDetailWindow : Window
    {
        Contact contact = null;
        SqlService SqlService = null;

        public ContactDetailWindow(Contact contact, SqlService sqlService)
        {
            InitializeComponent();

            this.contact = contact;
            this.SqlService = sqlService;

            SetFields();
        }

        private void SetFields()
        {
            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            SqlService.DeleteContact(contact);

            Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            SqlService.UpdateContact(new Contact
            {
                Id = contact.Id,
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneTextBox.Text
            }) ;

            Close();
        }
    }
}
