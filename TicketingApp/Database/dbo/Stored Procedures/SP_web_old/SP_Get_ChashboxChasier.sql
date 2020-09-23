CREATE PROCEDURE SP_Get_ChashboxChasier
	@Awal_Datetime nvarchar(max),
	@Akhir_Datetime nvarchar(max),
	@OpenBy nvarchar(max),
	@CloseBy nvarchar(max),
	@UpdateBy nvarchar(max),
	@Status int
AS
BEGIN
	SET NOCOUNT ON;
	if(@Status = 0)
	begin
		select*from DataChasierBox
		where 
	
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
		between 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Awal_Datetime,10),'/','-'), 105), 23),'-','')
		and 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Akhir_Datetime,10),'/','-'), 105), 23),'-','')	
		and isnull(CloseBy,'') like '%'+@CloseBy+'%'
		and isnull(OpenBy,'') like '%'+@OpenBy+'%'
		and isnull(UpdateBy,'') like '%'+@UpdateBy+'%'
		order by replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') desc
	end
	else
	begin
		select*from DataChasierBox
		where 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
		between 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Awal_Datetime,10),'/','-'), 105), 23),'-','')
		and 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Akhir_Datetime,10),'/','-'), 105), 23),'-','')	
		and isnull(CloseBy,'') like '%'+@CloseBy+'%'
		and isnull(OpenBy,'') like '%'+@OpenBy+'%'
		and isnull(UpdateBy,'') like '%'+@UpdateBy+'%'
		and Status = @Status
		order by replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') desc
	end
END