# R.O.B.O.: Portal de Simulação de Robo

O R.O.B.O é um Single Page Application desenvolvido com Angular 4 e ASP.NET Core Web API com o objetivo de ser um projeto de simulação de controle de robos.

Construído Hugo Estevam Longo - @hugoestevam.

## Tecnologias

### Server-Side

* ASP.NET Core Web API - Is a framework that makes it easy to build HTTP services. [Site oficial](http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api)
* CQRS - Command and Query Responsibility Segregation através do Framework MediatR. [Referência](https://github.com/jbogard/MediatR)
* FluentValidation -  Is a free and open-source Validation Framework. [Site oficial](https://fluentvalidation.net/)
* Repository (In-Memory) - Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.. [Referência](https://martinfowler.com/eaaCatalog/repository.html)


### Client-Side

* HTML5 - É a mais recente evolução do padrão que define o HTML. [Referência](http://www.w3schools.com/html/html5_intro.asp)
* CSS3 - Onde se define estilos para páginas web com efeitos de transição, imagem, e outros. [Referência](http://www.w3schools.com/css/css3_intro.asp)
* Angular 4 - Develop Web Applications across all plataforms. [Site oficial](https://angular.io/)
* TypeScript - Is a typed superset of JavaScript that compiles to plain JavaScript. [Site oficial](https://www.typescriptlang.org/)
* WebPack - Web Module bundler. [Site oficial](https://webpack.github.io/)

## Organização do projeto

Todos os arquivos do projeto se encontram no mesmo repositório. Os arquivos relacionados ao servidor
estão presentes dentro da pasta **Server** na raíz do projeto. Os arquivos do front-end ficam dentro
da pasta **Client**.

O projeto possui uma separação clara de camadas, na pasta **Client** existem os arquivos necessários para construção do projeto Front-end. 

Já os arquivos da pasta **Server**, que fazem parte da compilação do Back-end, estão organizados em camadas dentro da solução **robot.sln**. Segue abaixo uma breve explicação:

1. Distributed Layer ou Camada de Distribuição
	
    * Nesta camada devem ser desenvolvidos os projetos que são responsáveis
	 pela distribuição de dados e comunicação com a camada de apresentação. 

	* Tipos de Projeto: Web API, SignalR, Windows Service.
2. Application Layer ou Camada de Aplicação	
	
    * A orquestração das chamadas que compõem uma Feature devem ser  implementadas nesta camada. 

    * Camada responsável por definir os Serviços de Orquestração (AppServices). 

    * Camada responsável pela implementação dos Manipuladores (Handlers). 

    * Camada responsável pela implementação dos formatos Command e Query.

3. Domain Layer ou Camada de Dominio
	
    * Nesta camada devem ser desenvolvidos os objetos de negócio
	
    * Objetos que representam o domínio lógico da aplicação.
	
    * Interfaces de acesso aos repositórios de dados. 

4. Infrastructure Layer ou Camada de Infraestrutura
    
    * Nesta camada devem ser desenvolvidos os objetos que controlam o 
	acesso a dados, incluindo conexões com a base de dados e comandos.
	
    * As implentações das interfaces que definem o acesso aos bancos de 
	dados são implementados aqui: (classes para 
	SQL Server, classes para acesso In-Memory, classes para EF, classes 
	para NoSQL).
	   

## Dependências

As dependências do front-end são gerenciadas utilizando o NPM e as do back-end com o Nuget.

As instruções a seguir assumem que você utiliza o **Windows** como o sistema operacional.

1. Instalar o Git

    Para instalar o Git é necessário fazer o download do instalador [disponível aqui](https://git-for-windows.github.io/).
    Utilizar as configurações padrão do instalador é a melhor opção.

1. Instalar o NPM

    O NPM é principalmente um gerenciador de pacotes. O projeto possui um arquivo **package.json** no qual é listado
    todas as dependências do projeto. O R.O.B.O depende de vários pacotes mantidos pela comunidade,
    como o próprio angular e o bootstrap.

    O NPM é construído com base no Node.JS e é necessário instalar o Node.JS para utilizá-lo. Como o NPM é o principal
    gerenciador de pacotes do ecossistema Node, ele vem junto com a instalação do Node no Windows.

    [Obtenha o instalador do Node.JS aqui](https://nodejs.org/en/).

    **Instale a versão 6 ou maior**.

Instalando o Git e o Node você poderá adquirir e executar o projeto.

Para alterar o Front-end recomendo a utilização do **Visual Studio Code**, já para alterar ou executar a API é necessário o **Visual Studio 2017**.

### Instalar dependências do front-end

Agora é necessário instalar as dependências do front-end através do NPM.

Vamos instalar as dependências do projeto. Navegue até a pasta onde o repositório foi
baixado, entre na pasta **Client** que é onde estão os arquivos do Front-End e execute o seguinte comando:

```bash
> npm install
```

Com esse comando todas as dependências listadas no package.json do projeto serão instaladas na pasta node_modules.

### Instalar dependências do back-end

Ao abrir a solução do projeto existente na pasta **Server**, o Nuget é responsável por restaurar as dependências quando
o build da solution ocorrer. **Utilize a versão 2.7 ou superior do Nuget.**

* Esse projeto usa Kestrel e ASP.NET Core Web Api.
* No Visual Studio, faça um build na solution para instalar os pacotes do Nuget.
* Depois escolha o projeto **robot.WebApi** e marque-o como **Startup Project** e inicie a aplicação, o projeto irá iniciar em um servidor no IIS Express no endereço **http://localhost:9000**

Por padrão, a API irá abrir o **Swagger**, que dará toda a visão de funcionamento da API, bem como sua documentação.

## Executar o projeto

Por padrão, não há necessidade de mudar as configurações do front-end com o endereço correto da API. Isso já está definido no arquivo **client/src/app/shared/robot.service.ts**. Mas, caso seja necessário alteração para algum tipo de teste, basta mudar para o endereço desejado.

Entendo essas configurações da API pode-se então, executar o projeto utilizando alguma das tasks à seguir.

## Tasks do NPM

### Desenvolvimento

```bash
> npm start
```

Esse comando compila a aplicação seguindo as regras definidas para um ambiente de desenvolvimento e deixa ela disponível na
porta 8000 da máquina local (http://localhost:8000).

Esse servidor de desenvolvimento possui hot-reload, ou seja, ao alterar os arquivos TS, HTML ou CSS ele re-compila as partes
necessárias e aplica elas no navegador da maneira que for possível, sem a necessidade de ficar apertando F5 na aplicação sempre
que realiza alguma alteração.

### Build

```bash
> npm build
```

Esse comando irá realizar o build da aplicação com as configurações de produção e criará uma pasta **dist** com o front-end compilado.

## FIM

Qualquer dúvida entre em contato comigo!
Obrigado.

