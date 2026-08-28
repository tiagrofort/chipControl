# Modelo de Dados

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não definir campos de banco neste momento. Cada entidade e campo deve ser especificado aprovadamente.

## Histórico de alterações

* [x] **2026-08-28** — Prompt 007 executado. Definido o modelo conceitual de dados com 8 tabelas, campos, chaves, relacionamentos, histórico, integridade e observações. Compatível com SQLite (inicial) e preparado para PostgreSQL/MySQL futuramente.
* [x] **2026-08-28** — Prompt 011 executado. Relação entre SIMCards, HistoricoNumeros, HistoricoUtilizacao e HistoricoSubstituicao documentada explicitamente. Nota de que o modelo NÃO é Event Sourcing completo registrada. Formato de importação definido (EXCEL .xlsx). Correções tipográficas pontuais realizadas. Conteúdo do UX Design versionado.

## Modelo de Dados

### Princípio Fundamental

O modelo NUNCA sobrescreve informações históricas. Toda alteração gera um novo registro no histórico. O estado atual é derivado dos registros históricos mais recentes, sem exclusão de dados anteriores.

> **Regra final (Prompt 011):** o modelo NÃO é Event Sourcing completo. As entidades de cadastro (ex.: SIMCards) mantêm o estado atual do SIMCARD físico; as tabelas de histórico registram a evolução (números, utilização, substituição). O estado atual NÃO deve ser duplicado desnecessariamente dentro das tabelas de histórico. As tabelas de histórico registram períodos (data_inicio/data_fim) e nunca apagam ou sobrescrevem registros anteriores.

---

## Entidades / Tabelas Conceituais

### 1. UsuariosSistema

Cadastro de usuários que acessam o sistema.

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária, auto-incremento |
| nome | varchar(255) | sim | Nome completo do usuário |
| login | varchar(100) | sim | Nome de usuário para login (único) |
| senha | varchar(255) | sim | Senha (hash) |
| email | varchar(255) | não | E-mail (para recuperação) |
| nivel_acesso | enum | sim | Administrador ou Usuario |
| ativo | boolean | sim | Status ativo/inativo |
| observacoes | text | não | Observações |
| data_cadastro | datetime | sim | Data de cadastro |
| data_alteracao | datetime | não | Data da última alteração |

**Chave:** id (PK)

**Índice único:** login

---

### 2. Funcionarios

Cadastro de funcionários da empresa (independente de UsuarioSistema).

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária, auto-incremento |
| nome_completo | varchar(255) | sim | Nome completo |
| matricula | varchar(50) | não | Matrícula do funcionário |
| setor | varchar(100) | sim | Setor onde trabalha |
| cargo | varchar(100) | não | Cargo atual |
| telefone_pessoal | varchar(20) | não | Telefone pessoal |
| email | varchar(255) | não | E-mail pessoal |
| ativo | boolean | sim | Status ativo/inativo |
| observacoes | text | não | Observações |
| data_cadastro | datetime | sim | Data de cadastro |
| data_alteracao | datetime | não | Data da última alteração |

**Chave:** id (PK)

---

### 3. Operadoras

Cadastro de operadoras de telefonia.

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária, auto-incremento |
| nome | varchar(255) | sim | Nome da operadora |
| codigo | varchar(50) | não | Código de identificação |
| cnpj | varchar(18) | não | CNPJ |
| telefone | varchar(20) | não | Telefone de contato |
| email | varchar(255) | não | E-mail de contato |
| ativo | boolean | sim | Status ativo/inativo |
| observacoes | text | não | Observações |
| data_cadastro | datetime | sim | Data de cadastro |

**Chave:** id (PK)

---

### 4. SIMCards

Cadastro de SIMCARDs.

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária, auto-incremento |
| id_operadora | integer | sim | FK para Operadoras |
| identificacao_chip | varchar(100) | sim | Identificação física (ex: "Chip 01") |
| iccid | varchar(22) | sim | ICCID do SIMCARD |
| ddd | varchar(3) | não | DDD |
| plano_tipo | varchar(100) | não | Nome/tipo do plano |
| tem_minutagem | boolean | não | Indica se possui minutagem |
| quantidade_minutos | integer | não | Quantidade de minutos (quando aplicável) |
| tem_internet | boolean | não | Indica se possui franquia de internet |
| quantidade_internet | integer | não | Quantidade em MB/GB (quando aplicável) |
| data_aquisicao | date | não | Data de aquisição |
| data_ativacao | date | não | Data de ativação |
| status | enum | sim | Status atual do SIMCARD |
| observacoes | text | não | Observações |
| ativo | boolean | sim | Status ativo/inativo |
| data_cadastro | datetime | sim | Data de cadastro |
| data_alteracao | datetime | não | Data da última alteração |

