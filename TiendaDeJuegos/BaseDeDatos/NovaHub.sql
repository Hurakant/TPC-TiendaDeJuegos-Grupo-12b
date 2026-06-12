USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'NovaHub')
BEGIN
ALTER DATABASE NovaHub SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE NovaHub;
END
GO

CREATE DATABASE NovaHub;
GO

USE NovaHub;
GO

/*=========================================================
CATEGORIAS
=========================================================*/

CREATE TABLE Categoria
(
IDCategoria INT IDENTITY(1,1) PRIMARY KEY,
NombreCategoria NVARCHAR(100) NOT NULL
);
GO

/*=========================================================
PRODUCTOS
=========================================================*/

CREATE TABLE Producto
(
IDProducto INT IDENTITY(1,1) PRIMARY KEY,


Titulo NVARCHAR(200) NOT NULL,
Descripcion NVARCHAR(MAX),

Precio DECIMAL(18,2) NOT NULL CHECK (Precio > 0),
Stock INT NOT NULL DEFAULT 0 CHECK (Stock >= 0) ,

Activo BIT NOT NULL DEFAULT 1,

IDCategoria INT NOT NULL,

CONSTRAINT FK_Producto_Categoria
    FOREIGN KEY (IDCategoria)
    REFERENCES Categoria(IDCategoria)

);
GO

/*=========================================================
USUARIOS
=========================================================*/

CREATE TABLE Usuario
(
IDUsuario INT IDENTITY(1,1) PRIMARY KEY,


Nombre NVARCHAR(100) NOT NULL,
Apellido NVARCHAR(100) NOT NULL,

Email NVARCHAR(200) NOT NULL UNIQUE,
Contrasena NVARCHAR(255) NOT NULL,

Telefono NVARCHAR(20),

Rol INT NOT NULL CHECK (Rol BETWEEN 1 AND 3),

Activo BIT NOT NULL DEFAULT 1,

FechaAlta DATETIME NOT NULL DEFAULT GETDATE()


);
GO

/*=========================================================
FORMAS DE PAGO
=========================================================*/

CREATE TABLE FormaDePago
(
IDFormaDePago INT IDENTITY(1,1) PRIMARY KEY,


Nombre NVARCHAR(100) NOT NULL,

Activa BIT NOT NULL DEFAULT 1


);
GO

/*=========================================================
FORMAS DE ENTREGA
=========================================================*/

CREATE TABLE FormaDeEntrega
(
IDFormaDeEntrega INT IDENTITY(1,1) PRIMARY KEY,


Nombre NVARCHAR(100) NOT NULL,

Activa BIT NOT NULL DEFAULT 1


);
GO

/*=========================================================
ESTADOS DE PEDIDO
=========================================================*/

CREATE TABLE EstadoPedido
(
IDEstadoPedido INT IDENTITY(1,1) PRIMARY KEY,


Nombre NVARCHAR(50) NOT NULL


);
GO

/*=========================================================
CARRITO
=========================================================*/

CREATE TABLE Carrito
(
IDCarrito INT IDENTITY(1,1) PRIMARY KEY,


IDUsuario INT NOT NULL,

CONSTRAINT FK_Carrito_Usuario
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuario(IDUsuario)


);
GO

/*=========================================================
ITEMS DEL CARRITO
=========================================================*/

CREATE TABLE CarritoItem
(
IDCarritoItem INT IDENTITY(1,1) PRIMARY KEY,


IDCarrito INT NOT NULL,
IDProducto INT NOT NULL,

Cantidad INT NOT NULL DEFAULT 1,

CONSTRAINT FK_CarritoItem_Carrito
    FOREIGN KEY (IDCarrito)
    REFERENCES Carrito(IDCarrito),

CONSTRAINT FK_CarritoItem_Producto
    FOREIGN KEY (IDProducto)
    REFERENCES Producto(IDProducto),

CONSTRAINT CHK_CarritoItem_Cantidad
    CHECK (Cantidad > 0)


);
GO

