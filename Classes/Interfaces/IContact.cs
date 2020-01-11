using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Classes
{
    public interface IContact
    {
         int Id { get; set; }
         string Name { get; set; }
         string Email { get; set; }
         string Phone { get; set; }
    }
}
