CREATE PROCEDURE [dbo].[SP_FormData_GetSearch]
	@idLog bigint,
	@NamaForm nvarchar(max),
	@Type nvarchar(max),
	@Id nvarchar(max),
	@TextLabel nvarchar(max),
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@ValueInput nvarchar(max),
	@ListModel nvarchar(max),
	@Urutan bigint,
	@ShowHide nvarchar(max),
	@ReadOnly nvarchar(max),
	@Enable nvarchar(max),
	@Mandatory nvarchar(max),
	@IsNumber bigint,
	@FilterBy bigint
AS


select*from Master_Form
where 
	NamaForm like '%'+@NamaForm+'%'