-- SP_GetRekapanTenantBulanan '01/2020',''

CREATE PROCEDURE SP_GetRekapanTenantBulanan
	@SetBulan nvarchar(max),
	@NamaTenant nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	--declare @SetBulan nvarchar(max)
	--set @SetBulan = '01/2020'
	
	select
		dbo.Roman(e.WEEK_OF_MONTH) WEEK_OF_MONTH,e.Uraian,sum(e.Qty) Qty,sum(e.Jumlah) Jumlah into #temp
	from
	(
		select w.WEEK_OF_MONTH,w.NamaItem Uraian, sum(isnull(Harga,0)) Harga,sum(isnull(Qtx,0)) Qty, sum(isnull(Total,0)) Jumlah from 
		(
			select 
			DATEPART(WEEK,q.Tanggal )  -
							DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,q.Tanggal ), 0))+ 1 AS WEEK_OF_MONTH,q.*from
			(
				select CONVERT(VARCHAR(10), CONVERT(date, replace(left(a.Datetime,10),'/','-'), 105), 23) Tanggal,NamaItem,Harga,Qtx,Total
				from [LogItemsF&BTrx] a
				where right(left(a.Datetime,10),7) = @SetBulan
				and NamaTenant = @NamaTenant
			) q
		) w
		group by w.WEEK_OF_MONTH,w.NamaItem
	) e
	group by e.WEEK_OF_MONTH,e.Uraian

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
		Total float
	)
	
	declare @no bigint
	declare @Uraian nvarchar(max)
	declare @Qty float
	declare @Jumlah float
	declare @WEEK_OF_MONTH nvarchar(max)
	set @no = 1
	
	while EXISTS (select*from #temp)
	begin
		 SELECT TOP 1 @Uraian = Uraian,@Qty = Qty,@Jumlah=Jumlah,@WEEK_OF_MONTH = WEEK_OF_MONTH FROM  #temp 
		 
		 if(@WEEK_OF_MONTH = 'I')
		 begin
			insert into #Db (no,Uraian,Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,Jumlah6,Qty6,Total)
			values(@no,@Uraian,@Jumlah,@Qty,0,0,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = 'II')
		 begin
			insert into #Db (no,Uraian,Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,Jumlah6,Qty6,Total)
			values(@no,@Uraian,0,0,@Jumlah,@Qty,0,0,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = 'III')
		 begin
			insert into #Db (no,Uraian,Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,Jumlah6,Qty6,Total)
			values(@no,@Uraian,0,0,0,0,@Jumlah,@Qty,0,0,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = 'IV')
		 begin
			insert into #Db (no,Uraian,Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,Jumlah6,Qty6,Total)
			values(@no,@Uraian,0,0,0,0,0,0,@Jumlah,@Qty,0,0,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = 'V')
		 begin
			insert into #Db (no,Uraian,Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,Jumlah6,Qty6,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,@Jumlah,@Qty,0,0,@Jumlah)
		 end
		 else if(@WEEK_OF_MONTH = 'VI')
		 begin
			insert into #Db (no,Uraian,Jumlah1,Qty1,Jumlah2,Qty2,Jumlah3,Qty3,Jumlah4,Qty4,Jumlah5,Qty5,Jumlah6,Qty6,Total)
			values(@no,@Uraian,0,0,0,0,0,0,0,0,0,0,@Jumlah,@Qty,@Jumlah)
		 end

		 delete from #temp where WEEK_OF_MONTH = @WEEK_OF_MONTH and Uraian = @Uraian
		 set @no = @no+ 1
	end
	
	select 
		Uraian,sum(Jumlah1)Jumlah1,sum(Qty1)Qty1,sum(Jumlah2)Jumlah2,sum(Qty2)Qty2,sum(Jumlah3)Jumlah3,sum(Qty3)Qty3,
		sum(Jumlah4)Jumlah4,sum(Qty4)Qty4,sum(Jumlah5)Jumlah5,sum(Qty5)Qty5,sum(Jumlah6)Jumlah6,sum(Qty6)Qty6,sum(Total) Total 
	from #Db
	group by Uraian
	
	drop table #temp

	drop table #Db
END















