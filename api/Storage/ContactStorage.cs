
public class InMemoryStorage : IStorage
{
    private List<Contact> Contacts {get;set;}
    public InMemoryStorage()
    {
        this.Contacts = new List<Contact>();

        for(int i = 0; i < 5; i++)
        {
            this.Contacts.Add(new Contact()
            {
                Id = i,
                Name = $"Полное имя {i}",
                Email = $"{Guid.NewGuid().ToString().Substring(0, 5)}_{i}@ksergey.ru"
            });
        }
    }
    public List<Contact> GetContacts()
    {
        return Contacts;
    }
    public bool Add(Contact contact)
    {
        foreach (var item in Contacts)
        {
            if(contact.Id == item.Id)
            {
                return false;
            }
        }
        Contacts.Add(contact);
        return true;
    }
    public bool Remove(int id)
    {
        Contact contact;
        for(int i = 0; i < Contacts.Count; i++)
        {
            if(this.Contacts[i].Id  == id)
            {
                contact = this.Contacts[i];
                Contacts.Remove(contact);
                return true;
            }
        }
        return false;
    }
    public bool UpdateContact(ContactDto contactDto, int id)
    {
        Contact contact;
        for(int i = 0; i < Contacts.Count; i++)
        {
            if(Contacts[i].Id == id)
            {
                contact = Contacts[i];
                if(!String.IsNullOrEmpty(contactDto.Email))
                    contact.Email = contactDto.Email;
                if(!String.IsNullOrEmpty(contactDto.Name))
                    contact.Name = contactDto.Name;
                return true;
            }
        }
        return false;
    }
    public Contact FindContactId(int id)
    {
        foreach (var item in Contacts)
        {
            if(item.Id == id) return item;
        }
        return null;
    }
}