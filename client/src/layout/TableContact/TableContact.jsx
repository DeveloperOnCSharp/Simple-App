import RowTableContact from "./components/RowTableContact";

const TableContact = (props) => {
    return(
        <table className="table table-hover">
            
            <thead>
              <th>#</th>
              <th>Имя контакта</th>
              <th>E-mail</th>
            </thead>
            <tbody>
                {
                    props.contacts.map(
                        contact => (<RowTableContact
                            key={contact.id}
                            id={contact.id}
                            name={contact.name}
                            email={contact.email}
                            deleteContact={props.deleteContact}
                        />)
                    )
                }
            </tbody>
          </table>
    );
}

export default TableContact;