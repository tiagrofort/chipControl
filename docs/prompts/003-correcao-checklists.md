# Prompt 003 — Correção da classificação dos checklists

* [x] Identificação
* [x] Data
* [x] Objetivo
* [x] Prompt completo
* [x] Resultado esperado
* [x] Resultado obtido
* [x] Problemas encontrados
* [x] Correções realizadas

## Identificação

* [x] Número: 003
* [x] Título: Correção da classificação dos checklists

## Data

* [x] 2026-08-27

## Objetivo

Corrigir a documentação criada pelo Prompt 002, pois vários itens foram marcados como `[X]` apenas porque foram incluídos como instruções no Prompt 002, embora alguns ainda não tivessem sido formalmente aprovados como requisitos finais.

## Prompt completo

> PROMPT 003 — CORREÇÃO DA CLASSIFICAÇÃO DOS CHECKLISTS
>
> Estamos corrigindo a documentação criada pelo Prompt 002.
>
> IMPORTANTE:
>
> O Prompt 002 já foi executado e já foi enviado ao GitHub.
>
> NÃO devemos apagar o Prompt 002.
> NÃO devemos apagar o commit anterior.
> NÃO devemos apagar histórico.
> NÃO devemos simplesmente substituir documentos inteiros por versões novas.
>
> A documentação do projeto deve preservar sua evolução.
>
> Foi identificado um problema no Prompt 002: vários itens foram marcados como [X] apenas porque foram incluídos como instruções naquele prompt, embora alguns ainda não tenham sido formalmente aprovados como requisitos finais.
>
> Precisamos corrigir essa classificação.
>
> ==================================================
> 1. REGRA DEFINITIVA DOS CHECKLISTS
> ==================================================
>
> A partir deste momento, adote esta regra em toda a documentação:
>
> [X] significa que o item foi formalmente definido/aprovado para o projeto.
>
> [ ] significa que o item ainda está pendente de definição, aprovação ou execução.
>
> O simples fato de uma instrução ter sido escrita em um prompt NÃO significa que o requisito esteja aprovado.
>
> NUNCA apagar um item já concluído.
>
> Quando houver necessidade de corrigir uma decisão anterior, preservar o registro anterior e adicionar uma anotação de correção/histórico.
>
> ==================================================
> 2. NÃO ALTERAR O PROMPT 002
> ==================================================
>
> O arquivo:
>
> docs/prompts/002-objetivo-e-escopo.md
>
> deve permanecer preservado.
>
> NÃO apagar seu conteúdo original.
>
> NÃO reescrever o prompt original.
>
> Se o arquivo possuir campos de resultado, eles podem ser complementados para registrar que houve uma correção posterior.
>
> Adicionar, se ainda não existir, uma seção de histórico/correção informando:
>
> - O Prompt 002 foi executado.
> - Posteriormente foi identificado que alguns itens foram classificados como [X] prematuramente.
> - A classificação será corrigida pelo Prompt 003.
> - O conteúdo original do Prompt 002 permanece preservado para rastreabilidade.
>
> ==================================================
> 3. CORRIGIR A DOCUMENTAÇÃO SEM APAGAR HISTÓRICO
> ==================================================
>
> Analise:
>
> docs/01-REQUISITOS.md
> docs/02-REGRAS-DE-NEGOCIO.md
> docs/03-MODELO-DE-DADOS.md
> docs/04-ARQUITETURA.md
> docs/05-INTERFACE.md
> docs/06-RELATORIOS.md
> docs/07-PLANO-DE-DESENVOLVIMENTO.md
> docs/08-CHECKPOINTS.md
>
> Não altere conteúdo que não esteja relacionado a esta correção.
>
> Não invente requisitos.
>
> Não acrescente decisões que não tenham sido definidas.
>
> ==================================================
> 4. CLASSIFICAÇÃO DAS DECISÕES JÁ CONFIRMADAS
> ==================================================
>
> As seguintes decisões já foram efetivamente definidas no projeto e podem permanecer como [X] quando estiverem documentadas nos arquivos correspondentes:
>
> - Aplicação desktop Windows.
> - WPF como tecnologia da interface.
> - SQLite como banco inicial.
> - Arquitetura preparada para PostgreSQL.
> - Arquitetura preparada para MySQL.
> - Configuração do banco realizada por executável separado.
> - Sistema principal lendo a configuração do banco antes de inicializar o acesso ao banco.
> - Login por nome de usuário e senha.
> - E-mail não utilizado como nome de usuário.
> - E-mail disponível para recuperação de senha.
> - Usuários do sistema são diferentes dos funcionários que utilizam os chips.
> - Cadastros independentes para usuários, funcionários, operadoras e SIMCARDs.
> - Aparelho pode pertencer à empresa ou ao funcionário.
> - Menu lateral para navegação.
> - Botões principais de cada tela na parte superior.
> - Grid para listagem.
> - Inclusão/edição através de modal.
> - Cadastro rápido de registros relacionados sem cancelar o formulário atual.
> - Retorno ao formulário original após o cadastro relacionado, preservando os dados já preenchidos.
> - Pesquisas devem considerar todos os campos relevantes do respectivo cadastro.
> - O sistema deve preservar histórico.
> - Um chip fisicamente no estoque pode continuar associado a uma linha em uso.
> - Chip entregue e linha em uso são conceitos diferentes.
> - SIMCARD danificado, perdido ou não devolvido não deve ter seu histórico apagado.
>
> IMPORTANTE:
>
> Esses itens só devem ser marcados como [X] quando realmente estiverem documentados nos arquivos correspondentes.
>
> Não crie novos requisitos a partir desses itens além do que está explicitamente descrito.
>
> ==================================================
> 5. ITENS QUE NÃO DEVEM SER CONSIDERADOS DEFINIDOS AUTOMATICAMENTE
> ==================================================
>
> Os seguintes assuntos ainda precisam ser especificados posteriormente e NÃO devem ser considerados concluídos apenas porque apareceram no Prompt 002:
>
> - Lista completa de campos dos cadastros.
> - Estados/status definitivos.
> - Modelo de dados.
> - Relacionamentos detalhados.
> - Regras detalhadas de movimentação.
> - Lista definitiva de relatórios.
> - Permissões detalhadas.
> - Fluxo definitivo de recuperação de senha.
> - Auditoria detalhada.
> - Backup e restauração.
> - Telas individuais.
> - Identidade visual.
> - Componentes visuais.
> - Estratégia de migrations.
> - Estratégia de instalação/publicação.
> - Regras detalhadas de troca de número.
> - Regras detalhadas de substituição de SIMCARD.
> - Regras detalhadas para WhatsApp e WhatsApp Web.
>
> Esses assuntos devem continuar [ ] até serem formalmente definidos em etapas futuras.
>
> ==================================================
> 6. CHECKPOINT
> ==================================================
>
> Atualize:
>
> docs/08-CHECKPOINTS.md
>
> SEM apagar o checkpoint anterior.
>
> Adicione um novo registro:
>
> - [X] Prompt 003 executado.
> - [X] Identificado problema de classificação prematura dos checklists.
> - [X] Regra de significado do [X] formalizada.
> - [X] Documentação corrigida sem apagar o histórico do Prompt 002.
>
> Registre também uma observação explicando que o Prompt 002 permanece preservado por motivos de rastreabilidade.
>
> ==================================================
> 7. REGISTRAR ESTE PROMPT
> ==================================================
>
> Criar:
>
> docs/prompts/003-correcao-checklists.md
>
> O arquivo deve preservar integralmente este prompt.
>
> Registrar também:
>
> - Identificação.
> - Data.
> - Objetivo.
> - Resultado esperado.
> - Resultado obtido.
> - Problemas encontrados.
> - Correções realizadas.
>
> O Prompt 003 também deve seguir a regra de documentação permanente.
>
> ==================================================
> 8. GIT
> ==================================================
>
> Após concluir:
>
> 1. Execute git status.
> 2. Revise as alterações.
> 3. Verifique que o Prompt 002 continua preservado.
> 4. Verifique que nenhum histórico anterior foi apagado.
> 5. Faça um commit específico para esta correção.
> 6. Faça push para o GitHub.
> 7. Informe:
>    - hash do commit;
>    - branch;
>    - resultado do push;
>    - arquivos alterados.
>
> Não faça commit de alterações não relacionadas a esta correção.
>
> ==================================================
> 9. IMPORTANTE
> ==================================================
>
> NÃO desenvolver código.
>
> NÃO criar entidades.
>
> NÃO criar banco.
>
> NÃO criar telas.
>
> NÃO instalar dependências.
>
> Esta etapa é exclusivamente documental.
>
> Ao final, mostre um resumo do que estava [X] e foi mantido, do que foi corrigido para [ ], e das decisões que permanecem pendentes.

