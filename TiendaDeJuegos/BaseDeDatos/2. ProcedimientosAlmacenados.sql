USE NovaHub;
GO

CREATE OR ALTER PROCEDURE SP_Producto_ObtenerPorId
    @IDProducto INT
AS
BEGIN
    SET NOCOUNT ON;
 
    -- Producto
    SELECT
        IDProducto, Nombre, Descripcion, ImagenUrl, Precio, Descuento,
        Stock, FechaLanzamiento, EsDigital, Activo
    FROM Producto
    WHERE IDProducto = @IDProducto;
 
    -- Categorias (muchos a muchos)
    SELECT
        c.IDCategoria, c.NombreCategoria, c.Activo
    FROM Categoria c
        INNER JOIN ProductoCategoria pc ON c.IDCategoria = pc.IDCategoria
    WHERE pc.IDProducto = @IDProducto;
 
    -- Accesibilidades (muchos a muchos)
    SELECT
        a.IDAccesibilidad, a.NombreAccesibilidad, a.Activo
    FROM Accesibilidad a
        INNER JOIN ProductoAccesibilidad pa ON a.IDAccesibilidad = pa.IDAccesibilidad
    WHERE pa.IDProducto = @IDProducto;
END
GO
 
/*=========================================================
  PRODUCTO: listar activos (sin filtro), con categorias
  y accesibilidades de todos los productos devueltos
=========================================================*/
CREATE OR ALTER PROCEDURE SP_Producto_Listar
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        IDProducto, Nombre, Descripcion, ImagenUrl, Precio, Descuento,
        Stock, FechaLanzamiento, EsDigital, Activo
    FROM Producto
    WHERE Activo = 1
    ORDER BY Nombre ASC;
 
    SELECT
        pc.IDProducto, c.IDCategoria, c.NombreCategoria, c.Activo
    FROM ProductoCategoria pc
        INNER JOIN Categoria c ON c.IDCategoria = pc.IDCategoria
        INNER JOIN Producto p ON p.IDProducto = pc.IDProducto
    WHERE p.Activo = 1;
 
    SELECT
        pa.IDProducto, a.IDAccesibilidad, a.NombreAccesibilidad, a.Activo
    FROM ProductoAccesibilidad pa
        INNER JOIN Accesibilidad a ON a.IDAccesibilidad = pa.IDAccesibilidad
        INNER JOIN Producto p ON p.IDProducto = pa.IDProducto
    WHERE p.Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE SP_Producto_ListarFiltrado
    @Texto NVARCHAR(200) = NULL,
    @IdsCategorias NVARCHAR(MAX) = NULL,
    @Orden INT = 0
