# Manutec – Sistema de Gestão de Manutenções Veiculares

A **Manutec** é uma aplicação web desenvolvida com **.NET 8**, focada no gerenciamento de oficinas mecânicas. O projeto adota uma arquitetura em camadas e os princípios do **Domain Driven Design (DDD)**, permitindo o controle completo de clientes, veículos, serviços e manutenções.

A solução oferece **CRUD completo** para usuários, clientes, veículos, serviços e manutenções, além de recursos como filtros, geração de relatórios e autenticação por perfil. Ideal para oficinas que desejam modernizar sua gestão de forma simples e eficaz.

---

## 🛠 Tecnologias e Padrões Utilizados

- ✔️ **.NET 8** com ASP.NET Core MVC
- ✔️ **Entity Framework Core** com banco de dados SQL Server
- ✔️ **MediatR** para implementar CQRS (Command & Query Responsibility Segregation)
- ✔️ **FluentValidation** para validações limpas e reutilizáveis
- ✔️ **Arquitetura em camadas com DDD**
- ✔️ **Swagger** (caso queira expor como API futuramente)
- ✔️ **HostedService** para tarefas agendadas (background)
- ✔️ **TempData + Bootstrap** para feedback visual
- ✔️ **Rotas customizadas e tratamento de exceções**

---

## 📦 Funcionalidades

- ✅ **Cadastro de Clientes**
- ✅ **Cadastro de Veículos**, com busca por **placa**
- ✅ **Registro e controle de manutenções**
- ✅ **Cadastro de tipos de serviço**
- ✅ **Usuários com diferentes papéis**: Administrador, Recepcionista, Mecânico
- ✅ **Autenticação e Autorização**
- ✅ **Busca por nome e placa**
- ✅ **Feedback visual com TempData**
- ✅ **Concluir manutenção com data e km**
- ✅ **Geração de relatórios em PDF**
- ✅ **Envio de E-mail informando próxima manutenção por quilometragem**

---

## 📁 Estrutura do Projeto

```bash
Manutec.sln
│
├── Manutec.MVC               # Camada Web (Controllers, Views, Models, Reports, ExceptionHandler)
├── Manutec.Application       # Commands, EventHandlers, Events, Helpers, Models, Queries, Services, Validators
├── Manutec.Core              # Entities, Enums, Repositories
├── Manutec.Infrastructure    # Auth, Migrations, Persistence, Repositories, Services
```

## 💻 Requisitos
- Visual Studio 2022+ ou Visual Studio Code
- .NET 8 SDK
- SQL Server Express ou SQLite
- (Opcional) MySQL caso queira adaptar

## ▶️ Como rodar o projeto

```bash
git clone git@github.com:benicio227/Manutec-Version-MVC.git
```
1 - Abra a solução Manutec.sln no Visual Studio
2 - Configure a connection string no appsettings.Develoment.json
3 - Aplique as migratinons:
```bash
dotnet ef database update
```
4 - Rode o projeto (F5 ou dotnet run)

## 👨‍💻 Autor
Benício Brandão
- Desenvolvedor Backend C# | .NET
- 📧 beniciobrandao@hotmail.com
- 🔗 www.linkedin.com/in/benicio-brandao
- 🐙 benicio227
