// Интерфейс для работы с хранилищем контактов
using contact_app.dataContext;
using contact_app.storage;

public class PaginationSqliteEFStorage : SqliteEFStorage, IPaginationStorage
{
    public PaginationSqliteEFStorage(SqliteDbContext context) : base(context)
    {

    }

    public Contact GetContactById(int id)
    {
        return base.context.Contacts.Find(id);
    }

    public (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize)
    {
        int total = base.context.Contacts.Count();
        var contacts = base.context.Contacts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return (contacts, total);
    }

}
