using Contacts.Classes.OracleXE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Classes
{
    public class SqlService
    {
        ISqlDao SqlDao;

        public enum SqlDaoType
        {
            SQLiteDao,
            SQLOracle
        }

        public SqlService(SqlDaoType sqlDaoType)
        {
            InstantiateSqlDao(sqlDaoType);
        }

        private void InstantiateSqlDao(SqlDaoType sqlDaoType)
        {
            switch(sqlDaoType)
            {
                case SqlDaoType.SQLiteDao: SqlDao = new SQLiteDao(App.DB_NAME, App.db_folder_path); break;
                case SqlDaoType.SQLOracle: SqlDao = new SQLOracle("TODO connection string"); break;
            }
        }

        public void CommitContact(string name, string email, string phone)
        {
            IContact newContact = SqlDao.BuildContact(name, email, phone);
            SqlDao.CommitContact(newContact);
        }

        public List<Contact> ReadAllContacts()
        {
            return SqlDao.ReadAllContacts();
        }

        public void DeleteContact(Contact contact)
        {
            SqlDao.DeleteContact(contact);
        }

        public void UpdateContact(Contact contact)
        {
            SqlDao.UpdateContact(contact);
        }
    }
}
