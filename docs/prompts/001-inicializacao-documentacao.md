# Prompt 001 — Inicialização da estrutura de documentação

* [ ] Identificação do prompt
* [ ] Data
* [ ] Objetivo
* [ ] Prompt completo
* [ ] Resultado esperado
* [ ] Resultado obtido
* [ ] Problemas encontrados
* [ ] Correções necessárias

## Identificação do prompt

* [x] Número: 001
* [x] Título: Inicialização da estrutura de documentação do projeto "Controle de Chips"

## Data

* [x] 2026-08-27

## Objetivo

* [x] Preparar a estrutura de documentação do projeto na pasta `docs/`, sem implementar funcionalidades, telas, entidades, banco de dados ou instalar dependências.

## Prompt completo

> Você está iniciando o projeto Controle de Chips.
>
> ATENÇÃO: neste momento NÃO desenvolva funcionalidades, NÃO crie telas, NÃO crie entidades, NÃO crie banco de dados e NÃO instale dependências além das que já existam no projeto.
>
> Seu objetivo nesta etapa é SOMENTE preparar a estrutura de documentação do projeto.
>
> ### 1. Criar a pasta de documentação
>
> Na raiz do projeto, crie:
>
> docs/
>
> Dentro dela, crie:
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
> Crie também:
>
> docs/prompts/
>
> ### 2. Regra obrigatória da documentação
>
> Todos os documentos Markdown do projeto devem utilizar checklist como mecanismo de acompanhamento.
>
> Use:
>
> * [ ] para itens pendentes
> * [x] para itens concluídos
>
> NUNCA apague um item que tenha sido concluído.
>
> Quando uma decisão ou requisito for alterado posteriormente, preserve o registro anterior e adicione uma nova entrada registrando a alteração.
>
> A documentação deve funcionar como um histórico permanente do projeto.
>
> ### 3. Regra para os prompts
>
> Todos os prompts recebidos para desenvolvimento deverão ser preservados em:
>
> docs/prompts/
>
> Utilize nomes sequenciais, por exemplo:
>
> docs/prompts/001-inicializacao-documentacao.md
> docs/prompts/002-requisitos.md
> docs/prompts/003-modelo-dados
>
> O arquivo de cada prompt deve conter:
>
> * [ ] Identificação do prompt
> * [ ] Data
> * [ ] Objetivo
> * [ ] Prompt completo
> * [ ] Resultado esperado
> * [ ] Resultado obtido
> * [ ] Problemas encontrados
> * [ ] Correções necessárias
>
> Não altere nem apague o conteúdo original de um prompt já registrado.
>
> ### 4. Conteúdo inicial dos documentos
>
> Não invente requisitos neste momento.
>
> Crie apenas a estrutura inicial de cada documento usando checklists, deixando os itens que ainda precisam ser definidos como pendentes.
>
> ### 5. Não tomar decisões por conta própria
>
> Não invente regras de negócio.
> Não escolha entidades adicionais.
> Não defina campos de banco.
> Não defina telas.
> Não defina relatórios que ainda não foram aprovados.
> Não implemente nada.
>
> Se houver alguma informação já existente no projeto, preserve-a.
>
> ### 6. Resultado final
>
> Ao terminar:
>
> 1. Mostre a árvore de arquivos criada.
> 2. Informe quais arquivos foram criados.
> 3. Informe se algum arquivo existente foi alterado.
> 4. Não faça nenhuma alteração fora da pasta docs/, exceto se for estritamente necessário para registrar o prompt.
> 5. Não inicie o desenvolvimento do sistema.

## Resultado esperado

* [x] Estrutura de pastas `docs/` e `docs/prompts/` criada
* [x] 8 arquivos de documentação Markdown criados com estrutura de checklists (itens pendentes)
* [x] Arquivo de prompt preservado com seus 8 itens de checklist

## Resultado obtido

* [x] 01-REQUISITOS.md — estrutura de checklists criada
* [x] 02-REGRAS-DE-NEGOCIO.md — estrutura de checklists criada
* [x] 03-MODELO-DE-DADOS.md — estrutura de checklists criada
* [x] 04-ARQUITETURA.md — estrutura de checklists criada
* [x] 05-INTERFACE.md — estrutura de checklists criada
* [x] 06-RELATORIOS.md — estrutura de checklists criada
* [x] 07-PLANO-DE-DESENVOLVIMENTO.md — estrutura de checklists criada
* [x] 08-CHECKPOINTS.md — estrutura de checklists criada
* [x] docs/prompts/001-inicializacao-documentacao.md — este arquivo

## Problemas encontrados

* [ ] (nenhum até o momento)

## Correções necessárias

* [ ] (nenhuma até o momento)
