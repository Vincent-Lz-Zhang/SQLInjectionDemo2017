USE [SQLInjectionDemo]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 29/05/2017 4:33:46 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

GO
INSERT [dbo].[Admin] ([Id], [Email], [Password]) VALUES (1, N'joe@vmail.com', N'1q2w3e')
GO
INSERT [dbo].[Admin] ([Id], [Email], [Password]) VALUES (2, N'zoe@vmail.net', N'4r5t6y')
GO
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
