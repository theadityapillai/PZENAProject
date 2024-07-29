USE [EquityDataProcessing]
GO
--************************** Usage:
--DECLARE	@return_value int,
--		@AveragePrice decimal(19, 4),
--		@HighPrice decimal(19, 4),
--		@LowPrice decimal(19, 4)

--EXEC	[dbo].[TickerPrices]
--		@Ticker = N'abc',
--		@AveragePrice = @AveragePrice OUTPUT,
--		@HighPrice = @HighPrice OUTPUT,
--		@LowPrice = @LowPrice OUTPUT
--**************************

Create Procedure TickerPrices
	@Ticker varchar(10),
	@AveragePrice decimal(19,4) out, @HighPrice decimal(19,4) out, @LowPrice decimal(19,4) out
AS
BEGIN
SELECT @AveragePrice = AVG(dbo.[Prices].[close]) FROM PRICES WHERE [date] > DATEADD(DAY,-52, GETDATE()) AND TICKER = @Ticker
SELECT @HighPrice = MAX([high]) from PRICES WHERE [date] > DATEADD(WEEK,-52, GETDATE()) AND TICKER = @Ticker
SELECT @LowPrice = MIN([low]) from PRICES WHERE [date] > DATEADD(WEEK,-52, GETDATE()) AND TICKER = @Ticker
END