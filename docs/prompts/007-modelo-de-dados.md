PROMPT 007 — MODELO DE DADOS

OBJETIVO

Definir o modelo de dados do Controle de Chips com base EXCLUSIVAMENTE
nas decisões já documentadas nos arquivos docs/.

Esta etapa deve transformar as regras de negócio em um modelo de dados
coerente, preparado para SQLite inicialmente e compatível futuramente
com PostgreSQL e MySQL.

IMPORTANTE:

- NÃO desenvolver código.
- NÃO criar banco.
- NÃO criar migrations.
- NÃO criar entidades C#.
- NÃO criar telas.
- NÃO instalar dependências.
- NÃO alterar regras de negócio já aprovadas.
- NÃO inventar funcionalidades.
- NÃO inventar campos que não foram aprovados.
- NÃO apagar documentação anterior.
- NÃO modificar commits anteriores.
- Se encontrar uma necessidade que não esteja definida, registrar como
  pendência ou no docs/09-BACKLOG-FUTURO.md, conforme o caso.
- O projeto continua congelado quanto a novas funcionalidades.

Registrar este prompt integralmente em:

docs/prompts/007-modelo-de-dados.md


==================================================
1. DOCUMENTOS DE REFERÊNCIA
==================================================

Antes de trabalhar:

Leia:

- docs/01-REQUISITOS.md
- docs/02-REGRAS-DE-NEGOCIO.md
- docs/03-MODELO-DE-DADOS.md
- docs/04-ARQUITETURA.md
- docs/08-CHECKPOINTS.md
- docs/09-BACKLOG-FUTURO.md
- docs/prompts/005-consolidacao-cadastros.md
- docs/prompts/006-regras-negocio-e-historico.md

O modelo deve respeitar o que já foi definido nesses documentos.

Se o Prompt 006 ainda não estiver executado no momento da leitura,
NÃO tentar antecipar seu conteúdo.


==================================================
2. OBJETIVO DO MODELO
==================================================

O modelo deverá permitir:

- [X] Cadastro de usuários do sistema.
- [X] Cadastro de funcionários.
- [X] Cadastro de operadoras.
- [X] Cadastro de SIMCARDs.
- [X] Cadastro de aparelhos.
- [X] Histórico de números.
- [X] Histórico de utilização.
- [X] Histórico de substituição de SIMCARD.
- [X] Histórico de funcionários relacionados às utilizações.
- [X] Histórico de aparelhos relacionados às utilizações.
- [X] Preservação dos registros antigos.
- [X] Identificação do estado atual.
- [X] Futuramente suportar importação de troca de números.


==================================================
3. REGRA FUNDAMENTAL
==================================================

O modelo NÃO deve depender de sobrescrever informações históricas.

Exemplo:

Não fazer apenas:

funcionario_atual = Maria

quando anteriormente era:

funcionario_atual = João

O modelo deverá permitir reconstruir o histórico.

O mesmo princípio vale para:

- números;
- SIMCARDs;
- funcionários;
- aparelhos;
- utilização.


==================================================
4. CADASTROS PRINCIPAIS
==================================================

Criar no documento uma seção para cada cadastro:

### Usuários do Sistema

Campos já aprovados:

- ID
- Nome
- Nome de usuário/login
- Senha
- E-mail
- Nível de acesso
- Ativo
- Observações

Não inventar outros campos.

### Funcionários

Campos já aprovados:

- ID
- Nome completo
- Matrícula
- Setor
- Cargo
- Telefone pessoal
- E-mail
- Ativo
- Observações

### Operadoras

Campos já aprovados:

- ID
- Nome
- Código/identificação
- CNPJ
- Telefone/contato
- E-mail
- Ativo
- Observações

### SIMCARD

Campos já aprovados:

- ID
- Identificação do chip
- ICCID
- Operadora
- Plano/tipo de linha
- DDD
- Data de aquisição
- Data de ativação
- Observações
- Status

### Aparelhos

Campos já aprovados:

- ID
- Identificação do aparelho
- Tipo
- Marca
- Modelo
- IMEI 1
- IMEI 2
- Proprietário
- Funcionário proprietário
- Observações
- Ativo


