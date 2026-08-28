# Regras de Negócio

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não inventar regras de negócio neste momento. Cada regra deve ser validada antes de marcar como concluída.

## Histórico de alterações

* [x] **2026-08-28** — Prompt 006 executado. Documentadas as regras fundamentais de histórico e movimentação: princípio de preservação do histórico, ciclo de vida do SIMCARD, regras de números telefônicos, troca de números por importação, substituição de SIMCARD, troca simultânea de SIMCARD e número, histórico de funcionários, histórico de aparelhos, regras de WhatsApp, cadastro rápido, operações que preservam histórico. Itens pendentes preservados.

## Regras de Negócio

### Princípio Fundamental do Histórico (Prompt 006)

- [x] Alterações de utilização não devem apagar o histórico anterior.
- [x] Alterações de número não devem apagar números anteriores.
- [x] Substituição de SIMCARD não deve apagar o SIMCARD anterior.
- [x] Mudanças de funcionário não devem apagar usuários anteriores.
- [x] Mudanças de aparelho não devem apagar aparelhos utilizados anteriormente.
- [x] Registros históricos devem permanecer consultáveis.

### Ciclo de Vida do SIMCARD (Prompt 006)

Status definidos (conforme Prompt 005):
- [x] Em estoque
- [x] Em uso particular
- [x] WhatsApp
- [x] Danificado
- [x] Perdido
- [x] Não devolvido
- [x] Descartado
- [x] Inativo

Regras de ciclo de vida:
- [x] Um SIMCARD pode retornar ao estoque sem deixar de ter uma linha em uso.
- [x] Um SIMCARD em status WhatsApp pode estar fisicamente no estoque.
- [x] Um SIMCARD em uso particular representa utilização física no celular do usuário.
- [x] Danificado, perdido ou não devolvido não significa exclusão do cadastro.
- [x] O histórico do SIMCARD deve permanecer mesmo depois de sua desativação.

### Números Telefônicos (Prompt 006)

- [x] Um SIMCARD pode possuir diferentes números ao longo de sua vida.
- [x] O número anterior deve permanecer no histórico quando ocorrer uma troca.
- [x] O número atual deve ser identificável.
- [x] A troca de número não deve apagar o número anterior.
- [x] O histórico deve permitir saber quais números estiveram associados ao SIMCARD.

> Observação: a estrutura técnica desse histórico ainda NÃO está definida.

### Troca de Números por Importação (Prompt 006)

A operadora fornece lista contendo: número antigo, número do SIMCARD, número novo.

- [x] O sistema deverá possuir funcionalidade própria para importar essa lista.
- [x] A importação deverá relacionar número antigo, SIMCARD e número novo.
- [x] O SIMCARD será utilizado como elemento fundamental do relacionamento.
- [x] O sistema deverá validar a correspondência antes de efetivar a troca.
- [x] A importação deverá permitir visualizar uma prévia antes da confirmação.
- [x] Divergências deverão ser identificadas antes da confirmação.
- [x] Uma divergência entre o número antigo informado e o número atualmente registrado para o SIMCARD não deverá ser alterada automaticamente.
- [x] A troca somente deverá ser efetivada após confirmação.
- [x] Após a confirmação, o número anterior deverá permanecer no histórico.
- [x] O novo número deverá passar a ser o número atual.
- [x] A operação deverá preservar o histórico da alteração.

> Observação: o formato técnico do arquivo de importação ainda NÃO está definido.

### Substituição de SIMCARD (Prompt 006)

- [x] Um SIMCARD pode precisar ser substituído por dano, perda, não devolução ou outro motivo posteriormente definido.
- [x] O SIMCARD antigo não deve ser excluído.
- [x] O motivo da substituição deve ser preservado.
- [x] O novo SIMCARD deve ser cadastrado quando ainda não existir no sistema.
- [x] O sistema deverá permitir cadastrar o novo SIMCARD sem abandonar a operação atual.
- [x] Depois do cadastro rápido, a operação de substituição deverá continuar.
- [x] O histórico deverá permitir identificar o SIMCARD anterior e o novo.
- [x] O número poderá permanecer o mesmo após a substituição do SIMCARD.

> Observação: tabelas e implementação ainda NÃO definidas.

### Troca de Número e SIMCARD ao Mesmo Tempo (Prompt 006)

- [x] O sistema deverá suportar a situação em que o SIMCARD e o número sejam substituídos em uma mesma operação.
- [x] O histórico deverá preservar tanto o SIMCARD anterior quanto o número anterior.
- [x] O novo SIMCARD e o novo número deverão ficar identificados como atuais.

