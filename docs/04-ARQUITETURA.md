# Arquitetura

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não escolher frameworks ou definir tecnologias neste momento, a menos que já estejam definidas.

## Histórico de alterações

* [x] **2026-08-28** — Prompt 008 executado. Definida a arquitetura técnica completa: tecnologias, camadas, responsabilidades, dependências, configuração de banco, executável de configuração, autenticação, segurança, tratamento de erros, compatibilidade multi-SGBD.
* [x] **2026-08-28** — Prompt 011 executado. Fechamento final pré-codificação. Decisões de arquitetura mantidas sem alteração (SQLite inicial, EF Core, camada de persistência desacoplada). Nenhuma decisão estrutural alterada.

## Arquitetura

### 1. Tecnologias Principais

| Item | Decisão |
|------|---------|
| Tipo de aplicação | Desktop Windows |
| Framework de interface | WPF (Windows Presentation Foundation) |
| Linguagem | C# |
| Banco inicial | SQLite |
| Bancos futuros | PostgreSQL, MySQL |
| Compatibilidade | Arquitetura preparada para troca de SGBD |

---

### 2. Estilo de Arquitetura

**Arquitetura em Camadas (Layered Architecture) simples**

Camadas (da externa para a interna):

1. **Apresentação (Presentation/UI)** — WPF, Views, ViewModels
2. **Aplicação (Application)** — Casos de uso, orquestração, DTOs
3. **Domínio (Domain)** — Entidades, regras de negócio, interfaces de repositório
4. **Infraestrutura (Infrastructure)** — Implementações concretas (repositórios, serviços, persistência)
5. **Persistência (Persistence)** — Acesso a dados, migrações, configuração de banco

---

### 3. Responsabilidades por Camada

#### 3.1 Apresentação (Presentation)

- Views WPF (XAML + Code-behind mínimo)
- ViewModels (MVVM pattern)
- Navegação entre telas
- Binding de dados
- Validação de entrada na UI
- Exibição de mensagens ao usuário

**Não deve:** Acessar banco de dados diretamente, conter regras de negócio complexas, conhecer detalhes de persistência

#### 3.2 Aplicação (Application)

- Casos de uso (Use Cases / Services)
- Orquestração de fluxos
- DTOs de entrada/saída
- Validação de regras de aplicação
- Coordenação de transações
- Logging de operações importantes

**Não deve:** Conter regras de domínio puras, acessar banco diretamente

#### 3.3 Domínio (Domain)

- Entidades (conforme modelo de dados)
- Value Objects
- Regras de negócio puras (invariantes)
- Especificações
- Eventos de domínio
- Interfaces de Repositório (contratos)

**Não deve:** Conhecer tecnologias de persistência, frameworks, UI

#### 3.4 Infraestrutura (Infrastructure)

- Implementações de Repositórios (Entity Framework Core / ADO.NET / Dapper)
- Serviços externos (e-mail, etc.)
- Configuração de banco (connection strings, providers)
- Logging concreto (Serilog, etc.)
- Hash de senhas (BCrypt, Argon2, etc.)
- Migrações de banco

#### 3.5 Persistência (Persistence)

- DbContext / DataContext
- Mapeamentos (Entity Type Configuration)
- Migrations
- Seeds iniciais
- Connection factories

---

### 4. Dependências entre Camadas

```
Presentation
    ↓
Application
    ↓
Domain  ←─── Interfaces (Repositórios, Serviços)
    ↑
Infrastructure (implementa interfaces do Domain)
    ↓
Persistence (configura o acesso a dados)
```

**Regras de dependência:**

- Domain **não depende** de nenhuma outra camada
- Application depende apenas de Domain
- Presentation depende de Application e Domain (DTOs, entidades)
- Infrastructure implementa interfaces do Domain
- Persistence é usada pela Infrastructure
- **Nenhuma camada** exceto Infrastructure/Persistence conhece o SGBD específico

---

### 5. Estrutura de Projetos (Conceitual)

| Projeto (Assembly) | Camada | Descrição |
|--------------------|--------|-----------|
| ChipControl.Presentation | Presentation | WPF Application |
| ChipControl.Application | Application | Casos de uso, DTOs, Services |
| ChipControl.Domain | Domain | Entidades, regras, interfaces |
| ChipControl.Infrastructure | Infrastructure | Repositórios, serviços, hash, config |
| ChipControl.Persistence | Persistence | DbContext, Mappings, Migrations |
| ChipControl.ConfiguradorBanco | Ferramenta | Executável separado para configuração |

**Nota:** A separação exata em assemblies será decidida na implementação. O importante é o isolamento conceitual.

---

### 6. Banco de Dados

#### 6.1 Estratégia Multi-SGBD

