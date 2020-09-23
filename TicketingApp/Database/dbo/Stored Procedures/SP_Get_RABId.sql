Create PROCEDURE SP_Get_RABId
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	--select Text,Value,Urutan into #temp  from ERP.dbo.Master_ListItem where ListName='ListStatusRAB'
	select
		Id,NamaRAB,JenisRAB,DescriptionRAB,
		CreateDate,TargetImplementDate,Attachment1,Attachment2,Attachment3,
		TotalBudget,EstimasiPurchase,StatusRAB,PIC1,PIC2,DisetujuiOleh,IdUserBy
		from DataRAB a 
	where Id = @Id
END