using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Classes
{
    public class SQLiteDao : ISqlDao
    {
        public string DbName { get; set; }
        public string DbPath { get; set; }

        public SQLiteDao(string dbName, string dbPath) {
            DbName = dbName;
            DbPath = dbPath;
        }

        public bool checkDatabase()
        {
            return true;
        }

        private  SQLiteConnection getConnection()
        {
            return new SQLiteConnection(Path.Combine(DbPath, DbName));
        }

        public IContact BuildContact(string name, string email, string phone)
        {
            return new SQLiteContact
            {
                Name = name,
                Email = email,
                Phone = phone
            };
        }

        public void CommitContact(IContact contact)
        {
           using(SQLiteConnection connection = getConnection())
            {
                SQLiteContact SqliteContact = (SQLiteContact)contact;

                // Create Table if it dosnt exist
                connection.CreateTable<SQLiteContact>();

                connection.Insert(SqliteContact);
            }
        }

        public List<Contact> ReadAllContacts()
        {
            using (SQLiteConnection connection = getConnection())
            {
                // Create Table if it dosnt exist
                connection.CreateTable<SQLiteContact>();

                return connection.Table<SQLiteContact>().Select(contact => (Contact)contact).ToList();
            }
        }

        public void DeleteContact(IContact contact)
        {
            using (SQLiteConnection connection = getConnection())
            {
                SQLiteContact SqliteContact = SQLiteContact.ToSQLiteContact(contact);

                // Create Table if it dosnt exist
                connection.CreateTable<SQLiteContact>();

                connection.Delete(SqliteContact);
            }
        }

        public void UpdateContact(IContact contact)
        {
            using (SQLiteConnection connection = getConnection())
            {
                SQLiteContact SqliteContact = SQLiteContact.ToSQLiteContact(contact);

                // Create Table if it dosnt exist
                connection.CreateTable<SQLiteContact>();

                connection.Update(SqliteContact);
            }
        }
    }
}
