# Tecnologias

## API

<img src="https://w7.pngwing.com/pngs/240/85/png-transparent-c.png" height="40" />

**C#** Esta API foi desenvolvida em .NET (C#).

<img src="https://static-00.iconduck.com/assets.00/swagger-icon-1024x1024-09037v1r.png" width="30" height="30" />

**Swagger** - A API usa Swagger para uma melhor análise dos eventos e disparos.

<img src="https://seeklogo.com/images/R/react-logo-65B7CD91B5-seeklogo.com.png" height="30" />

**ReactJS** - A API usar ReactJS para uma interface amigável e intuitiva.

## Infraestrutura

<img src="https://www.proficom.de/blog//app/uploads/2018/09/Docker.png" style="margin-left: -12px" height="30" />

**Docker** - É necessário Docker para rodar o banco de dados deste projeto.

# 🔒 Sistema de Gerenciamento de Usuários (SGU)

## Requisitos funcionais

    - Atualizar o banco em tempo real com dados vindos da API (.NET).
    - Interação com a interface amigável.
    - Fácil manipulação de eventos (CRUD).

## Regras de negócio

    - GET /users: Retorna a lista de todos os usuários.
    - GET /users/{id}: Retorna os detalhes de um usuário específico.
    - POST /users: Cria um novo usuário. (Valida que o nome e o email não podem ser vazios e que o email é único)
    - PUT /users/{id}: Atualiza as informações de um usuário.
    - DELETE /users/{id}: Remove um usuário pelo ID.

# Instalação do Servidor Local

### Certifique-se de ter instalado

- Docker
- .NET
- Git
- NodeJS (npm)

### Primeiro passo

Abra seu terminal e clone este projeto

```shell
git clone https://github.com/RODRIGO20031112/DOTNET-REACTJS-CRUD.git
```

Inicie a imagem do SQL Server no docker

```shell
docker pull mcr.microsoft.com/mssql/server
```

Feito isso inicie o container

```shell
docker run --name <NOME_CONTAINER> -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Rodrigo12345" -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

- String para conexão: **Server=localhost,1433;Database=db_server;User ID=sa;Password=Rodrigo12345**

Depois de rodar os comandos para construção do banco, inicie a construção, para isso acesse a raiz do projeto em /server e execute no terminal

```shell
dotnet ef database update
```

Feito isso você pode iniciar o microserviço

```shell
dotnet run
```

Caso o aplicativo falhe execute

```shell
dotnet restore

dotnet build

dotnet publish
```

E rode novamente

- Lembre-se que o appsettings.json está configurado com as opções citadas aqui nessa documentação, se você mudar a senha, usuário ou banco terá que mudar lá também.

- Rota padrão: **http://localhost:5151/swagger/index.html** (Documentação não amigável)

# Instalando servidor client ReactJS

### Na raiz do projeto acesse /client

Em seguida execute os comandos

- ### Baixa as dependências

### `npm install`

- ### Inicia o app

### `npm start`

### Pronto, basta agora acessar a rota http://localhost:3000 para a interface amigável do ReactJS.
