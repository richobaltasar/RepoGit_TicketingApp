CREATE PROCEDURE [dbo].[SP_FormData_GetSearch]
	@NamaForm nvarchar(max)
AS


select*from Master_Form
where 
	REPLACE(RTRIM(LTRIM(NamaForm)),' ','') like '%'+ REPLACE(RTRIM(LTRIM(@NamaForm)),' ','')+'%'
