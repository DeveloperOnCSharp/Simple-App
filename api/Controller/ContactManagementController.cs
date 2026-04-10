using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
public class ContactManagementController : BaseController
{
    // переменная для хранения ссылки на модель подключения к БД
    private readonly IStorage storage;

    public ContactManagementController(IStorage storage)
    {
        this.storage = storage;
    }
    
    [HttpPost("contacts")]
    public IActionResult Create([FromBody]Contact contact)
    {
        bool result = storage.Add(contact);
        if(result)
        {
            return Created();
        }
        return Conflict("Контакт с указанным ID существует");
    }

    [HttpGet("contacts")]
    public ActionResult<List<Contact>> GetContacts()
    {
        return Ok(storage.GetContacts());
    }
    
    [HttpDelete("contacts/{id}")]
    public IActionResult DeleteContact(int id)
    {
        bool res = storage.Remove(id);
        if(res) return NoContent();
        return BadRequest("Ошибка ID");
    }
    
    [HttpPut("contacts/{id}")]
    public IActionResult UpdateContact([FromBody]ContactDto contactDto, int id)
    {
        bool res = storage.UpdateContact(contactDto, id);
        if(res) return Ok();
        return Conflict("Контакт с указанным ID не нашелся");
    }
    
    // [HttpGet("contacts/{id}")]
    // public ActionResult<Contact> FindContactId(int id)
    // {
    //     if (id < 1)
    //     return BadRequest("ID должно быть положительным числом.");

    // Contact contact = storage.FindContactId(id);
    
    // if (contact is null) 
    //     return NotFound($"Контакт с ID {id} не найден.");

    // return Ok(contact);
    // }
}