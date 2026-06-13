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
    IDCategoria INT IDENTITY(1,1),

    NombreCategoria NVARCHAR(100) NOT NULL,
    Activo BIT NOT NULL
        CONSTRAINT DF_Categoria_Activo DEFAULT (1),

    CONSTRAINT PK_Categoria
        PRIMARY KEY (IDCategoria),

    CONSTRAINT UQ_Categoria_NombreCategoria
        UNIQUE (NombreCategoria)
);
GO
/*=========================================================
ACCESIBILIDAD
=========================================================*/
CREATE TABLE Accesibilidad
(
    IDAccesibilidad INT IDENTITY(1,1),

    NombreAccesibilidad NVARCHAR(100) NOT NULL,
    Activo BIT NOT NULL
        CONSTRAINT DF_Accesibilidad_Activo DEFAULT (1),

    CONSTRAINT PK_Accesibilidad
        PRIMARY KEY (IDAccesibilidad),

    CONSTRAINT UQ_Accesibilidad_Nombre
        UNIQUE (NombreAccesibilidad)
);
GO
/*=========================================================
PRODUCTOS
=========================================================*/
CREATE TABLE Producto
(
    IDProducto INT IDENTITY(1,1),

    Nombre NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    ImagenUrl NVARCHAR(500) NULL,

    Precio DECIMAL(18,2) NOT NULL
        CONSTRAINT CK_Producto_Precio
        CHECK (Precio > 0),

    Descuento DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_Producto_Descuento DEFAULT (0)
        CONSTRAINT CK_Producto_Descuento
        CHECK (Descuento >= 0),

    Stock INT NOT NULL
        CONSTRAINT DF_Producto_Stock DEFAULT (0)
        CONSTRAINT CK_Producto_Stock
        CHECK (Stock >= 0),

    FechaLanzamiento DATE NOT NULL,

    EsDigital BIT NOT NULL
        CONSTRAINT DF_Producto_EsDigital DEFAULT (0),

    Activo BIT NOT NULL
        CONSTRAINT DF_Producto_Activo DEFAULT (1),

    CONSTRAINT PK_Producto
        PRIMARY KEY (IDProducto)
);
GO
/*=========================================================
PRODUCTO CATEGORIA
=========================================================*/
CREATE TABLE ProductoCategoria
(
    IDProducto INT NOT NULL,
    IDCategoria INT NOT NULL,

    CONSTRAINT PK_ProductoCategoria
        PRIMARY KEY (IDProducto, IDCategoria),

    CONSTRAINT FK_ProductoCategoria_Producto
        FOREIGN KEY (IDProducto)
        REFERENCES Producto(IDProducto),

    CONSTRAINT FK_ProductoCategoria_Categoria
        FOREIGN KEY (IDCategoria)
        REFERENCES Categoria(IDCategoria)
);
GO
/*=========================================================
PRODUCTO ACCESIBILIDAD
=========================================================*/
CREATE TABLE ProductoAccesibilidad
(
    IDProducto INT NOT NULL,
    IDAccesibilidad INT NOT NULL,

    CONSTRAINT PK_ProductoAccesibilidad
        PRIMARY KEY (IDProducto, IDAccesibilidad),

    CONSTRAINT FK_ProductoAccesibilidad_Producto
        FOREIGN KEY (IDProducto)
        REFERENCES Producto(IDProducto),

    CONSTRAINT FK_ProductoAccesibilidad_Accesibilidad
        FOREIGN KEY (IDAccesibilidad)
        REFERENCES Accesibilidad(IDAccesibilidad)
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

--SP que trae el producto completo para recorrerlo con datareader.nextresult
CREATE PROCEDURE SP_Producto_ObtenerPorId
(
    @IDProducto INT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Producto
    SELECT
        IDProducto,
        Nombre,
        Descripcion,
        ImagenUrl,
        Precio,
        Descuento,
        Stock,
        FechaLanzamiento,
        EsDigital,
        Activo
    FROM Producto
    WHERE IDProducto = @IDProducto;

    -- Categorías
    SELECT
        c.IDCategoria,
        c.NombreCategoria,
        c.Activo
    FROM Categoria c
        INNER JOIN ProductoCategoria pc
            ON c.IDCategoria = pc.IDCategoria
    WHERE pc.IDProducto = @IDProducto;

    -- Accesibilidades
    SELECT
        a.IDAccesibilidad,
        a.NombreAccesibilidad,
        a.Activo
    FROM Accesibilidad a
        INNER JOIN ProductoAccesibilidad pa
            ON a.IDAccesibilidad = pa.IDAccesibilidad
    WHERE pa.IDProducto = @IDProducto;
END
GO