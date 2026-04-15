using Microsoft.Data.Sqlite;
namespace contact_app.storage;

public class SqliteStorage(string connectionString) : IStorage
{
    private readonly string connectionString = connectionString;

    public List<Contact> GetContacts()
    {
        const string sql = "SELECT * FROM contacts;";

        return ExecuteQuerry(sql, cmd =>
        {
            var contacts = new List<Contact>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                contacts.Add(new Contact
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                });
            }
            return contacts;
        });
    }

    public Contact Add(Contact contact)
    {
        const string sql = @"INSERT INTO contacts(name, email) VALUES (@name, @email);
            SELECT last_insert_rowid();";

        var parameters = new Dictionary<string, object>
        {
            { "@name", contact.Name },
            { "@email", contact.Email }
        };

        int newId = ExecuteQuerry(sql, cmd => Convert.ToInt32(cmd.ExecuteScalar()), parameters);
        contact.Id = newId;
        return contact;
    }

    public bool Remove(int id)
    {
        const string sql = "DELETE FROM contacts WHERE id = @id;";
        var parameters = new Dictionary<string, object>
        {
            { "@id", id }
        };
        return ExecuteQuerry(sql, cmd => cmd.ExecuteNonQuery() > 0, parameters);
    }

    public bool UpdateContact(ContactDto contactDto, int id)
    {
        const string sql = "UPDATE contacts SET name = @name, email = @email WHERE id = @id;";
        var parameters = new Dictionary<string, object>
        {
            { "@name", contactDto.Name },
            { "@email", contactDto.Email },
            { "@id", id }
        };
        return ExecuteQuerry(sql, cmd => cmd.ExecuteNonQuery() > 0, parameters);
    }
    private SqliteConnection CreateAndOpenConnection()
    {
        var connection = new SqliteConnection(connectionString);
        connection.Open();
        return connection;
    }
    private T ExecuteQuerry<T>(
        string sql,
        Func<SqliteCommand, T> action,
        Dictionary<string, object> parameters = null)
    {
        using var connection = CreateAndOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = sql;

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }
        }

        return action(command);
    }
}
