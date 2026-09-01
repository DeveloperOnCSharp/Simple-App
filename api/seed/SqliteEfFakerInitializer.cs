using Bogus;
using contact_app.dataContext;
using Microsoft.EntityFrameworkCore;
namespace contact_app.seed;

public class SqliteEfFakerInitializer : IInitializer
{
    private readonly SqliteDbContext context;

    public SqliteEfFakerInitializer(SqliteDbContext context)
    {
        this.context = context;
    }

    public string GenerateEmailForName(string name)
    {
        string email = Translirate(name)
        .ToLower()
        .Replace(" ", ".") + "@example.com";
        return email;
    }

    private string Translirate(string name)
    {
        Dictionary<char, string> transliteTable = new()
        {
          {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"},
          {'е', "e"}, {'ё', "yo"}, {'ж', "zh"}, {'з', "z"}, {'и', "i"},
          {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"},
          {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
          {'у', "u"}, {'ф', "f"}, {'х', "h"}, {'ц', "c"}, {'ч', "ch"},
          {'ш', "sh"}, {'щ', "sch"}, {'ъ', "y"}, {'ы', "y"}, {'ь', "y"},
          {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
        };

        var result = "";
        foreach (var ch in name.ToLower())
        {
            if (transliteTable.ContainsKey(ch)) result += transliteTable[ch];
            else result += ch;
        }
        return result;
    }
    public void Initialize()
    {
        context.Database.Migrate();
        if (!context.Contacts.Any())
        {
            var faker = new Faker<Contact>("ru")
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Email, (f, c) => GenerateEmailForName(c.Name));
            var contacts = faker.Generate(20);
            context.Contacts.AddRange(contacts);
            context.SaveChanges();
        }
    }

}
