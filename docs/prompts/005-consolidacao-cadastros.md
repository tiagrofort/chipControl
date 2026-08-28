# Prompt 005 — Consolidação dos Cadastros e Campos

## Identificação

- [X] Prompt 005
- [X] Data: 2026-08-28

## Objetivo

- [X] Consolidar os cadastros e campos definidos.

## Prompt completo

PROMPT 005 — CONSOLIDAÇÃO DOS CADASTROS E CAMPOS DEFINIDOS

OBJETIVO

Consolidar na documentação todas as decisões sobre os cinco cadastros principais que foram definidas após o Prompt 004.

IMPORTANTE:

- NÃO desenvolver código.
- NÃO criar banco.
- NÃO criar entidades.
- NÃO criar telas.
- NÃO instalar dependências.
- NÃO alterar arquitetura.
- NÃO inventar campos.
- NÃO inventar regras.
- NÃO avançar para modelo de dados.
- NÃO avançar para implementação.

Esta etapa é exclusivamente documental.

==================================================
1. REGRA DE DOCUMENTAÇÃO
==================================================

Preservar todo histórico existente.

Nunca apagar itens [X].

Nunca transformar uma decisão já aprovada em outra decisão sem registrar a alteração.

[X] significa decisão formalmente definida/aprovada.

[ ] significa item ainda pendente.

Registrar este prompt integralmente em:

docs/prompts/005-consolidacao-cadastros.md

==================================================
2. FUNCIONÁRIOS
==================================================

Documentar definitivamente o cadastro de Funcionários com os seguintes campos:

- [X] ID — automático.
- [X] Nome completo — obrigatório.
- [X] Matrícula — opcional.
- [X] Setor — obrigatório.
- [X] Cargo — opcional.
- [X] Telefone pessoal — opcional.
- [X] E-mail — opcional.
- [X] Ativo — obrigatório.
- [X] Observações — opcional.

Registrar também:

- [X] Funcionário é independente de Usuário do Sistema.
- [X] Funcionário não deve ser excluído apenas por deixar de trabalhar na empresa.
- [X] Funcionário pode ficar inativo para preservar seu histórico.

==================================================
3. OPERADORAS
==================================================

Documentar definitivamente:

- [X] ID — automático.
- [X] Nome — obrigatório.
- [X] Código/identificação — opcional.
- [X] CNPJ — opcional.
- [X] Telefone/contato — opcional.
- [X] E-mail — opcional.
- [X] Ativo — obrigatório.
- [X] Observações — opcional.

Registrar:

- [X] Operadora é um cadastro independente.
- [X] Operadora utilizada em históricos não deve ser excluída apenas por deixar de ser utilizada.
- [X] Operadora poderá ficar inativa.

==================================================
4. SIMCARD
==================================================

Documentar definitivamente os campos:

- [X] ID — automático.
- [X] Identificação do chip — obrigatória.
- [X] ICCID — obrigatório.
- [X] Operadora — obrigatória.
- [X] Plano/tipo de linha — opcional.
- [X] DDD — opcional.
- [X] Data de aquisição — opcional.
- [X] Data de ativação — opcional.
- [X] Observações — opcional.
- [X] Status — obrigatório.

A identificação do chip é a identificação física escrita manualmente no SIMCARD, por exemplo:

Chip 01
Chip 02
Chip 03

Registrar:

- [X] O ICCID identifica o SIMCARD físico.
- [X] O SIMCARD deve possuir histórico.
- [X] Um SIMCARD que deixou de ser utilizado não deve ter seu histórico apagado.

==================================================
5. STATUS DO SIMCARD
==================================================

Registrar como lista aprovada:

- [X] Em estoque
- [X] Em uso particular
- [X] WhatsApp
- [X] Danificado
- [X] Perdido
- [X] Não devolvido
- [X] Descartado
- [X] Inativo

Registrar as definições:

"Em estoque":
O SIMCARD está fisicamente disponível no estoque.

"Em uso particular":
O SIMCARD está fisicamente no celular utilizado pelo usuário.

"WhatsApp":
A linha continua sendo utilizada para WhatsApp, mesmo que o SIMCARD físico tenha retornado ao estoque após a configuração.

IMPORTANTE:

Não criar neste prompt outras regras para os demais status.

Não definir ainda como ocorrerá a mudança de status.

==================================================
6. PLANO/TIPO DE LINHA
==================================================

Registrar somente a decisão já tomada:

- [X] O cadastro do SIMCARD deverá permitir registrar o plano/tipo de linha.
- [X] Deverá ser possível registrar se existe minutagem ativa.
- [X] Quando houver minutagem, deverá ser possível registrar sua quantidade.
- [X] Deverá ser possível registrar se existe franquia de internet.
- [X] Quando houver internet, deverá ser possível registrar sua quantidade.

IMPORTANTE:

A estrutura técnica desses dados ainda NÃO está definida.

Não criar tabelas.

Não criar campos adicionais além dos necessários para documentar esta decisão.

Não decidir neste prompt se plano será cadastro separado ou campos do SIMCARD.

==================================================
7. APARELHOS
==================================================

Documentar definitivamente:

- [X] ID — automático.
- [X] Identificação do aparelho — obrigatória.
- [X] Tipo — obrigatório.
- [X] Marca — opcional.
- [X] Modelo — opcional.
- [X] IMEI 1 — opcional.
- [X] IMEI 2 — opcional.
- [X] Proprietário — obrigatório.
- [X] Funcionário proprietário — aplicável quando o proprietário for funcionário.
- [X] Observações — opcional.
- [X] Ativo — obrigatório.

