
# Guia Prático: Integrando Material-UI (MUI) com Next.js (App Router)

Este guia mostra como instalar e configurar o Material-UI (MUI) em um projeto Next.js que utiliza a estrutura do `app/`, garantindo uma integração correta com a renderização no servidor (SSR).

### **Passo 1: Instalação das Dependências**

Abra o terminal na raiz do seu projeto e instale os pacotes necessários.

```
npm install @mui/material @emotion/react @emotion/styled
```

- `@mui/material`: A biblioteca principal com os componentes React.
    
- `@emotion/react` e `@emotion/styled`: Motores de estilização (CSS-in-JS) que o MUI utiliza para aplicar os estilos de forma dinâmica e eficiente.
    

### **Passo 2 (Opcional): Instalar a Fonte Padrão (Roboto)**

O MUI foi projetado com a fonte Roboto em mente. Para manter a identidade visual do Material Design, instale-a e importe-a no seu layout principal.

**1. Instale o pacote da fonte:**

```
npm install @fontsource/roboto
```

**2. Importe as variações no layout principal (`src/app/layout.tsx`):**

```
// src/app/layout.tsx
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pt-br">
      <body>{children}</body>
    </html>
  );
}
```

### **Passo 3: Configuração Essencial com `ThemeRegistry`**

Para que o MUI funcione corretamente com o Server-Side Rendering (SSR) do Next.js App Router, é crucial criar um registro de tema. Isso evita que os estilos "quebrem" ou pisquem na primeira carga da página.

**1. Crie um arquivo para o seu tema e o registro:**

Crie um novo arquivo, por exemplo, em `src/components/ThemeRegistry/ThemeRegistry.tsx`.

**2. Adicione o código a seguir:**

Este código cria um `ThemeProvider` personalizado que gerencia o cache de estilos do Emotion, garantindo a compatibilidade com o SSR.

**`src/components/ThemeRegistry/ThemeRegistry.tsx`**

```
'use client';

import * as React from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { NextAppDirEmotionCacheProvider } from './EmotionCache';

// Crie seu tema aqui. Você pode customizar paleta, tipografia, etc.
const theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

export default function ThemeRegistry({ children }: { children: React.ReactNode }) {
  return (
    <NextAppDirEmotionCacheProvider options={{ key: 'mui' }}>
      <ThemeProvider theme={theme}>
        {/* CssBaseline reinicia os estilos CSS para um baseline consistente */}
        <CssBaseline />
        {children}
      </ThemeProvider>
    </NextAppDirEmotionCacheProvider>
  );
}
```

**3. Crie o provedor de Cache do Emotion:**

No mesmo diretório, crie o arquivo `EmotionCache.tsx`.

**`src/components/ThemeRegistry/EmotionCache.tsx`**

```
'use client';

import * as React from 'react';
import createCache from '@emotion/cache';
import { useServerInsertedHTML } from 'next/navigation';
import { CacheProvider as EmotionCacheProvider } from '@emotion/react';

export function NextAppDirEmotionCacheProvider({ options, children }: any) {
  const [{ cache, flush }] = React.useState(() => {
    const cache = createCache(options);
    cache.compat = true;
    const prevInsert = cache.insert;
    let inserted: string[] = [];
    cache.insert = (...args) => {
      const serialized = args[1];
      if (cache.inserted[serialized.name] === undefined) {
        inserted.push(serialized.name);
      }
      return prevInsert(...args);
    };
    const flush = () => {
      const prevInserted = inserted;
      inserted = [];
      return prevInserted;
    };
    return { cache, flush };
  });

  useServerInsertedHTML(() => {
    const names = flush();
    if (names.length === 0) {
      return null;
    }
    let styles = '';
    for (const name of names) {
      styles += cache.inserted[name];
    }
    return (
      <style
        key={cache.key}
        data-emotion={`${cache.key} ${names.join(' ')}`}
        dangerouslySetInnerHTML={{
          __html: styles,
        }}
      />
    );
  });

  return <EmotionCacheProvider value={cache}>{children}</EmotionCacheProvider>;
}
```

**4. Aplique o `ThemeRegistry` no seu layout raiz:**

Agora, envolva o `<body>` do seu `layout.tsx` com o `ThemeRegistry`.

**`src/app/layout.tsx`**

```
import ThemeRegistry from '@/components/ThemeRegistry/ThemeRegistry';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

export const metadata = {
  title: 'Meu App com MUI',
  description: 'Um app Next.js usando Material-UI',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pt-br">
      <body>
        <ThemeRegistry>{children}</ThemeRegistry>
      </body>
    </html>
  );
}
```

### **Passo 4: Pronto para Usar!**

Com a configuração completa, você pode importar e usar componentes do MUI em qualquer **Client Component** (`'use client'`).

**`src/app/page.tsx`**

```
'use client'; // Obrigatório para componentes que usam hooks ou interatividade

import { Button, Typography, Box } from '@mui/material';

export default function HomePage() {
  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100vh',
        gap: 2,
      }}
    >
      <Typography variant="h4" component="h1" gutterBottom>
        Bem-vindo ao MUI no Next.js!
      </Typography>
      <Button variant="contained" onClick={() => alert('Clicado!')}>
        Clique Aqui
      </Button>
    </Box>
  );
}
```

Com estes passos, sua aplicação está perfeitamente configurada para aproveitar todo o poder do Material-UI e do Next.js juntos!