PROMPT 006 — REGRAS DE NEGÓCIO E HISTÓRICO

OBJETIVO

Documentar as regras de negócio fundamentais do Controle de Chips, principalmente o histórico de SIMCARDs, números, funcionários, aparelhos, WhatsApp, troca de números e substituição de SIMCARD.

IMPORTANTE:

- NÃO desenvolver código.
- NÃO criar banco de dados.
- NÃO criar entidades.
- NÃO criar telas.
- NÃO definir tabelas.
- NÃO instalar dependências.
- NÃO inventar regras.
- Preservar todo histórico existente.
- Nunca apagar itens [X].
- Novas ideias que não pertençam ao escopo atual devem ser registradas em docs/09-BACKLOG-FUTURO.md e não implementadas.

Registrar este prompt integralmente em:

docs/prompts/006-regras-negocio-e-historico.md


==================================================
1. PRINCÍPIO FUNDAMENTAL DO HISTÓRICO
==================================================

Registrar:

- [X] Alterações de utilização não devem apagar o histórico anterior.
- [X] Alterações de número não devem apagar números anteriores.
- [X] Substituição de SIMCARD não deve apagar o SIMCARD anterior.
- [X] Mudanças de funcionário não devem apagar usuários anteriores.
- [X] Mudanças de aparelho não devem apagar aparelhos utilizados anteriormente.
- [X] Registros históricos devem permanecer consultáveis.


==================================================
2. CICLO DE VIDA DO SIMCARD
==================================================

Registrar as seguintes situações já definidas:

- [X] Em estoque
- [X] Em uso particular
- [X] WhatsApp
- [X] Danificado
- [X] Perdido
- [X] Não devolvido
- [X] Descartado
- [X] Inativo

Regras:

- [X] Um SIMCARD pode retornar ao estoque sem deixar de ter uma linha em uso.
- [X] Um SIMCARD em status WhatsApp pode estar fisicamente no estoque.
- [X] Um SIMCARD em uso particular representa utilização física no celular do usuário.
- [X] Danificado, perdido ou não devolvido não significa exclusão do cadastro.
- [X] O histórico do SIMCARD deve permanecer mesmo depois de sua desativação.


==================================================
3. NÚMEROS TELEFÔNICOS
==================================================

Registrar:

- [X] Um SIMCARD pode possuir diferentes números ao longo de sua vida.
- [X] O número anterior deve permanecer no histórico quando ocorrer uma troca.
- [X] O número atual deve ser identificável.
- [X] A troca de número não deve apagar o número anterior.
- [X] O histórico deve permitir saber quais números estiveram associados ao SIMCARD.

Não definir ainda a estrutura técnica desse histórico.


==================================================
4. TROCA DE NÚMEROS POR IMPORTAÇÃO
==================================================

A operadora fornece uma lista contendo:

- Número antigo
- Número do SIMCARD
- Número novo

Registrar:

- [X] O sistema deverá possuir uma funcionalidade própria para importar essa lista.
- [X] A importação deverá relacionar número antigo, SIMCARD e número novo.
- [X] O SIMCARD será utilizado como elemento fundamental do relacionamento.
- [X] O sistema deverá validar a correspondência antes de efetivar a troca.
- [X] A importação deverá permitir visualizar uma prévia antes da confirmação.
- [X] Divergências deverão ser identificadas antes da confirmação.
- [X] Uma divergência entre o número antigo informado e o número atualmente registrado para o SIMCARD não deverá ser alterada automaticamente.
- [X] A troca somente deverá ser efetivada após confirmação.
- [X] Após a confirmação, o número anterior deverá permanecer no histórico.
- [X] O novo número deverá passar a ser o número atual.
- [X] A operação deverá preservar o histórico da alteração.

Não definir ainda o formato técnico do arquivo de importação.


==================================================
5. SUBSTITUIÇÃO DE SIMCARD
==================================================

Registrar:

- [X] Um SIMCARD pode precisar ser substituído por dano, perda, não devolução ou outro motivo posteriormente definido.
- [X] O SIMCARD antigo não deve ser excluído.
- [X] O motivo da substituição deve ser preservado.
- [X] O novo SIMCARD deve ser cadastrado quando ainda não existir no sistema.
- [X] O sistema deverá permitir cadastrar o novo SIMCARD sem abandonar a operação atual.
- [X] Depois do cadastro rápido, a operação de substituição deverá continuar.
- [X] O histórico deverá permitir identificar o SIMCARD anterior e o novo.
- [X] O número poderá permanecer o mesmo após a substituição do SIMCARD.

Não definir ainda tabelas ou implementação.


==================================================
6. TROCA DE NÚMERO E SIMCARD AO MESMO TEMPO
==================================================

Registrar:

- [X] O sistema deverá suportar a situação em que o SIMCARD e o número sejam substituídos em uma mesma operação.
- [X] O histórico deverá preservar tanto o SIMCARD anterior quanto o número anterior.
- [X] O novo SIMCARD e o novo número deverão ficar identificados como atuais.

Exemplo conceitual:

SIMCARD antigo + número antigo
            ↓
SIMCARD novo + número novo

Não implementar.


==================================================
7. FUNCIONÁRIOS E UTILIZAÇÃO
==================================================

Registrar:

- [X] Deve ser possível identificar o funcionário que utiliza atualmente uma linha.
- [X] Deve ser possível identificar funcionários que utilizaram anteriormente uma linha/SIMCARD.
- [X] A troca de funcionário não deve apagar o histórico.
- [X] Funcionário que deixar a empresa deve poder permanecer no cadastro como inativo.
- [X] O histórico deve continuar apontando para o funcionário anterior.


==================================================
8. APARELHOS
==================================================

Registrar:

- [X] Um aparelho pode pertencer à empresa.
- [X] Um aparelho pode pertencer ao funcionário.
- [X] Proprietário e usuário do aparelho são informações diferentes.
- [X] O usuário de uma linha não é automaticamente o proprietário do aparelho.
- [X] Um aparelho pode mudar de usuário sem mudar de proprietário.
- [X] O histórico de utilização do aparelho deve ser preservado.


==================================================
9. WHATSAPP
==================================================

Registrar:

- [X] Uma linha pode continuar sendo utilizada para WhatsApp mesmo quando o SIMCARD físico retorna ao estoque.
- [X] A situação WhatsApp deve ser diferente de Em uso particular.
- [X] A posse física do SIMCARD não determina sozinha se a linha está em uso.
- [X] O sistema deve preservar a informação de que a linha continua em uso para WhatsApp.

Não definir neste prompt detalhes técnicos da integração com WhatsApp ou WhatsApp Web.


==================================================
10. CADASTRO RÁPIDO
==================================================

Preservar como regra geral:

- [X] Quando uma operação depender de um cadastro que não existe, deve ser possível cadastrá-lo sem abandonar a operação atual.
- [X] O formulário original deve preservar os dados já preenchidos.
- [X] Após o cadastro, o novo registro deve estar disponível para seleção.
- [X] A operação original deve continuar sem necessidade de reiniciar o processo.


==================================================
11. OPERAÇÕES QUE DEVEM PRESERVAR HISTÓRICO
==================================================

Registrar como regra geral:

- [X] Troca de número.
- [X] Substituição de SIMCARD.
- [X] Troca de funcionário.
- [X] Troca de aparelho.
- [X] Perda de SIMCARD.
- [X] Dano de SIMCARD.
- [X] Não devolução de SIMCARD.
- [X] Retorno do SIMCARD ao estoque.
- [X] Desativação/inativação.


==================================================
12. ITENS AINDA PENDENTES
==================================================

NÃO resolver neste prompt:

- [ ] Modelo de dados.
- [ ] Relacionamentos técnicos.
- [ ] Estrutura das tabelas.
- [ ] Formato definitivo do arquivo de importação.
- [ ] Regras detalhadas de cada status.
- [ ] Regras detalhadas de movimentação.
- [ ] Campos específicos do histórico.
- [ ] Relatórios.
- [ ] Telas.
- [ ] Arquitetura definitiva.
- [ ] Segurança.
- [ ] Backup.
- [ ] Instalação/publicação.


==================================================
13. CHECKPOINT
==================================================

Atualizar:

docs/08-CHECKPOINTS.md

Adicionar o checkpoint do Prompt 006.

Registrar:

- [X] Prompt 006 executado.
- [X] Princípio de preservação do histórico definido.
- [X] Ciclo de vida do SIMCARD documentado.
- [X] Regras de números documentadas.
- [X] Processo de troca de números documentado.
- [X] Substituição de SIMCARD documentada.
- [X] Troca simultânea de SIMCARD e número documentada.
- [X] Histórico de funcionários documentado.
- [X] Histórico de aparelhos documentado.
- [X] Regras de WhatsApp documentadas.
- [X] Cadastro rápido preservado.
- [X] Itens ainda pendentes preservados.


==================================================
14. REGISTRAR O PROMPT
==================================================

Criar:

docs/prompts/006-regras-negocio-e-historico.md

Preservar integralmente este prompt.

Registrar:

# Prompt 006 — Regras de Negócio e Histórico

## Identificação

- [X] Prompt 006
- [X] Data

## Objetivo

- [X] Definir as regras fundamentais de histórico e movimentação.

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
- regras definidas;
- itens ainda pendentes;
- confirmação de que nenhum código foi criado;
- hash do commit;
- resultado do push.

NÃO avance para modelo de dados, arquitetura, telas, Stitch ou implementação.