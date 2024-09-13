# Teste para vaga de Desenvolvedor FullStack Função Sistemas

## Teste realizado conforme orientação, segue algumas considerações abaixo
- Disponibilzado este código aqui no github via repositório público.
- O banco de dados .mdf alterado segue de forma anexada ao projeto da mesma forma que me foi enviado com as devidas alterações.

### Tela de cadastro de cliente
- Adicionado input de CPF com máscara, utilizada lib "jquerymask".
- Adicionado uma validação usando Data Anotation customizado para validar CPF seguindo regras matemáticas.
- Adicionado uma validação para evitar que se tenha dois CPFs iguais na tabela CLIENTES na base de dados.
- Adicionado coluna de CPF com restrição de unicidade na tabela CLIENTES.
```sql
-- Comandos para gerar coluna de CPF e adicionar restrição de unicidade
alter table CLIENTES add CPF varchar(11) not null;
alter table CLIENTES add constraint UC_CPF unique (CPF);
```
- Adicionada validação em código para impedir que seja feita a tentativa de uma inserção de CPF duplicado antes mesmo de executar as procedures.
- Aadicionado botão de acesso ao modal de 'Beneficiários' na tela de edição de cliente. OBS: Não deixei o botão disponível na tela de cadastro de cliente para evitar inconsistência na hora de salvar o beneficiário com o vínculo.

### Tela de Beneficiários
- No backend, criado estrutura mantendo padrão arquitetural da empresa, adicionei apenas tratamentos de exceções customizadas seguindo um padrão para retorno de erros, e adicionado objeto padrão de retorno nas solicitações.
- Criadas procedures seguindo a mesma ideia proposta no projeto para realização de operações de acesso a dados.
- Criado modal utilizando padrão do Razor com Partial View para passagem de parâmetro de id do cliente.
- Listagem de beneficiários vinculados a um cliente feita via Ajax assim que o modal é renderizado.
- Criado estrutura de inputs conforme desenhado no documento de orientação, com a exceção de que foi adicionado um botão a mais para resetar o formulário.
- Criado layout responsivo.
- A tabela foi montada usando Javascript Vanilla, e os botões renderizados e adicionados eventos para cliques.
- Ao clicar no botão de editar um beneficiário na tabela, foi realizado uma busca no backend dos dados do beneficiário por id e renderizado nos inputs da tela, bem como alterado o label do botão de "Incluir" para "Editar". Caso o usuário desista da operação de edição, ao apertar o botão Reset o formulário volta ao estágio inicial para inclusão de novos beneficiários.
- Ao clicar no botão de deletar é aberto um alerta do tipo "confirm" no browser com a mensagem perguntando se o usuário quer mesmo remover o registro, caso ele aperte sim é enviada uma requisição Ajax com a solicitação passando o id do beneficiário para deleção.

### Pontos de melhoria
- Melhorar modal, pois não consegui em tempo hábil fazer ele fechar pelo "x" usando o padrão "data-dismiss" do bootsttrap, sendo assim quando o usuário aperta no "X" é feito um refresh na tela mantendo ao menos a funcionalidade de forma estável e usual para o usuário.
- Melhorar layout de inputs.
