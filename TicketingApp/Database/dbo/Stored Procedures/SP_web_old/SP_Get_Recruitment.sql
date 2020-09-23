
CREATE PROCEDURE SP_Get_Recruitment
	@FilterBy nvarchar(max),
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@FilterBy ='' and @search='')
	begin
		select top 100* from datarecruitment
		order by Id desc
	end
END
