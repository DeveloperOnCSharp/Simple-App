using Bogus;
using Microsoft.Data.Sqlite;

public class FakerInitializer : IInitializer
{
    private string connectionString;

    public FakerInitializer(string connectionString)
    {
        this.connectionString = connectionString;
    }


    public FakerInitializer()
    {
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
        CREATE TABLE IF NOT EXISTS contacts (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            email TEXT NOT NULL
        );
        ";
        command.ExecuteNonQuery();

        command.CommandText = @"SELECT count(*) FROM contacts";
        command.ExecuteNonQuery();
        long count = (long)command.ExecuteScalar();

        if (count == 0)
        {
            var faker = new Faker<Contact>("ru")
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .RuleFor(c => c.Email, f => f.Internet.Email());

            var contact = faker.Generate(20);

            foreach (var c in contact)
            {
                command.CommandText = @"INSERT INTO contacts (name, email) VALUES (@name, @email)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@name", c.Name);
                command.Parameters.AddWithValue("@email", c.Email);
                command.ExecuteNonQuery();
            }
        }
    }
}
