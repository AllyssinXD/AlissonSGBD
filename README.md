# 📄 AlissonSGBD - Sistema Gerenciador de Banco de Dados (SGBD)

O **AlissonSGBD** é um projeto de Sistema Gerenciador de Banco de Dados (SGBD) relacional, em desenvolvimento, com forte inspiração nos princípios e sintaxe do MySQL. O foco principal é em uma arquitetura limpa, seguindo padrões de engenharia de software (SOLID) e alta segurança, garantindo criptografia e persistência em formato binário.

## ✨ Status e Progresso Atual

| Categoria | Funcionalidade | Status |
| :--- | :--- | :--- |
| **Análise Léxica** | Lexer básico implementado | ✅ Completo |
| **Análise Sintática** | Parser com geração de Árvore de Sintaxe Abstrata (AST) para `SELECT` | 🏗️ Em Progresso |
| **Core** | Estrutura de persistência em arquivos binários (RNF1) | 🏗️ Em Progresso |
| **Segurança** | Criptografia para dados em repouso e em trânsito (RNF2) | 💡 Planejado |

---

## 🔑 Requisitos Funcionais (RFs)

O projeto visa atingir as seguintes capacidades essenciais:

| ID | Requisito Funcional | Descrição |
| :--- | :--- | :--- |
| **RF0** | **CRUD Completo** | Capacidade de Criar, Ler, Atualizar e Deletar (CRUD) bancos de dados, tabelas (entidades), colunas (atributos) e registros (inserções). |
| **RF1** | **Login e Permissões** | Sistema robusto de autenticação e autorização para controle granular de acesso a diferentes recursos e operações. |
| **RF2** | **Relacional** | Suporte a relacionamentos entre tabelas (Ex: Chaves Estrangeiras), garantindo a integridade referencial. |
| **RF3** | **Gerenciamento SQL** | Todas as operações de gerenciamento e manipulação de dados realizadas exclusivamente através de *Queries* no padrão SQL. |
| **RF4** | **Transactions (ACID)** | Suporte a transações, garantindo que as operações complexas sejam Atômicas, Consistentes, Isoladas e Duráveis. |

---

## 🔒 Requisitos Não Funcionais (RNFs)

A qualidade e segurança do sistema são regidas pelos seguintes requisitos:

| ID | Requisito Não Funcional | Descrição |
| :--- | :--- | :--- |
| **RNF0** | **Arquitetura S.O.L.I.D** | O código-base deve aderir rigorosamente aos princípios S.O.L.I.D. para garantir alta manutenibilidade, testabilidade e extensibilidade. |
| **RNF1** | **Arquivos em Binário** | O armazenamento persistente dos dados será realizado em formato binário otimizado para melhorar o desempenho de I/O em comparação a formatos textuais. |
| **RNF2** | **Criptografia** | Implementação de criptografia tanto para o transporte de dados em rede (segurança de comunicação) quanto para o salvamento de dados em disco (segurança em repouso). |

---

## ⚙️ Arquitetura de Execução de Query

A execução de uma query SQL no AlissonSGBD segue as seguintes camadas:

1.  **Lexer (Análise Léxica):** Converte a *query* de entrada em uma sequência de tokens (e.g., `SELECT` $\rightarrow$ `KEYWORD`, `nome` $\rightarrow$ `IDENTIFIER`).
2.  **Parser (Análise Sintática):** Utiliza os tokens para construir a **Árvore de Sintaxe Abstrata (AST)**, garantindo a correção gramatical da query.
3.  **Analisador Semântico:** Verifica a validade lógica (existência de tabelas, tipos de dados) e as permissões de usuário (RF1).
4.  **Otimizador:** Refatora a AST para determinar o plano de execução mais eficiente.
5.  **Executor:** Interage com o motor de armazenamento para realizar as operações nos datafiles binários (RNF1).

---

## 🚀 Como Contribuir

**(Insira aqui instruções específicas para configurar e rodar o projeto)**

Para começar a contribuir:

1.  Clone o repositório:
    ```bash
    git clone [https://github.com/SeuUsuario/AlissonSGBD.git](https://github.com/SeuUsuario/AlissonSGBD.git)
    ```
2.  Navegue até a pasta do projeto:
    ```bash
    cd AlissonSGBD
    ```
3.  (Ex: Instrução de compilação C#)
    ```bash
    dotnet build
    ```

Sua contribuição é muito bem-vinda! Sinta-se à vontade para abrir **Issues** ou enviar **Pull Requests**.
