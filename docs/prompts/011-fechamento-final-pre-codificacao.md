# Prompt 011 — Fechamento Final Pré-Codificação

## Identificação

- [x] Número: 011
- [x] Título: Fechamento final pré-codificação

## Data

- [x] 2026-08-28

## Objetivo

- [x] Última etapa documental antes do início da codificação. Resolver os últimos pontos estruturais e colocar TODO o conteúdo atual do projeto sob controle do Git, sem criar código, solução C#, projeto WPF, entidades, migrations ou dependências.

## Prompt completo

O conteúdo abaixo preserva integralmente o prompt recebido.

> PROMPT 011 — FECHAMENTO FINAL PRÉ-CODIFICAÇÃO
>
> OBJETIVO
>
> Esta é a ÚLTIMA etapa documental antes do início da codificação.
>
> Ao finalizar este prompt, a documentação e os artefatos existentes devem
> estar prontos para o início do desenvolvimento.
>
> Depois deste prompt, NÃO criar outro prompt de documentação geral.
>
> O próximo prompt será o início da implementação WPF.
>
> ==================================================
> 1. REGRA ABSOLUTA
> ==================================================
>
> NÃO criar código de aplicação.
>
> NÃO criar solução C#.
>
> NÃO criar projeto WPF.
>
> NÃO criar entidades C#.
>
> NÃO criar migrations.
>
> NÃO instalar dependências.
>
> NÃO iniciar implementação.
>
> Esta etapa serve SOMENTE para resolver os últimos pontos estruturais e
> colocar TODO o conteúdo atual do projeto sob controle do Git.
>
> A especificação continuará congelada após esta etapa.
>
>
> ==================================================
> 2. LEIA A DOCUMENTAÇÃO
> ==================================================
>
> Leia todos os documentos existentes em:
>
> docs/
>
> e todos os prompts existentes em:
>
> docs/prompts/
>
> Leia também o conteúdo de:
>
> docs/ux_design/
>
> Não apague nenhum documento anterior.

> ==================================================
> 3. STITCH / UX DESIGN
> ==================================================
>
> TODO o conteúdo existente em:
>
> docs/ux_design/
>
> deve ser versionado no Git.
>
> O usuário confirmou explicitamente:
>
> "NÃO temos informações confidenciais."
>
> Portanto, NÃO excluir arquivos do UX Design por tamanho, formato ou
> suposta inutilidade.
>
> Não ignorar arquivos.
>
> Não adicionar .gitignore para impedir o versionamento desses arquivos.
>
> Se existir ZIP do Stitch, também deve ser versionado.
>
> Se existirem imagens, arquivos exportados, JSON, HTML, assets ou outros
> arquivos relacionados ao UX Design dentro de docs/ux_design/, todos
> devem ser preservados e adicionados ao Git.
>
> O conteúdo do projeto deve permanecer disponível no repositório para
> referência durante a implementação.
>
>
> ==================================================
> 4. ARQUIVO RESIDUAL
> ==================================================
>
> Localizar:
>
> docs/prompts/002_objetivo_e_escopo.ps1
>
> Se o arquivo realmente for o artefato residual identificado anteriormente
> e não fizer parte da documentação funcional do projeto:
>
> REMOVER o arquivo.
>
> Registrar a remoção no relatório.
>
> Não remover nenhum outro arquivo.
>
>
> ==================================================
> 5. MODELO DE DADOS — REGRA FINAL
> ==================================================
>
> Revisar a relação entre:
>
> SIMCards
> HistoricoNumeros
> HistoricoUtilizacao
> HistoricoSubstituicao
>
> Definir/documentar claramente:
>
> SIMCards representa o cadastro atual do SIMCARD físico.
>
> HistoricoNumeros representa a evolução dos números associados ao SIMCARD.
>
> HistoricoUtilizacao representa os períodos de utilização da linha/SIMCARD,
> incluindo funcionário e/ou aparelho quando aplicável.
>
> HistoricoSubstituicao representa a substituição de um SIMCARD por outro.
>
> Não utilizar Event Sourcing completo.
>
> ==================================================
> 6. STATUS DO SIMCARD
> ==================================================
>
> Revisar a documentação para evitar a mistura conceitual entre:
>
> - situação física do SIMCARD;
> - utilização da linha.
>
> Manter os status já aprovados:
>
> - Em estoque
> - Em uso particular
> - WhatsApp
> - Danificado
> - Perdido
> - Não devolvido
> - Descartado
> - Inativo
>
> IMPORTANTE:
>
> NÃO remover ou renomear os status já aprovados nesta etapa sem necessidade.
>
> Documentar claramente que:
>
> - o SIMCARD físico pode estar fisicamente no estoque;
> - a linha pode continuar em utilização;
> - especialmente no cenário WhatsApp, o chip físico pode voltar ao estoque
>   enquanto a linha/WhatsApp continua vinculada à utilização.
>
> Se a documentação precisar separar conceitualmente "localização física"
> e "utilização da linha", faça isso sem criar uma nova funcionalidade.
>
> A solução deve continuar simples.
>
>
> ==================================================
> 7. TROCA DE NÚMEROS
> ==================================================
>
> Fechar a especificação do fluxo:
>
> IMPORTAÇÃO
> → LEITURA
> → RELACIONAMENTO
> → IDENTIFICAÇÃO DE DIVERGÊNCIAS
> → CONFERÊNCIA
> → CONFIRMAÇÃO
> → APLICAÇÃO
> → HISTÓRICO
>
> A lista recebida da operadora contém:
>
> - número antigo;
> - número do SIMCARD;
> - número novo.
>
> A implementação deve permitir relacionar os dados existentes.
>
> Deve identificar pelo menos:
>
> - SIMCARD encontrado;
> - SIMCARD não encontrado;
> - número antigo encontrado;
> - número antigo não encontrado;
> - número novo já existente;
> - inconsistências.
>
> NÃO alterar automaticamente registros com divergências sem confirmação.
>
> NÃO apagar números antigos.
>
> NÃO perder histórico.
>
>
> ==================================================
> 8. FORMATO INICIAL DA IMPORTAÇÃO
> ==================================================
>
> Para a primeira versão, definir:
>
> Formato principal de importação:
>
> EXCEL (.xlsx)
>
> A implementação deverá trabalhar com a lista real utilizada pelo
> usuário.
>
> A primeira versão deve considerar a existência das três informações:
>
> - número antigo;
> - número do SIMCARD;
> - número novo.
>
> Não criar neste momento suporte a dezenas de formatos.
>
> CSV poderá ser adicionado futuramente e, se desejado, deve ser colocado
> no backlog em vez de entrar no escopo atual.

