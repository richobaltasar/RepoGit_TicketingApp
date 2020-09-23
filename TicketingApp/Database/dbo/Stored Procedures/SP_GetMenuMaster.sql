
CREATE PROCEDURE SP_GetMenuMaster
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select UPPER(isnull(NamaMenu,'')+' '+isnull(Action,'')+' '+isnull(Controller,'')+' '+isnull(Platform,'')),*
		from DataMenu where UPPER(NamaMenu+' '+isnull(Action,'')+' '+isnull(Controller,'')+' '+isnull(Platform,'')) like '%'+UPPER(@search)+'%'
	end
	else if(@Id != 0 and @search = '')
	begin
		select*from DataMenu where idMenu = @Id
	end

END