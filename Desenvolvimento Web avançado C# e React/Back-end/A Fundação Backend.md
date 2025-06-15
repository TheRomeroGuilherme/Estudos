
- **Entendendo a CLI do .NET:** A CLI (Interface de Linha de Comando) é sua principal ferramenta para criar, gerenciar e publicar aplicações .NET sem depender de uma interface gráfica.
    
- **O Conceito de Solution:** Uma `Solution` é uma estrutura que organiza um ou mais projetos relacionados. Ela atua como um contêiner, permitindo que diferentes partes de um programa sejam desenvolvidas de forma isolada, mas trabalhando em conjunto. Isso melhora a colaboração e simplifica a depuração e os testes.
    
- **Comandos Essenciais da CLI:**
    - `dotnet new sln --output MySolution`: Cria uma nova Solution.
        
    - `dotnet new console --name NomeDoProjeto`: Cria uma aplicação de console simples.
        
    - `dotnet new webapi --name NomeDoProjeto`: Cria um projeto de Web API, que será nosso foco principal.
        
    - `dotnet sln add NomeDoProjeto`: Adiciona um projeto a uma Solution existente.
        
    - `dotnet run`: Executa sua aplicação.
        
    - `dotnet watch run`: Executa a aplicação e a reinicia automaticamente após qualquer alteração nos arquivos.