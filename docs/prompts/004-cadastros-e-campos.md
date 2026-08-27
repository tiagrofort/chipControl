# Prompt 004 — Definição dos Cadastros e Campos

## Identificação

- [x] Prompt 004
- [x] Data: 2026-08-27

## Objetivo

- [x] Definir a estrutura inicial dos cadastros.

## Prompt completo

> PROMPT 004 — DEFINIÇÃO DOS CADASTROS E CAMPOS
>
> Vamos continuar a especificação do projeto Controle de Chips.
>
> ESTADO DO PROJETO:
> O projeto está sendo especificado antes do desenvolvimento.
>
> REGRA FUNDAMENTAL:
> O escopo do projeto será considerado CONGELADO após cada etapa ser aprovada.
>
> Durante o desenvolvimento, novas ideias, melhorias ou funcionalidades NÃO devem alterar o escopo aprovado.
>
> Qualquer ideia nova deverá ser registrada em:
>
> docs/09-BACKLOG-FUTURO.md
>
> e NÃO deverá ser implementada durante o projeto atual.
>
> Uma funcionalidade registrada no backlog somente poderá ser executada futuramente mediante decisão explícita.
>
> ==================================================
> 1. DOCUMENTAÇÃO
> ==================================================
>
> Preserve todos os documentos existentes.
>
> NÃO apague itens já concluídos.
>
> NÃO reescreva documentos inteiros sem necessidade.
>
> Mantenha o histórico.
>
> Use:
>
> - [ ] para item pendente.
> - [X] para item formalmente definido/aprovado.
>
> Não marque como [X] uma decisão que não tenha sido definida neste prompt ou anteriormente documentada como decisão aprovada.
>
> ==================================================
> 2. BACKLOG FUTURO
> ==================================================
>
> Crie:
>
> docs/09-BACKLOG-FUTURO.md
>
> Esse arquivo será permanente.
>
> Ele deverá conter inicialmente:
>
> # Backlog Futuro
>
> ## Novas ideias e funcionalidades
>
> - [ ] Nenhuma ideia registrada até o momento.
>
> REGRAS:
>
> - Nunca apagar itens deste arquivo.
> - Nunca executar automaticamente itens deste arquivo.
> - Novas ideias deverão ser acrescentadas como novos itens.
> - Quando uma ideia for futuramente aprovada para desenvolvimento, manter o item original e registrar sua aprovação/execução no histórico.
> - O backlog não faz parte do escopo atual.
>
> ==================================================
> 3. OBJETIVO DESTE PROMPT
> ==================================================
>
> Neste prompt vamos definir SOMENTE os cadastros e seus campos.
>
> Não criar banco de dados.
>
> Não criar entidades no código.
>
> Não criar telas.
>
> Não instalar dependências.
>
> Não definir relacionamentos detalhados.
>
> Não definir migrations.
>
> Não implementar funcionalidades.
>
> ==================================================
> 4. CADASTROS A SEREM ESPECIFICADOS
> ==================================================
>
> Os cadastros iniciais já definidos são:
>
> 1. Usuários do sistema
> 2. Funcionários
> 3. Operadoras
> 4. SIMCARDs
> 5. Aparelhos
>
> Eles são cadastros independentes.
>
> Não adicionar novos cadastros principais neste prompt.
>
> ==================================================
> 5. USUÁRIOS DO SISTEMA
> ==================================================
>
> Documentar como requisitos pendentes os campos que ainda precisam ser definidos.
>
> Registrar que:
>
> - [X] Usuário do sistema é independente de funcionário.
> - [X] O login utiliza nome de usuário.
> - [X] E-mail não é utilizado como nome de usuário.
> - [X] E-mail poderá ser utilizado para recuperação de senha.
>
> Criar uma seção de campos e deixar pendente a definição dos campos que ainda não foram decididos.
>
> Não inventar campos.
>
> ==================================================
> 6. FUNCIONÁRIOS
> ==================================================
>
> Documentar o cadastro de funcionários como entidade independente dos usuários do sistema.
>
> Criar uma seção para os campos do funcionário.
>
> Não inventar campos que não tenham sido definidos.
>
> Deixar pendente a definição dos campos que ainda precisam ser decididos.
>
> ==================================================
> 7. OPERADORAS
> ==================================================
>
> Documentar o cadastro independente de operadoras de telefonia.
>
> Criar uma seção para os campos.
>
> Não inventar campos.
>
> Deixar pendente a definição dos campos ainda não decididos.
>
> ==================================================
> 8. SIMCARDs
> ==================================================
>
> Documentar o cadastro independente dos SIMCARDs.
>
> Já foi definido que o sistema deverá identificar fisicamente cada chip através de uma identificação interna, como:
>
> Chip 01
> Chip 02
> Chip 03
>
> Essa identificação corresponde à numeração escrita fisicamente no chip.
>
> Também foi definido que o SIMCARD/ICCID é uma informação fundamental do cadastro.
>
> Registrar:
>
> - [X] O SIMCARD possui identificação interna do chip físico.
> - [X] O SIMCARD possui ICCID/SIMCARD.
> - [X] O SIMCARD possui uma operadora relacionada.
> - [X] O SIMCARD deve manter histórico.
> - [X] Um SIMCARD não deve ser excluído simplesmente porque deixou de ser utilizado.
>
> Não definir neste prompt todos os demais campos ou status do SIMCARD.
>
> Deixar esses itens pendentes para definição posterior.
>
> ==================================================
> 9. APARELHOS
> ==================================================
>
> Documentar o cadastro independente de aparelhos.
>
> Já foi definido:
>
> - [X] Um aparelho pode pertencer à empresa.
> - [X] Um aparelho pode pertencer ao funcionário.
> - [X] O proprietário do aparelho é independente do usuário da linha.
> - [X] Um aparelho pode estar relacionado à utilização de uma linha.
>
> Não inventar todos os campos do aparelho.
>
> Deixar pendentes os campos que ainda precisam ser definidos.
>
> ==================================================
> 10. PESQUISA
> ==================================================
>
> Registrar como regra geral:
>
> - [X] A pesquisa de cada cadastro deve pesquisar todos os campos relevantes daquele cadastro.
> - [X] A pesquisa não deve ficar limitada a uma única coluna.
>
> Não definir implementação técnica.
>
> ==================================================
> 11. CADASTRO RÁPIDO
> ==================================================
>
> Preservar a decisão já definida:
>
> - [X] Quando um formulário depender de um registro relacionado que ainda não exista, deverá existir uma forma de cadastrar esse registro sem cancelar o formulário atual.
> - [X] Após salvar o novo registro, o formulário original deve ser retomado.
> - [X] Os dados já preenchidos no formulário original devem ser preservados.
>
> Não implementar.
>
> ==================================================
> 12. REGISTRAR O PROMPT
> ==================================================
>
> Criar:
>
> docs/prompts/004-cadastros-e-campos.md
>
> O arquivo deverá conter integralmente este prompt.
>
> Estrutura:
>
> # Prompt 004 — Definição dos Cadastros e Campos
>
> ## Identificação
>
> - [X] Prompt 004
> - [X] Data
>
> ## Objetivo
>
> - [X] Definir a estrutura inicial dos cadastros.
>
> ## Prompt completo
>
> Colocar aqui o conteúdo integral deste prompt.
>
> ## Resultado esperado
>
> - [ ] Definido após a execução.
>
> ## Resultado obtido
>
> - [ ] Preencher após a execução.
>
> ## Problemas encontrados
>
> - [ ] Nenhum registrado ainda.
>
> ## Correções necessárias
>
> - [ ] Nenhuma registrada ainda.
>
> Não apagar posteriormente o conteúdo original do prompt.
>
> ==================================================
> 13. CHECKPOINT
> ==================================================
>
> Atualizar:
>
> docs/08-CHECKPOINTS.md
>
> Adicionar um novo checkpoint do Prompt 004.
>
> Não apagar checkpoints anteriores.
>
> O checkpoint deve registrar que esta etapa trata da definição inicial dos cadastros e campos.
>
> Não marcar como concluídos campos que ainda não foram definidos.
>
> ==================================================
> 14. GIT
> ==================================================
>
> Após concluir:
>
> 1. Execute git status.
> 2. Revise todas as alterações.
> 3. Confirme que nenhuma funcionalidade foi implementada.
> 4. Confirme que nenhum documento histórico foi apagado.
> 5. Faça um commit.
> 6. Faça push para o GitHub.
> 7. Informe o hash completo do commit.
> 8. Informe a branch.
> 9. Informe o resultado do push.
> 10. Informe os arquivos alterados.
>
> Não inclua no commit arquivos ou alterações que não pertençam a esta etapa.
>
> ==================================================
> 15. RESULTADO FINAL
> ==================================================
>
> Ao terminar, informe:
>
> - documentos criados;
> - documentos alterados;
> - itens efetivamente definidos;
> - itens que continuam pendentes;
> - confirmação de que nenhum código foi desenvolvido;
> - confirmação de que o backlog futuro foi criado;
> - hash do commit;
> - resultado do push.
>
> NÃO avance para modelo de dados, arquitetura técnica, telas, Stitch ou código.
>
> A próxima etapa será definida somente depois que o resultado deste prompt for revisado.

