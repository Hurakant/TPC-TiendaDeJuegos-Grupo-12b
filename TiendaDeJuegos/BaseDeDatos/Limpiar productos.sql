--Esto es para borrar todos los productos

USE NovaHub;
GO

SET NOCOUNT ON;
GO

DELETE FROM ProductoAccesibilidad;
GO

DELETE FROM ProductoCategoria;
GO

DELETE FROM Producto;
GO

DELETE FROM Categoria;
GO

DELETE FROM Accesibilidad;
GO

DBCC CHECKIDENT ('Producto',      RESEED, 0);
DBCC CHECKIDENT ('Categoria',     RESEED, 0);
DBCC CHECKIDENT ('Accesibilidad', RESEED, 0);
GO

PRINT 'Tablas Producto, ProductoCategoria, ProductoAccesibilidad, Categoria y Accesibilidad vaciadas.';
GO