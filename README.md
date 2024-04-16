# Loja_Virtual_MVC
Projeto usa as mesma tecnologias do projeto Loja_virtual, contudo foi projetado utilizando o ASP NET CORE MVC;

- O objetivo do projeto é, sobre tudo, reforçar alguns conhecimentos do BackEnd, porém tem como objetivo aprender, através da prática, alguns conceitos e funcionamento do Html, Css e reforçar o JavaScript;

- Conexão com o Banco de Dados foi feita através do EF Core;
- Banco utilizado: MySql.  

## Tecnicas empregadas:
- S.O.L.I.D
- Clean Architecture
- IOC;

## Descrição:
- Projeto simula uma plataforma que é possível comprar produtos, realizar cadastro de usuario, logins e verificar produtos(Paginação) com o auxilio de interfaces visuais.

## Endpoints:
  ### HomeController:
* Sign-Up(registro/Cadastro de usuario), efetua o cadastro de usuarios no banco de dados.
* Login(Não foi implementado ainda), realiza o Login do usuario e concede autorização a certos controllers, utilizando o JWT.
* Index Página inicial da interface gráfica.

  ### ProdutosController
  #### Dispõe apenas de metódos HttpGet
  *GetProdutos, Permite a utilização de filtros e listagem dos produtos cadastrados no banco de dados.
  *GetProdutosDetalhados, Retorna uma View(Sql) com uma imagem e categoria do produto detalhado

  #### Observação: A API ainda está em desenvolvimento e, portanto, não possui todas as EndPoints completas.