==================================================
5. HISTÓRICO DE NÚMEROS
==================================================

Definir no modelo uma estrutura para preservar os números utilizados
por cada SIMCARD.

Deve ser possível identificar:

- SIMCARD;
- número;
- início da utilização;
- fim da utilização;
- qual número é o atual;
- histórico dos números anteriores.

Não simplesmente sobrescrever o número atual.

Não inventar informações adicionais sem necessidade.


==================================================
6. HISTÓRICO DE UTILIZAÇÃO
==================================================

Definir uma estrutura capaz de registrar a utilização da linha/SIMCARD.

O modelo deverá permitir preservar:

- SIMCARD;
- funcionário relacionado;
- aparelho relacionado quando aplicável;
- período de utilização;
- situação relacionada ao uso;
- histórico de mudanças.

Deve ser possível representar a situação em que:

1. o chip está fisicamente no aparelho do funcionário;
2. o chip está fisicamente no estoque;
3. a linha continua sendo utilizada para WhatsApp;
4. o funcionário muda;
5. o aparelho muda.

Não criar regras novas.


==================================================
7. WHATSAPP
==================================================

O modelo deve suportar o cenário:

SIMCARD fisicamente no estoque
+
número ainda utilizado para WhatsApp.

Não assumir que:

"Em estoque" = "linha sem utilização".

O histórico deve permitir identificar que a linha continua em uso
para WhatsApp mesmo sem o SIMCARD estar com o funcionário.

Não criar integração com WhatsApp.


==================================================
8. SUBSTITUIÇÃO DE SIMCARD
==================================================

Definir estrutura para preservar:

- SIMCARD antigo;
- SIMCARD novo;
- motivo da substituição;
- data;
- relação com a linha/número;
- histórico.

Deve suportar:

SIMCARD antigo
      ↓
SIMCARD novo

sem apagar o cadastro antigo.

Motivos já conhecidos:

- Danificado
- Perdido
- Não devolvido

Não criar outros motivos como regra obrigatória.


==================================================
9. TROCA DE NÚMERO
==================================================

O modelo deve suportar:

Número antigo
      ↓
Número novo

mantendo o histórico.

Também deve suportar:

SIMCARD antigo + número antigo
      ↓
SIMCARD novo + número novo

sem perder nenhum dos registros anteriores.


==================================================
10. IMPORTAÇÃO FUTURA
==================================================

A operadora fornece:

- Número antigo
- Número do SIMCARD
- Número novo

O modelo deve ser compatível com essa operação.

IMPORTANTE:

Não implementar a importação neste prompt.

Apenas garantir que o modelo de dados consiga suportar a operação.


==================================================
11. PLANO / TIPO DE LINHA
==================================================

Já foi definido que o SIMCARD deve permitir registrar:

- plano/tipo de linha;
- existência de minutagem;
- quantidade de minutos;
- existência de internet;
- quantidade de internet.

Neste prompt:

- [X] Definir uma representação adequada no modelo.
- [ ] Não criar funcionalidades adicionais.
- [ ] Não criar cadastro separado de planos sem decisão explícita.

Se houver dúvida real sobre a melhor representação, documentar a
dúvida e escolher a alternativa mais simples e compatível com o
escopo atual, sem criar funcionalidades novas.


==================================================
12. RELACIONAMENTOS
==================================================

Documentar claramente os relacionamentos entre:

- Usuários do sistema.
- Funcionários.
- Operadoras.
- SIMCARDs.
- Números.
- Aparelhos.
- Histórico de utilização.
- Histórico de substituição.

Para cada relacionamento, informar:

- origem;
- destino;
- se é obrigatório ou opcional;
- finalidade.

Não implementar.


==================================================
13. INTEGRIDADE
==================================================

Definir regras de integridade necessárias para evitar:

- duplicação indevida de ICCID;
- duplicação indevida de identificação física do chip;
- inconsistências entre histórico e cadastro atual;
- perda de histórico.

Não inventar regras de negócio adicionais.


==================================================
14. BANCO DE DADOS
==================================================

