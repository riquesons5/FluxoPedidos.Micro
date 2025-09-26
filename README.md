# FluxoPedidos.Micro

Microserviço responsável pelo registro e processamento de pedidos, com integração via API REST e RabbitMQ.

## Pré-requisitos

- Docker
- Docker Compose

## Como rodar o projeto

1. Clone o repositório:  
```bash
git clone https://github.com/riquesons5/FluxoPedidos.Micro.git
````

2. Entre na pasta do projeto:  
```bash
cd FluxoPedidos.Micro
````

Esta pasta contém a solução e o arquivo docker-compose.yml.

```bash
docker compose up --build
````

### A aplicação será iniciada em http://localhost:5000

### Para acessar a documentação dos endpoints, abra: http://localhost:5000/swagger

* Antes de inserir pedidos, é necessário cadastrar um cliente, pois este campo é obrigatório.

Inserção de pedidos

Via API REST: utilize o método POST nos endpoints correspondentes.

Via fila RabbitMQ:

### Acesse o dashboard do RabbitMQ: http://localhost:15672

Username: guest , Password: guest

Vá para a aba Queues and Streams, localize a fila fila_pedido.

Em Publish message, envie um JSON representando o pedido que deseja adicionar ao sistema.

Consultas de pedidos

Consultar pedidos por filtros:
http://localhost:5000/Pedido/BuscarComFiltros
* Pode filtrar por: Código do cliente, Número do pedido, Nome do cliente, Código do item, Produto (descrição do item) 

Quantidade de pedidos por cliente:
http://localhost:5000/Pedido/QuantidadePedidosPorCliente/{IdCliente}
* Retorna o número de pedidos e totais por cliente.
