--Valores por defecto para productos

USE NovaHub;
GO

SET NOCOUNT ON;
GO

SET IDENTITY_INSERT Categoria ON;
GO

IF NOT EXISTS (SELECT 1 FROM Categoria WHERE NombreCategoria = 'RPG')
    INSERT INTO Categoria (IDCategoria, NombreCategoria, Activo)
    VALUES (1, 'RPG', 1);

IF NOT EXISTS (SELECT 1 FROM Categoria WHERE NombreCategoria = 'Acción')
    INSERT INTO Categoria (IDCategoria, NombreCategoria, Activo)
    VALUES (2, 'Acción', 1);

IF NOT EXISTS (SELECT 1 FROM Categoria WHERE NombreCategoria = 'Shooter')
    INSERT INTO Categoria (IDCategoria, NombreCategoria, Activo)
    VALUES (3, 'Shooter', 1);

SET IDENTITY_INSERT Categoria OFF;
GO

SET IDENTITY_INSERT Accesibilidad ON;
GO

IF NOT EXISTS (SELECT 1 FROM Accesibilidad WHERE NombreAccesibilidad = 'Claridad visual')
    INSERT INTO Accesibilidad (IDAccesibilidad, NombreAccesibilidad, Activo)
    VALUES (1, 'Claridad visual', 1);

IF NOT EXISTS (SELECT 1 FROM Accesibilidad WHERE NombreAccesibilidad = 'Ayuda de apuntado')
    INSERT INTO Accesibilidad (IDAccesibilidad, NombreAccesibilidad, Activo)
    VALUES (2, 'Ayuda de apuntado', 1);

IF NOT EXISTS (SELECT 1 FROM Accesibilidad WHERE NombreAccesibilidad = 'Jugabilidad a una mano')
    INSERT INTO Accesibilidad (IDAccesibilidad, NombreAccesibilidad, Activo)
    VALUES (3, 'Jugabilidad a una mano', 1);

SET IDENTITY_INSERT Accesibilidad OFF;
GO

DECLARE @Nombre NVARCHAR(200);
DECLARE @Precio DECIMAL(18,2);
DECLARE @Desc DECIMAL(18,2);
DECLARE @Stock INT;
DECLARE @Fecha DATE;
DECLARE @Imagen NVARCHAR(500);
DECLARE @IDProducto INT;
DECLARE @Cats NVARCHAR(100);

SET @Nombre = 'The Witcher 3: Wild Hunt';
SET @Precio = 299.99; SET @Desc = 35.00; SET @Stock = 250;
SET @Fecha = '2015-05-19';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/292030/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 1)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 1);
END

SET @Nombre = 'Elden Ring';
SET @Precio = 599.00; SET @Desc = 15.00; SET @Stock = 480;
SET @Fecha = '2022-02-25';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/1245620/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 1)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 1);
END

SET @Nombre = 'Cyberpunk 2077';
SET @Precio = 459.99; SET @Desc = 50.00; SET @Stock = 600;
SET @Fecha = '2020-12-10';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/1091500/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 1)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 1);
END

SET @Nombre = 'Baldur''s Gate 3';
SET @Precio = 899.00; SET @Desc = 10.00; SET @Stock = 320;
SET @Fecha = '2023-08-03';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/1086940/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 1)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 1);
END

SET @Nombre = 'Grand Theft Auto V';
SET @Precio = 349.99; SET @Desc = 25.00; SET @Stock = 999;
SET @Fecha = '2013-09-17';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/271590/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 2)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 2);
END

SET @Nombre = 'Red Dead Redemption 2';
SET @Precio = 549.00; SET @Desc = 40.00; SET @Stock = 410;
SET @Fecha = '2019-10-26';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/1174180/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 2)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 2);
END

SET @Nombre = 'Devil May Cry 5';
SET @Precio = 199.99; SET @Desc = 20.00; SET @Stock = 150;
SET @Fecha = '2019-03-08';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/601150/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 2)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 2);
END

SET @Nombre = 'Counter-Strike 2';
SET @Precio = 10.00; SET @Desc = 0.00; SET @Stock = 999;
SET @Fecha = '2023-09-27';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/730/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 3)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 3);
END

SET @Nombre = 'DOOM Eternal';
SET @Precio = 399.00; SET @Desc = 60.00; SET @Stock = 280;
SET @Fecha = '2020-03-20';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/782330/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 3)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 3);
END

SET @Nombre = 'Valorant';
SET @Precio = 10.00; SET @Desc = 0.00; SET @Stock = 850;
SET @Fecha = '2020-06-02';
SET @Imagen = 'https://www.riotgames.com/darkroom/1440/8d5c497da1c2eeec8cffa99b01abc64b:5329ca773963a5b739e98e715957ab39/ps-f2p-val-console-launch-16x9.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 3)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 3);
END

SET @Nombre = 'Dark Souls III';
SET @Precio = 359.99; SET @Desc = 45.00; SET @Stock = 200;
SET @Fecha = '2016-04-12';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/374320/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 1)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 1);
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 2)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 2);
END

SET @Nombre = 'Sekiro: Shadows Die Twice';
SET @Precio = 429.00; SET @Desc = 30.00; SET @Stock = 340;
SET @Fecha = '2019-03-22';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/814380/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 1)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 1);
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 2)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 2);
END

SET @Nombre = 'Borderlands 3';
SET @Precio = 289.99; SET @Desc = 70.00; SET @Stock = 420;
SET @Fecha = '2019-09-13';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/397540/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 2)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 2);
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 3)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 3);
END

SET @Nombre = 'Destiny 2';
SET @Precio = 199.00; SET @Desc = 50.00; SET @Stock = 700;
SET @Fecha = '2017-09-06';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/1085660/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 2)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 2);
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 3)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 3);
END

SET @Nombre = 'Fallout 4';
SET @Precio = 149.99; SET @Desc = 25.00; SET @Stock = 500;
SET @Fecha = '2015-11-10';
SET @Imagen = 'https://cdn.akamai.steamstatic.com/steam/apps/377160/header.jpg';
IF NOT EXISTS (SELECT 1 FROM Producto WHERE Nombre = @Nombre)
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, ImagenUrl, Precio, Descuento, Stock, FechaLanzamiento, EsDigital, Activo)
    VALUES (@Nombre, NULL, @Imagen, @Precio, @Desc, @Stock, @Fecha, 1, 1);
    SET @IDProducto = SCOPE_IDENTITY();
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 1)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 1);
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = 3)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, 3);
END

GO

DECLARE @IDProducto INT;

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'The Witcher 3: Wild Hunt';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Elden Ring';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Cyberpunk 2077';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Baldur''s Gate 3';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Grand Theft Auto V';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Red Dead Redemption 2';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Devil May Cry 5';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Counter-Strike 2';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'DOOM Eternal';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Valorant';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Dark Souls III';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Sekiro: Shadows Die Twice';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Borderlands 3';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Destiny 2';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 2)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 2);
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 3)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 3);

SELECT @IDProducto = IDProducto FROM Producto WHERE Nombre = 'Fallout 4';
IF @IDProducto IS NOT NULL AND NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = 1)
    INSERT INTO ProductoAccesibilidad VALUES (@IDProducto, 1);

GO
--Prints para saber cosas
PRINT 'Valores por defecto NovaHub cargados correctamente.';
PRINT 'Categorias: 3 (RPG, Accion, Shooter)';
PRINT 'Accesibilidades: 3 (Claridad visual, Ayuda de apuntado, Jugabilidad a una mano)';
PRINT 'Productos: 15 juegos digitales';
GO