> ==================================================
> 9. SUBSTITUIÇÃO DE SIMCARD
> ==================================================
>
> Manter:
>
> SIMCARD ANTIGO
> → MOTIVO
> → NOVO SIMCARD
> → CONTINUIDADE DO NÚMERO
> → CONFIRMAÇÃO
> → HISTÓRICO
>
> O SIMCARD antigo nunca deve ser apagado.
>
> O novo SIMCARD pode ser cadastrado durante a operação caso ainda não
> exista.
>
> O formulário atual deve ser preservado ao abrir o cadastro rápido.
>
>
> ==================================================
> 10. BANCO DE DADOS
> ==================================================
>
> Manter:
>
> - SQLite como banco inicial;
> - PostgreSQL como provedor futuro;
> - MySQL como provedor futuro;
> - Entity Framework Core;
> - camada de persistência desacoplada da regra de negócio.
>
> Não alterar esta decisão.
>
> Não implementar banco nesta etapa.
>
>
> ==================================================
> 11. AUTENTICAÇÃO
> ==================================================
>
> Manter somente:
>
> - Administrador
> - Usuário
>
> Login:
>
> - nome de usuário;
> - senha.
>
> E-mail:
>
> - opcional;
> - utilizado para recuperação de senha.
>
> Senha:
>
> - nunca armazenada em texto puro;
> - utilizar hash seguro com salt.
>
> A escolha exata da biblioteca de hash pode ser feita durante a
> implementação, desde que respeite essa regra.
>
>
> ==================================================
> 12. ACESSO MASTER DE TESTES
> ==================================================
>
> Manter exatamente:
>
> Usuário:
> VAZIO
>
> Senha:
> @Ju145863
>
> Resultado:
>
> acesso como Administrador Master.
>
> Esse mecanismo é exclusivamente para:
>
> - desenvolvimento;
> - testes;
> - recuperação de acesso.
>
> NÃO deve funcionar em build de produção.
>
> A implementação poderá utilizar:
>
> #if DEBUG
>
> ou mecanismo equivalente de configuração/build.
>
> NÃO deixar a senha master disponível em produção.

