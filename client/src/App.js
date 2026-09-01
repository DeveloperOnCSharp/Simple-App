import axios from "axios";
import React, { useState, useEffect } from "react";
import TableContact from "./layout/TableContact/TableContact";
import FormContact from "./layout/FormContact/FormContact";
import ContactDetails from "./layout/ContactDetails/ContactDetails";
import { Routes, Route, useLocation } from "react-router-dom";

const baseApiUrl = process.env.REACT_APP_API_URL;

const App = () => {
  const [contacts, setContacts] = useState([]);
  //хук для обновления контактов после их изменения или удаления
  const location = useLocation();
  // хуки для обработки ошибок
  const url = `${baseApiUrl}/contacts`;
  const [error, setError] = useState("");
  useEffect(() => {
    axios.get(url).then((res) => setContacts(res.data));
  }, [location.pathname, url]);
  //сортировка массива
  const addContact = (contactName, contactEmail) => {
    //Обработка пустых полей
    if (contactName === "" || contactEmail === "") {
      setError("Имя и email не могут быть пустыми.");
      return;
    }
    // обнуляем состояние ошибки
    setError("");

    // создаем новый контакт, передаем в качестве параметров имя и email
    const item = {
      name: contactName,
      email: contactEmail,
    };
    axios.post(url, item).then((res) => setContacts([...contacts, res.data]));
  };
  // const deleteContact = (id) => {
  //   setContacts(contacts.filter((item) => item.id !== id));
  //   axios.delete(`${url}/${id}`);
  // }; логика удаления контака по щелчку мыши

  return (
    <div className="container mt-5">
      <Routes>
        <Route
          path="/"
          element={
            <div className="card">
              <div className="card-header">
                <h1>Список контактов</h1>
              </div>

              <div className="card-body">
                <TableContact
                  contacts={contacts}
                  // deleteContact={deleteContact} логика удаления контака по щелчку мыши
                />

                {error && (
                  <div className="alert alert-danger mt-3">{error}</div>
                )}

                <FormContact addContact={addContact} />
              </div>
            </div>
          }
        />
        <Route path="contact/:id" element={<ContactDetails />} />
      </Routes>
    </div>
  );
};

export default App;
