USE projetomercurio;

CREATE TABLE direcao(
	IdDirecao INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Movimento VARCHAR (10) NOT NULL
);

INSERT INTO direcao (Movimento) VALUES ('Frente');
INSERT INTO direcao (Movimento) VALUES ('Esquerda');
INSERT INTO direcao (Movimento) VALUES ('Direita');

CREATE TABLE sensor (
	IdSensor INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR(100) NOT NULL,
	DataCriacao DATETIME NOT NULL,
	Inicial  BOOLEAN NOT NULL DEFAULT false,
	IdSensorAnterior INT NOT NULL,
	IdDirecao INT NOT NULL,
	DirecaoRota ENUM('Ida','Volta'),
	FOREIGN KEY (IdSensorAnterior) REFERENCES sensor(IdSensor),
	FOREIGN KEY (IdDirecao) REFERENCES direcao(IdDirecao)
);

CREATE TABLE local(
	IdLocal INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR (60) NOT NULL,
	IdSensor INT NOT NULL,
	DataCriacao DATETIME NOT NULL,
	FOREIGN KEY (IdSensor) REFERENCES sensor(IdSensor)
);

CREATE TABLE item(
	IdItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR (60) NOT NULL, 
	DataCriacao DATETIME NOT NULL
);

CREATE TABLE usuario(
	IdUsuario INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR (60) NOT NULL, 
	DataCriacao DATETIME NOT NULL,
	Idade INT NOT NULL
);

CREATE TABLE pedido(
	IdPedido INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	IdUsuario INT NOT NULL,
	DataCriacao DATETIME NOT NULL,
	Rota VARCHAR(100),
	FOREIGN KEY (IdUsuario) REFERENCES usuario(IdUsuario)
);

CREATE TABLE PedidoXItem(
	IdPedidoxItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	IdPedido INT NOT NULL,
	IdItem INT NOT NULL,
	Quantitade INT NOT NULL,
	FOREIGN KEY (IdPedido) REFERENCES pedido(IdPedido),
	FOREIGN KEY (IdItem) REFERENCES item(IdItem)
);


	