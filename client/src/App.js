import React, { useState } from "react";
import TableContact from "./layout/TableContact/TableContact";
import FormContact from "./layout/FormContact/FormContact";

const App = () => {
  const [contacts, setContacts] = useState([
    { id: 1, name: "Иван Иванов", email: "ivan.ivanov@example.com" },
    { id: 2, name: "Петр Петров", email: "petr.petrov@example.com" },
    { id: 152, name: "Сидор Сидоров", email: "sidor.sidorov@example.com" },
    { id: 4, name: "Мария Смирнова", email: "maria.smirnova@example.com" }
  ]
)
  // хуки для обработки ошибок
  const [error, setError] = useState("");

  //сортировка массива
  const addContact = (contactName, contactEmail) => {
    
    //Обработка пустых полей
    if (contactName === "" || contactEmail === "") {
      setError("Имя и email не могут быть пустыми.");
      return;
    }
    // обнуляем состояние ошибки
    setError("");

    // Логика добавления нового контакта
    let newId = -1;
    for( let i = 0; i < contacts.length; i++) {
      const elementId = contacts[i].id;
      if(elementId > newId) {
        newId = elementId;
      }
    }
    // увеличиваем id на единицу
    newId++;

    // создаем новый контакт, передаем в качестве параметров имя и email
    const item = {
      id: newId,
      name: contactName,
      email: contactEmail
    };

    // добавляем новый контакт в массив контактов
    setContacts([...contacts, item]);
    console.log(contacts);
  }

  return (
    <div className="container mt-5">
      <div className="card">
        <div className="card-header">
          <h1>Список контактов</h1>
        </div>
        
        <div className="card-body">
          <TableContact contacts={contacts} />
          
          {error && (
            <div className="alert alert-danger mt-3">
              {error}
            </div>
          )}

          <FormContact addContact={addContact}/>
        </div>
      </div>
    </div>
  );
}

export default App;
