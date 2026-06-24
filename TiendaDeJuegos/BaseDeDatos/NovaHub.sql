USE master;
GO
 
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'NovaHub')
BEGIN
    CREATE DATABASE NovaHub;
END
GO
 
USE NovaHub;
GO
 
/*=========================================================
CATEGORIAS
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Categoria') AND type = 'U')
BEGIN
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
END
GO
 
/*=========================================================
ACCESIBILIDAD
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Accesibilidad') AND type = 'U')
BEGIN
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
END
GO
 
/*=========================================================
PRODUCTOS
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Producto') AND type = 'U')
BEGIN
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
        IDCategoria INT NOT NULL,
 
        EsDigital BIT NOT NULL
            CONSTRAINT DF_Producto_EsDigital DEFAULT (0),
 
        Activo BIT NOT NULL
            CONSTRAINT DF_Producto_Activo DEFAULT (1),
 
        CONSTRAINT PK_Producto
            PRIMARY KEY (IDProducto)
    );
END
GO
 
/*=========================================================
PRODUCTO CATEGORIA
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductoCategoria') AND type = 'U')
BEGIN
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
END
GO
 
/*=========================================================
PRODUCTO ACCESIBILIDAD
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductoAccesibilidad') AND type = 'U')
BEGIN
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
END
GO

/*=========================================================
USUARIOS
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Usuario') AND type = 'U')
BEGIN
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
END
GO

-- DIRECCION

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo' + N'.Direccion') AND type = 'U')
BEGIN
    CREATE TABLE DIRECCION
    (
        IdDireccion INT IDENTITY(1,1) PRIMARY KEY,
        IDUsuario INT NOT NULL,
        
        Calle NVARCHAR(150) NOT NULL,
        Numero NVARCHAR(20) NOT NULL,
        Piso NVARCHAR(20) NULL,
        Depto NVARCHAR(20) NULL,
        Localidad NVARCHAR(100) NOT NULL,
        Provincia NVARCHAR(100) NOT NULL,
        CodigoPostal NVARCHAR(20) NOT NULL,
        
        Activo BIT NOT NULL DEFAULT 1,

        CONSTRAINT FK_Direccion_Usuario
            FOREIGN KEY (IDUsuario)
            REFERENCES Usuario(IDUsuario)
    );
END
GO
 
/*=========================================================
FORMAS DE PAGO
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.FormaDePago') AND type = 'U')
BEGIN
    CREATE TABLE FormaDePago
    (
        IDFormaDePago INT IDENTITY(1,1) PRIMARY KEY,
 
        Nombre NVARCHAR(100) NOT NULL,
 
        Activa BIT NOT NULL DEFAULT 1
    );
END
GO
 
/*=========================================================
FORMAS DE ENTREGA
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.FormaDeEntrega') AND type = 'U')
BEGIN
    CREATE TABLE FormaDeEntrega
    (
        IDFormaDeEntrega INT IDENTITY(1,1) PRIMARY KEY,
 
        Nombre NVARCHAR(100) NOT NULL,
 
        Activa BIT NOT NULL DEFAULT 1
    );
END
GO
 
/*=========================================================
ESTADOS DE PEDIDO
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.EstadoPedido') AND type = 'U')
BEGIN
    CREATE TABLE EstadoPedido
    (
        IDEstadoPedido INT IDENTITY(1,1) PRIMARY KEY,
 
        Nombre NVARCHAR(50) NOT NULL
    );
END
GO
 
/*=========================================================
CARRITO
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Carrito') AND type = 'U')
BEGIN
    CREATE TABLE Carrito
    (
        IDCarrito INT IDENTITY(1,1) PRIMARY KEY,
 
        IDUsuario INT NOT NULL,
 
        CONSTRAINT FK_Carrito_Usuario
            FOREIGN KEY (IDUsuario)
            REFERENCES Usuario(IDUsuario)
    );
END
GO
 
/*=========================================================
ITEMS DEL CARRITO
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CarritoItem') AND type = 'U')
BEGIN
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
END
GO
 
/*=========================================================
PEDIDOS
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Pedido') AND type = 'U')
BEGIN
    CREATE TABLE Pedido
    (
        IDPedido INT IDENTITY(1,1) PRIMARY KEY,
 
        IDUsuario INT NOT NULL,
 
        ImagenUrl NVARCHAR(500),
 
        FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
 
        MontoTotal DECIMAL(18,2) NOT NULL DEFAULT 0,

        IdDireccion INT NULL,
 
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
            REFERENCES EstadoPedido(IDEstadoPedido),

        CONSTRAINT FK_Pedido_Direccion 
            FOREIGN KEY (IdDireccion)
            REFERENCES Direccion(IdDireccion)
    );
END
GO
 
/*=========================================================
DETALLE PEDIDO
=========================================================*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.DetallePedido') AND type = 'U')
BEGIN
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
END
GO
 
 /*DATOS INICIALES DE PAGOS*/
