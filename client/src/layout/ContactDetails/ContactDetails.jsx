import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";

const baseUrl = process.env.REACT_APP_API_URL;

const ContactDetails = () => {
  const [contact, setContact] = useState({ name: "", email: "" });
  const { id } = useParams();
  const navigate = useNavigate();
  /* Хук
    Для отображения ошибок*/
  const [error, setError] = useState("");

  useEffect(() => {
    const url = `${baseUrl}/contacts/${id}`;
    console.log(url);
    axios
      .get(url)
      .then((response) => setContact(response.data))
      .catch((err) => navigate("/"));
  }, [id, navigate]);

  const handleRemove = () => {
    if (window.confirm("Вы уверены, что хотите удалить этот контакт?")) {
      axios.delete(`${baseUrl}/contacts/${id}`).then(() => navigate("/"));
    }
  };

  const handleUpdate = () => {
    if (contact.name === "" || contact.email === "") {
      setError("Имя и email должны быть заполнены");
      return;
    } else {
      setError("");
      axios
        .put(`${baseUrl}/contacts/${id}`, contact)
        .then(() => navigate("/"))
        .catch((err) => "Ошибка обновления");
    }
  };

  return (
    <div className="container mt-5">
      <h2>Детали контакта</h2>
      <div>
        <label className="form-label">Имя: </label>
        <input
          type="text"
          className="form-control"
          value={contact.name}
          onChange={(e) => {
            setContact({ ...contact, name: e.target.value });
          }}
        />
      </div>
      <div className="mb-3">
        <label className="form-label">Email: </label>
        <input
          type="email"
          className="form-control"
          value={contact.email}
          onChange={(e) => {
            setContact({ ...contact, email: e.target.value });
          }}
        />
      </div>
      {error && <div className="alert alert-danger">{error}</div>}
      <button
        className="btn btn-primary me-2"
        onClick={(e) => {
          handleUpdate();
        }}
      >
        Обновить
      </button>
      <button
        className="btn btn-danger me-2"
        onClick={(e) => {
          handleRemove();
        }}
      >
        Удалить
      </button>
      <button
        className="btn btn-secondary me-2"
        onClick={(e) => {
          navigate("/");
        }}
      >
        Назад
      </button>
    </div>
  );
};

export default ContactDetails;
