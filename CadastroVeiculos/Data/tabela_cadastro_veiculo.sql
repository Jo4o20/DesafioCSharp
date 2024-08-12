CREATE DATABASE cadastro_veiculos;

USE cadastro_veiculos;

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
-- veiculo adicionado manualmente para teste
INSERT INTO Veiculos (Placa, Chassi, Marca, Modelo, AnoFabricacao, AnoModelo, ValorFipe, ValorVenda, Observacoes)
VALUES ('ABC1234', 'chassi01', 'Chevrolet', 'Celta', '2005', '2005', '10000', '1200', '4 portas');

SELECT * FROM Veiculos;


