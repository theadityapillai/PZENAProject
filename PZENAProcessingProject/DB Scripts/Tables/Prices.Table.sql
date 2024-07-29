USE [EquityDataProcessing]
GO

/****** Object:  Table [dbo].[Prices]    Script Date: 7/29/2024 8:19:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Prices]') AND type in (N'U'))
DROP TABLE [dbo].[Prices]
GO
CREATE TABLE [dbo].[Prices](
	[ticker] [varchar](10) NOT NULL,
	[date] [date] NOT NULL,
	[open] [decimal](19, 4) NOT NULL,
	[high] [decimal](19, 4) NOT NULL,
	[low] [decimal](19, 4) NOT NULL,
	[close] [decimal](19, 4) NOT NULL,
	[volume] [decimal](19, 4) NOT NULL,
	[closeadj] [decimal](19, 4) NOT NULL,
	[closeunadj] [decimal](19, 4) NOT NULL,
	[lastupdated] [date] NOT NULL
	
 CONSTRAINT [PK_Prices] PRIMARY KEY CLUSTERED 
(
	[ticker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_Tickers] FOREIGN KEY([ticker])
REFERENCES [dbo].[Tickers] ([ticker])
GO

ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_Tickers]
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Prices
   ON Prices (ticker, [date]);
