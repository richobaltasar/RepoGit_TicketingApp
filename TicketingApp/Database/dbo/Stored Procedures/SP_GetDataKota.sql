--select*from DataKotaPostalCode
--exec SP_GetDataKota 'Province','Bali','Kabupaten_Kota'



CREATE PROCEDURE SP_GetDataKota
	@NamaCollumnInduk nvarchar(max),
	@NamaInduk nvarchar(max),
	@NamaColumnDisplay nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	declare @sql nvarchar(max)
	set @sql = 'select '''' Text, '''' Value
				union all 
				select distinct '+@NamaColumnDisplay+' Text,'+@NamaColumnDisplay+' Value  from DataKotaPostalCode where '+@NamaCollumnInduk+' = '''+@NamaInduk+''''
	print @sql

	EXECUTE sp_executesql @sql
END
