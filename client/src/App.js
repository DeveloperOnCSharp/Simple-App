import TableContact from "./layout/TableContact/TableContact";

const contacts = [
    {id: 1, name: "Иван Иванов", email: "ivan.ivanov@example.com"},
    {id: 2, name: "Петр Петров", email: "petr.petrov@example.com"},
    {id: 3, name: "Сидор Сидоров", email: "sidor.sidorov@example.com"},
    {id: 4, name: "Мария Смирнова", email: "maria.smirnova@example.com"}
];

const App = () => {
  return (
    <div className="container mt-">
      <div className="card">
        <div className="card-header">
          <h1>Список контактов</h1>
        </div>
        
        <div className="card-body">
          <TableContact contacts={contacts} />
        </div>
      </div>
    </div>
  );
}

export default App;
