CREATE PROCEDURE SP_UpdateDepositData
	@O_TransactionDate nvarchar(max),
	@Succes int Output,   
	@Message nvarchar(max) Output   
AS
BEGIN
	SET NOCOUNT ON;
	
BEGIN TRY

	declare @Debit float
	declare @Credit float
	declare @Deposit float
	declare @DepositYesterday float
	declare @TotalUangDiBox float
	declare @TotalUangMasuk float
	declare @TotalUangKeluar float

	select @Debit=sum(isnull(Debit,0)),@Credit=sum(isnull(Credit,0))  from 
	(
		select
		case when TransactionType = 'DEBIT' then Nominal end as Debit,
		case when TransactionType = 'CREDIT' then Nominal end as Credit
		from LogDeposit 
		where 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23),'-','') 
	) q

	select @DepositYesterday=Deposit
	from DataDeposit 
	where  
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
	replace(dateadd(day,-1, cast(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23) as date)),'-','') 

	if not exists(	select*from DataDeposit  
	where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23),'-','') )
	begin
		insert into DataDeposit
		(Datetime,Deposit,DepositHariSebelumnya,Credit,Debit,Status)
		values
		(left(@O_TransactionDate,10),((@DepositYesterday-@Debit) + @Credit),@DepositYesterday,@Credit,@Debit,1)

	end
	else
	begin
		update DataDeposit 
		set Credit = @Credit, Debit = @Debit,DepositHariSebelumnya=@DepositYesterday,
		Deposit=((@DepositYesterday-@Debit) + @Credit)
		where 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23),'-','')
	end

	set @Succes = 1
	set @Message = 'Data Deposit Up to date'
	

END TRY
BEGIN CATCH  
	set @Succes = 0
	set @Message = 'sp error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()
		
END CATCH;  
Return;
END