> ==================================================
> 13. RELATÓRIOS
> ==================================================
>
> Manter os 9 relatórios definidos no Prompt 010:
>
> 1. SIMCARDs em Estoque
> 2. SIMCARDs em Uso
> 3. SIMCARDs por Status
> 4. Linhas sem Utilização Ativa
> 5. Utilização por Funcionário
> 6. Histórico de Números
> 7. Substituições Realizadas
> 8. SIMCARDs Danificados/Perdidos/Não Devolvidos
> 9. Aparelhos por Proprietário
>
> Não adicionar relatórios novos nesta etapa.
>
>
> ==================================================
> 14. INTERFACE / STITCH
> ==================================================
>
> O Google Stitch é a referência visual.
>
> Não recriar.
>
> Não redesenhar.
>
> Não alterar o design existente.
>
> A implementação será WPF.
>
> Manter:
>
> - menu lateral;
> - ações na parte superior;
> - grids;
> - modais;
> - pesquisa;
> - cadastro rápido.
>
> A ordem/quantidade das colunas das grids poderá ser ajustada durante
> a implementação.
>
> Isso não deve bloquear o início do código.
>
>
> ==================================================
> 15. BACKLOG
> ==================================================
>
> Não adicionar ideias novas ao escopo.
>
> Se durante a revisão aparecer alguma sugestão que não seja necessária
> para o funcionamento da primeira versão, registrar em:
>
> docs/09-BACKLOG-FUTURO.md
>
> Não implementar.
>
>
> ==================================================
> 16. VERIFICAÇÃO FINAL DA DOCUMENTAÇÃO
> ==================================================
>
> Faça uma revisão cruzada de:
>
> 01-REQUISITOS
> 02-REGRAS-DE-NEGOCIO
> 03-MODELO-DE-DADOS
> 04-ARQUITETURA
> 05-INTERFACE
> 06-RELATORIOS
> 07-PLANO-DE-DESENVOLVIMENTO
> 08-CHECKPOINTS
> 09-BACKLOG-FUTURO
>
> Corrija SOMENTE inconsistências que possam causar retrabalho estrutural
> durante a codificação.
>
> Não reescrever documentos inteiros.
>
> Não mudar decisões já aprovadas sem necessidade.
>
> Não criar novas funcionalidades.
>
>
> ==================================================
> 17. CONGELAMENTO FINAL
> ==================================================
>
> Após a revisão, registrar em:
>
> docs/08-CHECKPOINTS.md
>
> que:
>
> [X] Revisão estrutural final concluída.
> [X] UX Design versionado.
> [X] Modelo de histórico fechado.
> [X] Relação SIMCARD/número fechada.
> [X] Fluxo de troca de números fechado.
> [X] Fluxo de substituição fechado.
> [X] Autenticação fechada.
> [X] Acesso master de testes documentado.
> [X] Banco inicial/futuro definido.
> [X] Relatórios da primeira versão definidos.
> [X] Especificação pronta para implementação.
> [X] Projeto congelado para início da codificação.

