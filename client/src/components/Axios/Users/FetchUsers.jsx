import { toast } from "react-toastify";
import axios from "axios";

export const fetchUsers = async ({ setUsers, API_URL }) => {
  try {
    const response = await axios.get(API_URL);
    setUsers(response.data);
  } catch (error) {
    toast.error("Erro ao buscar usuários. Tente novamente!");
    console.error("Erro ao buscar usuários:", error);
  }
};
