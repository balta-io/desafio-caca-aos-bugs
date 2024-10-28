![balta](https://baltaio.blob.core.windows.net/static/images/dark/balta-logo.svg)

## 🎖️ Desafio
**Caça aos Bugs 2024** é a sexta edição dos **Desafios .NET** realizados pelo [balta.io](https://balta.io). Durante esta jornada, fizemos parte da equipe __NOME_DA_BANDA__ onde resolvemos todos os bugs de uma aplicação e aplicamos testes de unidade no projeto.

## 📱 Projeto
Depuração e solução de bugs, pensamento crítico e analítico, segurança e qualidade de software aplicando testes de unidade.

## Participantes
### 🚀 Líder Técnico
[NOME e GITHUB]

### 👻 Caçadores de Bugs
* [NOME e GITHUB]

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
[DESCREVER COMO EXECUTAR O PROJETO]

# 💜 Participe
Quer participar dos próximos desafios? Junte-se a [maior comunidade .NET do Brasil 🇧🇷 💜](https://balta.io/discord)

# 🕵️‍♂️ Invetigando os bugs🐞 e coleta de evidências 📖
## 🐞BUG 1 - Erro na Home do App

Após realizado o passo a passo sugerido foi criado o usuário.

```text
email : testedima@email.com
pass  : D1m@D1m@
```

## 🐞BUG 2 - Erro na Home do App

Dentro do sistema, vamos abrir o DevTools (F12) e ir até a aba de Network. Foi possível constatar o bug descrito no `.pdf`.

![image](./evidencias/BUG2/bug02-001-erro_encontrado_no_network.PNG)

Agora, vamos fazer o debug do endpoint `expenses`, chegando no método `GetExpensesByReportAsync`.

![image](./evidencias/BUG2/02.001-debug.PNG)

Continuando o debug desta consulta:

![image](./evidencias/BUG2/02.002-debug.PNG)

Ao executar, encontramos a exceção.

![image](./evidencias/BUG2/02.003-debug.PNG)

Investigando dentro da solução, encontramos onde a view está mapeada.

![image](./evidencias/BUG2/02.004-debug.PNG)

Porém, ao consultar o banco, não encontramos a view.

![image](./evidencias/BUG2/bug02-002-banco_sem_views.PNG)

Analisando o código, vimos que os scripts para criação das views estão disponíveis.

![image](./evidencias/BUG2/bug02-003-scripts_das_views.PNG)

Neste ponto, poderíamos executar os scripts manualmente, mas passar essa responsabilidade para outro dev poderia se tornar um hábito custoso e fácil de esquecer, aumentando o tempo necessário para iniciar a aplicação. Por isso, decidi criar uma nova migration para incluir esses scripts e automatizar o processo.

```bash
dotnet ef migrations add CreateViews
```

E lá vamos nós subir a migration.

```bash
dotnet ef database update 
```

![image](./evidencias/BUG2/bug02-004-views_criadas.PNG)

Antes de voltar ao nosso querido debug, notei que havia uma pasta chamada Scripts com um seed.sql. Já que criamos uma migration para as views, por que não criar uma para os seeds também? 😎

```text
🚨🚨🚨🚨 
Em uma aplicação real, existem seeds que PRECISAM estar em produção (por exemplo, tabelas de tipos/types) e outros que servem apenas para desenvolvimento. Por isso, essa ação pode não ser recomendada no dia a dia e deve ser avaliada caso a caso.
🚨🚨🚨🚨 
```

Com esse disclaimer feito, vamos criar a migration para aplicar os seeds.

```bash
dotnet ef migrations add ApplySeeds
```

E lá vamos nós de novo.

```bash
dotnet ef database update 
```

Validando se as migrations foram aplicadas:

![image](./evidencias/BUG2/bug02-005-seed_aplicado.PNG)

Verificando se um dos seeds foi aplicado com sucesso:

![image](./evidencias/BUG2/bug02-006-validando_dados_inseridos.PNG)

Voltando ao DevTools na aba de Network, vimos que não temos mais erros.

![image](./evidencias/BUG2/bug02-007-sem_erros.PNG)

E na tela inicial, tudo carregou corretamente.

![image](./evidencias/BUG2/bug02-008-home.PNG)

## 🐞BUG 3 - Categorias não lista

Ao entrar na tela de categorias, recebemos a mensagem de erro descrito no documentos de bugs.

<div style"color=red"> Oject reference not set to an instance of an object.</div>

Ao verificar no DevTools, pode-se verificar que não apresenta nenhum erro.

![image](./evidencias/BUG3/bug03-001-sem_erros.PNG)

Então vamos analizar o codigo da tela, e ao abrir a IDE já está apontando um aviso da lista não instanciada.

![image](./evidencias/BUG3/bug03-002-warning_list_category.PNG)

E outro aviso que pode estar dentando inserir na coleção um objeto que pode estar vazio.

![image](./evidencias/BUG3/bug03-003-warning_addRange.PNG)

Vamos aplicar a correção de instanciar a lista que é apresentada na tela, e um tratamento para não inserir um objeto vazio e sim um lista vazia.

![image](./evidencias/BUG3/bug03-004-fiz_applied_fixes.PNG)

Agora realizando o teste novamente, pode-se ver que a pela carregou normalmente, borá para o proximo bug que o negócio está ficando quente🔥.
![image](./evidencias/BUG3/bug03-005-screen_categories.PNG)

## 🐞 BUG 4 - Erro ao Criar Categoria

Ao tentar replicar o erro de criação de uma categoria, recebemos um erro diferente do descrito, mas vamos atrás da correção!

![image](./evidencias/BUG4/bug04-001-try-replicated-error.PNG)

No projeto Web, vamos verificar o handler de criação. Aqui já chama a atenção o fato de o método HTTP estar como `PUT`. Porém, antes de alterar, vamos conferir a implementação na API.

![image](./evidencias/BUG4/bug04-002-handle_web.PNG)

Verificando a implementação na API, é possível ver que ela está como método HTTP `POST`, o que explica o erro.

![image](./evidencias/BUG4/bug04-003-mapPost.PNG)

Vamos ajustar a implementação para o método HTTP `POST`, que foi o método implementado e é o correto a ser usado por convenção para criação.

![image](./evidencias/BUG4/bug04-004-change_hanble.PNG)

Realizando um novo teste de salvamento:

![image](./evidencias/BUG4/bug04-005-save_test.PNG)

Voltando à tela de listagem, podemos ver que o nosso registro foi criado corretamente.

![image](./evidencias/BUG4/bug04-006-list_item_after_save.PNG)

Trem que pula! Bora gente, só faltam mais dois bugzinhos 🐞 para terminarmos nossa jornada.

## 🐞 BUG 5 - Transação é criada mas não aparece

Agora vamos para a parte de transação. Ao tentar replicar o erro, recebemos a mensagem de que a transação foi salva.

![image](./evidencias/BUG5/bug05-001-new_transaction.PNG)

Ao verificar o DevTools, não encontramos nenhum erro ao tentar obter a lista.

![image](./evidencias/BUG5/bug05-002-dev-tools.PNG)

Porém, ao checar o banco de dados, vemos que o nosso registro não foi salvo 😨.

![image](./evidencias/BUG5/bug05-003-check_in_database.PNG)

Ao abrir o handler de salvamento, a IDE (nossa amiga 😗) já está dando dois warnings indicando operações assíncronas sem o uso de `await`. Bora adicionar o `await` e testar novamente.

![image](./evidencias/BUG5/bug05-004-created_error.PNG)

Criando uma nova transação... agora vai!

![image](./evidencias/BUG5/bug05-005-new_transaction.PNG)

Ops, nossa transação foi salva, mas com o ID de outro usuário 😅. Bom, melhor que nada, né?

![image](./evidencias/BUG5/bug05-006_created_transaction_to_anothe_user.PNG)

Voltando ao código, vamos ajustar para preencher o `userId` corretamente com o ID do usuário da requisição.

![image](./evidencias/BUG5/bug05-007-fix_userId.PNG)

E lá vamos nós 🚂, criando outra transação.

![image](./evidencias/BUG5/bug05-008-new_transaction.PNG)

Agora a transação aparece na tela, ufa 😁.

![image](./evidencias/BUG5/bug05-009-list_ok.PNG)

Verificando no banco de dados, podemos confirmar que o registro agora está associado ao nosso `userId`.

![image](./evidencias/BUG5/bug05-010-check_database.PNG)

## 🐞 BUG 6 - Atualização de uma transação não funciona

Chegamos no último chefão! Bora acabar com ele 💪.

Ao tentar editar o registro, somos recebidos com o erro abaixo:

![image](./evidencias/BUG6/bug06-002_edinting_error.PNG)

Porém, ao realizar a correção do **BUG 5**, já conseguimos ver que no `TransactionHandler` o `updateAsync` havia sido esquecido de ser implementado. Alguém deve ter se apressado para terminar a User Story 😒. Espero que ainda não tenha passado pelo PR-Review 🙏.

![image](./evidencias/BUG6/bug06-003-not_implemented.PNG)

Vamos implementar o código, e sem dar aquele clássico `ctrl+c` e `ctrl+v` do método de categoria, só trocando variáveis e descrição 😅.

![image](./evidencias/BUG6/bug06-004-implemented_code.PNG)

Agora, com o método implementado, vamos realizar outro teste de edição.

![image](./evidencias/BUG6/bug06-005-edit_test.PNG)

Aeeeeee! Registro editado com sucesso 🎉.

![image](./evidencias/BUG6/bug06-006-transaction_updateded.PNG)

## PLUS 

Para enriquecer os testes, vamos criar um usuário com vários registros no seed:

```text
email : teste@balta.io
pass  : D1m@D1m@
```

Olha como a tela inicial fica mais rica de informações com esse usuário:

![image](./evidencias/PLUS/plus-001-dashboard.PNG)

![image](./evidencias/PLUS/plus-002-dashboard.PNG)