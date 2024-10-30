![balta](https://baltaio.blob.core.windows.net/static/images/dark/balta-logo.svg)

## 🎖️ Desafio
**Caça aos Bugs 2024** é a sexta edição dos **Desafios .NET** realizados pelo [balta.io](https://balta.io). Durante esta jornada, fizemos parte da equipe __NOME_DA_BANDA__ onde resolvemos todos os bugs de uma aplicação e aplicamos testes de unidade no projeto.

## 📱 Projeto
Depuração e solução de bugs, pensamento crítico e analítico, segurança e qualidade de software aplicando testes de unidade.

## Participantes
### 🚀 Líder Técnico
[Guilherme Bley](https://github.com/GuilhermeBley)

### 👻 Caçadores de Bugs
* [Jorge Lima](http://github.com/CastionDev)
* [Matheus Sanches](https://github.com/MatheusSanches02)
* [Valmir Silva](https://github.com/vmrsilva)
* [Vitor Galache](https://github.com/vitor-galache)

## ⚙️ Tecnologias
* C# 12
* .NET 8
* ASP.NET
* Minimal APIs
* Blazor Web Assembly
* xUnit

## 🥋 Skills Desenvolvidas
* Comunicação
* Trabalho em Equipe
* Networking
* Muito conhecimento técnico

## 🧪 Como testar o projeto

Nesse tópico vai ser abordado como executar localmente o projeto `Dima.Api` e `Dima.Web`. 

É necessário possuir as seguintes ferramentas para execução local:

- .NET 8
- Docker

### Criando banco de dados com Docker

Nessa etapa você vai subir a imagem do SQL Server, caso deseje alterar alguma configuração do banco de dados, va no arquivo `...\desafio-caca-aos-bugs\bugs\docker-compose.yml`

Certifique-se de estar no diretório `...\desafio-caca-aos-bugs\bugs` e execute o seguinte comando no console:

```bash
docker-compose up -d
```

Seu banco de dados agora está rodando localmente.

### Adicione as configurações de desenvolvimento

Va no arquivo `...\desafio-caca-aos-bugs\bugs\Dima.Api\appsettings.json`, comece inserindo um valor para o campo `ConnectionStrings:DefaultConnection`, esse valor deve ser de acordo com o banco de dados criado no passo anterior.
Caso o docker-compose não tenha sido alterado, as configurações serão as seguintes:

```json
{
  "FrontendUrl": "http://localhost:5028",
  "BackendUrl": "http://localhost:5164",
  "ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver,1433;Database=dima-dev;User Id=sa;Password=Secret123!"
  },
  "StripeApiKey": "",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Inserindo dados iniciais ao banco de dados

Após arquivo de configuração alterado, deve ser executado no console os seguintes comandos.

Caso não possua o Entity Framework tools instalado na máquina, execute o seguinte comando:
```bash
dotnet tool install --global dotnet-ef
```

Após, basta utilizar o comando para criar as tabelas:
```bash
dotnet ef database update
```

Partindo para o banco de dados, com um SGDB você deve acessar o database `dima-dev` e executar os comandos contidos em `desafio-caca-aos-bugs\bugs\Dima.Api\Data\Scripts\seed.sql` e `desafio-caca-aos-bugs\bugs\Dima.Api\Data\Scripts\views.sql`.

### Executando API e UI

Nessa etapa vai ser executado as aplicações, onde será necessário deixa aberto dois terminais.

Vá até a pasta `...\desafio-caca-aos-bugs\bugs` e execute os seguintes comandos (linha por linha) no terminal 1:

```bash
dotnet clean
dotnet restore
dotnet build
```

No mesmo terminal, vá até a pasta `...\desafio-caca-aos-bugs\bugs\Dima.Web` e execute os seguintes comandos para executar a UI:

```bash
dotnet run
```

Vá até a pasta `...\desafio-caca-aos-bugs\bugs\Dima.Api` e execute os seguintes comandos no terminal 2 para executar a API:

```bash
dotnet run
```

Pronto! Suas aplicações estão rodando localmente, basta verificar no terminal 1 a URL que está disponível a UI.

# 💜 Participe
Quer participar dos próximos desafios? Junte-se a [maior comunidade .NET do Brasil 🇧🇷 💜](https://balta.io/discord)
