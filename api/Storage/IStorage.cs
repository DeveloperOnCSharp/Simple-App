// Интерфейс для работы с хранилищем контактов
public interface IStorage
{
    List<Contact> GetContacts();
    bool Add(Contact contact);
    bool Remove(int id);
    bool UpdateContact(ContactDto contactDto, int id);
    Contact FindContactId(int id);
}
