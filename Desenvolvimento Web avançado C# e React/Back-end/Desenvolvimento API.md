
#### **Criando uma Web API com Controllers**

O foco é criar os endpoints (as "portas de entrada") da nossa API de forma organizada.

- **Objetivo:** Construir uma API RESTful utilizando o VS Code e o terminal, com rotas descritivas como `api/produto/listar`.
    
- **Criação do Projeto:**
    - Use o comando `dotnet new webapi -n NomeDoProjeto1 --no-https --use-controllers`. A flag `--use-controllers` é importante para criar a estrutura baseada em controllers, que é mais organizada.
        
- **Estrutura da API:**
    - **Controllers:** Classes que recebem as requisições HTTP e devolvem as respostas. É aqui que a lógica de cada endpoint é definida.
        
    - **Models:** Classes que representam os dados da aplicação (ex: um `Produto` com Id, Nome e Preço). É uma boa prática mantê-las em uma pasta separada para organização.
        
- **Definindo Rotas:**
    - O atributo `[ApiController]` indica que a classe é um controller de API.
    - `[Route("api/produto")]` define o prefixo da URL para todos os métodos dentro do controller.
        
    - `[HttpGet("listar")]` e `[HttpPost("cadastrar")]` especificam as rotas e os verbos HTTP para cada método. O método para cadastrar um produto deve receber os dados via `[FromBody]`.