# Prompt 010 — Revisão Final e Congelamento da Especificação

## Identificação

- [X] Prompt 010
- [X] Data: 2026-08-28

## Objetivo

- [X] Fazer a última revisão estrutural da documentação antes do início da codificação.
- [X] Congelar a especificação.

## Prompt completo

PROMPT 010 — REVISÃO FINAL E CONGELAMENTO DA ESPECIFICAÇÃO

[Conteúdo integral do prompt original preservado]

## Resultado esperado

- [ ] Especificação revisada e congelada.

## Resultado obtido

- [ ] Preencher após execução.

## Decisões Finais (Prompt 010)

### 1. Modelo de Histórico — DECISÃO FECHADA

**Abordagem adotada:** Append-only com tabelas de histórico separadas.

O sistema mantém:
- Cadastro atual das entidades (SIMCards, Funcionarios, etc.)
- Tabelas de histórico separadas para alterações relevantes

**Não é Event Sourcing completo** — é simplesmente um modelo relacional com registros históricos que nunca são apagados ou sobrescritos. O estado atual é DERIVADO dos registros mais recentes.

**Regras:**
- Nunca atualizar campos de histórico — sempre criar novo registro
- data_fim = null indica registro atual
- data_fim preenchido indica fim do período
- Usuário do sistema responsável pela operação é registrado em cada histórico

**O que pode ser reconstruído:**
- [x] Números anteriores e atuais
- [x] Quem utilizou cada SIMCARD (atual e anteriores)
- [x] Aparelhos relacionados (atuais e anteriores)
- [x] Substituições e motivos
- [x] Datas de todas as operações
- [x] Usuário do sistema responsável por cada registro

### 2. Relação SIMCARD e Números — DECISÃO FECHADA

```
SIMCARD
    ↓
Histórico de Números (HistoricoNumeros)
    ↓
→ número atual (data_fim = null)
→ números anteriores (data_fim preenchido)
```

**Mantém:**
- [x] Número antigo (preservado com data_fim)
- [x] Número novo (novo registro com data_fim = null)
- [x] SIMCARD relacionado
- [x] Data de início
- [x] Data de fim, quando aplicável
- [x] Operação responsável (id_usuario_registro)

### 3. Fluxo de Troca de Números por Importação — DECISÃO FECHADA

**Entrada:** Lista da operadora com número antigo, número do SIMCARD, número novo.

**Fluxo:**
1. IMPORTAÇÃO — leitura dos registros do arquivo
2. TENTATIVA DE RELACIONAMENTO — correlacionar número antigo com SIMCARD existente
3. IDENTIFICAÇÃO:
   - SIMCARD encontrado → processar
   - SIMCARD não encontrado → marcar como pendente
   - Número antigo encontrado → validar correspondência
   - Número antigo não encontrado → marcar como pendente
   - Número novo já existente em outro SIMCARD → marcar como inconsistência
4. CONFERÊNCIA — exibir prévia para revisão
5. CONFIRMAÇÃO — aplicar alterações
6. REGISTRO — criar histórico (número antigo com data_fim, número novo com data_inicio)

**Regras:**
- [x] Divergência entre número antigo informado e número registrado → NÃO alterar automaticamente
- [x] SIMCARD novo que não existe → permitir cadastro rápido durante o processo
- [x] Nenhum dado anterior é apagado

### 4. Fluxo de Substituição de SIMCARD — DECISÃO FECHADA

**Fluxo:**
1. Seleção do SIMCARD antigo
2. Registro do motivo (Danificado, Perdido, Não devolvido, outro)
3. Se novo SIMCARD não existe → CADASTRO RÁPIDO (sem abandonar operação)
4. Confirmação da substituição
5. Registro em HistoricoSubstituicao (vincula antigo e novo)
6. Número pode ser mantido ou trocado (conforme contexto)

**Mantém:**
- [x] SIMCARD antigo (com status alterado, não excluído)
- [x] Motivo da substituição
- [x] SIMCARD novo
- [x] Número relacionado
- [x] Usuário que realizou a operação
- [x] Data da operação

### 5. Utilização do SIMCARD — DECISÃO FECHADA