> ==================================================
> 18. PROMPT 011
> ==================================================
>
> Criar:
>
> docs/prompts/011-fechamento-final-pre-codificacao.md
>
> Preservar o prompt integral.
>
> Registrar:
>
> - objetivo;
> - resultado esperado;
> - resultado obtido;
> - decisões finais;
> - problemas;
> - correções;
> - arquivos envolvidos.
>
>
> ==================================================
> 19. GIT — VERSIONAR TODO O PROJETO
> ==================================================
>
> O usuário confirmou que NÃO existem informações confidenciais.
>
> Portanto, nesta etapa:
>
> TODO o conteúdo relevante existente do projeto deve ser versionado.
>
> Incluindo:
>
> - docs/
> - docs/prompts/
> - docs/ux_design/
> - arquivos do Stitch;
> - imagens;
> - ZIPs;
> - arquivos de documentação;
> - demais artefatos relevantes já existentes.
>
> NÃO adicionar arquivos ao .gitignore simplesmente para evitar
> versioná-los.
>
> ANTES DO COMMIT:
>
> 1. git status
> 2. git diff
> 3. git diff --stat
> 4. verificar arquivos untracked
> 5. verificar especialmente docs/ux_design/
> 6. verificar se nenhum arquivo importante ficou de fora
> 7. verificar se nenhum código foi criado
> 8. verificar se nenhuma informação confidencial existe
> 9. revisar o conjunto final de alterações
>
> REMOVER SOMENTE o artefato residual explicitamente identificado:
>
> docs/prompts/002_objetivo_e_escopo.ps1
>
> Depois:
>
> git add .
>
> git commit -m "docs: finaliza especificacao e adiciona UX design"
>
> git push origin main
>
> O push deve ser realizado.
>
> Não deixar alterações importantes sem commit.
>
>
> ==================================================
> 20. NÃO ALTERAR COMMITS ANTERIORES
> ==================================================
>
> NÃO fazer rebase.
>
> NÃO fazer force push.
>
> NÃO alterar commits anteriores.
>
> Criar apenas um novo commit para esta etapa.
>
>
> ==================================================
> 21. RELATÓRIO FINAL OBRIGATÓRIO
> ==================================================
>
> O relatório deve ser apresentado DIRETAMENTE NA RESPOSTA FINAL.
>
> O Git NÃO substitui o relatório.
>
> O relatório deve ser escrito para que o usuário possa copiar e colar
> integralmente nesta conversa.
>
> Informar:
>
> 1. Status da execução.
> 2. Correções realizadas.
> 3. Modelo de histórico final.
> 4. Regra SIMCARD/número.
> 5. Regra de utilização/estoque.
> 6. Fluxo de troca de números.
> 7. Formato de importação.
> 8. Fluxo de substituição de SIMCARD.
> 9. Autenticação.
> 10. Acesso master.
> 11. Estratégia de banco.
> 12. Relatórios.
> 13. Situação do Stitch.
> 14. Arquivos adicionados ao Git.
> 15. Arquivos alterados.
> 16. Arquivos removidos.
> 17. Verificação de arquivos untracked.
> 18. Confirmação de que todo o conteúdo relevante foi versionado.
> 19. Problemas encontrados.
> 20. Correções realizadas.
> 21. Confirmação de que nenhum código foi criado.
> 22. Confirmação de que nenhuma funcionalidade nova foi adicionada.
> 23. Confirmação de que a especificação está congelada.
> 24. Commit realizado.
> 25. HASH COMPLETO do commit.
> 26. Branch.
> 27. Resultado do push.
>
> IMPORTANTE:
>
> O relatório precisa aparecer na resposta final.
>
> NÃO responder apenas "concluído".
>
> NÃO fornecer somente o hash.
>
> NÃO encerrar sem relatório.
>
> NÃO iniciar a codificação.
>
> NÃO executar o Prompt 012.
>
> Após apresentar o relatório, AGUARDAR INSTRUÇÕES.

## Resultado esperado

- [x] Restam apenas resoluções estruturais finais e versionamento completo no Git.
- [x] UX Design (docs/ux_design/) e ZIP do Stitch versionados.
- [x] Artefato residual `docs/prompts/002_objetivo_e_escopo.ps1` removido.
- [x] Modelo de histórico e relação SIMCARD/número fechados.
- [x] Fluxos de troca de números e de substituição fechados.
- [x] Formato inicial de importação definido (EXCEL .xlsx).
- [x] Especificação revisada e congelada para o início da codificação.
- [x] Commit e push realizados.

## Resultado obtido

- [x] Toda a documentação em `docs/`, `docs/prompts/` e `docs/ux_design/` lida e preservada.
- [x] `docs/ux_design/` (9 subpastas do Stitch, code.html + screen.png) e `docs/ux_design.zip` versionados no Git.
- [x] `docs/prompts/002_objetivo_e_escopo.ps1` identificado como artefato residual (conteúdo apenas `$content = 'Prompt 002 body placeholder'`) e REMOVIDO.
- [x] Relação entre SIMCards, HistoricoNumeros, HistoricoUtilizacao e HistoricoSubstituicao documentada em `docs/03-MODELO-DE-DADOS.md` (sem Event Sourcing completo).
- [x] Separação conceitual situação física do SIMCARD × utilização da linha documentada; os 8 status aprovados mantidos sem renomeação ou remoção.
- [x] Fluxo de troca de números detalhado em 8 etapas em `docs/02-REGRAS-DE-NEGOCIO.md`.
- [x] Formato inicial de importação definido: EXCEL (.xlsx).
- [x] Fluxo de substituição registrado; cadastro rápido preserva o formulário atual.
- [x] Congelamento final registrado em `docs/08-CHECKPOINTS.md` com os 12 checkpoints.
- [x] Backlog `docs/09-BACKLOG-FUTURO.md` atualizado com o suporte futuro a CSV; nenhuma ideia nova incorporada ao escopo.
- [x] Nenhum código criado. Nenhuma funcionalidade nova adicionada.
- [x] Commit `docs: finaliza especificacao e adiciona UX design` criado e push para `origin/main` realizado.

## Decisões finais

