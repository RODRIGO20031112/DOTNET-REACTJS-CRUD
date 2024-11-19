export const Form = ({
  handleSubmit,
  setFormData,
  formData,
  isEditing,
  setIsEditing,
}) => {
  return (
    <form onSubmit={handleSubmit} className="mb-4">
      <div className="mb-3">
        <label htmlFor="name" className="form-label">
          Nome
        </label>
        <input
          type="text"
          id="name"
          className="form-control"
          value={formData.name}
          onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          required
        />
      </div>
      <div className="mb-3">
        <label htmlFor="email" className="form-label">
          Email
        </label>
        <input
          type="email"
          id="email"
          className="form-control"
          value={formData.email}
          onChange={(e) => setFormData({ ...formData, email: e.target.value })}
          required
        />
      </div>
      <div className="mb-3">
        <label htmlFor="phone" className="form-label">
          Telefone
        </label>
        <input
          type="text"
          id="phone"
          className="form-control"
          value={formData.phone}
          onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
          required
        />
      </div>
      <button type="submit" className="btn btn-primary">
        {isEditing ? "Atualizar Usuário" : "Adicionar Usuário"}
      </button>
      {isEditing && (
        <button
          type="button"
          className="btn btn-secondary ms-2"
          onClick={() => {
            setIsEditing(false);
            setFormData({ name: "", email: "", phone: "" });
          }}
        >
          Cancelar
        </button>
      )}
    </form>
  );
};
