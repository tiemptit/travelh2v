use TravelH2V_DW
/****** Object:  Table [dbo].[segments]    Script Date: 12/13/2011 22:49:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[segments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[budget] [int] NULL,
	[companion] [int] NULL,
	[familiarity] [int] NULL,
	[mood] [int] NULL,
	[temperature] [int] NULL,
	[travelLength] [int] NULL,
	[weather] [int] NULL,
	[performance] [float] NULL,
	CorrelationAvg float NULL
 CONSTRAINT [PK_segments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


