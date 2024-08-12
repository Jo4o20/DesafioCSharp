Este projeto é uma aplicação desktop desenvolvida em C# utilizando Windows Forms e MySQL, com o objetivo de gerenciar o cadastro de veículos. A aplicação permite ao usuário adicionar, editar, excluir e visualizar veículos registrados em um banco de dados. O projeto também faz uso de APIs para carregar informações adicionais, como a lista de marcas de veículos.
Estrutura do Projeto

O projeto está estruturado da seguinte forma:

    Data: Contém a classe ConexaoBD, responsável pela conexão com o banco de dados MySQL.
    Models: Contém as classes de modelos que representam as entidades do banco de dados e dados retornados das APIs.
        Veiculo.cs: Representa a entidade Veículo no banco de dados.
        Marcas.cs: Representa os dados de marcas obtidos da API de marcas.
        Modelo.cs: Representa os dados de modelos obtidos da API de modelos (se aplicável).
    Service: Contém as classes responsáveis por manipulações e operações lógicas relacionadas aos modelos, como comunicação com APIs.
    Views: Contém as interfaces gráficas do projeto (Windows Forms).
        MainForm.cs: Formulário principal da aplicação onde os veículos são gerenciados.
        LoginForm.cs: Formulário de login para acessar a aplicação (se aplicável).

Funcionalidades

    Adicionar Veículo: Permite adicionar um novo veículo ao banco de dados. O formulário coleta informações como placa, chassi, marca, ano de fabricação, valor da FIPE, entre outros.

    Editar Veículo: Permite editar os detalhes de um veículo existente. O usuário seleciona um veículo na lista, faz as alterações desejadas e salva as mudanças no banco de dados.

    Excluir Veículo: Permite excluir um veículo do banco de dados.

    Visualizar Veículos: Exibe uma lista de todos os veículos cadastrados no banco de dados.

    Integração com API de Marcas: A aplicação consome uma API para carregar dinamicamente a lista de marcas de veículos no formulário.

Requisitos

    .NET Framework (versão 4.7.2 ou superior).
    MySQL Server.
    MySQL Connector/NET.
    Pacotes NuGet:
        Newtonsoft.Json: Para manipulação de JSON.
        System.Net.Http: Para realizar chamadas HTTP.

Configuração

    Banco de Dados:
        Crie um banco de dados MySQL chamado cadastro_veiculos.
        Execute o seguinte comando SQL para criar a tabela Veiculos:

sql

CREATE TABLE Veiculos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Placa VARCHAR(10) NOT NULL,
    Chassi VARCHAR(50) NOT NULL,
    Marca VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    AnoFabricacao VARCHAR(50) NOT NULL,
    AnoModelo VARCHAR(50) NOT NULL,
    ValorFipe DECIMAL(10, 2) NOT NULL,
    ValorVenda DECIMAL(10, 2) NOT NULL,
    Observacoes TEXT
);

    Configuração do Projeto:
        Abra o projeto no Visual Studio.
        Verifique a string de conexão com o banco de dados na classe ConexaoBD.cs e ajuste conforme necessário:

csharp

private static string connectionString = "Server=localhost;Database=cadastro_veiculos;Uid=root;Pwd=1234;";

    Execução:
        Compile e execute o projeto através do Visual Studio.
        Use o formulário de login para acessar a aplicação (se aplicável).
        No formulário principal, você poderá adicionar, editar, excluir e visualizar os veículos cadastrados.

Observações

    A função de obtenção de modelos de veículos foi inicialmente implementada para consumir uma API, mas devido a uma mudança de requisitos, a utilização da cmbModelo foi descontinuada.
    Certifique-se de que o MySQL está configurado corretamente no ambiente onde a aplicação será executada.
    A aplicação faz chamadas para a API pública https://parallelum.com.br/fipe/api/v1/carros/marcas para preencher a lista de marcas.
