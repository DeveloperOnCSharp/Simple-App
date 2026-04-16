import React, { useState } from "react";

const FormContact = (props) => {
  const [contactName, setContactName] = useState("");
  const [contactEmail, setContactEmail] = useState("");

  const submit = () => {
    props.addContact(contactName, contactEmail);
    //очистить поля формы
    setContactName("");
    setContactEmail("");
  };

  return (
    <div>
      <div className="mb-3">
        <form>
          <div className="mb-3">
            <label className="form-label">Введите имя:</label>
            <input
              className="form-control"
              type="text"
              //добавить значение из стейта равное пустой строке
              value={contactName}
              onChange={(e) => {
                setContactName(e.target.value);
              }}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Введите e-mail:</label>
            <textarea
              className="form-control"
              //добавить значение из стейта равное пустой строке
              value={contactEmail}
              onChange={(e) => {
                setContactEmail(e.target.value);
              }}
              rows={1}
            ></textarea>
          </div>
        </form>
      </div>
      <div>
        <button
          className="btn btn-primary"
          onClick={() => {
            submit();
          }}
        >
          Добавить новый контакт
        </button>
      </div>
    </div>
  );
};

export default FormContact;