AS
BEGIN
    SET NOCOUNT ON;
 
    DECLARE @TextoLike NVARCHAR(204) = NULL;
    IF (@Texto IS NOT NULL AND LTRIM(RTRIM(@Texto)) <> '')
        SET @TextoLike = '%' + LTRIM(RTRIM(@Texto)) + '%';
 
    SELECT
        IDProducto, Nombre, Descripcion, ImagenUrl, Precio, Descuento,
        Stock, FechaLanzamiento, EsDigital, Activo
    INTO #ProductosFiltrados
    FROM Producto P
    WHERE P.Activo = 1
        AND (@TextoLike IS NULL OR P.Nombre LIKE @TextoLike)
        AND (
            @IdsCategorias IS NULL OR LTRIM(RTRIM(@IdsCategorias)) = ''
            OR (
                SELECT COUNT(DISTINCT PC.IDCategoria)
                FROM ProductoCategoria PC
                WHERE PC.IDProducto = P.IDProducto
                  AND PC.IDCategoria IN (SELECT CAST(value AS INT) FROM STRING_SPLIT(@IdsCategorias, ','))
            ) = (SELECT COUNT(value) FROM STRING_SPLIT(@IdsCategorias, ','))
        );
 
    SELECT * FROM #ProductosFiltrados
    ORDER BY
        CASE WHEN @Orden = 1 THEN Precio END ASC,
        CASE WHEN @Orden = 2 THEN Precio END DESC,
        CASE WHEN @Orden NOT IN (1, 2) THEN Nombre END ASC;
 
    SELECT
        PC.IDProducto, C.IDCategoria, C.NombreCategoria, C.Activo
    FROM ProductoCategoria PC
        INNER JOIN Categoria C ON C.IDCategoria = PC.IDCategoria
    WHERE PC.IDProducto IN (SELECT IDProducto FROM #ProductosFiltrados);
 
    SELECT
        PA.IDProducto, A.IDAccesibilidad, A.NombreAccesibilidad, A.Activo
    FROM ProductoAccesibilidad PA
        INNER JOIN Accesibilidad A ON A.IDAccesibilidad = PA.IDAccesibilidad
    WHERE PA.IDProducto IN (SELECT IDProducto FROM #ProductosFiltrados);
 
    DROP TABLE #ProductosFiltrados;
END
GO
 
/*PRODUCTO: agregar / modificar / eliminar / utilidades
  Nota: @FechaLanzamiento es opcional, si no se manda
  se usa GETDATE().*/
CREATE OR ALTER PROCEDURE SP_Producto_Agregar
    @Nombre NVARCHAR(200),
    @Descripcion NVARCHAR(MAX),
    @Precio DECIMAL(18,2),
    @Descuento DECIMAL(18,2),
    @Stock INT,
    @EsDigital BIT,
    @ImagenUrl NVARCHAR(500),
    @FechaLanzamiento DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
 
    INSERT INTO Producto (Nombre, Descripcion, Precio, Descuento, Stock, EsDigital, Activo, ImagenUrl, FechaLanzamiento)
    VALUES (@Nombre, @Descripcion, @Precio, @Descuento, @Stock, @EsDigital, 1, @ImagenUrl, ISNULL(@FechaLanzamiento, GETDATE()));
 
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
 
CREATE OR ALTER PROCEDURE SP_Producto_Modificar
    @IDProducto INT,
    @Nombre NVARCHAR(200),
    @Descripcion NVARCHAR(MAX),
    @Precio DECIMAL(18,2),
    @Descuento DECIMAL(18,2),
    @Stock INT,
    @EsDigital BIT,
    @Activo BIT,
    @ImagenUrl NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
 
    UPDATE Producto
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        Precio = @Precio,
        Descuento = @Descuento,
        Stock = @Stock,
        EsDigital = @EsDigital,
        Activo = @Activo,
        ImagenUrl = @ImagenUrl
    WHERE IDProducto = @IDProducto;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Producto_Eliminar
    @IDProducto INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Producto SET Activo = 0 WHERE IDProducto = @IDProducto;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Producto_ExisteNombre
    @Nombre NVARCHAR(200),
    @IdActual INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IDProducto
    FROM Producto
    WHERE Nombre = @Nombre AND Activo = 1 AND IDProducto <> @IdActual;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Producto_ActualizarStock
    @IDProducto INT,
    @Cantidad INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Producto
    SET Stock = Stock + @Cantidad
    WHERE IDProducto = @IDProducto
    AND Stock + @Cantidad >= 0;
END
GO
 
/*al modificar un producto se borran
  TODAS sus relaciones y se vuelven a insertar las actuales
  (mas simple que comparar diferencias).*/
CREATE OR ALTER PROCEDURE SP_ProductoCategoria_Agregar
    @IDProducto INT,
    @IDCategoria INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM ProductoCategoria WHERE IDProducto = @IDProducto AND IDCategoria = @IDCategoria)
        INSERT INTO ProductoCategoria (IDProducto, IDCategoria) VALUES (@IDProducto, @IDCategoria);
END
GO
 
CREATE OR ALTER PROCEDURE SP_ProductoCategoria_EliminarPorProducto
    @IDProducto INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM ProductoCategoria WHERE IDProducto = @IDProducto;
END
GO
 
CREATE OR ALTER PROCEDURE SP_ProductoAccesibilidad_Agregar
    @IDProducto INT,
    @IDAccesibilidad INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto AND IDAccesibilidad = @IDAccesibilidad)
        INSERT INTO ProductoAccesibilidad (IDProducto, IDAccesibilidad) VALUES (@IDProducto, @IDAccesibilidad);
END
GO
 
CREATE OR ALTER PROCEDURE SP_ProductoAccesibilidad_EliminarPorProducto
    @IDProducto INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM ProductoAccesibilidad WHERE IDProducto = @IDProducto;
END
GO
 
