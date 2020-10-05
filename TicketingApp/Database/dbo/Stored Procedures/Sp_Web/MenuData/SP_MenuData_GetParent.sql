CREATE PROCEDURE [dbo].[SP_MenuData_GetParent]
	@IdModule bigint, 
	@IdMenu bigint,
	@IdPosisi bigint
AS

	select
	a.IdMenu Value, b.NamaMenu Text
	from Role_MenuTree a
	left join DataMenu b on b.idMenu = a.IdMenu
	where a.Posisi = (@IdPosisi-1) and a.IdModule = @IdModule
	and a.IdMenu not in (@IdMenu)