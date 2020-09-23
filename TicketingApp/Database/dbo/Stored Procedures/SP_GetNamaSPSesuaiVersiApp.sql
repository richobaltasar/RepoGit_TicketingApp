-- exec SP_GetNamaSPSesuaiVersiApp @VersiID='', @NamaSp='SP_CheckClosingMerchant'
CREATE PROCEDURE SP_GetNamaSPSesuaiVersiApp 
	@VersiID nvarchar(max),
	@NamaSp  nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	declare @SPName nvarchar(max)
	if(@VersiID != '')
	begin
		set @SPName = @NamaSp+'_V'+@VersiID;
	end
	else
	begin
		set @SPName = @NamaSp;
	end 
	if exists(SELECT * FROM sys.procedures WHERE name = @SPName)
	begin
		SELECT name as NamaSP FROM sys.procedures WHERE name = @SPName
	end
	else
	begin
		SELECT top 1 name as NamaSP FROM sys.procedures WHERE name LIKE '%'+@NamaSp+'%'
		order by object_id desc
	end
END
