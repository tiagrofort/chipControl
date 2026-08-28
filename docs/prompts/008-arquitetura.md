PROMPT 008 — ARQUITETURA DO SISTEMA

OBJETIVO

Definir a arquitetura técnica do Controle de Chips antes do início do
desenvolvimento.

IMPORTANTE:

- NÃO escrever código.
- NÃO criar projetos.
- NÃO instalar dependências.
- NÃO criar banco.
- NÃO criar telas.
- NÃO iniciar implementação.
- Não alterar decisões já aprovadas.
- Novas ideias devem ir para docs/09-BACKLOG-FUTURO.md.
- Preservar todo histórico da documentação.

Leia antes:

- docs/01-REQUISITOS.md
- docs/02-REGRAS-DE-NEGOCIO.md
- docs/03-MODELO-DE-DADOS.md
- docs/04-ARQUITETURA.md
- docs/08-CHECKPOINTS.md
- docs/09-BACKLOG-FUTURO.md
- docs/prompts/007-modelo-de-dados.md


==================================================
1. TECNOLOGIA PRINCIPAL
==================================================

Registrar:

- [X] Aplicação desktop para Windows.
- [X] WPF como tecnologia da interface.
- [X] Linguagem C#.
- [X] SQLite como banco inicial.
- [X] Arquitetura preparada para PostgreSQL.
- [X] Arquitetura preparada para MySQL.

Não substituir WPF por outra tecnologia.


==================================================
2. ARQUITETURA EM CAMADAS
==================================================

Definir uma arquitetura simples e organizada, evitando complexidade
desnecessária.

Documentar responsabilidades para:

- Apresentação/UI
- Aplicação
- Domínio
- Infraestrutura
- Persistência

A arquitetura deve permitir que a interface não dependa diretamente
do banco de dados.

Evitar criar camadas ou projetos sem necessidade.


==================================================
3. BANCO DE DADOS
==================================================

Registrar:

- [X] SQLite será utilizado inicialmente.
- [X] PostgreSQL deverá ser suportado futuramente.
- [X] MySQL deverá ser suportado futuramente.
- [X] O sistema principal não deve depender diretamente de uma única
  implementação de banco.
- [X] A escolha do banco deverá ser configurável.

Não criar código de persistência nesta etapa.


==================================================
4. EXECUTÁVEL DE CONFIGURAÇÃO DO BANCO
==================================================

Registrar:

- [X] A configuração do banco será feita através de um executável
  separado do sistema principal.
- [X] Esse programa permitirá selecionar/configurar o banco.
- [X] Deverá permitir informar as credenciais necessárias.
- [X] Deverá gerar ou alterar um arquivo de configuração.
- [X] O sistema principal deverá ler essa configuração antes de
  inicializar o acesso ao banco.

O executável de configuração NÃO será implementado agora.


==================================================
5. SISTEMA PRINCIPAL
==================================================

Documentar que o sistema principal deverá:

- Ler a configuração do banco.
- Validar a configuração antes de iniciar o acesso ao banco.
- Inicializar a aplicação.
- Utilizar a camada de persistência configurada.

Não definir ainda detalhes de implementação.


==================================================
6. AUTENTICAÇÃO
==================================================

Registrar:

- [X] O sistema terá login.
- [X] Login através de nome de usuário e senha.
- [X] E-mail não será utilizado como login.
- [X] E-mail poderá ser utilizado para recuperação de senha.
- [X] Existem dois níveis: Administrador e Usuário.
- [X] Usuários do sistema são independentes dos funcionários.

Documentar somente a posição da autenticação na arquitetura.

Não implementar login neste prompt.


==================================================
7. SEGURANÇA DA SENHA
==================================================

Registrar como requisito arquitetural:

- [X] Senhas não devem ser armazenadas em texto puro.
- [X] A implementação deverá utilizar armazenamento seguro de senha.

Não escolher biblioteca ou algoritmo neste prompt.


==================================================
8. CONFIGURAÇÃO
==================================================

Definir conceitualmente:

- Onde ficará o arquivo de configuração.
- Como o sistema principal localizará esse arquivo.
- Como evitar que credenciais fiquem espalhadas pela aplicação.
- Como permitir futura troca de SQLite para PostgreSQL/MySQL.

Não criar o arquivo real.


==================================================
9. DEPENDÊNCIA ENTRE PROJETOS
==================================================

Documentar as dependências permitidas entre as camadas/projetos.

Objetivo:

- Domínio independente de infraestrutura.
- Aplicação não deve depender diretamente de implementação específica
  do banco.
- UI não deve acessar banco diretamente.
- Infraestrutura implementa os serviços necessários.

Manter a solução simples.


==================================================
10. LOG E ERROS
==================================================

Definir conceitualmente:

- A aplicação deverá possuir tratamento de erros.
- Erros importantes deverão poder ser registrados.
- Falhas de conexão com banco deverão ser tratadas de forma clara.
- O usuário deverá receber mensagens compreensíveis.

Não escolher biblioteca de logging neste prompt.


==================================================
11. BACKUP
==================================================

Registrar apenas:

- [ ] Estratégia definitiva de backup ainda não definida.

Não criar solução de backup agora.


==================================================
12. PUBLICAÇÃO
==================================================

Registrar:

- [ ] Estratégia definitiva de instalação/publicação ainda não definida.

Não implementar instalador agora.


==================================================
13. DOCUMENTO DE ARQUITETURA
==================================================

Atualizar:

docs/04-ARQUITETURA.md

Documentar:

- tecnologias;
- camadas;
- responsabilidades;
- dependências;
- configuração do banco;
- executável de configuração;
- autenticação;
- segurança;
- tratamento de erros;
- compatibilidade SQLite/PostgreSQL/MySQL.

Não inserir código de implementação.


==================================================
14. CHECKPOINT
==================================================

Atualizar:

docs/08-CHECKPOINTS.md

Adicionar:

- [X] Prompt 008 executado.
- [X] WPF definido.
- [X] C# definido.
- [X] Arquitetura em camadas definida.
- [X] SQLite inicial definido.
- [X] PostgreSQL futuro definido.
- [X] MySQL futuro definido.
- [X] Executável separado de configuração do banco definido.
- [X] Autenticação posicionada na arquitetura.
- [X] Segurança de senha registrada.
- [X] Nenhum código criado.


==================================================
15. REGISTRAR O PROMPT
==================================================

Criar:

docs/prompts/008-arquitetura.md

Preservar integralmente este prompt.

Adicionar:

# Prompt 008 — Arquitetura

## Identificação

- [X] Prompt 008
- [X] Data

## Objetivo

- [X] Definir a arquitetura técnica.

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
16. NÃO AVANÇAR
==================================================

Não criar:

- código;
- solução C#;
- projetos;
- banco;
- migrations;
- entidades;
- telas;
- componentes WPF;
- integração com Stitch.

A próxima etapa será o planejamento visual no Google Stitch.


==================================================
17. GIT
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
18. RELATÓRIO FINAL OBRIGATÓRIO
==================================================

Ao concluir, gere um relatório completo e objetivo.

Este relatório será copiado integralmente pelo usuário e enviado
de volta para análise.

Informe:

1. Status da execução.
2. O que foi realizado.
3. Arquivos criados.
4. Arquivos alterados.
5. Arquivos excluídos, se houver.
6. Arquitetura definida.
7. Tecnologias definidas.
8. Decisões sobre banco de dados.
9. Decisões sobre configuração do banco.
10. Decisões sobre autenticação.
11. Pendências restantes.
12. Problemas encontrados.
13. Correções realizadas.
14. Validações executadas.
15. Confirmação de que nenhum código foi criado.
16. Confirmação de que nenhuma decisão não autorizada foi inventada.
17. Commit realizado.
18. Hash completo do commit.
19. Branch.
20. Resultado do push para o GitHub.

O relatório deve ser apresentado somente após concluir todo o trabalho.

NÃO avance para a próxima etapa.