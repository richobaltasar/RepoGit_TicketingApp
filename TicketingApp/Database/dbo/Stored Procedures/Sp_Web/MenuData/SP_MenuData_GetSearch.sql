CREATE PROCEDURE [dbo].[SP_MenuData_GetSearch]
	@NamaMenu nvarchar(max),
	@Controller nvarchar(max),
	@Platform nvarchar(max)
AS
if(@Platform !='')
begin
	select*from DataMenu
	where REPLACE(RTRIM(LTRIM(NamaMenu)),' ','') like '%'+REPLACE(RTRIM(LTRIM(@NamaMenu)),' ','')+'%' and REPLACE(RTRIM(LTRIM(Platform)),' ','') = REPLACE(RTRIM(LTRIM(@Platform)),' ','')
	and REPLACE(RTRIM(LTRIM(Controller)),' ','') like '%'+REPLACE(RTRIM(LTRIM(@Controller)),' ','')+'%'
end
else
begin
	select*from DataMenu
	where REPLACE(RTRIM(LTRIM(NamaMenu)),' ','') like '%'+REPLACE(RTRIM(LTRIM(@NamaMenu)),' ','')+'%' 
	and REPLACE(RTRIM(LTRIM(Controller)),' ','') like '%'+REPLACE(RTRIM(LTRIM(@Controller)),' ','')+'%'
end