CREATE PROCEDURE [dbo].[SP_UpdateAccountDataRead] 
	@AccountNumber nvarchar(50), -- Account number == ID card
	@Balanced float,
	@JaminanGelang float,
	@Ticket float
AS
BEGIN
	SET NOCOUNT ON;
	if exists(select*from DataAccount where AccountNumber = @AccountNumber)
	begin
		update DataAccount set Balanced = @Balanced, Ticket = @Ticket,UpdateDate = FORMAT(GetDate(), 'dd/MM/yyyy HH:mm:ss'),
		UangJaminan = @JaminanGelang,
		ExpiredDate = FORMAT(dateadd(day, 30, getdate()),'dd/MM/yyyy HH:mm:ss')
		where AccountNumber = @AccountNumber
		select 'Update Success' as Result
	end
	else
	begin
		insert into DataAccount (AccountNumber, Balanced, Ticket,UangJaminan, CreateDate, ExpiredDate,UpdateDate, Status)
		values(@AccountNumber, @Balanced,@Ticket,@JaminanGelang,FORMAT(GetDate(), 'dd/MM/yyyy HH:mm:ss'),FORMAT(dateadd(day, 30, getdate()),'dd/MM/yyyy HH:mm:ss'),FORMAT(GetDate(), 'yyyy-MM-dd hh:mm:ss'),1)
		select 'Insert Success' as Result
	end

	declare @Debit float
	declare @Credit float
	declare @Deposit float
	declare @DepositYesterday float

	select @Debit=sum(isnull(Debit,0)),@Credit=sum(isnull(Credit,0))  from 
	(
		select
		case when TransactionType = 'DEBIT' then Nominal end as Debit,
		case when TransactionType = 'CREDIT' then Nominal end as Credit
		from LogDeposit 
		where 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(FORMAT(GetDate(), 'dd/MM/yyyy HH:mm:ss'),10),'/','-'), 105), 23),'-','') 
	) q

	print cast(@Debit as nvarchar(max))
	print cast(@Credit as nvarchar(max))

	select @DepositYesterday=Deposit
	from DataDeposit 
	where  
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
	replace(dateadd(day,-1, cast(CONVERT(VARCHAR(10), CONVERT(date, replace(left(FORMAT(GetDate(), 'dd/MM/yyyy HH:mm:ss'),10),'/','-'), 105), 23) as date)),'-','') 

	update DataDeposit 
	set Credit = @Credit, Debit = @Debit,DepositHariSebelumnya=@DepositYesterday,
	Deposit=((@DepositYesterday-@Debit) + @Credit)
	where 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(FORMAT(GetDate(), 'dd/MM/yyyy HH:mm:ss'),10),'/','-'), 105), 23),'-','') 

END
