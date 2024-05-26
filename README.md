# Tech Chalange - Fiap - Software Architecture
## Problema
Há uma lanchonete de bairro que está expandindo devido seu grande sucesso. Porém, com a expansão e sem um sistema de controle de pedidos, o atendimento aos clientes pode ser caótico e confuso. Por exemplo, imagine que um cliente faça um pedido complexo, como um hambúrguer personalizado com ingredientes específicos, acompanhado de batatas fritas e uma bebida. O atendente pode anotar o pedido em um papel e entregá-lo à cozinha, mas não há garantia de que o pedido será preparado corretamente. Sem um sistema de controle de pedidos, pode haver confusão entre os atendentes e a cozinha, resultando em atrasos na preparação e entrega dos pedidos. Os pedidos podem ser perdidos, mal interpretados ou esquecidos, levando à insatisfação dos clientes e a perda de negócios. Em resumo, um sistema de controle de pedidos é essencial para garantir que a lanchonete possa atender os clientes de maneira eficiente, gerenciando seus pedidos e estoques de forma adequada. Sem ele, expandir a lanchonete pode acabar não dando certo, resultando em clientes insatisfeitos e impactando os negócios de forma negativa. Para solucionar o problema, a lanchonete irá investir em um sistema de autoatendimento de fast food, que é composto por uma série de dispositivos e interfaces que permitem aos clientes selecionar e fazer pedidos sem precisar interagir com um atendente.

# Fase 1 - Solicitações
1. Documentação do sistema (DDD) com Event Storming, incluindo todos os passos/tipos de diagrama mostrados na aula 6 do módulo de DDD, e utilizando a linguagem ubíqua, dos seguintes fluxos: 
    - Realização do pedido e pagamento; 
    - Preparação e entrega do pedido. 
    - É importante que os desenhos sigam os padrões utilizados na explicação. 
2. Uma aplicação para todo o sistema de backend (monolito) que deverá ser desenvolvido seguindo os padrões apresentados nas aulas:
    - Utilizando arquitetura hexagonal 
    - APIs: 
        -  Cadastro do Cliente 
        -  Identificação do Cliente via CPF 
        -  Criar, editar e remover produtos 
        -  Buscar produtos por categoria 
        -  Fake checkout, apenas enviar os produtos escolhidos para a fila. O checkout é a finalização do pedido. 
        -  Listar os pedidos 
    - Disponibilizar também o Swagger para consumo dessas APIs
    - Banco de dados à sua escolha
         - Inicialmente deveremos trabalhar e organizar a fila dos pedidos apenas em banco de dados 
3. A aplicação deve ser entregue com um Dockerfile configurado para executá-la corretamente, e um docker-compose.yml para subir o ambiente completo. 
4. Para validação da POC, temos a seguinte limitação de infraestrutura: 
    - 1 instância para banco de dados 
    -  1 instâncias para executar aplicação

# Fase 1 - Entrega
1. Documentação do sistema (DDD) com Event Storming, incluindo todos os passos/tipos de diagrama mostrados na aula 6 do módulo de DDD, e utilizando a linguagem ubíqua, dos seguintes fluxos; poderá ser encontrado no link do Miro abaixo:
    - [Miro](https://miro.com/app/board/uXjVKaMIl9E=/?share_link_id=150094922925)
2. APIs podem ser analisada pelos endpoints; recomendado rodar e abrir pelo Swagger.
3. Dockerfile e compose estão na raiz do projeto.
4. Para o projeto utilizaremos o MongoDB e a API em C# e .NET.

## Iniciando o projeto

1 - Faça o fork ou clone do projeto;
2 - Crie seu arquivo .env com as variáveis seguindo o mesmo esquema do .env.example.
3 - Pelo terminal entre na pasta, no mesmo nível do docker-compose.yml e rode o comando:
```sh
docker compose up
```
4 - Acesse o localhos, de preferencia com a rota para o Swagger: http://localhost:5187/swagger

# Participantes
- [Eric Silva](https://github.com/ericdss)
- [Felipe Augusto Lopes de Carvalho Magalhães](https://github.com/ALCM-bit)
- [Higor Hotz Vasconcelo](https://github.com/highotz)
- [Paulo Avelino Junior](https://github.com/PauloAvelino)
- [Victor Gustavo Duarte](https://github.com/victorg-duarte)
