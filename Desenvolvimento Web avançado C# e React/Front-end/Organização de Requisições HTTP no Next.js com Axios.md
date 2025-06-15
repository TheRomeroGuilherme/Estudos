
#  Guia: Organizando Requisições HTTP no Next.js com Axios

Esta é uma abordagem estruturada para centralizar a configuração do Axios em uma aplicação Next.js. O objetivo é criar uma camada de serviço de API reutilizável que lida automaticamente com a injeção de tokens de autenticação e o tratamento global de erros, como o `401 Não Autorizado`.

### **Objetivos Principais**

- **Inclusão Automática de Token:** Adicionar o token JWT a todas as requisições autenticadas sem repetição de código.
    
- **Tratamento Global de Erros:** Centralizar a lógica para lidar com respostas de erro da API (ex: um erro `401` redireciona para a página de login).
    
- **Código Limpo e Reutilizável:** Manter a lógica de comunicação com a API isolada e fácil de usar em qualquer componente.
    

###  **Passo 1: Instalação do Axios**

Primeiro, adicione o Axios e seus tipos ao seu projeto.

```
npm install axios
# ou
yarn add axios
```

### **Passo 2: Estrutura de Arquivos Recomendada**

Organize seus arquivos para manter a lógica da API separada da UI.

```
src/
├── services/
│   └── api.ts      # Instância central e configurada do Axios
├── app/
│   ├── produtos/
│   │   └── page.tsx    # Componente que consome a API (Client Component)
│   ├── nao-autenticado/
│   │   └── page.tsx    # Página para onde o usuário é redirecionado
│   └── login/
│       └── page.tsx    # Página de login que obtém o token
└── ...
```

###  **Passo 3: Criar a Instância do Axios com Interceptors**

Este é o núcleo da nossa estrutura. Criamos uma instância do Axios com `interceptors` que irão "interceptar" todas as requisições e respostas.

**`src/services/api.ts`**

```
import axios from 'axios';

// 1. Criação da instância do Axios com uma baseURL
// A baseURL é pega de uma variável de ambiente para flexibilidade entre ambientes (dev, prod)
const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:3333',
});

// 2. Interceptor de Requisição (Request Interceptor)
// Executa antes de cada requisição ser enviada
api.interceptors.request.use((config) => {
  // Verificamos se o código está rodando no lado do cliente (browser)
  if (typeof window !== 'undefined') {
    const token = localStorage.getItem('app-token');
    
    // Se o token existir, nós o adicionamos ao header de Authorization
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
  }
  return config; // Retorna a configuração para a requisição prosseguir
});

// 3. Interceptor de Resposta (Response Interceptor)
// Executa após uma resposta ser recebida
api.interceptors.response.use(
  // A primeira função lida com respostas de sucesso (status 2xx)
  // Simplesmente retornamos a resposta
  (response) => response,
  
  // A segunda função lida com respostas de erro
  (error) => {
    // Verificamos se o erro é de "Não Autorizado" (401)
    if (typeof window !== 'undefined' && error.response?.status === 401) {
      // Se for, removemos o token inválido/expirado
      localStorage.removeItem('app-token');
      // E redirecionamos o usuário para a página de não-autenticado
      window.location.href = '/nao-autenticado'; 
    }
    
    // Rejeitamos a promise para que o erro possa ser tratado no local da chamada (ex: em um .catch)
    return Promise.reject(error);
  }
);

export default api;
```

###  **Passo 4: Utilizando a API em Componentes**

Agora, em qualquer **Client Component** (`'use client'`), você pode importar e usar a instância `api` diretamente.

**`src/app/produtos/page.tsx`**

```
'use client';

import { useEffect, useState } from 'react';
import api from '@/services/api'; // Importa nossa instância configurada

interface Produto {
  id: number;
  nome: string;
  preco: number;
}

export default function ProdutosPage() {
  const [produtos, setProdutos] = useState<Produto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Função assíncrona para buscar os dados
    const fetchProdutos = async () => {
      try {
        const response = await api.get<Produto[]>('/produtos');
        setProdutos(response.data);
      } catch (error) {
        console.error("Falha ao buscar produtos:", error);
        // Aqui você pode definir um estado de erro para exibir uma mensagem na UI
      } finally {
        setLoading(false);
      }
    };

    fetchProdutos();
  }, []); // O array vazio garante que o useEffect rode apenas uma vez

  if (loading) {
    return <p>Carregando produtos...</p>;
  }

  return (
    <div>
      <h1>Lista de Produtos</h1>
      <ul>
        {produtos.map((produto) => (
          <li key={produto.id}>
            {produto.nome} - R$ {produto.preco}
          </li>
        ))}
      </ul>
    </div>
  );
}
```

###  **Passo 5: Salvando o Token Após o Login**

Na sua página de login, após a autenticação bem-sucedida, salve o token no `localStorage` e redirecione o usuário.

**`src/app/login/page.tsx`**

```
'use client';

import { useRouter } from 'next/navigation';
import { useState } from 'react';

export default function LoginPage() {
  const router = useRouter();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      // Em um caso real, você faria uma chamada POST para sua API
      // const response = await api.post('/login', { username, password });
      // const token = response.data.token;
      
      // Para este exemplo, usamos um token falso
      const fakeToken = 'seu-jwt-token-recebido-da-api';
      
      // Salva o token no localStorage
      localStorage.setItem('app-token', fakeToken);
      
      // Redireciona para a página de produtos
      router.push('/produtos');

    } catch (error) {
      console.error("Falha no login", error);
      alert("Credenciais inválidas!");
    }
  };

  return (
    <form onSubmit={handleLogin}>
      <h2>Login</h2>
      <input 
        type="text" 
        placeholder="Usuário" 
        value={username} 
        onChange={(e) => setUsername(e.target.value)} 
      />
      <input 
        type="password" 
        placeholder="Senha" 
        value={password} 
        onChange={(e) => setPassword(e.target.value)} 
      />
      <button type="submit">Entrar</button>
    </form>
  );
}
```

### 📌 **Considerações Finais e Boas Práticas**

- **Client-Side:** Lembre-se que `localStorage` e `window` só existem no navegador. Todo código que depende deles precisa estar em um componente marcado com `'use client'`.
    
- **Segurança:** A abordagem descrita é excelente para SPAs (Single Page Applications) e interações no lado do cliente. Para proteger rotas no servidor (Server Components ou API Routes), o ideal é usar **Middleware** do Next.js para verificar o token (geralmente armazenado em um `httpOnly cookie` por segurança).
    
- **Variáveis de Ambiente:** Sempre armazene URLs de API e outras configurações sensíveis em variáveis de ambiente (`.env.local`)