**Definição clara de dois conceitos:**

| Conceito | Descrição |
|----------|-----------|
| LOCALIZAÇÃO FÍSICA DO SIMCARD | Onde está o chip físico (estoque, com funcionário, danificado, etc.) |
| SITUAÇÃO/UTILIZAÇÃO DA LINHA | Se a linha está ativa e como está sendo usada |

**Cenário suportado:**
- SIMCARD físico está no estoque (localização física)
- MAS a linha continua ativa para WhatsApp (utilização da linha)

**Status do SIMCARD (localização física):**
- [x] Em estoque
- [x] Em uso particular
- [x] WhatsApp
- [x] Danificado
- [x] Perdido
- [x] Não devolvido
- [x] Descartado
- [x] Inativo

**Situação de utilização (capturada em HistoricoUtilizacao):**
- Chip em estoque
- Chip em uso particular
- Linha em uso para WhatsApp
- Chip danificado/devolvido
- Chip perdido
- Chip não devolvido
- Chip descartado
- Chip inativo

### 6. Cadastro Rápido — REGRA CONFIRMADA

**Regra geral:**
- [x] Quando uma operação depender de um cadastro que não existe, deve haver botão para cadastrar sem abandonar a operação
- [x] Formulário original preserva dados já preenchidos
- [x] Após cadastro rápido, novo registro fica disponível para seleção
- [x] Operação original continua sem reiniciar

**Exemplo:** Troca de número → SIMCARD não encontrado → Cadastrar SIMCARD → Salvar → Retornar à troca → Continuar operação.

### 7. Autenticação — DECISÃO FECHADA

- [x] Login por nome de usuário (não e-mail)
- [x] Senha (nunca em texto puro — hash com salt)
- [x] Dois níveis: Administrador, Usuário
- [x] E-mail opcional, apenas para recuperação de senha
- [x] Usuários do sistema são independentes de funcionários

### 8. Acesso Master para Testes/Desenvolvimento — DECISÃO REGISTRADA

**Mecanismo especial para DESENVOLVIMENTO e TESTES:**

**Condição de ativação:**
- Campo usuário: VAZIO
- Campo senha: @Ju145863

**Comportamento:**
- [x] Abre o sistema como Administrador
- [x] Permite testar o sistema mesmo quando houver problema com senha normal do administrador
- [x] NÃO cria usuário comum
- [x] NÃO altera senha do administrador
- [x] NÃO altera dados do banco por realizar o login

