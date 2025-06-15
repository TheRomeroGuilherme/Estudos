#### **Comandos Essenciais da CLI**

A seguir, um detalhamento dos comandos de linha de comando (CLI) apresentados:

**1. Criando uma Nova Aplicação**

- **Comando:** `npx create-react-app my-app --template typescript`
    
- **Propósito Detalhado:** Este é o comando inicial para gerar a estrutura completa de um novo projeto React.
    - `npx create-react-app`: Executa o pacote oficial para criação de projetos React, que configura automaticamente o ambiente de desenvolvimento com tudo o que é necessário para começar.
    - `my-app`: É o nome que você escolhe para o seu projeto. Este nome será usado para criar a pasta principal da aplicação.
    - `--template typescript`: Esta é uma flag opcional que instrui o `create-react-app` a configurar o projeto para usar TypeScript em vez de JavaScript puro.

**2. Executando a Aplicação**

- **Comando:** `npm start`
    
- **Propósito Detalhado:** Após a criação do projeto, este comando inicia o servidor de desenvolvimento. Ele compila a aplicação e a torna acessível em um endereço local (geralmente `http://localhost:3000`) no seu navegador. Além disso, ele ativa o "hot-reloading", que atualiza a página automaticamente sempre que você salva uma alteração no código.

**3. Instalando Dependências**

- **Comando:** `npm i`
    
- **Propósito Detalhado:** Este comando é a abreviação de `npm install`. Sua função é ler o arquivo `package.json` de um projeto e baixar todas as bibliotecas e pacotes (dependências) listados nele. É o primeiro comando que você deve executar ao clonar um projeto React de um repositório ou ao começar a trabalhar em um código já existente, para garantir que seu ambiente tenha todas as ferramentas necessárias para rodar a aplicação.