**Chave:** id (PK)

**Índices únicos:**
- iccid (único)
- identificacao_chip por id_operadora (único composto — evitar chips com mesma identificação em operadoras diferentes)

**Status válidos (conforme Prompt 005):**
- Em estoque
- Em uso particular
- WhatsApp
- Danificado
- Perdido
- Não devolvido
- Descartado
- Inativo

---

### 5. Aparelhos

Cadastro de aparelhos smartphones.

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária, auto-incremento |
| identificacao | varchar(100) | sim | Identificação do aparelho |
| tipo | varchar(50) | sim | Tipo (celular, tablet, etc.) |
| marca | varchar(100) | não | Marca |
| modelo | varchar(100) | não | Modelo |
| imei_1 | varchar(15) | não | IMEI principal |
| imei_2 | varchar(15) | não | IMEI secundário |
| tipo_proprietario | enum | sim | Empresa ou Funcionario |
| id_funcionario_proprietario | integer | não | FK para Funcionarios (quando tipo_proprietario = Funcionario) |
| ativo | boolean | sim | Status ativo/inativo |
| observacoes | text | não | Observações |
| data_cadastro | datetime | sim | Data de cadastro |
| data_alteracao | datetime | não | Data da última alteração |

**Chave:** id (PK)

**Índices únicos:**
- imei_1 (quando não nulo)
- imei_2 (quando não nulo)
- identificacao

**Relacionamento condicional:**
- Se tipo_proprietario = Funcionario → id_funcionario_proprietario é obrigatório
- Se tipo_proprietario = Empresa → id_funcionario_proprietario é nulo

---

### 6. HistoricoNumeros

Registra a sequência de números telefônicos utilizados por cada SIMCARD ao longo do tempo.

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária |
| id_simcard | integer | sim | FK para SIMCards |
| numero | varchar(20) | sim | Número telefônico |
| data_inicio | datetime | sim | Início da utilização deste número |
| data_fim | datetime | não | Fim da utilização deste número (nulo = atual) |
| observacao | text | não | Motivo da troca, se aplicável |
| id_usuario_registro | integer | sim | FK para UsuariosSistema (quem registrou) |

**Chave:** id (PK)

**Índices:**
- id_simcard + data_inicio
- numero

**Regra:** data_fim = null indica número atual. A troca gera novo registro com data_fim preenchido no registro anterior.

---

### 7. HistoricoUtilizacao

Registra a sequência de utilizações de cada SIMCARD, associando funcionário, aparelho e situação ao longo do tempo.

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária |
| id_simcard | integer | sim | FK para SIMCards |
| id_funcionario | integer | não | FK para Funcionarios (usuário atual, pode ser nulo) |
| id_aparelho | integer | não | FK para Aparelhos (aparelho em uso, pode ser nulo) |
| situacao | enum | sim | Situação de uso |
| data_inicio | datetime | sim | Início deste período de utilização |
| data_fim | datetime | não | Fim deste período (nulo = atual) |
| observacao | text | não | Detalhes da situação |
| id_usuario_registro | integer | sim | FK para UsuariosSistema (quem registrou) |

**Chave:** id (PK)

**Índices:**
- id_simcard + data_inicio
- id_funcionario + data_inicio

**Situação de uso (derivada do status do SIMCARD + contexto):**
- Chip em estoque
- Chip em uso particular (no aparelho do funcionário)
- Linha em uso para WhatsApp
- Chip em manutenção/devolvido danificado
- Chip perdido
- Chip não devolvido
- Chip descartado
- Chip inativo

**Regra:** data_fim = null indica utilização atual. Mudança de funcionário/aparelho/situação gera novo registro.

---

### 8. HistoricoSubstituicao

Registra substituições de SIMCARD, preservando tanto o SIMCARD antigo quanto o novo.

