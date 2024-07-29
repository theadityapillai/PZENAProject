USE [EquityDataProcessing]
GO

/****** Object:  Table [dbo].[Tickers]    Script Date: 7/29/2024 8:34:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tickers]') AND type in (N'U'))
DROP TABLE [dbo].[Tickers]
GO
CREATE TABLE [dbo].[Tickers](
	[exchange] [varchar](100) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[table] [varchar](10) NOT NULL,
	[permaticker] [int] NOT NULL,
	[ticker] [varchar](10) NOT NULL,
	[isdelisted] [char](1) NOT NULL,
	[category] [varchar](100) NOT NULL,
	[cusips] [varchar](max) NULL,
	[siccode] [varchar](50) NOT NULL,
	[sicsector] [varchar](100) NOT NULL,
	[sicindustry] [varchar](100) NOT NULL,
	[famasector] [varchar](100) NULL,
	[famaindustry] [varchar](100) NOT NULL,
	[sector] [varchar](100) NOT NULL,
	[industry] [varchar](100) NOT NULL,
	[scalemarketcap] [varchar](50) NULL,
	[scalerevenue] [varchar](50) NULL,
	[relatedtickers] [varchar](max) NULL,
	[currency] [nchar](10) NOT NULL,
	[location] [varchar](100) NULL,
	[lastupdated] [date] NULL,
	[firstadded] [date] NULL,
	[firstpricedate] [date] NULL,
	[lastpricedate] [date] NULL,
	[firstquarter] [date] NULL,
	[lastquarter] [date] NULL,
	[secfilings] [varchar](max) NOT NULL,
	[companysite] [varchar](max) NULL,
 CONSTRAINT [PK_Tickers] PRIMARY KEY CLUSTERED 
(
	[ticker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tickers]  WITH CHECK ADD  CONSTRAINT [FK_Tickers_Tickers] FOREIGN KEY([ticker])
REFERENCES [dbo].[Tickers] ([ticker])
GO

ALTER TABLE [dbo].[Tickers] CHECK CONSTRAINT [FK_Tickers_Tickers]
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Tickers
   ON Tickers ([table], permaticker,ticker);


