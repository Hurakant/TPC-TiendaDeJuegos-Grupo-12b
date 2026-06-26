
USE NovaHub;
GO

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