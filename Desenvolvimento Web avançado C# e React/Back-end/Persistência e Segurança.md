
#### **Conectando a um Banco de Dados com Entity Framework Core**

O objetivo é fazer com que os dados da API sejam permanentes, salvando-os em um banco de dados MySQL.

- **Objetivo:** Implementar um CRUD (Criar, Ler, Atualizar, Deletar) completo, persistindo os dados em um banco.
    
- **Instalação de Pacotes:**
    - `dotnet add package Pomelo.EntityFrameworkCore.MySql`: O driver que permite ao EF Core se comunicar com o MySQL.
        
    - `dotnet add package Microsoft.EntityFrameworkCore.Design`: Ferramentas para executar `migrations`.
        
- **Configuração do Entity Framework (EF Core):**
    - **`DbContext`:** Crie uma classe `AppDbContext`. Ela serve como a ponte entre a aplicação e o banco de dados, mapeando suas classes `Model` para tabelas.
        
    - **String de Conexão:** Adicione as credenciais do seu banco de dados (servidor, nome do banco, usuário e senha) no arquivo `appsettings.json`.
        
    - **Registro do Serviço:** Configure o `DbContext` no arquivo `Program.cs` para que a aplicação saiba como usá-lo.
        
- **Migrations (Evolução do Banco):**
    - `dotnet tool install --global dotnet-ef`: Instala a ferramenta de linha de comando do EF Core, caso não a tenha.
    - `dotnet ef migrations add NomeDaMigration`: Analisa seus `Models` e gera um script para criar ou atualizar as tabelas no banco de dados.
        
    - `dotnet ef database update`: Aplica o script gerado, alterando o banco de dados para que ele corresponda aos seus modelos.
        
- **Implementando o CRUD no Controller:**
    - Injete o `AppDbContext` no construtor do seu controller para ter acesso ao banco.
        
    - Use os métodos do EF Core para manipular os dados: `_context.Produtos.ToList()` para listar , `_context.Produtos.Add()` para adicionar e `_context.Produtos.Remove()` para deletar.