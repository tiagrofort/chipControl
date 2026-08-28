# Requisitos

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não inventar requisitos neste momento. Cada item deve ser definido aprovadamente antes de marcar como concluído.

## Histórico de alterações

* [ ] (inserir aqui, quando houver, alterações posteriores de requisitos)

## Requisitos

## Histórico de alterações

* [x] **2026-08-27** — Objetivo do sistema, escopo inicial, pesquisa, interface, histórico e fora do escopo definidos no Prompt 002.
* [x] **2026-08-27** — Prompt 003 executado. Formalizada a regra de significado de `[X]` (definido/aprovado) e `[ ]` (pendente). Verificada e corrigida a classificação dos checklists em todos os documentos da pasta `docs/`. O histórico do Prompt 002 foi preservado. O arquivo `docs/prompts/002-objetivo-e-escopo.md` recebeu anotação de correção para rastreabilidade.
* [x] **2026-08-27** — Prompt 004 executado. Definida a estrutura inicial dos cadastros (Usuários do sistema, Funcionários, Operadoras, SIMCARDs, Aparelhos), com as decisões já confirmadas registradas por cadastro. As listas de campos de cada cadastro e os status do SIMCARD permanecem pendentes. Criado o backlog permanente `docs/09-BACKLOG-FUTURO.md` (fora do escopo atual).
* [x] **2026-08-28** — Prompt 005 executado. Consolidou os campos dos cinco cadastros principais (Funcionários, Operadoras, SIMCARDs, Aparelhos, Usuários do Sistema) com todas as decisões já definidas.

## Objetivo do sistema

O Controle de Chips será uma aplicação desktop Windows para controlar SIMCARDs, linhas telefônicas, números utilizados pela empresa, funcionários que utilizam as linhas, operadoras, aparelhos e todo o histórico relacionado a esses recursos.

O sistema deve permitir rastrear a situação atual e o histórico de cada SIMCARD e de cada linha, inclusive quando houver troca de número, substituição do SIMCARD, perda, dano, não devolução ou mudança de usuário.

O sistema também deve representar situações em que o SIMCARD físico não está com o funcionário, mas a linha continua em uso, por exemplo quando o SIMCARD foi utilizado apenas para habilitar/configurar o WhatsApp em um aparelho e posteriormente retornou ao estoque enquanto o WhatsApp continua sendo utilizado e conectado ao WhatsApp Web.

## Escopo

### Aplicação

* [x] Aplicação desktop para Windows.
* [x] Interface gráfica.
* [x] Menu lateral para navegação entre telas.
* [x] Sistema de login.
* [x] Usuário do sistema identificado por nome de usuário e senha.
* [x] E-mail não será utilizado como nome de usuário.
* [x] E-mail poderá ser cadastrado para recuperação de senha.

### Banco de dados

* [x] SQLite será o banco utilizado inicialmente.
* [x] A arquitetura deverá permitir PostgreSQL futuramente.
* [x] A arquitetura deverá permitir MySQL futuramente.
* [x] A configuração do banco será realizada por um executável separado.
* [x] O executável de configuração deverá permitir selecionar o tipo de banco e informar suas configurações e credenciais.
* [x] O executável principal deverá ler a configuração antes de inicializar o acesso ao banco.

### Cadastros principais

* [x] Usuários do sistema.
* [x] Funcionários.
* [x] Operadoras de telefonia.
* [x] SIMCARDs.
* [x] Aparelhos.

> Funcionários e usuários do sistema são entidades diferentes.

> Os cinco cadastros são independentes entre si.

#### Usuários do sistema

Decisões já definidas:

* [x] Usuário do sistema é independente de funcionário.
* [x] O login utiliza nome de usuário.
* [x] E-mail não é utilizado como nome de usuário.
* [x] E-mail poderá ser utilizado para recuperação de senha.

Campos (definidos no Prompt 005):

- [x] ID — automático.
- [x] Nome — obrigatório.
- [x] Nome de usuário/login — obrigatório.
- [x] Senha — obrigatória.
- [x] E-mail — opcional.
- [x] Nível de acesso — obrigatório.
- [x] Ativo — obrigatório.
- [x] Observações — opcional.

> Observação: o Prompt 005 não definiu permissões detalhadas de Administrador e Usuário.

#### Funcionários

Decisões já definidas:

* [x] O cadastro de funcionários é independente do cadastro de usuários do sistema.

Campos (definidos no Prompt 005):

- [x] ID — automático.
- [x] Nome completo — obrigatório.
- [x] Matrícula — opcional.
- [x] Setor — obrigatório.
- [x] Cargo — opcional.
- [x] Telefone pessoal — opcional.
- [x] E-mail — opcional.
- [x] Ativo — obrigatório.
- [x] Observações — opcional.

Decisões adicionais (Prompt 005):

- [x] Funcionário é independente de Usuário do Sistema.
- [x] Funcionário não deve ser excluído apenas por deixar de trabalhar na empresa.
- [x] Funcionário pode ficar inativo para preservar seu histórico.

#### Operadoras

Decisões já definidas:

* [x] O cadastro de operadoras de telefonia é independente dos demais cadastros.

Campos (definidos no Prompt 005):

- [x] ID — automático.
- [x] Nome — obrigatório.
- [x] Código/identificação — opcional.
- [x] CNPJ — opcional.
- [x] Telefone/contato — opcional.
- [x] E-mail — opcional.
- [x] Ativo — obrigatório.
- [x] Observações — opcional.

Decisões adicionais (Prompt 005):

- [x] Operadora é um cadastro independente.
- [x] Operadora utilizada em históricos não deve ser excluída apenas por deixar de ser utilizada.
- [x] Operadora poderá ficar inativa.

#### SIMCARDs

Decisões já definidas:

* [x] O SIMCARD possui identificação interna do chip físico (ex.: Chip 01, Chip 02, Chip 03).
* [x] A identificação interna corresponde à numeração escrita fisicamente no chip.
* [x] O SIMCARD possui ICCID/SIMCARD (informação fundamental do cadastro).
* [x] O SIMCARD possui uma operadora relacionada.
* [x] O SIMCARD deve manter histórico (ver seções Controle de SIMCARD e Histórico).
* [x] Um SIMCARD não deve ser excluído simplesmente porque deixou de ser utilizado.

Campos (definidos no Prompt 005):

- [x] ID — automático.
- [x] Identificação do chip — obrigatória.
- [x] ICCID — obrigatório.
- [x] Operadora — obrigatória.
- [x] Plano/tipo de linha — opcional.
- [x] DDD — opcional.
- [x] Data de aquisição — opcional.
- [x] Data de ativação — opcional.
- [x] Observações — opcional.
- [x] Status — obrigatório.

Status do SIMCARD (definidos no Prompt 005):

- [x] Em estoque
- [x] Em uso particular
- [x] WhatsApp
- [x] Danificado
- [x] Perdido
- [x] Não devolvido
- [x] Descartado
- [x] Inativo

Definições de status (Prompt 005):

- "Em estoque": O SIMCARD está fisicamente disponível no estoque.
- "Em uso particular": O SIMCARD está fisicamente no celular utilizado pelo usuário.
- "WhatsApp": A linha continua sendo utilizada para WhatsApp, mesmo que o SIMCARD físico tenha retornado ao estoque após a configuração.

Decisões adicionais (Prompt 005):

- [x] O ICCID identifica o SIMCARD físico.
- [x] O SIMCARD deve possuir histórico.
- [x] Um SIMCARD que deixou de ser utilizado não deve ter seu histórico apagado.

> Observação: a estrutura técnica do plano/tipo de linha ainda NÃO está definida. Não criar tabelas nem campos adicionais. Não decidir se plano será cadastro separado ou campos do SIMCARD.

#### Aparelhos

Decisões já definidas:

* [x] Um aparelho pode pertencer à empresa.
* [x] Um aparelho pode pertencer ao funcionário.
* [x] O proprietário do aparelho é independente do usuário da linha.
* [x] Um aparelho pode estar relacionado à utilização de uma linha.

Campos (definidos no Prompt 005):

- [x] ID — automático.
- [x] Identificação do aparelho — obrigatória.
- [x] Tipo — obrigatório.
- [x] Marca — opcional.
- [x] Modelo — opcional.
- [x] IMEI 1 — opcional.
- [x] IMEI 2 — opcional.
- [x] Proprietário — obrigatório.
- [x] Funcionário proprietário — aplicável quando o proprietário for funcionário.
- [x] Observações — opcional.
- [x] Ativo — obrigatório.

Decisões adicionais (Prompt 005):

- [x] O aparelho pode pertencer à empresa.
- [x] O aparelho pode pertencer a um funcionário.
- [x] Proprietário do aparelho é diferente do usuário da linha.
- [x] O usuário de uma linha não deve ser tratado automaticamente como proprietário do aparelho.
- [x] Um aparelho pode mudar de usuário sem mudar de proprietário.
- [x] Aparelho utilizado em histórico não deve ser excluído apenas porque deixou de ser utilizado.

#### Seleção de Funcionários (regra documentada no Prompt 005)

- [x] Funcionário relacionado ao aparelho não será digitado manualmente como texto.
- [x] O funcionário deverá ser selecionado a partir do cadastro de Funcionários.
- [x] O sistema deverá permitir pesquisar funcionários existentes.
- [x] Se o funcionário não existir, deverá existir uma opção para cadastrá-lo sem sair do formulário atual.
- [x] Depois do cadastro rápido, o formulário original deverá ser retomado.
- [x] Os dados já preenchidos no formulário original deverão ser preservados.
- [x] O novo funcionário deverá ficar disponível para seleção.

> Observação: essa regra deverá posteriormente ser generalizada para outros cadastros relacionados. Não implementar agora.

#### Plano/Tipo de Linha (decisão documentada no Prompt 005)

- [x] O cadastro do SIMCARD deverá permitir registrar o plano/tipo de linha.
- [x] Deverá ser possível registrar se existe minutagem ativa.
- [x] Quando houver minutagem, deverá ser possível registrar sua quantidade.
- [x] Deverá ser possível registrar se existe franquia de internet.
- [x] Quando houver internet, deverá ser possível registrar sua quantidade.