## Resultado esperado

* [x] Formalizada a regra de significado de `[X]` (item formalmente definido/aprovado) e `[ ]` (item pendente de definição, aprovação ou execução).
* [x] Documentação corrigida sem apagar o histórico do Prompt 002.
* [x] Arquivo `docs/prompts/002-objetivo-e-escopo.md` preservado, com anotação de correção adicionada.
* [x] Novo checkpoint registrado em `docs/08-CHECKPOINTS.md` sem apagar o registro anterior.
* [x] Arquivo `docs/prompts/003-correcao-checklists.md` criado preservando integralmente este prompt.
* [x] Commit específico para esta correção e push para o GitHub.

## Resultado obtido

* [x] Regra de significado de `[X]`/`[ ]` formalizada e aplicada na verificação de todos os documentos da pasta `docs/`.
* [x] `docs/prompts/002-objetivo-e-escopo.md` — anotação de correção/histórico adicionada; conteúdo original preservado.
* [x] `docs/01-REQUISITOS.md` — entrada adicionada ao histórico de alterações registrando a execução do Prompt 003.
* [x] `docs/08-CHECKPOINTS.md` — checkpoint do Prompt 003 registrado; histórico anterior preservado; observação sobre a preservação do Prompt 002 adicionada.
* [x] `docs/prompts/003-correcao-checklists.md` — criado com este prompt integral e com todos os campos de registro.
* [x] `docs/02-REGRAS-DE-NEGOCIO.md`, `docs/03-MODELO-DE-DADOS.md`, `docs/04-ARQUITETURA.md`, `docs/05-INTERFACE.md`, `docs/06-RELATORIOS.md` e `docs/07-PLANO-DE-DESENVOLVIMENTO.md` — classificação verificada; todos os itens permaneceram corretamente como `[ ]`, sem necessidade de correção.
* [x] Commit específico realizado.
* [x] Push realizado para o GitHub.

