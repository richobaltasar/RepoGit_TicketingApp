
-- exec SP_Get_KolomLog_bulanan @Month='08/2020'

CREATE PROCEDURE SP_Get_TanggalLog_bulanan
	@Month nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    value  into #temp
	FROM 
		STRING_SPLIT(@Month, '/')
	order by value desc
	
	declare @SMonth nvarchar(max)
	set @SMonth = ''
	while exists(select *from #temp)
	begin
		declare @S nvarchar(max)
		select top 1 @S=value from #temp order by value desc
		set @SMonth = @SMonth + @S
		
		delete #temp where value = @S
	end
	drop table #temp
	print @SMonth

	--Ticketing
	select distinct left(b.Datetime,10) Tanggal
	from LogTransaksiListDetailPOS a
	left join LogTransaksiPOS b on b.idTrx = a.IdTrx
	where 
	left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
	and b.Status=1

END
