# 🏫 Edu Connect — API Back-End

![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=fff)
![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff)

## 📘 Descrição

A **Edu Connect API** é o back-end responsável por toda a camada de dados e lógica do sistema escolar **Edu Connect**.\ Ela fornece endpoints seguros para autenticação, gerenciamento de usuários, turmas, presença, calendário escolar e muito mais.

Desenvolvida em **C# com .NET**, a API foi projetada para ser **escalável, organizada, segura e de fácil manutenção**.

## 🧰 Tecnologias Utilizadas

### 🖥️ Linguagem e Framework

- [C#](https://dotnet.microsoft.com/pt-br/languages/csharp)
- [.NET](https://dotnet.microsoft.com/pt-br/)

### 🛢️ Banco de Dados 

- [SQL Server](https://www.microsoft.com/pt-br/sql-server/)

### 🔐 Autenticação

- [JWT](https://www.jwt.io/)

### 🧹 Qualidade e Organização

- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [Mapeamento via Models]()
- [Controllers Rest]()

## ✨ Funcionalidades

- **Autenticação JWT** (login e controle de acesso)
- **CRUD de usuários** (alunos, professores, administradores)
- **CRUD de turmas**
- **Gerenciamento de presença**
- **Integração com calendário escolar**
- **Estrutura modular e extensível**

## ⚙️ Instalação e Execução

``` bash
# Clone o repositório
git clone https://github.com/DevPeress/EduConnect-API
cd EduConnect-API

# Restaure as dependências
dotnet restore

# Execute a aplicação
dotnet run
```

## 🗂 Estrutura do Projeto

```
📁 EduConnect-API
  ┣ 📂 Controllers        # Endpoints da API
  ┣ 📂 Entities           # Modelos de dados (entidades)
  ┣ 📂 Services           # Regras de negócio e lógica
  ┣ 📂 Interfaces         # Interfaces para serviços e contratos
  ┣ 📂 Data               # Configurações de banco, migrations e contexto
  ┣ 📂 DTOs               # Objetos de transferência de dados
  ┣ 📄 Program.cs         # Inicialização da API
  ┣ 📄 appsettings.json   # Configurações (DB, JWT, etc.)
```
