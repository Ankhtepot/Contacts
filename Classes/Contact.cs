using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Classes
{
    public class Contact : IContact
    {
        public int Id { get; set ; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"Contact Nr. {Id} | Name: {Name} | Email: {Email} | Phone: {Phone}";
        }

        public static explicit operator Contact(SQLiteContact sqliteContact) => new Contact 
        {
            Id = sqliteContact.Id,
            Name = sqliteContact.Name,
            Email = sqliteContact.Email,
            Phone = sqliteContact.Phone
        };
        

    }
}
