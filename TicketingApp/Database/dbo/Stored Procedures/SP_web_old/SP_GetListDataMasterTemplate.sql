CREATE PROCEDURE SP_GetListDataMasterTemplate
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	if(@Id = 0)
	begin
		select*from ERP.dbo.Master_ListItem
		where ListName like '%'+@search+'%'
	end
	else
	begin
		select*from ERP.dbo.Master_ListItem
		where id = @Id
	end

END