| Campo | Tipo Conceitual | Obrigatório | Descrição |
|-------|----------------|-------------|-----------|
| id | integer | sim | Chave primária |
| id_simcard_antigo | integer | sim | FK para SIMCards (SIMCARD substituído) |
| id_simcard_novo | integer | sim | FK para SIMCards (novo SIMCARD) |
| id_numero | integer | sim | FK para HistoricoNumeros (número mantido na substituição) |
| motivo | enum | sim | Motivo da substituição |
| data_substituicao | datetime | sim | Data da substituição |
| observacao | text | não | Detalhes adicionais |
| id_usuario_registro | integer | sim | FK para UsuariosSistema (quem registrou) |

**Chave:** id (PK)

**Motivos conhecidos:**
- Danificado
- Perdido
- Não devolvido

**Regra:** Mantém o vínculo entre o SIMCARD antigo e o novo, preservando ambos os cadastros.

---

## Relacionamentos

| Origem | Destino | Cardinalidade | Obrigatório | Finalidade |
|--------|---------|---------------|-------------|-----------|
| SIMCards | Operadoras | N:1 | Sim | Identificar a operadora do SIMCARD |
| HistoricoNumeros | SIMCards | N:1 | Sim | Associar números ao SIMCARD |
| HistoricoUtilizacao | SIMCards | N:1 | Sim | Associar utilização ao SIMCARD |
| HistoricoUtilizacao | Funcionarios | N:1 | Não | Identificar o funcionário que utiliza a linha |
| HistoricoUtilizacao | Aparelhos | N:1 | Não | Identificar o aparelho em uso |
| Aparelhos | Funcionarios | N:1 | Não | Identificar proprietário quando tipo = Funcionario |
| HistoricoSubstituicao | SIMCards | N:2 | Sim | Relacionar SIMCARD antigo e novo |
| HistoricoSubstituicao | HistoricoNumeros | N:1 | Sim | Manter vínculo com o número |
| HistoricoNumeros | UsuariosSistema | N:1 | Sim | Registrar quem fez o registro |
| HistoricoUtilizacao | UsuariosSistema | N:1 | Sim | Registrar quem fez o registro |
| HistoricoSubstituicao | UsuariosSistema | N:1 | Sim | Registrar quem fez o registro |
| UsuariosSistema | NivelAcesso | 1:1 | Sim | Definir nível de acesso |
| SIMCards | Status | 1:1 | Sim | Status atual do SIMCARD |

---

## Integridade

### Regras de Integridade

1. **ICCID único:** Cada SIMCARD deve ter ICCID único no banco.

2. **Identificação do chip única por operadora:** Evitar duplicidade de "Chip 01" na mesma operadora.

3. **IMEI único:** Cada IMEI (quando informado) deve ser único no banco.

4. **Login único:** Nome de usuário de login não pode repetir.

5. **Histórico não-deletável:** Registros de histórico não devem ser removidos. A exclusão deve ser logicalmente negada.

6. **Números sem sobrescrita:** Nunca atualizar o campo numero em HistoricoNumeros. Sempre criar novo registro.

7. **Utilização sem sobrescrita:** Nunca atualizar os campos de relacionamento em HistoricoUtilizacao. Sempre criar novo registro.

8. **Substituição preserva ambos:** Um SIMCARD substituído não é excluído. Ambos (antigo e novo) permanecem no cadastro.

9. **Funcionário inativo preserva histórico:** Um funcionário inativo não deve ser excluído. Seu histórico permanece consultável.

10. **Operadora inativa preserva histórico:** Uma operadora inativa não deve ser excluída. SIMCARDs antigos permanecem vinculados.

11. **Aparelho inativo preserva histórico:** Um aparelho inativo não deve ser excluído. Seu histórico permanece consultável.

---

## Observações Importantes

### Sobre o Histórico

O modelo utiliza append-only para histórico. Qualquer alteração gera um novo registro, com data_fim preenchido no registro anterior.

**Exemplo — Troca de Funcionário:**

Estado inicial:
```
Registro 1: SIMCARD A → Funcionario João → data_inicio: 01/01 → data_fim: null
```

Após troca para Maria:
```
Registro 1: SIMCARD A → Funcionario João → data_inicio: 01/01 → data_fim: 15/01
Registro 2: SIMCARD A → Funcionario Maria → data_inicio: 15/01 → data_fim: null
```

### Sobre WhatsApp

O modelo permite representar a situação onde:
- SIMCARD está em status "Em estoque" (fisicamente no estoque)
- Mas a linha continua ativa para WhatsApp

Isso é feito através de:
- HistoricoUtilizacao com situacao = "Linha em uso para WhatsApp"
- Data de fim em branco indica que a linha continua ativa

### Sobre Substituição Simultânea

