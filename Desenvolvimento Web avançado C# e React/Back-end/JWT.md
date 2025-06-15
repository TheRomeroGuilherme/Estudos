
- **Objetivo:** Implementar autenticação usando JSON Web Tokens (JWT), controlando o acesso por perfis (Roles).
    
- **Configuração da Segurança:**
    - **Chave Secreta:** Armazene uma chave secreta e segura no `appsettings.json`. Essa chave é usada para assinar e validar os tokens, garantindo sua integridade.
        
    - **Instalação do Pacote:** `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`.
        
    - **Registro da Autenticação:** No `Program.cs`, configure o serviço de autenticação JWT, informando ao sistema como ele deve validar os tokens recebidos (verificando a assinatura, o tempo de expiração, etc.).
        
- **Geração e Uso de Tokens:**
    - **`AuthController`:** Crie um controller específico para autenticação com um método de `login`.
        
    - **Geração do Token:** Após validar as credenciais do usuário, gere um token JWT. Dentro do token, inclua **`Claims`**, que são informações sobre o usuário, como seu email (`ClaimTypes.Name`) e seu perfil (`ClaimTypes.Role`).
        
    - **Protegendo Endpoints:**
        - Use o atributo `[Authorize]` acima de um método ou controller para exigir que o usuário esteja autenticado.
        - Use `[Authorize(Roles = "admin")]` para restringir o acesso apenas a usuários com um perfil específico, garantindo um controle de acesso mais granular.