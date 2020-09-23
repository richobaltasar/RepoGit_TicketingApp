CREATE PROCEDURE SP_GetListDataMaster
	@Data nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if exists(select*from ERP.dbo.Master_ListItem where ListName = @Data)
	begin
		select*from ERP.dbo.Master_ListItem where ListName = @Data
		order by Urutan asc
	end
	else
	begin
		if(@Data ='ListMasterModule')
		begin
            select*from 
			(
				select '' Text, 0 Value
				union all
				select NamaModule Text, IdModul Value from DataModule where Status = 1
			) q
			order by q.Text
		end
		else if(@Data = 'ListJenisTicket')
		begin
			select*from 
			(
				select '' Text, 0 Value
				union all
				select namaticket Text, IdTicket Value from DataTicket
				where status = 1
			) q
			order by q.Text
		end
		else if(@Data = 'ListKaryawan')
		begin
			
			select*from 
			(
				select '' Text, 0 Value
				union all
				select NamaLengkap +' - '+ NamaDivisi Text,id Value from UserData
				where Status = 1
			) q
			order by q.Text
		end
		else if(@Data = 'ListRABApproved')
		begin
			
			select*from 
			(
				select '' Text, 0 Value
				union all
				select 'RAB-'+cast(Id as nvarchar(max))+' '+NamaRAB+' ' Text,Id Value from DataRAB
				where StatusRAB = 3
			) q
			order by q.Text
		end
		else if(@Data = 'ListKodeTelpon')
		begin
			
			select*from 
			(
				select '' Text, 0 Value
				union all
				select NamaNegara +' (+'+cast(KodeTelpon as nvarchar(max))+')' Text,KodeTelpon Id from DataKodeTelpon
			) q
			order by q.Text
		end
		else if(@Data = 'ListDataProvinsi')
		begin
			
			select*from 
			(
				select '' Text, '' Value
				union all
				select
					distinct Province Text, Province Value
				from DataKotaPostalCode
			) q
			where Text is not null
			order by q.Text
		end
        else if(@Data = 'ListCategoryTransaksi')
		begin
			
			select*from 
			(
				select '' Text, '' Value
				union all
                select
                    distinct Category Text, Category Value 
                from LogTransaksiListDetailPOS
			) q
			where Text is not null
			order by q.Text
		end
        else if(@Data = 'ListPaymentMethod')
		begin
			
			select*from 
			(
				select '' Text, '' Value
				union all
                select
                    distinct PaymentMethod Text, 
                    PaymentMethod Value 
                from LogTransaksiPOS
			) q
			where Text is not null
			order by q.Text
		end
        else if(@Data = 'ListPlatform')
		begin
			
			select*from 
			(
				select '' Text, '' Value
				union all
                select distinct Platform Text, Platform Value from DataMenu
			) q
			where Text is not null
			order by q.Text
		end
        else if(@Data ='ListMasterModuleFilter')
		begin
            select*from 
			(
				select '' Text, '' Value
				union all
				select NamaModule Text, NamaModule Value from DataModule where Status = 1
			) q
			order by q.Text
		end
        else if(@Data ='ListMainMenu')
		begin
            select*from 
			(
				select '' Text, '' Value
				union all
				select NamaMenu Text, NamaMenu Value from DataMenu where idMenu IN
				(
					select idMenu from Role_MenuTree where IdRole in
					(select IdParent from Role_MenuTree where IdParent != 0)
				)
				

			) q
			order by q.Text
		end
		else if(@Data ='ListUserKasir')
		begin
            select*from 
			(
				select 'All' Text, '' Value
				union all
				select  
				distinct ChasierName Text,ChasierName Value
				from LogTransaksiPOS where Status=1

			) q
			order by q.Text
		end
	end
END