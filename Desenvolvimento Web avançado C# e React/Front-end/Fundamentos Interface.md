
Aqui estão os pilares que sustentam o desenvolvimento com React:

- **Componentes:** São os blocos de construção fundamentais de uma aplicação React. Cada componente é uma peça de UI independente que encapsula sua própria lógica e aparência. Eles podem ser simples como um botão ou complexos como uma página inteira.
    
- **JSX/TSX:** É uma extensão de sintaxe para JavaScript (ou TypeScript, no caso do TSX) que permite escrever uma estrutura semelhante a HTML diretamente no código. Isso torna a criação da UI mais visual e intuitiva.

	JavaScript

    ```
    // Exemplo de JSX
    const elemento = <h1>Olá, Mundo!</h1>;
    ```
    
- **Props (Propriedades):** São a forma de passar dados de um componente pai para um componente filho. As `props` são somente leitura, o que significa que um componente filho não pode alterar as propriedades que recebeu.
    
    JavaScript
    
    ```
    // O componente 'Saudacao' recebe a prop 'nome'
    function Saudacao(props) {
      return <h1>Olá, {props.nome}!</h1>;
    }
    
    // O componente 'App' passa a prop 'nome' para 'Saudacao'
    function App() {
      return <Saudacao nome="Maria" />;
    }
    ```
    
- **State (Estado):** É um objeto que armazena dados que podem mudar ao longo do tempo dentro de um componente. Quando o `state` de um componente muda, o React o re-renderiza automaticamente para refletir essa mudança na tela.
    
    JavaScript
    
    ```
    import React, { useState } from 'react';
    
    function Contador() {
      // 'contagem' é a variável de estado e 'setContagem' é a função para atualizá-la
      const [contagem, setContagem] = useState(0);
    
      return (
        <div>
          <p>Você clicou {contagem} vezes</p>
          <button onClick={() => setContagem(contagem + 1)}>
            Clique aqui
          </button>
        </div>
      );
    }
    ```
    
- **Renderização e o Virtual DOM:** Em vez de manipular o DOM (Document Object Model) do navegador diretamente, o React cria uma cópia em memória chamada **Virtual DOM**. Quando o estado de um componente muda, o React atualiza o Virtual DOM, compara-o com a versão anterior e calcula a maneira mais eficiente de aplicar apenas as mudanças necessárias ao DOM real. Esse processo, chamado de **reconciliação**, torna as atualizações da UI muito rápidas e eficientes.
    
- **Ciclo de Vida (Lifecycle):** Cada componente em React passa por um ciclo de vida: montagem (quando é inserido no DOM), atualização (quando suas `props` ou `state` mudam) e desmontagem (quando é removido do DOM). Com os **Hooks** (como `useEffect` em componentes de função), é possível executar código em momentos específicos desses ciclos, como buscar dados de uma API quando o componente é montado.