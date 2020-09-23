-- SP_GETTICKETPRICE ''
CREATE PROCEDURE [dbo].[SP_GETTICKETPRICE]
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	declare @asuransi float

	select @asuransi = val2 from DataParam where NamaParam = 'Asuransi' and val1='Asuransi Kecelakaan'
	
	select Top 1 IdJenisTicket,idPromo,NamaPromo,Diskon into #TempPromo from DataPromo
	WHERE   
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(BerlakuDari,10),'/','-'), 105), 23),'-','') <= CONVERT(VARCHAR(10),FORMAT(GETDATE() , 'yyyyMMdd'))
	and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(BerlakuSampai,10),'/','-'), 105), 23),'-','')   >= CONVERT(VARCHAR(10),FORMAT(GETDATE() , 'yyyyMMdd'))
	order by idPromo desc

	if exists(select*from DataHariLiburNasional
	WHERE   
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(DariTanggal,10),'/','-'), 105), 23),'-','') <= CONVERT(VARCHAR(10),FORMAT(GETDATE() , 'yyyyMMdd'))
	and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(SampaiTanggal,10),'/','-'), 105), 23),'-','')   >= CONVERT(VARCHAR(10),FORMAT(GETDATE() , 'yyyyMMdd')))
	begin
		select IdTicket id,namaticket NamaTicket,HargaHoliday Harga,Img,@asuransi Asuransi,isnull(b.NamaPromo,'') NamaPromo ,isnull(b.Diskon,0) Diskon  
		from DataTicket	a
		left join #TempPromo b on b.IdJenisTicket = a.IdTicket
		where NamaTicket like '%'+@search+'%' and status = 1
	end
	else
	begin
		declare @harike bigint
		SELECT @harike = DATEPART(DW, GETDATE()) 
		if(@harike = 1 or @harike = 7)
		begin 
			select IdTicket id,namaticket NamaTicket,HargaWeekEnd Harga,Img,@asuransi Asuransi, isnull(b.NamaPromo,'') NamaPromo ,isnull(b.Diskon,0) Diskon  
			from DataTicket	a
				left join #TempPromo b on b.IdJenisTicket = a.IdTicket
			where NamaTicket like '%'+@search+'%' and status = 1

		end
		else
		begin
			select IdTicket id,namaticket NamaTicket,HargaWeekDay Harga,Img,@asuransi Asuransi,isnull(b.NamaPromo,'') NamaPromo ,isnull(b.Diskon,0) Diskon  
			from DataTicket	a
			left join #TempPromo b on b.IdJenisTicket = a.IdTicket
			where NamaTicket like '%'+@search+'%' and status = 1
		end
	end

END