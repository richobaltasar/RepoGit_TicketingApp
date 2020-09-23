--SP_GetRekapanTenantTahun '2020','Persewaan'

CREATE PROCEDURE [dbo].[SP_GetRekapanTenantTahun]
	@SetTahunan nvarchar(max),
	@NamaTenant nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	--declare @SetTahunan nvarchar(max)
	--set @SetTahunan = '2020'
	
	select Month(Tanggal) Bulan,q.NamaItem Uraian,sum(ISNULL(Qtx,0)) Qty, sum(ISNULL(Total,0)) Jumlah into #temp from
	(
		select CONVERT(VARCHAR(10), CONVERT(date, replace(left(a.Datetime,10),'/','-'), 105), 23) Tanggal,NamaItem,Qtx,Total
		from [LogItemsF&BTrx] a
		where right(left(a.Datetime,10),4) = @SetTahunan
		and NamaTenant = @NamaTenant
	) q
	group by  Month(Tanggal),NamaItem

	create table #Db
	(
		no bigint,
		Uraian nvarchar(max),
		
		Qty1 float,
		Jumlah1 float,
		Qty2 float,
		Jumlah2 float,
		Qty3 float,
		Jumlah3 float,
		Qty4 float,
		Jumlah4 float,
		Qty5 float,
		Jumlah5 float,

		Qty6 float,
		Jumlah6 float,
		Qty7 float,
		Jumlah7 float,
		Qty8 float,
		Jumlah8 float,
		Qty9 float,
		Jumlah9 float,
		Qty10 float,
		Jumlah10 float,

		Qty11 float,
		Jumlah11 float,
		Qty12 float,
		Jumlah12 float,
		
		Total float,
	)
	
	declare @no bigint
	declare @Uraian nvarchar(max)
	declare @Qty float
	declare @Jumlah float
	declare @WEEK_OF_MONTH nvarchar(max)
	set @no = 1

	while EXISTS (select*from #temp)
	begin
		 SELECT TOP 1 @Uraian = Uraian,@Qty = Qty,@Jumlah=Jumlah,@WEEK_OF_MONTH = Bulan FROM  #Temp 
		 
		 if(@WEEK_OF_MONTH = '1')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '2')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '3')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '4')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '5')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '6')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '7')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '8')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '9')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '10')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '11')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = '12')
		 begin
			insert into #Db (no,Uraian,
				Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,
				Jumlah6,Qty6,Jumlah7,Qty7,Jumlah8,Qty8,Jumlah9,Qty9,Jumlah10,Qty10,
				Jumlah11,Qty11,Jumlah12,Qty12,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,@Jumlah)
		 end

		 delete from #temp where Bulan = @WEEK_OF_MONTH and Uraian = @Uraian and Qty=@Qty

		 set @no = @no+ 1
	end
	
	select Uraian,
	sum(Jumlah1)Jumlah1,sum(Qty1)Qty1,
	sum(Jumlah2)Jumlah2,sum(Qty2)Qty2,
	sum(Jumlah3)Jumlah3,sum(Qty3)Qty3,
	sum(Jumlah4)Jumlah4,sum(Qty4)Qty4,
	sum(Jumlah5)Jumlah5,sum(Qty5)Qty5,

	sum(Jumlah6)Jumlah6,sum(Qty6)Qty6,
	sum(Jumlah7)Jumlah7,sum(Qty7)Qty7,
	sum(Jumlah8)Jumlah8,sum(Qty8)Qty8,
	sum(Jumlah9)Jumlah9,sum(Qty9)Qty9,
	sum(Jumlah10)Jumlah10,sum(Qty10)Qty10,

	sum(Jumlah11)Jumlah11,sum(Qty11)Qty11,
	sum(Jumlah12)Jumlah12,sum(Qty12)Qty12,

	sum(Total) Total from #Db
	group by Uraian
	
	drop table #Db
	drop table #temp
END