Registrar:

- [X] O aparelho pode pertencer à empresa.
- [X] O aparelho pode pertencer a um funcionário.
- [X] Proprietário do aparelho é diferente do usuário da linha.
- [X] O usuário de uma linha não deve ser tratado automaticamente como proprietário do aparelho.
- [X] Um aparelho pode mudar de usuário sem mudar de proprietário.
- [X] Aparelho utilizado em histórico não deve ser excluído apenas porque deixou de ser utilizado.

==================================================
8. SELEÇÃO DE FUNCIONÁRIOS
==================================================

Registrar:

- [X] Funcionário relacionado ao aparelho não será digitado manualmente como texto.
- [X] O funcionário deverá ser selecionado a partir do cadastro de Funcionários.
- [X] O sistema deverá permitir pesquisar funcionários existentes.
- [X] Se o funcionário não existir, deverá existir uma opção para cadastrá-lo sem sair do formulário atual.
- [X] Depois do cadastro rápido, o formulário original deverá ser retomado.
- [X] Os dados já preenchidos no formulário original deverão ser preservados.
- [X] O novo funcionário deverá ficar disponível para seleção.

Essa regra deverá posteriormente ser generalizada para outros cadastros relacionados.

Não implementar agora.

==================================================
9. USUÁRIOS DO SISTEMA
==================================================

Documentar:

- [X] ID — automático.
- [X] Nome — obrigatório.
- [X] Nome de usuário/login — obrigatório.
- [X] Senha — obrigatória.
- [X] E-mail — opcional.
- [X] Nível de acesso — obrigatório.
- [X] Ativo — obrigatório.
- [X] Observações — opcional.

Registrar:

- [X] Usuário do sistema é independente de funcionário.
- [X] O login utiliza nome de usuário.
- [X] E-mail não é utilizado como nome de usuário.
- [X] E-mail poderá ser utilizado para recuperação de senha.
- [X] Existirão somente dois níveis de acesso.
- [X] Administrador.
- [X] Usuário.
- [X] Não haverá sistema granular de permissões nesta versão.

Não definir neste prompt as permissões detalhadas de Administrador e Usuário.

==================================================
10. REGRA GERAL SOBRE CAMPOS
==================================================

Registrar:

- [X] Campos somente devem ser obrigatórios quando realmente necessários.
- [X] Informações complementares devem ser opcionais.
- [X] Campos relacionados a outros cadastros devem utilizar registros existentes, e não nomes digitados livremente.
- [X] Deve existir mecanismo de cadastro rápido para registros relacionados.

==================================================
11. ITENS QUE CONTINUAM PENDENTES
==================================================

Preservar como pendentes:

- [ ] Modelo de dados.
- [ ] Relacionamentos detalhados.
- [ ] Histórico detalhado.
- [ ] Regras de movimentação.
- [ ] Regras detalhadas de troca de número.
- [ ] Importação da lista de troca de números.
- [ ] Regras de substituição de SIMCARD.
- [ ] Regras detalhadas de cada status.
- [ ] Estrutura técnica do plano/tipo de linha.
- [ ] Regras detalhadas de WhatsApp.
- [ ] Relatórios.
- [ ] Telas.
- [ ] Arquitetura definitiva.
- [ ] Banco de dados.
- [ ] Segurança.
- [ ] Backup.
- [ ] Instalação/publicação.

Não resolver esses itens neste prompt.

==================================================
12. HISTÓRICO DA DOCUMENTAÇÃO
==================================================

Atualizar:

docs/01-REQUISITOS.md

registrando que o Prompt 005 consolidou os campos dos cinco cadastros.

Não apagar histórico anterior.

Se existirem informações conflitantes, NÃO escolher silenciosamente uma delas. Registrar a divergência para revisão.

==================================================
13. CHECKPOINT
==================================================

Atualizar:

docs/08-CHECKPOINTS.md

Adicionar o checkpoint do Prompt 005.

Registrar:

- [X] Prompt 005 executado.
- [X] Cadastro de Funcionários definido.
- [X] Cadastro de Operadoras definido.
- [X] Cadastro de SIMCARD definido.
- [X] Status iniciais do SIMCARD definidos.
- [X] Cadastro de Aparelhos definido.
- [X] Cadastro de Usuários do Sistema definido.
- [X] Regra de seleção/cadastro rápido de registros relacionados documentada.
- [X] Itens ainda pendentes preservados.

Não apagar checkpoints anteriores.

==================================================
14. REGISTRAR O PROMPT
==================================================

Criar:

docs/prompts/005-consolidacao-cadastros.md

Preservar integralmente este prompt.

Registrar:

# Prompt 005 — Consolidação dos Cadastros e Campos

## Identificação

- [X] Prompt 005
- [X] Data

## Objetivo

- [X] Consolidar os cadastros e campos definidos.

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

Não apagar o conteúdo original.

==================================================
15. GIT
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
16. RESULTADO FINAL
==================================================

Ao terminar, informe:

- documentos alterados;
- documentos criados;
- campos definidos;
- itens ainda pendentes;
- confirmação de que nenhum código foi criado;
- hash do commit;
- resultado do push.

NÃO avance para modelo de dados, arquitetura, telas, Stitch ou implementação.