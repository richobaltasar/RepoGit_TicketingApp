CREATE PROCEDURE SP_Get_RecruitmentById
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataRecruitment where Id = @Id
END