## Problemas encontrados

* [ ] (nenhum até o momento)

## Correções realizadas

* [x] Formalizada a regra de significado de `[X]` (definido/aprovado) e `[ ]` (pendente) em toda a documentação.
* [x] Adicionada anotação de correção/histórico no `docs/prompts/002-objetivo-e-escopo.md`, informando que o Prompt 002 foi executado, que alguns itens foram classificados como `[X]` prematuramente, que a correção foi realizada pelo Prompt 003 e que o conteúdo original permanece preservado para rastreabilidade.
* [x] Adicionada entrada no histórico de alterações do `docs/01-REQUISITOS.md` registrando a execução do Prompt 003.
* [x] Atualizado o `docs/08-CHECKPOINTS.md` com o checkpoint do Prompt 003 e a observação de que o Prompt 002 permanece preservado por motivos de rastreabilidade.
* [x] Criado o `docs/prompts/003-correcao-checklists.md` preservando integralmente este prompt.
* [x] Verificada a classificação de todos os itens nos oito documentos da pasta `docs/`: os itens da lista de decisões já confirmadas permanecem como `[X]` (pois estão documentados em `docs/01-REQUISITOS.md`); os itens da lista de assuntos ainda não definidos permanecem como `[ ]`. Nenhum histórico anterior foi apagado.