Documentar:

- SQLite será o banco inicial.
- O modelo deve evitar dependência desnecessária de recursos exclusivos
  do SQLite.
- O modelo deve permitir futura utilização com PostgreSQL e MySQL.
- Não criar código de acesso ao banco nesta etapa.

Não escolher ORM nesta etapa.


==================================================
15. DOCUMENTO 03
==================================================

Atualizar:

docs/03-MODELO-DE-DADOS.md

Documentar:

- entidades/tabelas conceituais;
- campos;
- tipos conceituais;
- chaves;
- relacionamentos;
- histórico;
- integridade;
- observações importantes.

IMPORTANTE:

Não transformar automaticamente cada conceito em tabela se isso não for
necessário.

O documento deve explicar a decisão.


==================================================
16. DOCUMENTO 01
==================================================

Atualizar:

docs/01-REQUISITOS.md

Somente se necessário para registrar decisões de modelo que tenham
impacto direto nos requisitos.

Não duplicar desnecessariamente todo o modelo de dados.


==================================================
17. DOCUMENTO 08
==================================================

Atualizar:

docs/08-CHECKPOINTS.md

Adicionar:

- [X] Prompt 007 executado.
- [X] Modelo conceitual definido.
- [X] Relacionamentos documentados.
- [X] Histórico de números definido.
- [X] Histórico de utilização definido.
- [X] Substituição de SIMCARD suportada.
- [X] Troca de números suportada.
- [X] Compatibilidade futura com PostgreSQL/MySQL considerada.
- [X] Nenhum código criado.


==================================================
18. NÃO AVANÇAR
==================================================

Não iniciar:

- arquitetura detalhada;
- criação de projetos C#;
- WPF;
- banco;
- migrations;
- entidades;
- repositórios;
- telas;
- Google Stitch;
- instalação;
- autenticação;
- código.

Essas etapas serão executadas posteriormente.


==================================================
19. REGISTRAR O PROMPT
==================================================

Criar:

docs/prompts/007-modelo-de-dados.md

Preservar integralmente este prompt.

Adicionar:

# Prompt 007 — Modelo de Dados

## Identificação

- [X] Prompt 007
- [X] Data

## Objetivo

- [X] Definir o modelo de dados.

## Prompt completo

Inserir o conteúdo integral deste prompt.

## Resultado esperado

- [ ] Preencher após execução.

## Resultado obtido

- [ ] Preencher após execução.

## Problemas encontrados

- [ ] Nenhum registrado ainda.

## Correções necessárias

- [ ] Nenhuma registrada ainda.


==================================================
20. GIT
==================================================

Após concluir:

1. Execute git status.
2. Revise todas as alterações.
3. Confirme que somente documentação foi alterada.
4. Confirme que nenhum código foi criado.
5. Faça commit.
6. Faça push para o GitHub.
7. Informe o hash completo.
8. Informe a branch.
9. Informe o resultado do push.
10. Informe os arquivos alterados.

Não faça commit de arquivos não relacionados.


==================================================
21. RELATÓRIO FINAL OBRIGATÓRIO
==================================================

Ao concluir a execução, gere um relatório completo e objetivo.

Este relatório será copiado integralmente pelo usuário e enviado
de volta para análise na próxima etapa.

Informe obrigatoriamente:

1. Status da execução.
2. O que foi realizado.
3. Arquivos criados.
4. Arquivos alterados.
5. Arquivos excluídos, se houver.
6. Modelo de dados definido.
7. Entidades/tabelas conceituais definidas.
8. Relacionamentos definidos.
9. Regras de histórico definidas.
10. Pendências restantes.
11. Problemas encontrados.
12. Correções realizadas.
13. Validações executadas.
14. Confirmação de que nenhum código foi criado.
15. Confirmação de que nenhuma decisão não autorizada foi inventada.
16. Commit realizado.
17. Hash completo do commit.
18. Branch.
19. Resultado do push para o GitHub.

O relatório deve ser apresentado SOMENTE após concluir todo o trabalho.

Não omita problemas ou divergências encontrados.

NÃO avance para a próxima etapa.