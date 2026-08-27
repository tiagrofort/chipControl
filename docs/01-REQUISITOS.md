# Requisitos

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não inventar requisitos neste momento. Cada item deve ser definido aprovadamente antes de marcar como concluído.

## Histórico de alterações

* [ ] (inserir aqui, quando houver, alterações posteriores de requisitos)

## Requisitos

## Histórico de alterações

* [x] **2026-08-27** — Objetivo do sistema, escopo inicial, pesquisa, interface, histórico e fora do escopo definidos no Prompt 002.

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
