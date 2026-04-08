import axios from 'axios';
import React, { useState, useEffect } from "react";
import TableContact from "./layout/TableContact/TableContact";
import FormContact from "./layout/FormContact/FormContact";

const baseApiUrl = process.env.REACT_APP_API_URL;

const App = () => {
  
  const [contacts, setContacts] = useState([])
  const url = `${baseApiUrl}/contacts`;
  useEffect(()=>{
    console.log(url);
  axios.get(url).then(
    res => setContacts(res.data)
  );
  },[url]);

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
    let newId = 0;
    for (let i = 0; i < contacts.length; i++) {
      const elementId = contacts[i].id;
      if (elementId > newId) {
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
    axios.post(url, item);
    // добавляем новый контакт в массив контактов
    setContacts([...contacts, item]);
  }

  const deleteContact = (id) => {
    setContacts(contacts.filter(item => item.id !== id));
    axios.delete(`${url}/${id}`);
  }

  return (
    <div className="container mt-5">
      <div className="card">
        <div className="card-header">
          <h1>Список контактов</h1>
        </div>
        
        <div className="card-body">
          <TableContact 
            contacts={contacts}
            deleteContact={deleteContact}
          />
          
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