INSERT INTO FormaDePago (Nombre)
SELECT v.Nombre
FROM (VALUES ('Efectivo'), ('Transferencia'), ('Tarjeta')) AS v(Nombre)
WHERE NOT EXISTS (
    SELECT 1 FROM FormaDePago f WHERE f.Nombre = v.Nombre
);
GO
 
INSERT INTO FormaDeEntrega (Nombre)
SELECT v.Nombre
FROM (VALUES ('Retiro en local'), ('Envio a domicilio')) AS v(Nombre)
WHERE NOT EXISTS (
    SELECT 1 FROM FormaDeEntrega f WHERE f.Nombre = v.Nombre
);
GO
 
INSERT INTO EstadoPedido (Nombre)
SELECT v.Nombre
FROM (VALUES ('Pendiente'), ('Pagado'), ('En preparacion'), ('Enviado'), ('Entregado'), ('Cancelado')) AS v(Nombre)
WHERE NOT EXISTS (
    SELECT 1 FROM EstadoPedido e WHERE e.Nombre = v.Nombre
);
GO
 
/*DATOS INICIALES USUARIOS*/
INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, Telefono, Rol)
SELECT v.Nombre, v.Apellido, v.Email, v.Contrasena, v.Telefono, v.Rol
FROM (VALUES
    -- ADMIN
    ('Admin', 'NovaHub', 'admin@gmail.com', '123456789', '1111111111', 3),
 
    -- VENDEDORES
    ('Juan', 'Perez', 'vendedor1@gmail.com', '123456789', '1122222222', 2),
    ('Maria', 'Gomez', 'vendedor2@gmail.com', '123456789', '1133333333', 2),
 
    -- CLIENTES
    ('Carlos', 'Lopez', 'cliente1@gmail.com', '123456789', '1144444444', 1),
    ('Ana', 'Martinez', 'cliente2@gmail.com', '123456789', '1155555555', 1),
    ('Lucas', 'Fernandez', 'cliente3@gmail.com', '123456789', '1166666666', 1),
    ('Sofia', 'Rodriguez', 'cliente4@gmail.com', '123456789', '1177777777', 1)
 
) AS v(Nombre, Apellido, Email, Contrasena, Telefono, Rol)
WHERE NOT EXISTS (
    SELECT 1 FROM Usuario u WHERE u.Email = v.Email
);
GO
 
-- sp para el registro :D
CREATE OR ALTER PROCEDURE SP_InsertarUsuario
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Email VARCHAR(100),
    @Contrasena VARCHAR(50),
    @Telefono VARCHAR(20)
AS
BEGIN
    INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, Telefono, Rol)
    VALUES (@Nombre, @Apellido, @Email, @Contrasena, @Telefono, 1)
 
    SELECT scope_identity()
END
GO
 
/*SP: OBTENER PRODUCTO COMPLETO (con datareader.nextresult)*/
CREATE OR ALTER PROCEDURE SP_Producto_ObtenerPorId
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