
CREATE PROCEDURE SP_SubmitRAB
	@idrab bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	--select*from ERP.dbo.Master_ListItem where ListName ='ListStatusRAB'
	if exists(select*from DataRAB where Id = @idrab)
	begin
		update DataRAB 
			set StatusRAB = 'Waiting Approve'
		where Id = @idrab
		declare @submitby nvarchar(max)
		set @submitby =(
			select
			case when DisetujuiOleh !='' then 
				(select NamaLengkap + ' - '+ NamaPosisi +', Div : '+NamaDivisi from UserData where id = DisetujuiOleh) 
			else null end as DisetujuiOleh
			from DataRAB where Id = @idrab)

		declare @Omessage nvarchar(max)
		select  @Omessage='Data  '+NamaRAB+' - '+JenisRAB+' total budget :Rp '+CAST(TotalBudget as nvarchar(max))+' berhasil disubmit ke '+@submitby,@idrab = id from DataRAB where id = @idrab
			
		exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataRAB',@IdRow = @idrab,@message=@Omessage,@Action='SUBMIT'
			

		select 'Succes' title, @Omessage  message,'success' status
	end
	else
	begin
		select 'Sorry' title, 'data does not exists' message,'error' status
	end
END
