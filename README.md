
# Desafio Back-end Easynvest

### A aplicação está disponível em: 

Swagger: https://matheus-terra-easynvest-test.herokuapp.com/swagger/index.html


## Decisões técnicas e arquiteturais

A Api foi desenvolvida utilizando, basicamente, uma arquitetura em camadas. API, Domain e Infra. A requisição HTTP chega através de uma endpoint da controller (API), que invoca um método da Query (Domain), que por sua vez chama um método da Domain Service, e essa service, por fim, utiliza as interfaces de Query Repository para obter os dados necessários para completar a requisição. Essas interfaces são implementadas na Infra, deixando o domínio totalmente independente das implementações de infra estrutura.

Durante a elaboração da arquitetura e desenvolvimento do projeto, foi levado em conta conceitos primordiais para a escrita de um código coeso, limpo, de fácil manutenção e bem estruturado, como DDD, SOLID e Clean Code.

Como a listagem de investimentos necessita da integração com diferentes serviços, o conceito de Domain Services do DDD foi utilizado para realizar a orquestração entre essas diversas entidades, no qual um método executa os demais de forma assíncrona para construir a lista final de investimentos. Essa integração é feita a partir de interfaces, possibilitando deixar o domínio sem dependências externas e utilizando a Inversão de Dependência do SOLID. Para evitar a concorrência ao adicionar itens na lista, uma estrutura thread-safe foi utilizada, evitando assim exceptions não tratadas.

As entidades de domínio herdam de uma entidade base que contém definições de propriedades e métodos que são utilizadas para o cálculo de IR, Valor de Resgate e a criação do objeto de investimento.

Os Queries Repositories herdam de uma classe base que contem os métodos para leitura e escrita no cache. Caso exista cache, o resultado é retornado, caso contrário, é feita uma requisição para o serviço em questão. Essa requisição é realizada através de um Client HTTP REST genérico, facilitando as requisições nos demais repositórios. Caso a requisição seja efetuada com sucesso, o retorno é salvo em cache para evitar futuras requisições nos serviços, possibilitando que a aplicação tenha um melhor desempenho. A sua duração é até as 23:59:59 do dia da primeira requisição realizada. O cache é feito de duas forma: no ambiente de desenvolvimento, é utilizado um Memory Cache, evitando a utilização de um servidor de cache local, e nos demais ambientes, é utilizado um serviço de Redis, tornando assim o cache totalmente distribuído. O gerenciamento da utilização de cada sistema de cache é feito via injeção de dependência no projeto de IoC.

Uma controller de HealthCheck foi adicionada ao projeto para facilitar a monitoração do estado da aplicação. Essa controller conta com dois métodos: healthcheck e healthcheck/details. O primeiro apenas retorna um objeto caso a aplicação esteja online e o segundo retorna uma lista de objetos que indicam se os sistemas que a aplicação consome estão online.

Para documentação de cada endpoint, foi implementado o swagger que pode ser acesso através da url: {url}/swagger/index.html

Para criação dos testes de unidade, é utilizado o XUnit. Essa biblioteca foi escolhida pela familiaridade e facilidade no desenvolvimento dos testes. 
É utilizado, também, o NSubstitute para o mock das interfaces e o AutoFixture que facilita a criação dos objetos utilizados nos testes e gerenciar as injeções dos mocks das interfaces.