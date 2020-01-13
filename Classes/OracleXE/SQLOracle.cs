using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Classes.OracleXE
{
    public class SQLOracle : ISqlDao
    {
        public string ConnectionString { get; set; }

        private const string CONTACTS_TABLE = "contacts";
        private const string NAME_COLUMN = "Name";
        private const string EMAIL_COLUMN = "Email";
        private const string PHONE_COLUMN = "Phone";
        private const string ID_COLUMN = "Id";

        public SQLOracle(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private void checkDatabase()
        {
            if(!CheckForContactsTable())
            {

            }
        }

        public bool CheckForContactsTable()
        {
            try
            {
                using (OracleConnection connection = getConnection())
                {
                    connection.ConnectionString = buildConnectionString();

                    connection.Open();

                    OracleCommand cmd = connection.CreateCommand();

                    OracleTransaction txn = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    cmd.CommandText = "SELECT COUNT(*) FROM " + CONTACTS_TABLE;

                    if(int.Parse(cmd.ExecuteScalar().ToString()) == 0)
                    {
                        return false;
                    }
                }
            }
            catch (OracleException e)
            {
                App.SqlError = e.Number + ": " + e.Message;
                return false;
            }

            return true;
        }

        private OracleConnection getConnection()
        {
            return new OracleConnection();
        }

        public IContact BuildContact(string name, string email, string phone)
        {
            return new Contact
            {
                Name = name,
                Email = email,
                Phone = phone
            };
        }

        public void CommitContact(IContact contact)
        {
            string SqlCommand = "INSERT INTO " + CONTACTS_TABLE + " (" + ID_COLUMN + " ," + NAME_COLUMN + " ," + EMAIL_COLUMN + " ," + PHONE_COLUMN + ") VALUES (" +
                "contacts_id_seq.nextval,'" + contact.Name + "', '" + contact.Email + "', '" + contact.Phone + "')";
            Console.WriteLine("[SQLOracle>CommitContact] sql: " + SqlCommand);

            ExecuteAndCommitCommand(SqlCommand);
        }

        public List<Contact> ReadAllContacts()
        {
            List<Contact> resultSet = null;

            try
            {
                using (OracleConnection connection = getConnection())
                {
                    
                    connection.ConnectionString = buildConnectionString();

                    connection.Open();

                    string sql = "SELECT * FROM " + CONTACTS_TABLE;

                    OracleDataAdapter oda = new OracleDataAdapter(sql, connection);
                    DataTable dt = new DataTable();
                    oda.Fill(dt);

                    resultSet = new List<Contact>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        resultSet.Add(new Contact
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["NAME"].ToString(),
                            Email = dr["EMAIL"].ToString(),
                            Phone = dr["PHONE"].ToString(),
                        });
                    }
                }
            }
            catch (OracleException e)
            {
                App.SqlError = e.Number + ": " + e.Message;
            }

            return resultSet;
        }

        public void DeleteContact(IContact contact)
        {
            try
            {
                using (OracleConnection connection = getConnection())
                {
                    connection.ConnectionString = buildConnectionString();

                    connection.Open();

                    string sql = "DELETE FROM " + CONTACTS_TABLE + " WHERE id=" + contact.Id;

                    ExecuteAndCommitCommand(sql);
                }
            }
            catch (OracleException e)
            {
                App.SqlError = e.Number + ": " + e.Message;
            }
        }

        public void UpdateContact(IContact contact)
        {
            try
            {
                using (OracleConnection connection = getConnection())
                {
                    connection.ConnectionString = buildConnectionString();

                    connection.Open();

                    string SqlCommand = "UPDATE " + CONTACTS_TABLE + " SET " +
                        NAME_COLUMN + " = '" + contact.Name + "', " +
                        EMAIL_COLUMN + " = '" + contact.Email + "', " +
                        PHONE_COLUMN + " = '" + contact.Phone + "'" +
                        " WHERE id=" + contact.Id;
                    Console.WriteLine("[SQLOracle>CommitContact] sql: " + SqlCommand);

                    ExecuteAndCommitCommand(SqlCommand);
                }
            }
            catch (OracleException e)
            {
                App.SqlError = e.Number + ": " + e.Message;
            }
        }

        private string buildConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("User ID="); sb.Append(App.USER_ID); sb.Append("; ");
            sb.Append("Password ="); sb.Append(App.PASSWORD); sb.Append(";");
            sb.Append("Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = tcp)");
            sb.Append("(HOST = "); sb.Append(App.HOST); sb.Append(")");
            sb.Append("(PORT = "); sb.Append(App.PORT); sb.Append("))");
            sb.Append("(CONNECT_DATA = (SERVICE_NAME = "); sb.Append(App.SERVICE_NAME); sb.Append(")))");
                       
            return sb.ToString();
        }

        private void ExecuteAndCommitCommand(string sqlCommand)
        {
            try
            {
                using (OracleConnection connection = getConnection())
                {
                    connection.ConnectionString = buildConnectionString();

                    connection.Open();

                    OracleCommand cmd = connection.CreateCommand();

                    OracleTransaction txn = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    cmd.CommandText = sqlCommand;
                    cmd.ExecuteNonQuery();
                    txn.Commit();
                }
            }
            catch (OracleException e)
            {
                App.SqlError = e.Number + ": " + e.Message;
            }
        }
    }
}
