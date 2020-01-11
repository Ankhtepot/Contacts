using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Classes
{
    public interface ISqlDao
    {
        IContact BuildContact(string name, string email, string phone);

        void CommitContact(IContact contact);

        void DeleteContact(IContact contact);

        void UpdateContact(IContact contact);

        List<Contact> ReadAllContacts();
    }
}
