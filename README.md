<h1>ClientesTGS</h1> 

> Status do Projeto: :heavy_check_mark:

### Tópicos 

:small_blue_diamond: [Descrição do projeto](#descrição-do-projeto)

:small_blue_diamond: [Solicitação do Cliente](#solicitação-do-cliente)

:small_blue_diamond: [Funcionalidades](#funcionalidades)

:small_blue_diamond: [Pré-requisitos](#pré-requisitos)

:small_blue_diamond: [Tecnologias implementadas](#tecnologias-implementadas)

:small_blue_diamond: [Arquitetura](#arquitetura)

## Descrição do projeto 

<p align="justify">
  Projeto de uma API para controle de Clientes. Para poder utilizar alguns endpoints da API é preciso que estejam autenticados com dados de usuários, apos a autenticação é 
  possível ter acesso a todas as funcionalidades da API, passando sempre o token recebido na autenticação no header das requisições.
</p>

## Solicitação do Cliente

<ul>
  <li>Deve ser possível criar, atualizar, visualizar e remover Cliente.</span></li>
  <ul>
    <li><span>O cadastro dos clientes deve conter apenas os seguintes campos:</span></li>
    <li><span>Nome</span></li>
    <li><span>e-mail</span></li>
    <li><span>Logotipo;</span></li>
    <li><span>Logradouro, Um cliente pode conter vários logradouros</span></li>
    <li><span>Um cliente não pode se registrar duas vezes com o mesmo endereço de e-mail</span></li>
    <li><span>Deve ser possível criar, atualizar, visualizar e remover os logradouros</span></li>
	<li><span>O acesso à API deve ser aberto ao mundo, porém deve possuir autenticação e autorização</span></li>
	<li><span>A API terá um grande volume de requisições então tenha em mente que a preocupação com performance é algo que temos constantemente preocupação</span></li>
  </ul>
</ul>

## Funcionalidades

:heavy_check_mark: Cadastro de Clientes: É possível realizar cadastros de clientes, para cada cliente cadastrado ira gerar um usuario para manutenção dos dados

:heavy_check_mark: Cadastro de Logradouros: É possível realizar cadastros de logradouros para um determinado cliente

:heavy_check_mark: Autenticação: É possível se autenticar na API e alterar senha depois de autenticado

## Pré-requisitos

Caso for rodar o projeto localmente
<ul>
  <li>:warning: .NET Core SDK 6.0</li>
  <li>:warning: SQL SERVER</li>
  <li>:warning: Visual Studio 2022 ou VS Code</li>
  </ul>

## Como rodar a aplicação :arrow_forward:

Abra um terminal e clone o projeto: 

```
git clone https://github.com/herbertSampaio/teste_thomasgregandsons.git
```

<b>Rodar projeto localmente via terminal</b>
<ul>
  <li>Vá até a pasta src\Clients\Presentantion\WebAPI abra o arquivo appsettings.json e altere os dados da string da conexão DefaultConnection com os dados de acesso ao SQL</li>
  <li>Abra um terminal e navegue até a pasta src\Clients\Presentantion\WebAPI</li>
  <li>Fazer restore do nuget executando o comando dotnet restore</li>
  <li>Fazer a execucao do projeto com o comando dotnet run</li>
  <li>Será feito a compilação da API, no final será exibido à URL para abrir a API, acesse a url em um browser passando no final da urm o link /swagger/index.html</li>
</ul>
<ul>
  <li>No browser cole uma das URLs</li>
  <li><a href="https://localhost:7094/swagger/index.html" target="_blank">href="https://localhost:7094/swagger/index.html</a></li>
  <li><a href="http://localhost:5093/swagger/index.html" target="_blank">http://localhost:5039/swagger/index.html</a></li>
  <li>Será exibido o Swagger da API com os endpoints criados</li>
</ul>

<b>Rodar projeto localmente via VS2022</b>
<ul>
  <li>Também é possível executar o projeto pelo visual studio 2022</li>
  <li>Vá até a pasta src\Clients e abra a solution Clients.sln</li>
  <li>Faça restore dos pacotes nugets</li>
  <li>Marque o projeto WebApi como startup project</li>
  <li>Marque o projeto WebApi como startup project</li>
  <li>Abra o arquivo appsettings.json e altere os dados da string da conexão DefaultConnection com os dados de acesso ao SQL</li>
  <li>Execute o projeto com F5 ou Ctrl+F5</li>
  <li>Será exibido o Swagger da API com os endpoints criados</li>
</ul>
<br>

## Resumo
<ul>
<li>Os endpoint api/v1/auth e api/v1/cliente são do tipo post e não precisam estar autenticados para utilização, os demais endpoints precisam de autenticação</li>
<li>É preciso fazer o cadastrado do Cliente para ser gerado um usuario, ao cadastrar o usuario o retorno será a senha inicial para pode se autenticar</li>
<li>Após autenticar será gerado um token com duração de 90 minutos, com esse token é possivel acessar os outros endpoints</li>
<li>Os endpoints de Address são para mantenção de logradouros, onde é preciso informar o Cliente, nesses endpoints é possível adiconar um novo Logradouro,
	excluir um Logradouro existente, buscar um Logradouro e buscar todos logradouros de um Cliente e atualizar um Logradouro</li>
<li>Os demais endpoints de Cliente são para mantenção do mesmo, onde é possível buscar as informações do Cliente, atualizar as informações do Cliente e excluir
	os dados do Cliente, na exclusão será deletado os logradouros e usuario vinculado ao Cliente</li>
<li>No endpoint api/v1/auth/update-password é possível alterar a senha do usuario, mas para isso é preciso estar autenticado</li>
<li>FluentValidation</li>
</ul>

## Tecnologias implementadas
<ul>
<li>ASP.NET Core 6.0</li>
<li>ASP.NET WebApi Core</li>
<li>Entity Framework</li>
<li>Dapper</li>
<li>SQL Server</li>
<li>Swagger UI</li>
<li>Migrations</li>
<li>FluentValidation</li>
<li>Filters exceptions</li>
</ul>

## Arquitetura
<ul>
  <li>Arquitetura com separação de responsabilidade SOLID e Clean Code</li>
  <li>Design orientado por domínio (camadas e padrão de modelo de domínio)</li>
  <li>Eventos de domínio</li>
  <li>Notificação de domínio</li>
  <li>Repositório</li>
  <li>Injeção de dependência</li>
</ul>

## Segurança
<ul>
  <li>Tokens de autorização com JWT Bearer</li>
</ul>

## Licença 

The [MIT License]() (MIT)

Copyright :copyright: 2023 - ClientesTGS
