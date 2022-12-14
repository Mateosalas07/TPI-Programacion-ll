

CREATE PROCEDURE SP_TRAER_MARCAS
AS
BEGIN
	select * 
	from Marcas
	where id_marca in (select ma.id_marca
						from Vehiculos v join Modelos m on v.id_modelo = m.id_modelo
						join Marcas ma on ma.id_marca = m.id_marca)
END


CREATE PROCEDURE SP_CONSULTAR_VEHICULOS_MARCAS
(
@MARCA VARCHAR(100)
)
AS
BEGIN
	select
	id_vehiculo as 'id_Vehículo',
	m.modelo 'Modelo',	
	marca 'Marca',
	tipo_vehiculo 'Tipo de Vehículo',
	precio_act 'Precio',
	color 'Color',
	año 'Año',
	descripcion 'Descripcion'

	FROM Vehiculos v join Modelos m on v.id_modelo = m.id_modelo join Marcas ma on m.id_marca = ma.id_marca
	join Tipo_Vehiculos tp on m.id_tipo_vehiculo = tp.id_tipo_vehiculo
	WHERE MARCA = @MARCA
END

select * from Vehiculos
select * from Modelos
select * from Marcas


insert into Vehiculos values (12,'Tope de Gama, OKm...',3750000,'2022-11-13','Blanco',35)

EXEC SP_CONSULTAR_VEHICULOS;
--Consultar Clientes
USE [Automotrizp]
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_CLIENTES]    Script Date: 12/11/2022 22:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_CONSULTAR_CLIENTES]
AS
BEGIN
	SELECT cod_cliente, nombre + ' '+ apellido as Nombre
	FROM Clientes
END


select * from Vehiculos

--Consultar vehiculos
ALTER PROCEDURE SP_CONSULTAR_VEHICULOS

AS
BEGIN
	select 
	id_vehiculo as 'id_Vehículo',
	(marca+'  '+m.modelo) 'Modelo',
	tipo_vehiculo 'Tipo de Vehículo',
	precio_act 'Precio',
	color 'Color',
	year(año) 'Año',
	descripcion 'Descripcion',
	Produccion 'Produccion'

	FROM Vehiculos v join Modelos m on v.id_modelo = m.id_modelo join Marcas ma on m.id_marca = ma.id_marca
	join Tipo_Vehiculos tp on m.id_tipo_vehiculo = tp.id_tipo_vehiculo
	--order by 2
	
END

--actualizar la tabla vehiculos y autopartes para que tenga una columna si esta en produccion o no
select * from Vehiculos

alter table Vehiculos
add  Produccion varchar (10) 

update Vehiculos
set Produccion = 'Si'

exec SP_CONSULTAR_VEHICULOS