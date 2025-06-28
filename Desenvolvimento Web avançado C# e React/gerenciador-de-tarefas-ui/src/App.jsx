import { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css'; // Vamos criar este CSS básico

// Configure a URL base da sua API. Altere a porta se a sua for diferente.
const API_URL = 'http://localhost:5292/api/tarefas'; 

function App() {
  const [tarefas, setTarefas] = useState([]);
  const [novaTarefaTitulo, setNovaTarefaTitulo] = useState('');

  // READ: Carrega as tarefas da API quando o componente é montado
  useEffect(() => {
    axios.get(API_URL)
      .then(response => {
        setTarefas(response.data);
      })
      .catch(error => console.error("Houve um erro ao buscar as tarefas!", error));
  }, []); // O array vazio [] significa que este efeito roda apenas uma vez

  // CREATE: Adiciona uma nova tarefa
  const adicionarTarefa = (e) => {
    e.preventDefault(); // Impede o recarregamento da página
    if (!novaTarefaTitulo) return;

    const novaTarefa = { titulo: novaTarefaTitulo, concluida: false };
    axios.post(API_URL, novaTarefa)
      .then(response => {
        setTarefas([...tarefas, response.data]); // Adiciona a nova tarefa à lista
        setNovaTarefaTitulo(''); // Limpa o campo de input
      })
      .catch(error => console.error("Houve um erro ao adicionar a tarefa!", error));
  };

  // UPDATE: Altera o status de 'concluida'
  const marcarComoConcluida = (id) => {
    const tarefa = tarefas.find(t => t.id === id);
    const tarefaAtualizada = { ...tarefa, concluida: !tarefa.concluida };

    axios.put(`${API_URL}/${id}`, tarefaAtualizada)
      .then(() => {
        setTarefas(tarefas.map(t => t.id === id ? tarefaAtualizada : t));
      })
      .catch(error => console.error("Houve um erro ao atualizar a tarefa!", error));
  };

  // DELETE: Remove uma tarefa
  const deletarTarefa = (id) => {
    axios.delete(`${API_URL}/${id}`)
      .then(() => {
        setTarefas(tarefas.filter(t => t.id !== id)); // Remove a tarefa da lista
      })
      .catch(error => console.error("Houve um erro ao deletar a tarefa!", error));
  };


  return (
    <div className="app">
      <div className="container">
        <h1>Gerenciador de Tarefas</h1>
        
        <form onSubmit={adicionarTarefa} className="form">
          <input
            type="text"
            value={novaTarefaTitulo}
            onChange={(e) => setNovaTarefaTitulo(e.target.value)}
            placeholder="Adicionar nova tarefa..."
          />
          <button type="submit">Adicionar</button>
        </form>

        <div className="lista-tarefas">
          {tarefas.map(tarefa => (
            <div key={tarefa.id} className={`tarefa ${tarefa.concluida ? 'concluida' : ''}`}>
              <p onClick={() => marcarComoConcluida(tarefa.id)}>
                {tarefa.titulo}
              </p>
              <button onClick={() => deletarTarefa(tarefa.id)} className="btn-delete">
                Excluir
              </button>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;