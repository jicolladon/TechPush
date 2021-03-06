USE [TechdenciasPushSample]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 17/06/2018 20:36:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Tag] [nvarchar](max) NULL,
	[Image] [varbinary](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PushRegistrations]    Script Date: 17/06/2018 20:36:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PushRegistrations](
	[Id] [uniqueidentifier] NOT NULL,
	[DeviceId] [nvarchar](255) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[PlatformType] [nvarchar](max) NOT NULL,
	[Tags] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.PushRegistrations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserData]    Script Date: 17/06/2018 20:36:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserData](
	[Id] [uniqueidentifier] NOT NULL,
	[UserEmail] [nvarchar](100) NOT NULL,
	[UserPassword] [nvarchar](max) NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](max) NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Messages_dbo.UserData_User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserData] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_dbo.Messages_dbo.UserData_User_Id]
GO
ALTER TABLE [dbo].[PushRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PushRegistrations_dbo.UserData_User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserData] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PushRegistrations] CHECK CONSTRAINT [FK_dbo.PushRegistrations_dbo.UserData_User_Id]
GO
