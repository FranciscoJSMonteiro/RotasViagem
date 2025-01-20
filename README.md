# Rota de Viagem

## Descrição do Projeto
Este projeto busca determinar a rota de viagem mais barata entre dois pontos, independentemente do número de conexões. Ele permite:

- Registrar novas rotas, que são persistidas para futuras consultas.
- Consultar a melhor rota entre dois pontos de origem e destino.

O sistema é baseado em uma arquitetura em camadas, garantindo separação de responsabilidades, extensibilidade e facilidade de manutenção.

---

## Como Executar a Aplicação

### Pré-requisitos
- .NET 6.0 ou superior instalado na máquina.
- Ferramenta de versionamento Git.

### Passos para Execução
1. Clone o repositório:
   ```bash
   git clone https://github.com/FranciscoJSMonteiro/RotasdeViagem.git
   ```

2. Acesse a pasta do projeto:
   ```bash
   cd repo-privado
   ```

3. Restaure as dependências do projeto:
   ```bash
   dotnet restore
   ```

4. Compile o projeto:
   ```bash
   dotnet build
   ```

5. Execute a aplicação:
   ```bash
   dotnet run
   ```

Ao iniciar, o programa criará automaticamente um diretório na raiz (`C:\rota`) com um arquivo `rotas.txt` contendo as rotas iniciais. O menu exibido permitirá a interação com as seguintes opções:

- `0`: Consultar rotas existentes.
- `1`: Adicionar nova rota.
- `2`: Consultar a melhor rota entre dois pontos.
- `3`: Sair.

---

## Estrutura do Projeto

```plaintext
src/
|-- Application/       # Lógica de aplicação e orquestração.
|   |-- Interfaces/    # Interfaces para repositórios e serviços.
|   |-- Services/      # Serviços que encapsulam a lógica de negócios.
|
|-- Domain/            # Entidades e interfaces principais.
|   |-- Entities/      # Classes representando as entidades do domínio.
|
|-- Infrastructure/    # Persistência e interação com arquivos.
|   |-- Persistence/   # Implementação de repositórios e acesso a dados.
|
|-- Tests/             # Testes unitários e de integração.
|-- Program.cs         # Arquivo principal de entrada do programa.
|-- rotas.txt          # Arquivo de rotas persistidas.
```

---

## Decisões de Design para a Solução

### 1. Arquitetura em Camadas
- **Application**: Contém lógica de negócios e orquestração.
- **Domain**: Define as entidades e regras do domínio.
- **Infrastructure**: Cuida da persistência e suporte técnico.
- **Tests**: Valida o comportamento das camadas.

### 2. Princípios de SOLID
- **SRP**: Cada classe possui uma responsabilidade única.
- **DIP**: Interfaces desacoplam lógica de negócios de implementações concretas.

### 3. Uso de Interfaces
Interfaces como `IRotaRepository` definem contratos claros e permitem a injeção de dependências, facilitando o uso de mocks nos testes.

### 4. Persistência de Dados
As rotas são armazenadas em arquivos para simplicidade, com design que permite migração futura para bancos de dados.

### 5. Testabilidade
- Utiliza mocks (biblioteca Moq) para isolar dependências.
- Testes cobrem casos de sucesso e falha.

### 6. Simplicidade e Extensibilidade
- Algoritmo personalizado para encontrar a rota mais barata.
- Permite futura inclusão de restrições como tempo ou distância.

---

## Exemplo de Uso

**Entrada**:
```
Digite a rota: GRU-CDG
```

**Saída**:
```
Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
```

---

## Contribuição
Este repositório é privado. Caso tenha acesso, sinta-se à vontade para sugerir melhorias ou relatar problemas.

---

## Autor
- Francisco Monteiro

---

## Licença
Este projeto está sob a licença MIT.