## Resultado esperado

- [x] Criado o arquivo permanente `docs/09-BACKLOG-FUTURO.md` com as regras do backlog e o item inicial "Nenhuma ideia registrada até o momento".
- [x] Estrutura inicial dos cinco cadastros documentada em `docs/01-REQUISITOS.md` (Usuários do sistema, Funcionários, Operadoras, SIMCARDs, Aparelhos), sem adicionar novos cadastros.
- [x] Decisões já confirmadas registradas como `[x]` dentro da seção de cada cadastro.
- [x] Listas de campos de cada cadastro e os status do SIMCARD registrados como pendentes `[ ]`, sem inventar campos.
- [x] Regras gerais de pesquisa e cadastro rápido preservadas como `[x]` (já definidas no Prompt 002), sem definição de implementação técnica.
- [x] Checkpoint do Prompt 004 registrado em `docs/08-CHECKPOINTS.md`, sem apagar checkpoints anteriores.
- [x] Este arquivo criado com o conteúdo integral do prompt.
- [x] Commit e push realizados contendo apenas os arquivos desta etapa.

## Resultado obtido

- [x] `docs/09-BACKLOG-FUTURO.md` — criado, permanente, com as regras registradas e item inicial "Nenhuma ideia registrada até o momento".
- [x] `docs/01-REQUISITOS.md` — adicionadas subseções por cadastro dentro de "Cadastros principais", com decisões `[x]` e campos pendentes `[ ]`; histórico de alterações atualizado com a execução do Prompt 004.
- [x] Usuários do sistema — decisões registradas: independente de funcionário; login por nome de usuário; e-mail não é utilizado como nome de usuário; e-mail poderá ser utilizado para recuperação de senha. Lista de campos pendente.
- [x] Funcionários — cadastro independente dos usuários do sistema registrado. Lista de campos pendente.
- [x] Operadoras — cadastro independente de operadoras de telefonia registrado. Lista de campos pendente.
- [x] SIMCARDs — decisões registradas: identificação interna do chip físico (correspondente à numeração escrita fisicamente no chip, ex.: Chip 01, Chip 02, Chip 03); ICCID/SIMCARD como informação fundamental; operadora relacionada; histórico; não exclusão por simplesmente ter deixado de ser utilizado. Demais campos e status permanecem pendentes.
- [x] Aparelhos — decisões registradas: pode pertencer à empresa; pode pertencer ao funcionário; proprietário independente do usuário da linha; pode estar relacionado à utilização de uma linha. Lista de campos pendente.
- [x] Pesquisa — regras gerais já registradas como `[x]` na seção Pesquisa do `docs/01-REQUISITOS.md` (todos os campos relevantes; não limitada a uma única coluna); implementação técnica permanece não definida.
- [x] Cadastro rápido — decisões já registradas como `[x]` na seção Interface do `docs/01-REQUISITOS.md`; nada implementado.
- [x] `docs/08-CHECKPOINTS.md` — checkpoint do Prompt 004 adicionado ao histórico e à lista de checkpoints concluídos; registros anteriores preservados; nenhum campo não definido foi marcado como concluído.
- [x] `docs/prompts/004-cadastros-e-campos.md` — criado com o conteúdo integral deste prompt.
- [x] Commit específico realizado e push efetuado para `origin/main`.

## Problemas encontrados

- [ ] (nenhum registrado até o momento)

## Correções necessárias

- [ ] (nenhuma registrada até o momento)