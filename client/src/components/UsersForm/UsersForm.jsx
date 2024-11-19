import { ToastContainer, toast } from "react-toastify";
import { UsersList } from "../UsersList/UsersList";
import React, { useState, useEffect } from "react";
import "react-toastify/dist/ReactToastify.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Form } from "../Form/Form";
import axios from "axios";

export const UsersForm = () => {
  const [users, setUsers] = useState([]);
  const [formData, setFormData] = useState({ name: "", email: "", phone: "" });
  const [isEditing, setIsEditing] = useState(false);
  const [editUserId, setEditUserId] = useState(null);

  const API_URL = "http://localhost:5151/api/User";

  // Função para buscar todos os usuários
  const fetchUsers = async () => {
    try {
      const response = await axios.get(API_URL);
      setUsers(response.data);
    } catch (error) {
      toast.error("Erro ao buscar usuários. Tente novamente!");
      console.error("Erro ao buscar usuários:", error);
    }
  };

  // Função para salvar um usuário
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (isEditing) {
        await axios.put(`${API_URL}/${editUserId}`, formData);
        toast.success("Usuário atualizado com sucesso!");
        setIsEditing(false);
        setEditUserId(null);
      } else {
        await axios.post(API_URL, formData);
        toast.success("Usuário adicionado com sucesso!");
      }
      setFormData({ name: "", email: "", phone: "" });
      fetchUsers({ setUsers, API_URL });
    } catch (error) {
      toast.error(`Erro ao salvar usuário. ${error.response.data.message}`);
      console.error("Erro ao salvar usuário:", error.response.data.message);
    }
  };

  // Função para editar um usuário
  const handleEdit = (user) => {
    setFormData({ name: user.name, email: user.email, phone: user.phone });
    setIsEditing(true);
    setEditUserId(user.id);
  };

  // Função para deletar um usuário
  const handleDelete = async (id) => {
    try {
      await axios.delete(`${API_URL}/${id}`);
      toast.success("Usuário removido com sucesso!");
      fetchUsers({ setUsers, API_URL });
    } catch (error) {
      toast.error("Erro ao deletar usuário. Tente novamente!");
      console.error("Erro ao deletar usuário:", error);
    }
  };

  // Carregar os usuários ao montar o componente
  useEffect(() => {
    fetchUsers({ setUsers, API_URL });
  }, []);

  return (
    <div className="container mt-5">
      <ToastContainer />
      <h1 className="text-center mb-4">Gerenciador de Usuários</h1>

      {/* Formulário */}
      <Form
        handleSubmit={handleSubmit}
        setFormData={setFormData}
        formData={formData}
        isEditing={isEditing}
        setIsEditing={setIsEditing}
      />

      {/* Lista de Usuários */}
      <UsersList
        users={users}
        handleEdit={handleEdit}
        handleDelete={handleDelete}
      />
    </div>
  );
};
