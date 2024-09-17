# Google OAuth Integration (.NET Backend + Angular Frontend)

Este repositório contém uma implementação de integração com o OAuth do Google, utilizando um backend em .NET e um frontend em Angular.

## Visão Geral

O projeto demonstra como implementar a autenticação do Google OAuth 2.0 em uma aplicação web full-stack. Ele permite que os usuários façam login usando suas contas do Google e acessem recursos protegidos na aplicação.

## Estrutura do Projeto

- `/backend`: Contém o código do servidor .NET
- `/oauth-frontend`: Contém o código do cliente Angular

## Pré-requisitos

- .NET 6.0 SDK ou superior
- Node.js 14.x ou superior
- Angular CLI 12.x ou superior
- Conta de desenvolvedor do Google e credenciais OAuth configuradas

## Configuração

### Backend (.NET)

1. Navegue até o diretório `/backend`
2. Copie o arquivo `appsettings.example.json` para `appsettings.json`
3. Preencha as credenciais do OAuth do Google no arquivo `appsettings.json`:

```json
{
  "Google": {
    "ClientId": "SEU_CLIENT_ID_AQUI",
    "ClientSecret": "SEU_CLIENT_SECRET_AQUI"
  }
}
```

4. Execute `dotnet restore` para instalar as dependências
5. Execute `dotnet run` para iniciar o servidor

### Frontend (Angular)

1. Navegue até o diretório `/oauth-frontend`
2. Execute `npm install` para instalar as dependências
3. Atualize o arquivo `src/environment.ts` com a URL do seu backend:

```typescript
export const enviroment = {
  production: false,
  apiURL: "url da sua api",
  clientId: "seu client id"
}
```

4. Execute `ng serve` para iniciar o aplicativo Angular

## Uso

1. Acesse `http://localhost:4200` no seu navegador
2. Clique no botão "Login com Google"
3. Selecione sua conta Google e autorize o aplicativo
4. Você será redirecionado de volta para o aplicativo, agora autenticado

## Recursos Adicionais

- [Documentação do Google OAuth 2.0](https://developers.google.com/identity/protocols/oauth2)
- [Autenticação em .NET](https://docs.microsoft.com/en-us/aspnet/core/security/authentication)
- [Angular HttpClient](https://angular.io/guide/http)

## Contribuição

Contribuições são bem-vindas! Por favor, abra uma issue ou envie um pull request com suas sugestões.
