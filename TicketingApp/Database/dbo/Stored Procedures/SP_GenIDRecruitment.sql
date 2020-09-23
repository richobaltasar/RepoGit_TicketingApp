
CREATE PROCEDURE SP_GenIDRecruitment
AS
BEGIN
	SET NOCOUNT ON;

    select Top 1 (isnull(Id,0)+1) Id from DataRecruitment
	order by Id desc 
END
