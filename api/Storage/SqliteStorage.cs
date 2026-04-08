using System.Text;
using Microsoft.Data.Sqlite;

public class SqliteStorage : IStorage
{
    string connectionString = "Data Source=contact.db";
    public bool Add(Contact contact)
    {
        //Создаем соединение к БД, используем using для автоматического закрытия соединения
        using var connection = new SqliteConnection(connectionString);
        //Открываем соединение
        connection.Open();

        //Создаем команду для выполнения SQL
        var command = connection.CreateCommand();
        //Создаем SQL запрос на добавление контакта в БД 
        // и добавляем его в команду
        string sql = new StringBuilder()
        .Append("INSERT INTO contacts(name, email) VALUES")
        .Append($"('{contact.Name}','{contact.Email}');").ToString();
        command.CommandText = sql;
        return command.ExecuteNonQuery() > 0;
    }

    public Contact FindContactId(int id)
    {
        throw new NotImplementedException();
    }

    public List<Contact> GetContacts()
    {
        var contact = new List<Contact>();

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM contacts";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            contact.Add(new Contact()
            {
               Id = reader.GetInt32(0),
               Name = reader.GetString(1),
               Email = reader.GetString(2), 
            });
        }


        return contact;
    }

    public bool Remove(int id)
    {
        throw new NotImplementedException();
    }

    public bool UpdateContact(ContactDto contactDto, int id)
    {
        throw new NotImplementedException();
    }
}