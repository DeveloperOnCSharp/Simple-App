import { Link } from "react-router-dom";

const RowTableContact = (props) => {
  return (
    <tr>
      <td>{props.id}</td>
      <td>{props.name}</td>
      <td>{props.email}</td>
      <td>
        <Link to={`/contact/${props.id}`}>
          <button className="btn btn-sm btn-primary">Подробнее</button>
        </Link>
      </td>
    </tr>
  );
};

export default RowTableContact;