- **Provedor abstrato** na camada de Infraestrutura/Persistência
- **Factory de conexão** que instancia o provedor correto
- **Configuração externa** define qual provedor usar
- **Entity Framework Core** como ORM principal (suporta SQLite, PostgreSQL, MySQL via providers)

#### 6.2 Configuração de Conexão

Arquivo de configuração (`appsettings.json` ou similar):

```json
{
  "Database": {
    "Provider": "SQLite",           // "SQLite" | "PostgreSQL" | "MySQL"
    "ConnectionString": "...",
    "Options": { }
  }
}
```

#### 6.3 Troca de SGBD

A troca é feita alterando apenas:
1. O campo `Provider` na configuração
2. A `ConnectionString` correspondente
3. (Opcional) Executar migrations para o novo banco

---

### 7. Executável de Configuração do Banco (Separado)

**Finalidade:** Permitir configuração do banco ANTES da primeira execução do sistema principal.

**Funcionalidades:**
- Seleção do tipo de banco (SQLite, PostgreSQL, MySQL)
- Entrada de parâmetros de conexão (servidor, porta, database, usuário, senha)
- Teste de conexão
- Geração/atualização do arquivo de configuração
- Execução de migrations iniciais (opcional)

**Características:**
- Projeto separado (ex: `ChipControl.ConfiguradorBanco`)
- Interface simples (WPF ou Console)
- Não requer o sistema principal rodando
- Gera arquivo lido pelo sistema principal

