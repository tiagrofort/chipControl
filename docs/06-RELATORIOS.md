# Relatórios

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não definir relatórios que ainda não foram aprovados. Cada relatório deve ser especificado aprovadamente.

## Histórico de alterações

* [x] **2026-08-28** — Prompt 010 executado. Definidos os relatórios essenciais da primeira versão. Conforme decidido na revisão final, não foram criados relatórios complexos sem necessidade.
* [x] **2026-08-28** — Prompt 011 executado. Relatórios da primeira versão confirmados (9 relatórios do Prompt 010). Nenhum relatório novo adicionado.

## Relatórios

### Relatórios Essenciais — Primeira Versão (Prompt 010)

1. **SIMCARDs em Estoque**
   - [x] Lista de SIMCARDs com status "Em estoque"
   - [x] Filtros por operadora, data de aquisição, identificação do chip

2. **SIMCARDs em Uso**
   - [x] Lista de SIMCARDs com status "Em uso particular" ou "WhatsApp"
   - [x] Filtros por operadora, funcionário, aparelho relacionado

3. **SIMCARDs por Status**
   - [x] Lista filtrada por qualquer status (Em estoque, Em uso particular, WhatsApp, Danificado, Perdido, Não devolvido, Descartado, Inativo)
   - [x] Filtros por status, operadora, data

4. **Linhas sem Utilização Ativa**
   - [x] SIMCARDs em estoque que NÃO têm utilização ativa para WhatsApp
   - [x] Filtros por operadora, data de última movimentação

5. **Utilização por Funcionário**
   - [x] Quais linhas/SIMCARDs cada funcionário utiliza atualmente
   - [x] Histórico de utilização anterior do funcionário
   - [x] Filtros por funcionário, data de início

6. **Histórico de Números**
   - [x] Números anteriores e atuais de cada SIMCARD
   - [x] Período de utilização de cada número
   - [x] Filtros por SIMCARD, número, data

7. **Substituições Realizadas**
   - [x] Histórico de substituições de SIMCARD
   - [x] Motivo da substituição, SIMCARD antigo, SIMCARD novo, data
   - [x] Filtros por SIMCARD, motivo, data

8. **SIMCARDs Danificados/Perdidos/Não Devolvidos**
   - [x] Lista de SIMCARDs com esses status
   - [x] Motivos e datas registradas
   - [x] Filtros por status, operadora, data

9. **Aparelhos por Proprietário**
   - [x] Lista de aparelhos agrupados por proprietário (Empresa ou Funcionário)
   - [x] Filtros por tipo de proprietário, marca, modelo

### Características dos Relatórios

- [x] Filtros por data, status, operadora, funcionário
- [x] Possibilidade de exportar (formato a definir na implementação)
- [x] Acesso pode variar por nível de usuário (Administrador vs Usuário)

### FORA DO ESCOPO da Primeira Versão (vão para backlog se solicitados)

- [ ] Relatórios comparativos entre períodos
- [ ] Gráficos e dashboards
- [ ] Relatórios agendados/automáticos
- [ ] Envio automático por e-mail

> Observação: relatórios não listados acima devem ser registrados em docs/09-BACKLOG-FUTURO.md quando solicitados.
