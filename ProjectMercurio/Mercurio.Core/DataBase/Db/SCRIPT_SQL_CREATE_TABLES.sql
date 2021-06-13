CREATE SCHEMA `projetomercurio` ;
USE projetomercurio;

CREATE TABLE direcao(
	IdDirecao INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	DataCriacao DATETIME NOT NULL,
	Movimento VARCHAR (10) NOT NULL
);

INSERT INTO direcao (Movimento,DataCriacao) VALUES ('Frente', NOW());
INSERT INTO direcao (Movimento,DataCriacao) VALUES ('Esquerda', NOW());
INSERT INTO direcao (Movimento,DataCriacao) VALUES ('Direita', NOW());

CREATE TABLE sensor (
	IdSensor INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR(100) NOT NULL,
	DataCriacao DATETIME NOT NULL,
	Inicial  BOOLEAN NOT NULL DEFAULT false,
	IdSensorAnterior INT,
	IdDirecao INT NOT NULL,
	DirecaoRota ENUM('Ida','Volta'),
	HashNum VARCHAR(100),
	Ativo BOOLEAN NOT NULL DEFAULT true,
	FOREIGN KEY (IdSensorAnterior) REFERENCES sensor(IdSensor),
	FOREIGN KEY (IdDirecao) REFERENCES direcao(IdDirecao)
);

CREATE TABLE local(
	IdLocal INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR (60) NOT NULL,
	IdSensor INT NOT NULL,
	DataCriacao DATETIME NOT NULL,
	Ativo BOOLEAN NOT NULL DEFAULT true,
	FOREIGN KEY (IdSensor) REFERENCES sensor(IdSensor)
);

CREATE TABLE item(
	IdItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR (60) NOT NULL, 
	Ativo BOOLEAN NOT NULL DEFAULT true,
	DataCriacao DATETIME NOT NULL
);

CREATE TABLE usuario(
	IdUsuario INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR (60) NOT NULL, 
	DataCriacao DATETIME NOT NULL,
	Ativo BOOLEAN NOT NULL DEFAULT true,
	Idade INT NOT NULL,
	Senha TEXT NOT NULL
);

CREATE TABLE rota(
	IdRota INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	IdSensorInicial INT NOT NULL,
	IdSensorFinal INT NOT NULL,
	DataCriacao DATETIME NOT NULL,
	Rota VARCHAR(100),
	FOREIGN KEY (IdSensorInicial) REFERENCES sensor(IdSensor),
	FOREIGN KEY (IdSensorFinal) REFERENCES sensor(IdSensor)

);

CREATE TABLE pedido(
	IdPedido INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	IdUsuario INT NOT NULL,
	DataCriacao DATETIME NOT NULL,
	IdRota INT NOT NULL,
	Ativo BOOLEAN NOT NULL DEFAULT true,
	PedidoEntregue BOOLEAN NOT NULL DEFAULT false,
	FOREIGN KEY (IdUsuario) REFERENCES usuario(IdUsuario),
	FOREIGN KEY (IdRota) REFERENCES rota(IdRota)
);

CREATE TABLE PedidoXItem(
	IdPedidoxItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	IdPedido INT NOT NULL,
	IdItem INT NOT NULL,
	Quantidade INT NOT NULL,
	FOREIGN KEY (IdPedido) REFERENCES pedido(IdPedido),
	FOREIGN KEY (IdItem) REFERENCES item(IdItem)
);

CREATE TABLE grupo(
	IdGrupo INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR(50) NOT NULL
);

CREATE TABLE GrupoXUsuario(
	IdGrupoXUsuario INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	IdGrupo INT NOT NULL,
	IdUsuario INT NOT NULL,
	FOREIGN KEY (IdUsuario) REFERENCES usuario(IdUsuario),
	FOREIGN KEY (IdGrupo) REFERENCES grupo(IdGrupo)
);

CREATE TABLE carrinho(
	IdCarrinho INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Nome VARCHAR (60) NOT NULL, 
	IdPedido INT,
	Disponivel BOOLEAN NOT NULL DEFAULT true,
	IdUltimoSensor INT,
	HashRota TEXT NOT NULL,
	DataCriacao DATETIME NOT NULL,
	FOREIGN KEY (IdUltimoSensor) REFERENCES sensor(IdSensor),
	FOREIGN KEY (IdPedido) REFERENCES pedido(IdPedido)
);


INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota) VALUES ('Sensor 1',now(),true,1,'Ida');
INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, IdSensorAnterior) VALUES ('Sensor 2',now(),false,1,'Ida',1);
INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, IdSensorAnterior) VALUES ('Sensor 3',now(),false,3,'Ida',2);
INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, IdSensorAnterior) VALUES ('Sensor 4',now(),false,3,'Ida',3);
INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, IdSensorAnterior) VALUES ('Sensor 5',now(),false,2,'Ida',3);
INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, IdSensorAnterior) VALUES ('Sensor 6',now(),false,1,'Ida',3);


INSERT INTO projetomercurio.grupo(Nome) VALUES ('SuperUser');
INSERT INTO projetomercurio.grupo(Nome) VALUES ('Gerente');
INSERT INTO projetomercurio.grupo(Nome) VALUES ('Estoque');
INSERT INTO projetomercurio.grupo(Nome) VALUES ('Ponta');
INSERT INTO projetomercurio.grupo(Nome) VALUES ('Comum');