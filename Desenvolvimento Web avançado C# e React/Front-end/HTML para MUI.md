
# Guia: Estruturando um Layout Padrão com MUI e Next.js

Este guia demonstra como criar um layout reutilizável que inclui uma barra de navegação superior (header) e um rodapé (footer), aplicando-o a todas as páginas da aplicação usando o `layout.tsx` do Next.js App Router.

### **Objetivo Principal**

Criar uma estrutura visual consistente e reutilizável, composta por:

- Uma **barra superior** (`AppBar`) com o título da aplicação.
    
- Uma **área de conteúdo principal** onde as páginas serão renderizadas.
    
- Um **rodapé** (`Footer`) fixado na parte inferior da página.
    

###  **Estrutura de Arquivos**

A lógica principal de layout residirá no arquivo global `layout.tsx`, que "envolve" todas as páginas da sua aplicação.

```
src/
├── app/
│   ├── layout.tsx       # Arquivo de layout global que estamos criando
│   ├── page.tsx         # Exemplo de página que herdará o layout
│   └── globals.css      # Estilos globais
└── components/
    └── ThemeRegistry/
        └── ThemeRegistry.tsx # Provedor do tema MUI (criado anteriormente)
```

###  **Implementação do `layout.tsx`**

Este arquivo é o coração da nossa estrutura. Ele define os elementos visuais que se repetirão em todo o site e especifica onde o conteúdo exclusivo de cada página (`children`) deve ser renderizado.

**`src/app/layout.tsx`**

```
import ThemeRegistry from '@/components/ThemeRegistry/ThemeRegistry';
import { AppBar, Box, Container, CssBaseline, Toolbar, Typography } from '@mui/material';
import type { Metadata } from 'next';
import './globals.css';

// Metadados da página (bom para SEO)
export const metadata: Metadata = {
  title: 'Projeto com MUI',
  description: 'Exemplo com AppBar e Footer usando Material UI',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pt-BR">
      <body>
        <ThemeRegistry>
          {/* CssBaseline normaliza os estilos, removendo inconsistências entre navegadores */}
          <CssBaseline />
          
          <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
            {/* 1. BARRA SUPERIOR (HEADER) */}
            <AppBar position="static" component="header">
              <Toolbar>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                  Minha Aplicação
                </Typography>
              </Toolbar>
            </AppBar>

            {/* 2. CONTEÚDO PRINCIPAL DA PÁGINA */}
            {/* O 'children' representa o conteúdo de `page.tsx` ou de outras rotas */}
            <Box component="main" sx={{ flexGrow: 1, py: 4 }}>
              <Container>{children}</Container>
            </Box>

            {/* 3. RODAPÉ (FOOTER) */}
            <Box
              component="footer"
              sx={{
                py: 3, // Padding vertical
                px: 2, // Padding horizontal
                mt: 'auto', // Empurra o rodapé para o final do flex container
                backgroundColor: (theme) =>
                  theme.palette.mode === 'light'
                    ? theme.palette.grey[200]
                    : theme.palette.grey[800],
              }}
            >
              <Container maxWidth="sm">
                <Typography variant="body2" color="text.secondary" align="center">
                  {'© '}
                  {new Date().getFullYear()} Minha Aplicação. Todos os direitos reservados.
                </Typography>
              </Container>
            </Box>
          </Box>
        </ThemeRegistry>
      </body>
    </html>
  );
}
```

# Guia: Migrando um Formulário de HTML para MUI

Este guia mostra como migrar um formulário básico de login feito com HTML tradicional para um componente React usando o Material UI (MUI), resultando em um código mais limpo, acessível e responsivo.

###  **Formulário Original (HTML + React)**

O código abaixo representa uma estrutura comum de formulário feita com HTML puro e controlada por React:

```
<div>
  <h1>Login</h1>
  <form onSubmit={efetuarLogin}>
    <div id="email">
      <label htmlFor="email">E-mail:</label>
      <input
        type="text"
        name="email"
        id="email"
        required
        onChange={(e) => setEmail(e.target.value)}
      />
    </div>
    <div id="senha">
      <label htmlFor="senha">Senha:</label>
      <input
        type="password"
        name="senha"
        id="senha"
        required
        onChange={(e) => setSenha(e.target.value)}
      />
    </div>
    <div>
      <button type="submit">Login</button>
    </div>
  </form>
</div>
```

###  **Limitações da Abordagem Original**

- **Estilização Manual:** Requer CSS customizado para margens, cores, fontes, etc.
    
- **Inconsistência Visual:** A aparência pode variar entre diferentes navegadores.
    
- **Baixa Acessibilidade:** Falta de feedback visual claro para foco, validação e outros estados.
    
- **Sem Sistema de Design:** Não se integra a um sistema coeso como o Material Design.
    

###  **Refatorando para MUI Passo a Passo**

####  1. Organização visual com `Container` e `Paper`

- **Container:** Centraliza e limita a largura do formulário, melhorando a leitura em telas grandes.
    
- **Paper:** Cria um "cartão" com elevação (sombra) para destacar visualmente o formulário.
    

```
<Container maxWidth="sm">
  <Paper elevation={3} sx={{ padding: 4, mt: 8 }}>
    {/* conteúdo aqui dentro */}
  </Paper>
</Container>
```

####  2. Título com `Typography`

O `Typography` padroniza os estilos de texto da aplicação, garantindo hierarquia visual e responsividade.

```
<Typography variant="h5" component="h1" gutterBottom>
  Login
</Typography>
```

####  3. Campos de entrada com `TextField`

O `TextField` é um componente robusto que substitui `<input>` e `<label>`, já incluindo:

- Rótulo flutuante embutido.
    
- Estilos de validação e foco.
    
- Acessibilidade e temas nativos do MUI.
    

```
<TextField
  fullWidth
  margin="normal"
  label="E-mail"
  type="email"
  required
  value={email}
  onChange={(e) => setEmail(e.target.value)}
/>
```

####  4. Botão com `Button`

O `Button` do MUI já vem com estilos para diferentes estados (hover, focus, disabled) e variantes.

```
<Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
  Entrar
</Button>
```

###  **Código Final Comentado**

Aqui está o componente de login completo, refatorado com MUI.

```
'use client';

import { useState } from 'react';
import { Box, Button, Container, TextField, Typography, Paper } from '@mui/material';

export default function LoginPage() {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');

  function efetuarLogin(e: React.FormEvent) {
    e.preventDefault();
    console.log('Login attempt com:', { email, senha });
    // Aqui iria a lógica de autenticação
  }

  return (
    // Container limita a largura máxima do formulário para 'small' (600px)
    <Container maxWidth="sm">
      {/* Paper cria o efeito de cartão com sombra e padding */}
      <Paper elevation={3} sx={{ padding: 4, mt: { xs: 4, md: 8 } }}>
        {/* Typography para um título padronizado */}
        <Typography variant="h5" component="h1" align="center" gutterBottom>
          Login
        </Typography>

        {/* Box se comporta como uma <form> */}
        <Box component="form" onSubmit={efetuarLogin} noValidate autoComplete="off">
          <TextField
            fullWidth       // Ocupa 100% da largura
            margin="normal" // Adiciona margem vertical
            label="E-mail"
            type="email"
            required        // Adiciona o asterisco e a validação do navegador
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Senha"
            type="password"
            required
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
          />
          <Button 
            type="submit" 
            variant="contained" // Estilo de botão principal
            color="primary" 
            fullWidth 
            sx={{ mt: 3, mb: 2 }} // Margem superior e inferior
          >
            Entrar
          </Button>
        </Box>
      </Paper>
    </Container>
  );
}
```

###  **Benefícios da Refatoração com MUI**

- **Velocidade:** Componentes prontos aceleram o desenvolvimento.
    
- **Consistência:** A aplicação segue um sistema de design coeso.
    
- **Acessibilidade:** Os componentes já são construídos com acessibilidade em mente.
    
- **Manutenibilidade:** O código fica mais limpo, declarativo e fácil de manter.