- [x] Modelo de histórico: append-only com tabelas de histórico separadas; NÃO é Event Sourcing completo; estado atual derivado dos registros mais recentes (data_fim = null).
- [x] SIMCards = cadastro atual do SIMCARD físico; HistoricoNumeros = evolução dos números; HistoricoUtilizacao = períodos de utilização (funcionário e/ou aparelho quando aplicável); HistoricoSubstituicao = substituição de um SIMCARD por outro.
- [x] Status do SIMCARD representam a situação física; utilização da linha é registrada em HistoricoUtilizacao. Nenhum status removido ou renomeado.
- [x] Fluxo de troca de números: IMPORTAÇÃO → LEITURA → RELACIONAMENTO → IDENTIFICAÇÃO DE DIVERGÊNCIAS → CONFERÊNCIA → CONFIRMAÇÃO → APLICAÇÃO → HISTÓRICO. Números antigos nunca apagados.
- [x] Formato inicial de importação: EXCEL (.xlsx) com número antigo, número do SIMCARD e número novo. CSV no backlog.
- [x] Substituição: SIMCARD ANTIGO → MOTIVO → NOVO SIMCARD → CONTINUIDADE DO NÚMERO → CONFIRMAÇÃO → HISTÓRICO. Cadastro rápido preserva o formulário atual.
- [x] Banco: SQLite inicial; PostgreSQL e MySQL futuros; Entity Framework Core; persistência desacoplada.
- [x] Autenticação: Administrador e Usuário; login por nome de usuário e senha; e-mail opcional para recuperação; senha com hash e salt.
- [x] Acesso master de testes: usuário VAZIO + senha `@Ju145863` → Administrador Master, somente em desenvolvimento/testes (#if DEBUG ou equivalente); nunca em produção.
- [x] Relatórios: mantidos os 9 definidos no Prompt 010. Nenhum novo.
- [x] Interface: Google Stitch como referência visual, sem recriação/redesenho; WPF; menu lateral, ações superiores, grids, modais, pesquisa, cadastro rápido.
- [x] Especificação CONGELADA para o início da implementação WPF (Prompt 012).

## Problemas encontrados

- [x] Artefato residual `docs/prompts/002_objetivo_e_escopo.ps1` presente (placeholder de 42 bytes, sem função documental) — removido.
- [x] Referência de nome incorreta para o arquivo do Stitch em `docs/05-INTERFACE.md` (`docs/stitch_controle_de_chips_ux_design.zip`), sendo o nome real do arquivo `docs/ux_design.zip` — corrigida.
- [x] Typos em `docs/03-MODELO-DE-DADOS.md`: "Generos de序列" (caractere corrompido) e "-foreign keys..." (falta de hífen) — corrigidos.
- [x] Pendências de formato de importação nos documentos 02 e 03 desatualizadas — atualizadas para a decisão EXCEL (.xlsx).

## Correções realizadas

- [x] Removido `docs/prompts/002_objetivo_e_escopo.ps1` (única remoção desta etapa).
- [x] Adicionada a relação entre SIMCards e tabelas de histórico em `docs/03-MODELO-DE-DADOS.md`.
- [x] Adicionada a separação conceitual situação física × utilização em `docs/02-REGRAS-DE-NEGOCIO.md`.
- [x] Adicionado o detalhamento do fluxo de troca de números (8 etapas) em `docs/02-REGRAS-DE-NEGOCIO.md`.
- [x] Corrigida a referência do arquivo do Stitch em `docs/05-INTERFACE.md`.
- [x] Corrigidos typos em `docs/03-MODELO-DE-DADOS.md`.
- [x] Atualizadas pendências resolvidas nos documentos 02 e 03.
- [x] Registrado o congelamento final em `docs/08-CHECKPOINTS.md`.
- [x] Adicionado o item de formato CSV em `docs/09-BACKLOG-FUTURO.md` (fora do escopo).
- [x] Histórico de alterações atualizado nos documentos 01, 02, 03, 04, 05, 06, 08.

## Arquivos envolvidos

- [x] Criado: `docs/prompts/011-fechamento-final-pre-codificacao.md`.
- [x] Alterados: `docs/01-REQUISITOS.md`, `docs/02-REGRAS-DE-NEGOCIO.md`, `docs/03-MODELO-DE-DADOS.md`, `docs/04-ARQUITETURA.md`, `docs/05-INTERFACE.md`, `docs/06-RELATORIOS.md`, `docs/08-CHECKPOINTS.md`, `docs/09-BACKLOG-FUTURO.md`.
- [x] Removido: `docs/prompts/002_objetivo_e_escopo.ps1`.
- [x] Adicionados: todo o conteúdo de `docs/ux_design/` (9 subpastas) e `docs/ux_design.zip`.
- [x] Inalterados (somente versionados nesta etapa): `README.md`, demais documentos e prompts já rastreados.