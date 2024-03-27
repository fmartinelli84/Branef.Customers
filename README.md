# Branef Customers

Aplicação para cadastro de clientes.

## Compilação e Execução

Todos os pré-requisitos para compilar e executar a aplicação estão nos containers, para executar basta rodar o comando abaixo na pasta raiz da solution:

```
docker-compose up
```
Também é possível utilizar o Visual Studio 2022, basta abrir o arquivo ```Branef.Customers.sln```.

## Acesso

Segue abaixo uma descrição de cada container:

- **branef.customers.web**: Aplicação Angular, esse container executa o comando "ng serve", se algum arquivo fonte for alterado o site vai recompilar automaticamente. 
Obs, o primeiro acesso pode demorar alguns segundos, até a compilação ser concluída.
  - **Endereço**: http://localhost:55900

- **branef.customers.api**: Swagger das APIs.
  - **Endereço**: https://localhost:55901/swagger

- **branef.customers.logging**: Dashboard do Seq Log para visualização dos logs.
  - **Endereço**: http://localhost:55903

- **branef.customers.database**: Banco de Dados MS SQL Server 2022 Developer Edition.
  - **Server**: localhost:1433
  - **Database**: Customers
  - **User**: sa
  - **Password**: d93@1FfF#9756

- **branef.customers.mongodb**: Banco de Dados MongoDB Community Server.
  - **Server**: localhost:27017
  - **Database**: Customers