/*=========================================================
PEDIDOS
=========================================================*/

CREATE TABLE Pedido
(
IDPedido INT IDENTITY(1,1) PRIMARY KEY,

IDUsuario INT NOT NULL,

ImagenUrl NVARCHAR(500),

FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),

MontoTotal DECIMAL(18,2) NOT NULL DEFAULT 0,

IDFormaDePago INT NOT NULL,
IDFormaDeEntrega INT NOT NULL,
IDEstadoPedido INT NOT NULL,

CONSTRAINT FK_Pedido_Usuario
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuario(IDUsuario),

CONSTRAINT FK_Pedido_FormaPago
    FOREIGN KEY (IDFormaDePago)
    REFERENCES FormaDePago(IDFormaDePago),

CONSTRAINT FK_Pedido_FormaEntrega
    FOREIGN KEY (IDFormaDeEntrega)
    REFERENCES FormaDeEntrega(IDFormaDeEntrega),

CONSTRAINT FK_Pedido_Estado
    FOREIGN KEY (IDEstadoPedido)
    REFERENCES EstadoPedido(IDEstadoPedido)


);
GO

/*=========================================================
DETALLE PEDIDO
=========================================================*/

CREATE TABLE DetallePedido
(
IDDetallePedido INT IDENTITY(1,1) PRIMARY KEY,


IDPedido INT NOT NULL,
IDProducto INT NOT NULL,

Cantidad INT NOT NULL CHECK (Cantidad > 0),

PrecioUnitario DECIMAL(18,2) NOT NULL CHECK (PrecioUnitario >= 0),
Subtotal DECIMAL(18,2) NOT NULL,

CONSTRAINT FK_DetallePedido_Pedido
    FOREIGN KEY (IDPedido)
    REFERENCES Pedido(IDPedido),

CONSTRAINT FK_DetallePedido_Producto
    FOREIGN KEY (IDProducto)
    REFERENCES Producto(IDProducto)


);
GO

/*=========================================================
DATOS INICIALES
=========================================================*/

INSERT INTO FormaDePago (Nombre)
VALUES
('Efectivo'),
('Transferencia'),
('Tarjeta');
GO

INSERT INTO FormaDeEntrega (Nombre)
VALUES
('Retiro en local'),
('Envio a domicilio');
GO

INSERT INTO EstadoPedido (Nombre)
VALUES
('Pendiente'),
('Pagado'),
('En preparacion'),
('Enviado'),
('Entregado'),
('Cancelado');
GO



INSERT INTO Usuario
(
    Nombre,
    Apellido,
    Email,
    Contrasena,
    Telefono,
    Rol
)
VALUES

-- ADMIN
('Admin', 'NovaHub', 'admin@gmail.com', '1234', '111111111', 3),

-- VENDEDORES
('Juan', 'Perez', 'vendedor1@gmail.com', '1234', '222222222', 2),
('Maria', 'Gomez', 'vendedor2@gmail.com', '1234', '333333333', 2),

-- CLIENTES
('Carlos', 'Lopez', 'cliente1@gmail.com', '1234', '444444444', 1),
('Ana', 'Martinez', 'cliente2@gmail.com', '1234', '555555555', 1),
('Lucas', 'Fernandez', 'cliente3@gmail.com', '1234', '666666666', 1),
('Sofia', 'Rodriguez', 'cliente4@gmail.com', '1234', '777777777', 1);
GO


-- sp para el registro :D
--CREATE PROCEDURE SP_InsertarUsuario
--    @Nombre VARCHAR(50),
--    @Apellido VARCHAR(50),
--    @Email VARCHAR(100),
--    @Contrasena VARCHAR(50),
--    @Telefono VARCHAR(20)
--AS
--BEGIN
--    INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, Telefono, Rol) 
--    VALUES (@Nombre, @Apellido, @Email, @Contrasena, @Telefono, 1)

--    SELECT scope_identity() 
--END


