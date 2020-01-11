using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Classes
{
    [Table("Contacts")]
    public class SQLiteContact : IContact
    {
        [PrimaryKey, AutoIncrement, Column("Id"), Indexed]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }

        public SQLiteContact() { }

        private SQLiteContact(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public static SQLiteContact ToSQLiteContact(IContact contact)
        {
            return new SQLiteContact( contact.Id, contact.Name, contact.Email, contact.Phone );
        }
}
}