Exemplo conceitual:
```
SIMCARD antigo + número antigo
            ↓
SIMCARD novo + número novo
```

### Funcionários e Utilização (Prompt 006)

- [x] Deve ser possível identificar o funcionário que utiliza atualmente uma linha.
- [x] Deve ser possível identificar funcionários que utilizaram anteriormente uma linha/SIMCARD.
- [x] A troca de funcionário não deve apagar o histórico.
- [x] Funcionário que deixar a empresa deve poder permanecer no cadastro como inativo.
- [x] O histórico deve continuar apontando para o funcionário anterior.

### Aparelhos (Prompt 006)

- [x] Um aparelho pode pertencer à empresa.
- [x] Um aparelho pode pertencer ao funcionário.
- [x] Proprietário e usuário do aparelho são informações diferentes.
- [x] O usuário de uma linha não é automaticamente o proprietário do aparelho.
- [x] Um aparelho pode mudar de usuário sem mudar de proprietário.
- [x] O histórico de utilização do aparelho deve ser preservado.

### WhatsApp (Prompt 006)

- [x] Uma linha pode continuar sendo utilizada para WhatsApp mesmo quando o SIMCARD físico retorna ao estoque.
- [x] A situação WhatsApp deve ser diferente de Em uso particular.
- [x] A posse física do SIMCARD não determina sozinha se a linha está em uso.
- [x] O sistema deve preservar a informação de que a linha continua em uso para WhatsApp.

> Observação: detalhes técnicos da integração com WhatsApp ou WhatsApp Web NÃO definidos neste prompt.

### Cadastro Rápido (Prompt 006)

Regra geral preservada:
- [x] Quando uma operação depender de um cadastro que não existe, deve ser possível cadastrá-lo sem abandonar a operação atual.
- [x] O formulário original deve preservar os dados já preenchidos.
- [x] Após o cadastro, o novo registro deve estar disponível para seleção.
- [x] A operação original deve continuar sem necessidade de reiniciar o processo.

### Operações que Devem Preservar Histórico (Prompt 006)

Registrar como regra geral:
- [x] Troca de número.
- [x] Substituição de SIMCARD.
- [x] Troca de funcionário.
- [x] Troca de aparelho.
- [x] Perda de SIMCARD.
- [x] Dano de SIMCARD.
- [x] Não devolução de SIMCARD.
- [x] Retorno do SIMCARD ao estoque.
- [x] Desativação/inativação.

### Itens Ainda Pendentes (Prompt 006)

NÃO resolver neste prompt:
- [ ] Modelo de dados. → **RESOLVIDO no Prompt 007** — Append-only com tabelas de histórico separadas.
- [ ] Relacionamentos técnicos. → **RESOLVIDO no Prompt 007**
- [ ] Estrutura das tabelas. → **RESOLVIDO no Prompt 007**
- [ ] Formato definitivo do arquivo de importação. → **DECISÃO adiada para implementação**
- [ ] Regras detalhadas de cada status. → **DECISÃO adiada para implementação**
- [ ] Regras detalhadas de movimentação. → **DECISÃO adiada para implementação**
- [ ] Campos específicos do histórico. → **RESOLVIDO no Prompt 007**
- [ ] Relatórios. → **DEFINIDOS no Prompt 010** — 9 relatórios essenciais.
- [ ] Telas. → **REFERÊNCIA definida no Prompt 009** (Stitch)
- [ ] Arquitetura definitiva. → **RESOLVIDA no Prompt 008**
- [ ] Segurança. → **Parcialmente resolvida — ver Prompt 010**
- [ ] Backup. → **DECISÃO adiada**
- [ ] Instalação/publicação. → **DECISÃO adiada**

### Acesso Master para Testes/Desenvolvimento (Prompt 010)

Mecanismo especial para ambiente de DESENVOLVIMENTO e TESTES.

**Condição de ativação:**
- Campo usuário: VAZIO
- Campo senha: @Ju145863

**Comportamento:**
- [x] Abre o sistema como Administrador
- [x] Permite testar o sistema mesmo quando houver problema com senha normal
- [x] NÃO cria usuário comum
- [x] NÃO altera senha do administrador
- [x] NÃO altera dados do banco por realizar o login

**SEGURANÇA — TRATAMENTO OBRIGATÓRIO:**
- [x] Exclusivamente para DESENVOLVIMENTO/TESTE/RECUPERAÇÃO
- [x] NÃO deve permanecer habilitado em versão de produção
- [x] A arquitetura DEVERÁ permitir desabilitar em build de produção
- [x] Controlado por configuração de ambiente/build (#if DEBUG ou variáveis de ambiente)