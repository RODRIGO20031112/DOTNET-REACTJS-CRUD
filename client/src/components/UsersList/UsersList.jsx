export const UsersList = ({ users, handleEdit, handleDelete }) => {
  return (
    <table className="table table-bordered">
      <thead>
        <tr>
          <th>Nome</th>
          <th>Email</th>
          <th>Telefone</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        {users.map((user) => (
          <tr key={user.id}>
            <td>{user.name}</td>
            <td>{user.email}</td>
            <td>{user.phone}</td>
            <td>
              <div className="d-flex d-md-flex flex-column flex-md-row gap-2">
                <button
                  className="btn btn-warning btn-sm w-100"
                  onClick={() => handleEdit(user)}
                >
                  Editar
                </button>
                <button
                  className="btn btn-danger btn-sm w-100"
                  onClick={() => handleDelete(user.id)}
                >
                  Remover
                </button>
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};