Quando SIMCARD e número são substituídos juntos:
1. SIMCARD antigo permanece com status "Descartado" ou similar
2. Novo SIMCARD é cadastrado normalmente
3. Número antigo vai para histórico com data_fim
4. Novo número entra como registro atual
5. HistoricoSubstituicao registra o vínculo entre os dois SIMCARDs

### Sobre Plano/Tipo de Linha

Conforme decisão do Prompt 005, o plano/tipo de linha será registrado como campos no próprio SIMCARD:
- plano_tipo: texto livre
- tem_minutagem: booleano
- quantidade_minutos: número (quando aplicável)
- tem_internet: booleano
- quantidade_internet: número (quando aplicável)

A decisão sobre criar um cadastro separado de Planos será tomada em momento futuro, se necessário.

---

## Relação entre SIMCards e as Tabelas de Histórico (Prompt 011)

### Papéis de cada tabela — REGRA FINAL

| Tabela | Papel |
|--------|-------|
| **SIMCards** | Cadastro atual do SIMCARD físico (identificação do chip, ICCID, operadora, status, plano). Representa o estado atual. |
| **HistoricoNumeros** | Evolução dos números associados ao SIMCARD ao longo do tempo (número, data_inicio, data_fim). O registro com data_fim = null é o número atual. |
| **HistoricoUtilizacao** | Períodos de utilização da linha/SIMCARD, incluindo funcionário e/ou aparelho quando aplicável (data_inicio, data_fim, situação). O registro com data_fim = null é a utilização atual. |
| **HistoricoSubstituicao** | Substituição de um SIMCARD por outro (SIMCARD antigo, SIMCARD novo, motivo, data). Preserva ambos os cadastros. |

### Regras fechadas

- [x] **NÃO é Event Sourcing completo.** Simples modelo relacional com registros históricos que nunca são apagados ou sobrescritos.
- [x] **SIMCards** mantém o estado atual do SIMCARD físico (não é reescrito pelas tabelas de histórico).
- [x] **HistoricoNumeros** registra a evolução dos números; o número anterior permanece com data_fim preenchido e o novo número é um novo registro com data_fim = null.
- [x] **HistoricoUtilizacao** registra os períodos de utilização da linha/SIMCARD, com funcionário e/ou aparelho quando aplicável; mudanças geram novos registros.
- [x] **HistoricoSubstituicao** registra a substituição de um SIMCARD por outro, sem apagar o SIMCARD antigo.
- [x] NÃO duplicar desnecessariamente o estado atual dentro das tabelas de histórico.
- [x] O estado atual (número atual, utilização atual, status) é derivado dos registros mais recentes (data_fim = null).

---

## Compatibilidade de Banco de Dados

### SQLite (Inicial)
- Tipos: INTEGER, TEXT, REAL, BLOB
- AUTOINCREMENT para chaves primárias
- CHECK constraints para enum (via trigger ou application-level)
- Boolean como INTEGER (0/1)

### PostgreSQL (Futuro)
- Tipos nativos: SERIAL, VARCHAR, TEXT, BOOLEAN, DATE, TIMESTAMP
- ENUM nativo para status e situações
- Sequências

### MySQL (Futuro)
- Tipos: INT, VARCHAR, TEXT, BOOLEAN, DATE, DATETIME
- ENUM para status e situações
- AUTO_INCREMENT

### Recomendações
- Usar tipos de dados portáveis entre bancos
- Evitar recursos específicos de um SGBD
- Deferir validações complexas para a aplicação
- Foreign keys com ON DELETE RESTRICT

---

## Pendências e Dúvidas Registradas

| Item | Descrição | Status |
|------|-----------|--------|
| 1 | Estrutura técnica do plano/tipo de linha | Decisão postergada — campos simples adotados |
| 2 | Cadastro separado de Planos | Decisão postergada — não criar sem necessidade |
| 3 | Formato do arquivo de importação | RESOLVIDO no Prompt 011 — EXCEL (.xlsx); primeira versão trabalha com número antigo, número do SIMCARD e número novo |
| 4 | Regras detalhadas de cada status | DECISÃO REGISTRADA no Prompt 011 — separação conceitual situação física × utilização da linha; regras transicionais operacionais para a implementação |
| 5 | Permissões granulares de usuários | Fora do escopo — apenas 2 níveis |
| 6 | Auditoria detalhada | Pendente de definição |
| 7 | Backup e restore | Pendente de definição |
| 8 | Estratégia de migrations | Pendente de definição |