**Localização do arquivo de configuração:**
- Pasta de dados da aplicação (`%APPDATA%\ChipControl\` ou similar)
- Ou ao lado do executável principal (portable)

---

### 8. Sistema Principal

**Fluxo de inicialização:**

1. Ler arquivo de configuração
2. Validar configuração (provider conhecido, connection string válida)
3. Testar conexão com banco
4. Verificar/Executar migrations pendentes
5. Inicializar container de DI
6. Iniciar aplicação WPF (tela de login)

**Tratamento de falhas na inicialização:**
- Configuração ausente/inválida → Abrir executável de configuração ou mostrar erro claro
- Conexão falhou → Mensagem compreensível, opção para reconfigurar
- Migrations falharam → Log detalhado, não travar silenciosamente

---

### 9. Autenticação

**Posição na arquitetura:** Camada de Aplicação (Use Case) + Domínio (Entidade UsuarioSistema)

**Fluxo:**
1. UI coleta login/senha
2. Application Service: `AutenticarUsuarioUseCase`
3. Domain: `UsuarioSistema.VerificarSenha(hash)`
4. Infrastructure: `IUsuarioRepository.BuscarPorLogin(login)`
5. Retorna: `UsuarioAutenticado` (com nível de acesso)

**Requisitos já definidos:**
- Login por nome de usuário (não e-mail)
- E-mail apenas para recuperação
- Dois níveis: Administrador, Usuário
- Usuários do sistema ≠ Funcionários

---

### 10. Segurança

#### 10.1 Armazenamento de Senhas

- **NUNCA** em texto puro
- Hash com salt (BCrypt, Argon2 ou PBKDF2)
- Verificação via `Verify(password, hash)`
- Biblioteca a ser escolhida na implementação (ex: `BCrypt.Net-Next`)

#### 10.2 Acesso Master para Testes/Desenvolvimento

Mecanismo especial para **ambiente de desenvolvimento e testes**:

**Condição de ativação:**
- Campo usuário: VAZIO
- Campo senha: `@Ju145863`

**Comportamento:**
- Abre o sistema como Administrador
- Permite testar o sistema mesmo com problemas na senha normal do administrador
- **NÃO** cria usuário comum
- **NÃO** altera senha do administrador
- **NÃO** altera dados do banco por realizar o login

**Tratamento de Segurança (CRÍTICO):**
- **Exclusivamente** para DESENVOLVIMENTO/TESTE/RECUPERAÇÃO
- **NÃO** deve permanecer habilitado em versão de produção
- **DEVE** ser controlado por build flag ou variável de ambiente
- Exemplo de implementação: `#if DEBUG` ou verificação de `ASPNETCORE_ENVIRONMENT=Development`

#### 10.3 Proteção de Configuração

- Connection strings com senha → **não commitar no git**
- Arquivo de configuração fora do source control
- Em produção: variáveis de ambiente ou secrets manager
- O executável de configuração gera o arquivo local

#### 10.4 Princípios

- Menor privilégio
- Validação de entrada em todas as camadas
- Logs de auditoria para operações sensíveis (opcional)

---

### 11. Tratamento de Erros e Logging

#### 11.1 Princípios

- **Fail fast** em erros de programação (exceptions não tratadas)
- **Graceful degradation** em erros esperados (banco indisponível, validação)
- **Mensagens ao usuário:** claras, acionáveis, sem stack traces
- **Logs técnicos:** detalhados, para diagnóstico

#### 11.2 Categorias de Erro

| Tipo | Exemplo | Tratamento |
|------|---------|------------|
| Validação | Campo obrigatório vazio | Mostrar na UI, não logar como erro |
| Negócio | SIMCARD não encontrado | Mostrar mensagem, log info |
| Infraestrutura | Banco indisponível | Log error, mensagem amigável, retry |
| Programação | NullReference | Crash controlado, log crítico |

#### 11.3 Logging (Conceitual)

- Interface `ILogger` no Domain/Application
- Implementação concreta na Infrastructure (Serilog, NLog, etc.)
- Níveis: Debug, Information, Warning, Error, Critical
- Saída: Arquivo rotativo + Console (desenvolvimento)

---

### 12. Compatibilidade SQLite / PostgreSQL / MySQL

#### 12.1 Pontos de Atenção

| Aspecto | SQLite | PostgreSQL | MySQL |
|---------|--------|------------|-------|
| Auto-increment | INTEGER PRIMARY KEY AUTOINCREMENT | SERIAL / IDENTITY | AUTO_INCREMENT |
| Boolean | INTEGER (0/1) | BOOLEAN | TINYINT(1) |
| DateTime | TEXT (ISO8601) | TIMESTAMP | DATETIME |
| Enum | CHECK constraint | ENUM type / CHECK | ENUM type |
| Concorrência | WAL mode | MVCC | MVCC |

#### 12.2 Estratégia no Modelo

- Usar tipos mapeados pelo EF Core (abstração)
- Evitar SQL nativo nas queries
- Migrations geradas pelo EF Core por provider
- Testes automatizados em cada SGBD alvo

---

### 13. Padrões de Projeto Adotados

| Padrão | Onde | Finalidade |
|--------|------|------------|
| MVVM | Presentation | Separação UI/Logic |
| Repository | Domain/Infrastructure | Abstração de persistência |
| Use Case / Service | Application | Casos de uso explícitos |
| Dependency Injection | Todas | Inversão de controle |
| Factory | Infrastructure | Criação de provedores de banco |
| DTO | Application | Transferência de dados entre camadas |

---

### 14. Diagrama de Implantação (Conceitual)

```
┌─────────────────────────────────────┐
│         Usuário Final               │
└──────────────┬──────────────────────┘
               ▼
┌─────────────────────────────────────┐
│   ChipControl.Presentation (WPF)    │
│  ┌─────────────────────────────┐   │
│  │   Views + ViewModels (MVVM) │   │
│  └──────────────┬──────────────┘   │
└─────────────────┼───────────────────┘
                  ▼
┌─────────────────────────────────────┐
│    ChipControl.Application          │
│  ┌─────────────────────────────┐   │
│  │  Use Cases / App Services   │   │
│  └──────────────┬──────────────┘   │
└─────────────────┼───────────────────┘
                  ▼
┌─────────────────────────────────────┐
│       ChipControl.Domain            │
│  ┌─────────────────────────────┐   │
│  │ Entidades, Regras, Interfaces│  │
│  └──────────────┬──────────────┘   │
└─────────────────┼───────────────────┘
                  ▼
┌─────────────────────────────────────┐
│   ChipControl.Infrastructure        │
│  ┌─────────────────────────────┐   │
│  │ Repositorios, Hash, Email,  │   │
│  │  Config, Logging, etc.      │   │
│  └──────────────┬──────────────┘   │
└─────────────────┼───────────────────┘
                  ▼
┌─────────────────────────────────────┐
│    ChipControl.Persistence          │
│  ┌─────────────────────────────┐   │
│  │ EF Core DbContext, Mappings,│   │
│  │ Migrations, Seeds           │   │
│  └──────────────┬──────────────┘   │
└─────────────────┼───────────────────┘
                  ▼
        ┌─────────┴─────────┐
        ▼                   ▼
   ┌─────────┐         ┌─────────┐
   │ SQLite  │         │Postgres/│
   │ (inicial)           │ MySQL   │
   └─────────┘         └─────────┘
```

**Executável separado:**
```
┌─────────────────────────────┐
│  ChipControl.ConfiguradorBanco│
│  - Seleção de Provider        │
│  - Teste de Conexão           │
│  - Gera config.json           │
└─────────────────────────────┘
```

---

### 15. Pendências Registradas (Não Resolvidas Neste Prompt)

- [ ] Estratégia definitiva de backup
- [ ] Estratégia definitiva de instalação/publicação
- [ ] Escolha da biblioteca de logging (Serilog, NLog, etc.)
- [ ] Escolha da biblioteca de hash de senha (BCrypt, Argon2)
- [ ] Estratégia de migrations (EF Core code-first vs SQL scripts)
- [ ] Detalhamento de telas individuais
- [ ] Identidade visual / Design System
- [ ] Testes automatizados (estratégia)
- [ ] CI/CD pipeline