CREATE PROCEDURE [dbo].[SP_FormData_GetById]
	@Id bigint
AS
	select*from Master_Form
	where idLog = @Id