**SEGURANÇA — TRATAMENTO OBRIGATÓRIO:**
- [x] Este mecanismo é exclusivamente para DESENVOLVIMENTO/TESTE/RECUPERAÇÃO
- [x] NÃO deve permanecer habilitado em versão de produção
- [x] A arquitetura DEVERÁ permitir desabilitar em build de produção
- [x] Pode ser controlado por configuração de ambiente/build (ex: #if DEBUG, variáveis de ambiente)

**Não é:**
- [x] Funcionalidade de usuário comum
- [x] Backdoor para uso em produção
- [x] Substituto de recuperação de senha oficial

### 9. Banco de Dados — ESTRATÉGIA FECHADA

- [x] SQLite como banco inicial
- [x] PostgreSQL como banco futuro
- [x] MySQL como banco futuro
- [x] Código da aplicação NÃO é dependente estruturalmente do SQLite
- [x] Camada de persistência permite trocar provedor sem reescrever regra de negócio
- [x] Entity Framework Core como ORM (suporta todos os três bancos)

### 10. Relatórios da Primeira Versão — DEFINIDOS

**Relatórios essenciais:**

1. **SIMCARDs em Estoque** — Lista de SIMCARDs com status "Em estoque"
2. **SIMCARDs em Uso** — Lista de SIMCARDs com status "Em uso particular" ou "WhatsApp"
3. **SIMCARDs por Status** — Lista filtrada por qualquer status
4. **Linhas sem Utilização** — SIMCARDs em estoque sem utilização ativa para WhatsApp
5. **Utilização por Funcionário** — Quais linhas/SIMCARDs cada funcionário utiliza/utilizou
6. **Histórico de Números** — Números anteriores e atuais de cada SIMCARD
7. **Substituições Realizadas** — Histórico de substituições de SIMCARD
8. **SIMCARDs Danificados/Perdidos/Não Devolvidos** — Lista de SIMCARDs com esses status
9. **Aparelhos por Proprietário** — Lista de aparelhos agrupados por proprietário

**Características:**
- [x] Filtros por data, status, operador, funcionário
- [x] Possibilidade de exportar (formato a definir na implementação)
- [x] Acesso pode variar por nível de usuário (Administrador vs Usuário)

**FORA DO ESCOPO da primeira versão (vão para backlog se solicitado):**
- [ ] Relatórios comparativos entre períodos
- [ ] Gráficos e dashboards
- [ ] Relatórios agendados/automáticos
- [ ] Envio automático por e-mail

### 11. Interface / Google Stitch — CONFIRMADO

- [x] Google Stitch é a referência visual
- [x] NÃO foi redesenhado
- [x] NÃO foi alterado
- [x] Implementação será em WPF conforme arquitetura

**Mantido:**
- Menu lateral
- Área de ações superior
- Grids para listagens
- Modais para edição/inclusão
- Pesquisa por campos relevantes
- Cadastro rápido relacionado

**Ajuste permitido:**
- Disposição das colunas nas grids pode ser ajustada durante implementação
- NÃO é problema no design, é decisão de implementação

### 12. Pesquisa — REGRA CONFIRMADA

- [x] Pesquisa por todos os campos relevantes da tela
- [x] Não limitada a uma coluna selecionada
- [x] Aplica-se a todas as telas de cadastro

## Problemas encontrados

- [ ] Nenhum problema estrutural crítico encontrado na revisão.
- [ ] A documentação estava coerente e completa.
- [ ] Apenas ajustes documentais foram necessários.

## Correções realizadas

- [ ] Nenhuma correção de dados — apenas registro de decisões finais.
- [ ] Adição do Acesso Master de Testes como requisito de segurança.
- [ ] Definição dos relatórios essenciais da primeira versão.
- [ ] Fechamento definitivo das decisões pendentes.

## Pendências que permaneceram

As seguintes decisões são INDEPENDENTES da especificação e não bloqueiam o início da codificação:

- [ ] Escolha da biblioteca de logging (Serilog, NLog, etc.)
- [ ] Escolha da biblioteca de hash de senha (BCrypt, Argon2)
- [ ] Estratégia de migrations (EF Core code-first vs SQL scripts)
- [ ] Detalhamento de telas individuais (será feito na implementação)
- [ ] Identidade visual / Design System (será definido se necessário)
- [ ] Estratégia de testes automatizados
- [ ] Estratégia de backup definitivo
- [ ] Estratégia de publicação/instalação

## Confirmações Finais

- [x] Especificação revisada.
- [x] Todas as decisões estruturais fechadas ou classificadas.
- [x] Modelo de histórico definido (append-only com tabelas separadas).
- [x] Relação SIMCARD/números definida.
- [x] Fluxo de troca de números definido.
- [x] Fluxo de substituição de SIMCARD definido.
- [x] Diferença entre localização física e utilização da linha definida.
- [x] Cadastro rápido confirmado.
- [x] Autenticação definida.
- [x] Acesso master de testes registrado.
- [x] Estratégia de banco definida.
- [x] Relatórios essenciais definidos.
- [x] Interface/Stitch confirmada.
- [x] Nenhum código criado.
- [x] Nenhuma funcionalidade nova inventada.
- [x] Ideias futuras foram para o backlog.
- [x] Documentação está CONSISTENTE e COERENTE.

## Status do Congelamento

**[X] ESPECIFICAÇÃO CONGELADA**

A partir deste ponto:
- Novas funcionalidades NÃO entram no escopo.
- Novas ideias vão para docs/09-BACKLOG-FUTURO.md.
- Regras já aprovadas NÃO serão alteradas sem justificativa documentada.
- Mudanças estruturais futuras devem ser tratadas como exceção e aprovadas explicitamente.
- Bugs e correções de erros são permitidos.
- Ajustes de implementação que não alterem regras de negócio são permitidos.