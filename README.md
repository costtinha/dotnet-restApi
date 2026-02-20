# 🏢 API de Gerenciamento de Escritórios e Funcionários (.NET 8)

API REST em **.NET 8 + C#** para gerenciamento de **escritórios (Offices)** e **funcionários (Employees)**, inspirada no schema clássico **ClassicModels**.

Foco em uma arquitetura limpa, uso correto de camadas (Controllers → Services/Repositories → EF Core), mapeamento automático com **AutoMapper**, validação de modelos e tratamento básico de erros.

---

## 📋 Índice

- [Sobre o projeto](#-sobre-o-projeto)
- [Tecnologias](#-tecnologias)
- [Funcionalidades principais](#-funcionalidades-principais)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação e Execução](#-instalação-e-execução)
- [Endpoints da API](#-endpoints-da-api)
- [Exemplos de Requisições](#-exemplos-de-requisições)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Decisões & Aprendizados](#-decisões--aprendizados)
- [Melhorias sugeridas](#-melhorias-sugeridas)
- [Licença](#-licença)

---

## 🚀 Sobre o projeto

API backend simples e organizada para gerenciar **escritórios** e **funcionários**, com relacionamento 1:N (um escritório pode ter vários funcionários).

Ideal para portfólio, estudos de .NET moderno, entrevistas técnicas ou como base para projetos mais complexos (adicionar autenticação, paginação, cache, etc.).

---

## 🛠 Tecnologias

| Tecnologia            | Versão    | Finalidade principal                             |
|-----------------------|-----------|--------------------------------------------------|
| .NET                  | 8.0       | Plataforma / Runtime                             |
| ASP.NET Core          | 8.0       | Framework Web API                                |
| Entity Framework Core | 8.x       | ORM + migrations                                 |
| AutoMapper            | 12+       | Mapeamento objeto → DTO                          |
| PostgreSQL            | 15+       | Banco de dados relacional                        |
| Swagger / OpenAPI     | —         | Documentação interativa                          |
| Docker                | —         | (opcional) containerização                       |

---

## ✨ Funcionalidades principais

- CRUD completo para **Offices** e **Employees**
- Busca por cidade, estado, país (Offices)
- Busca por nome e por escritório (Employees)
- Validação de modelos com Data Annotations
- Mapeamento automático DTO ↔ Entity com **AutoMapper**
- Tratamento básico de erros (NotFound, BadRequest)
- Relacionamento 1:N configurado corretamente no EF Core
- Documentação automática via Swagger

---

## 📦 Pré-requisitos

- .NET 8 SDK
- PostgreSQL 15+ (ou container via Docker)
- (opcional) Docker + Docker Compose

---

## 🚀 Instalação e Execução

### 1. Com Docker (recomendado para PostgreSQL)

# 1. Clone o repositório
```bash
git clone https://github.com/costtinha/dotnet-restApi.git
cd office-api-dotnet
```
# 2. Subir PostgreSQL + API
docker compose up -d

API estará em: http://localhost:5157
Swagger: http://localhost:5157/swagger

### 2. Execução local (Sem docker)
# 1. Clone o projeto
```bash
git clone https://github.com/SEU_USUARIO/office-api-dotnet.git
cd office-api-dotnet
```
# 2. Criar e aplicar migrations (se necessário)
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
# 3. Rodar a API
```bash
dotnet run
```
---------------------------------------------------------------------------------------------------------
## Endpoints da API

### Offices

| Metodo   | EndPoint                    | Descrição                                     | Autenticação |
|----------|-----------------------------|-----------------------------------------------|--------------|
| `GET`    | `/api/Office`               | Lista todos os escritorios                    | Não          |
| `GET`    | `/api/Office/{id}`          | Busca escritório por ID                       | Não          |
| `GET`    | `/api/Office/local/{local}` | Busca Escritório por local(city,state/country | Não          |
| `POST`   | `/api/Office`               | Salva novo escritório                         | Não          |
| `PUT`    | `/api/Office/{id}`          | Atualiza escritório                           | Não          |
| `DELETE` | `/api/Office/{id}`           | Delete escritório                             | Não          |



| Metodo   | EndPoint                          | Descrição                         | Autenticação |
|----------|-----------------------------------|-----------------------------------|--------------|
| `GET`    | `/api/Employee`                   | Lista todos os funcionarioa       | Não          |
| `GET`    | `/api/Employee/{id}`              | Busca funcionario por ID          | Não          |
| `GET`    | `/api/Employee/officecode/{code}` | Busca funcionarios por escritorio | Não          |
| `POST`   | `/api/Employee`                   | Salva novo funcionario            | Não          |
| `PUT`    | `/api/Employee/{id}`              | Atualiza funcionario              | Não          |
| `DELETE` | `/api/Employee/{id}`              | Delete funcionario                | Não          |

----------------------------------------------------------------------------------------------
# Exemplos de requisição 

### Criar um escritório

```bash
curl -X POST http://localhost:5157/api/Office \
  -H "Content-Type: application/json" \
  -d '{
    "city": "São Paulo",
    "phone": "+55 11 99999-9999",
    "address": "Av. Paulista, 1000",
    "state": "SP",
    "country": "Brazil",
    "postalCode": 01310000,
    "territory": "South America"
  }'
```

### Criar um funcionário vinculado a um escritório
```bash
curl -X POST http://localhost:5157/api/Employee \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Ana Silva",
    "officeCode": 1
  }'
```

### Listar funcionários de um escritório
```bash
curl -X GET http://localhost:5157/api/Employee/officecode/1
```

-----------------------------------------------------------------------------------------------

# Estrutura do projeto

OfficeApi/
├── Controllers/
│   ├── OfficeController.cs
│   └── EmployeeController.cs
├── Data/
│   └── OfficeDbContext.cs
├── Dtos/
│   ├── OfficeDto.cs
│   ├── OfficeResponseDto.cs
│   ├── EmployeeDto.cs
│   └── EmployeeResponseDto.cs
├── Models/
│   ├── Office.cs
│   └── Employee.cs
├── Repositories/
│   ├── OfficeRepository.cs
│   └── EmployeeRepository.cs
├── Program.cs
├── appsettings.json
└── docker-compose.yml

---
# Decisões e aprendizados
Separação clara entre Controller → Repository (poderia evoluir para Services)
Uso de AutoMapper para evitar mapeamento manual repetitivo
Tratamento de NotFound com KeyNotFoundException customizado
Validação automática via [Required] e ModelState
Configuração correta de relacionamento 1:N no OnModelCreating
Retorno de DTOs específicos para respostas (evitar expor entidades diretamente)

---

## 📄 Licença

Este projeto é de uso educacional e está disponível sob a licença [MIT](LICENSE).

---

<p align="center">
  Desenvolvido por <a href="https://github.com/costtinha">Daniel Costa</a>
</p>