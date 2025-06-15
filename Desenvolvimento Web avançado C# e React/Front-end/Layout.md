
Este guia demonstra como criar um layout reutilizável que inclui uma barra de navegação superior (header) e um rodapé (footer), aplicando-o a todas as páginas da aplicação usando o `layout.tsx` do Next.js App Router.

###  **Objetivo Principal**

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

###  **Análise do Código**

- **`ThemeRegistry`**: Nosso provedor de tema do MUI, garantindo que os componentes sejam renderizados corretamente no servidor e no cliente.
    
- **`<CssBaseline />`**: Componente do MUI que aplica uma normalização de estilos, criando uma base consistente para o design.
    
- **`<Box sx={{ display: 'flex', ... }}>`**: O `Box` principal funciona como um contêiner flexível na vertical (`flexDirection: 'column'`) e com altura mínima de toda a tela (`minHeight: '100vh'`). Isso é fundamental para que o rodapé se posicione corretamente.
    
- **`<AppBar>`**: Cria a barra superior. `position="static"` a mantém no fluxo normal do documento.
    
- **`<Box component="main">`**: Define a área principal para o conteúdo. A propriedade `flexGrow: 1` faz com que este elemento se expanda para ocupar todo o espaço vertical disponível, empurrando o rodapé para baixo.
    
- **`<Container>`**: Centraliza o conteúdo da página, adicionando margens laterais em telas maiores para melhor legibilidade.
    
- **`<Box component="footer">`**: Define o rodapé. A propriedade `mt: 'auto'` (margin-top: auto) é o segredo para que ele fique sempre no final da página, mesmo quando o conteúdo principal é curto.
    

Com essa estrutura, qualquer nova página criada dentro do diretório `app` herdará automaticamente a barra superior, o rodapé e os estilos base, promovendo consistência e agilizando o desenvolvimento.