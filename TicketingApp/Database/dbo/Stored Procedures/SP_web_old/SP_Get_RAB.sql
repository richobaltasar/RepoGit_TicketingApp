CREATE PROCEDURE SP_Get_RAB
	@Id bigint,
	@IdUser bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select Text,Value,Urutan into #temp  from ERP.dbo.Master_ListItem where ListName='ListStatusRAB'
	if(@Id = 0)
	begin
		if(@IdUser = 0)
		begin
			select
			a.Id,NamaRAB,JenisRAB,DescriptionRAB,
			CreateDate,TargetImplementDate,Attachment1,Attachment2,Attachment3,
			TotalBudget,
			--EstimasiPurchase,
			b.Text StatusRAB ,
			case when exists(select*from DataRAB_Item where IdRAB = a.Id) then
				(select sum(isnull(SubTotal,0)) from DataRAB_Item where IdRAB = a.Id) else 0 end as EstimasiPurchase,
			case when a.PIC1 is not null then (select NamaLengkap from UserData where id = a.PIC1) else null end as PIC1,
			case when a.PIC2 is not null then (select NamaLengkap from UserData where id = a.PIC2) else null end as PIC2,
			case when a.DisetujuiOleh is not null then (select NamaLengkap from UserData where id = a.DisetujuiOleh) else null end as DisetujuiOleh,
			case when a.IdUserBy is not null then (select NamaLengkap from UserData where id = a.IdUserBy) else null end as IdUserBy
			from DataRAB a 
			left join #temp b on b.Value COLLATE Latin1_General_CI_AS = a.StatusRAB
			where NamaRAB like '%'+@search+'%'
		end
		else
		begin
			select
			Id,NamaRAB,JenisRAB,DescriptionRAB,
			CreateDate,TargetImplementDate,Attachment1,Attachment2,Attachment3,
			TotalBudget,EstimasiPurchase,StatusRAB,
			case when a.PIC1 is not null then (select NamaLengkap from UserData where id = a.PIC1) else null end as PIC1,
			case when a.PIC2 is not null then (select NamaLengkap from UserData where id = a.PIC2) else null end as PIC2,
			case when a.DisetujuiOleh is not null then (select NamaLengkap from UserData where id = a.DisetujuiOleh) else null end as DisetujuiOleh,
			case when a.IdUserBy is not null then (select NamaLengkap from UserData where id = a.IdUserBy) else null end as IdUserBy
			from DataRAB a 
			where IdUserBy = @IdUser and NamaRAB like '%'+@search+'%'
		end
	end
	else
	begin
		select
		Id,NamaRAB,JenisRAB,DescriptionRAB,
		CreateDate,TargetImplementDate,Attachment1,Attachment2,Attachment3,
		TotalBudget,EstimasiPurchase,StatusRAB,PIC1,PIC2,DisetujuiOleh,IdUserBy
		from DataRAB a 
		where Id = @Id
	end
END