/*CATEGORIA: CRUD completo*/
CREATE OR ALTER PROCEDURE SP_Categoria_Listar
    @Filtro NVARCHAR(100) = NULL,
    @SoloActivos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IDCategoria, NombreCategoria, Activo
    FROM Categoria
    WHERE (@SoloActivos = 0 OR Activo = 1)
      AND (@Filtro IS NULL OR LTRIM(RTRIM(@Filtro)) = '' OR NombreCategoria LIKE '%' + @Filtro + '%')
    ORDER BY NombreCategoria ASC;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Categoria_ObtenerPorId
    @IDCategoria INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IDCategoria, NombreCategoria, Activo
    FROM Categoria
    WHERE IDCategoria = @IDCategoria;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Categoria_ExisteNombre
    @Nombre NVARCHAR(100),
    @IdExcluir INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 IDCategoria
    FROM Categoria
    WHERE LOWER(LTRIM(RTRIM(NombreCategoria))) = LOWER(LTRIM(RTRIM(@Nombre)))
      AND Activo = 1
      AND (@IdExcluir = 0 OR IDCategoria <> @IdExcluir);
END
GO
 
CREATE OR ALTER PROCEDURE SP_Categoria_Agregar
    @Nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Categoria (NombreCategoria, Activo) VALUES (LTRIM(RTRIM(@Nombre)), 1);
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
 
CREATE OR ALTER PROCEDURE SP_Categoria_Modificar
    @IDCategoria INT,
    @Nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Categoria SET NombreCategoria = LTRIM(RTRIM(@Nombre)) WHERE IDCategoria = @IDCategoria;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Categoria_Eliminar
    @IDCategoria INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Categoria SET Activo = 0 WHERE IDCategoria = @IDCategoria;
END
GO
 
/*ACCESIBILIDAD: CRUD completo igual que categorias*/
CREATE OR ALTER PROCEDURE SP_Accesibilidad_Listar
    @Filtro NVARCHAR(100) = NULL,
    @SoloActivos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IDAccesibilidad, NombreAccesibilidad, Activo
    FROM Accesibilidad
    WHERE (@SoloActivos = 0 OR Activo = 1)
      AND (@Filtro IS NULL OR LTRIM(RTRIM(@Filtro)) = '' OR NombreAccesibilidad LIKE '%' + @Filtro + '%')
    ORDER BY NombreAccesibilidad ASC;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Accesibilidad_ObtenerPorId
    @IDAccesibilidad INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IDAccesibilidad, NombreAccesibilidad, Activo
    FROM Accesibilidad
    WHERE IDAccesibilidad = @IDAccesibilidad;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Accesibilidad_ExisteNombre
    @Nombre NVARCHAR(100),
    @IdExcluir INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 IDAccesibilidad
    FROM Accesibilidad
    WHERE LOWER(LTRIM(RTRIM(NombreAccesibilidad))) = LOWER(LTRIM(RTRIM(@Nombre)))
      AND Activo = 1
      AND (@IdExcluir = 0 OR IDAccesibilidad <> @IdExcluir);
END
GO
 
CREATE OR ALTER PROCEDURE SP_Accesibilidad_Agregar
    @Nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Accesibilidad (NombreAccesibilidad, Activo) VALUES (LTRIM(RTRIM(@Nombre)), 1);
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
 
CREATE OR ALTER PROCEDURE SP_Accesibilidad_Modificar
    @IDAccesibilidad INT,
    @Nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Accesibilidad SET NombreAccesibilidad = LTRIM(RTRIM(@Nombre)) WHERE IDAccesibilidad = @IDAccesibilidad;
END
GO
 
CREATE OR ALTER PROCEDURE SP_Accesibilidad_Eliminar
    @IDAccesibilidad INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Accesibilidad SET Activo = 0 WHERE IDAccesibilidad = @IDAccesibilidad;
END
GO
--SP listar usuarios
CREATE OR ALTER PROCEDURE SP_Usuario_Listar
    @Filtro NVARCHAR(100) = NULL,
    @SoloActivos BIT = NULL,
    @Rol INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IDUsuario,
        Nombre,
        Apellido,
        Email,
        Telefono,
        Activo,
        Rol,
        FechaAlta
    FROM Usuario
    WHERE (
            @Filtro IS NULL
            OR Nombre LIKE '%' + @Filtro + '%'
            OR Apellido LIKE '%' + @Filtro + '%'
            OR Email LIKE '%' + @Filtro + '%'
            )
        AND (@SoloActivos IS NULL OR Activo = @SoloActivos) AND (@Rol IS NULL OR Rol = @Rol) ORDER BY Nombre, Apellido;
END;
GO