> Observação: a estrutura técnica do plano/tipo de linha ainda NÃO está definida. Não criar tabelas nem campos adicionais. Não decidir se plano será cadastro separado ou campos do SIMCARD.

#### Regra Geral sobre Campos (documentada no Prompt 005)

- [x] Campos somente devem ser obrigatórios quando realmente necessários.
- [x] Informações complementares devem ser opcionais.
- [x] Campos relacionados a outros cadastros devem utilizar registros existentes, e não nomes digitados livremente.
- [x] Deve existir mecanismo de cadastro rápido para registros relacionados.

### Controle de SIMCARD

O sistema deverá controlar:

* [x] Identificação interna do chip físico, como Chip 01, Chip 02 etc.
* [x] ICCID/SIMCARD.
* [x] Operadora.
* [x] Situação física do SIMCARD.
* [x] Número atual relacionado ao SIMCARD.
* [x] Histórico de números utilizados pelo SIMCARD.
* [x] Histórico de funcionários que utilizaram o SIMCARD.
* [x] Substituição de SIMCARD.
* [x] Chip danificado.
* [x] Chip perdido.
* [x] Chip não devolvido.
* [x] Retorno do chip ao estoque.
* [x] Preservação do histórico mesmo depois de o SIMCARD deixar de ser utilizado.

### Controle de utilização

* [x] O sistema não deve considerar "chip entregue" como sinônimo de "linha em uso".
* [x] Um SIMCARD pode estar fisicamente no estoque e sua linha continuar em uso.
* [x] Deve ser possível registrar que um SIMCARD foi utilizado para habilitação/configuração de um aparelho sem permanecer fisicamente no aparelho.
* [x] Deve ser possível identificar quem utiliza atualmente uma linha.
* [x] Deve ser possível identificar quem já utilizou uma linha ou SIMCARD.
* [x] Deve ser possível identificar se o SIMCARD está fisicamente com o funcionário, em um aparelho ou no estoque.

### Aparelhos

* [x] O aparelho relacionado à utilização de uma linha pode pertencer à empresa.
* [x] O aparelho relacionado à utilização de uma linha pode pertencer ao funcionário.
* [x] A propriedade do aparelho deve ser independente do funcionário que utiliza a linha.

## Pesquisa

* [x] As pesquisas das telas devem pesquisar por todos os campos relevantes do respectivo cadastro.
* [x] A pesquisa não deverá ficar limitada somente à coluna atualmente selecionada.
* [x] A pesquisa deve permitir localizar registros utilizando informações como nome, número, SIMCARD, operadora e demais campos relevantes existentes naquele cadastro.

> Não definido ainda: implementação técnica da pesquisa.

## Interface

* [x] Menu lateral para navegação.
* [x] Cada tela terá seus principais botões na parte superior.
* [x] A listagem principal será apresentada em um grid na parte inferior.
* [x] A inclusão e edição de registros será realizada por modal.
* [x] Quando um formulário depender de um registro de outro cadastro, deverá existir uma opção para cadastrar rapidamente o registro relacionado sem cancelar o formulário atual.
* [x] Depois de cadastrar o registro relacionado, o sistema deverá retornar ao formulário original preservando os dados já preenchidos e selecionando o novo registro.

> Esse recurso deverá ser chamado provisoriamente de "cadastro rápido relacionado".

## Histórico

* [x] O sistema deve preservar históricos.
* [x] Registros históricos não devem ser apagados somente porque deixaram de estar ativos.
* [x] Trocas de número devem preservar os números anteriores.
* [x] Trocas de SIMCARD devem preservar o SIMCARD anterior e o motivo da substituição.
* [x] Mudanças de funcionário devem preservar quem utilizou anteriormente a linha/SIMCARD.
* [x] Perda, dano ou não devolução não devem apagar o histórico.

> Ainda NÃO definido: modelo das tabelas. Será tratado em docs/03-MODELO-DE-DADOS.md.

## Fora do escopo neste momento

* [ ] Definir lista completa de relatórios.
* [ ] Definir regras detalhadas de status.
* [ ] Definir todos os campos de cada cadastro.
* [ ] Definir modelo de dados.
* [ ] Definir relacionamentos.
* [ ] Definir regras detalhadas de movimentação.
* [ ] Definir permissões detalhadas dos usuários.
* [ ] Definir recuperação de senha.
* [ ] Definir auditoria.
* [ ] Definir backup e restauração.
* [ ] Definir telas individualmente.
* [ ] Definir identidade visual.
* [ ] Definir tecnologia exata dos componentes visuais.
* [ ] Definir estratégia de migrations.
* [ ] Definir estratégia de publicação/instalação.

## Requisitos

* [x] Definir objetivo do sistema
* [x] Definir escopo
* [ ] Definir usuários do sistema
* [x] Definir cadastros
* [ ] Definir operações
* [x] Definir pesquisas
* [ ] Definir relatórios
* [x] Definir histórico
* [ ] Definir requisitos de segurança
