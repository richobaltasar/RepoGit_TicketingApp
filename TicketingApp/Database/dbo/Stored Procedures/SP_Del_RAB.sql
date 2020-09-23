
CREATE PROCEDURE SP_Del_RAB
	@Id bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataRAB where id = @Id)
		begin
			declare @Omessage nvarchar(max)
			select @Omessage='Data  '+NamaRAB+' - '+JenisRAB+' total budget :Rp '+CAST(TotalBudget as nvarchar(max))+' berhasil dihapus' 
			from DataRAB where id = @Id
			
			delete from DataRAB where id = @Id
			
			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataRAB',@IdRow = @Id,@message=@Omessage,@Action='DELETE'

			if not exists(select  * from DataVoucher where id = @Id)
			begin
				select 'Succes' title, @Omessage message,'success' status
			end
			else 
			begin
				select 'Sorry' title, 'data gagal delete' message,'error' status
			end
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Del_RAB error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
