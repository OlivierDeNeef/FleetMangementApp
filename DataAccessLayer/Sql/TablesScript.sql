USE [FleetManagement]
GO
/****** Object:  Table [dbo].[Adressen]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adressen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Straat] [nvarchar](255) NULL,
	[Huisnummer] [nvarchar](255) NULL,
	[Busnummer] [nvarchar](255) NOT NULL,
	[Stad] [nvarchar](255) NULL,
	[Postcode] [nvarchar](255) NULL,
	[Land] [nvarchar](255) NULL,
 CONSTRAINT [PK_Adressen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bestuurders]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bestuurders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](255) NOT NULL,
	[Voornaam] [nvarchar](255) NOT NULL,
	[Geboortedatum] [datetime2](7) NOT NULL,
	[Rijksregisternummer] [nvarchar](255) NOT NULL,
	[Gearchiveerd] [bit] NOT NULL,
	[TankkaartId] [int] NULL,
	[VoertuigId] [int] NULL,
	[AdresId] [int] NULL,
 CONSTRAINT [PK_Bestuurders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandstofTypes]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandstofTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_BrandstofTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RijbewijsTypes]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RijbewijsTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_RijbewijsTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RijbewijsTypes_Bestuurders]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RijbewijsTypes_Bestuurders](
	[RijbewijsTypeId] [int] NOT NULL,
	[BestuurderId] [int] NOT NULL,
 CONSTRAINT [PK_RijbewijsTypes_Bestuurders] PRIMARY KEY CLUSTERED 
(
	[RijbewijsTypeId] ASC,
	[BestuurderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tankkaarten]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tankkaarten](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kaartnummer] [nvarchar](255) NOT NULL,
	[Geldigheidsdatum] [datetime2](7) NOT NULL,
	[Pincode] [nvarchar](255) NULL,
	[Gearchiveerd] [bit] NOT NULL,
	[Geblokkeerd] [bit] NOT NULL,
 CONSTRAINT [PK_Tankkaarteno] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tankkaarten_BrandstofTypes]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tankkaarten_BrandstofTypes](
	[TankkaartId] [int] NOT NULL,
	[BrandstofTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Tankkaarten_BrandstofTypes_1] PRIMARY KEY CLUSTERED 
(
	[TankkaartId] ASC,
	[BrandstofTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voertuigen]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voertuigen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Merk] [nvarchar](255) NOT NULL,
	[Model] [nvarchar](255) NOT NULL,
	[Chassinummer] [nvarchar](255) NOT NULL,
	[Nummerplaat] [nvarchar](255) NOT NULL,
	[Gearchiveerd] [bit] NOT NULL,
	[Kleur] [nvarchar](255) NULL,
	[AantalDeuren] [int] NULL,
	[Hybride] [bit] NOT NULL,
	[WagenTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Tankkaarten] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WagenTypes]    Script Date: 3/11/2021 17:58:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WagenTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_WagenTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurders_Adressen] FOREIGN KEY([AdresId])
REFERENCES [dbo].[Adressen] ([Id])
GO
ALTER TABLE [dbo].[Bestuurders] CHECK CONSTRAINT [FK_Bestuurders_Adressen]
GO
ALTER TABLE [dbo].[Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurders_Tankkaarten] FOREIGN KEY([AdresId])
REFERENCES [dbo].[Tankkaarten] ([Id])
GO
ALTER TABLE [dbo].[Bestuurders] CHECK CONSTRAINT [FK_Bestuurders_Tankkaarten]
GO
ALTER TABLE [dbo].[Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurders_Voertuigen] FOREIGN KEY([Id])
REFERENCES [dbo].[Voertuigen] ([Id])
GO
ALTER TABLE [dbo].[Bestuurders] CHECK CONSTRAINT [FK_Bestuurders_Voertuigen]
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_RijbewijsTypes_Bestuurders_Bestuurders] FOREIGN KEY([BestuurderId])
REFERENCES [dbo].[Bestuurders] ([Id])
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders] CHECK CONSTRAINT [FK_RijbewijsTypes_Bestuurders_Bestuurders]
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_RijbewijsTypes_Bestuurders_RijbewijsTypes] FOREIGN KEY([RijbewijsTypeId])
REFERENCES [dbo].[RijbewijsTypes] ([Id])
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders] CHECK CONSTRAINT [FK_RijbewijsTypes_Bestuurders_RijbewijsTypes]
GO
ALTER TABLE [dbo].[Tankkaarten]  WITH CHECK ADD  CONSTRAINT [FK_Tankkaarten_Tankkaarten] FOREIGN KEY([Id])
REFERENCES [dbo].[Tankkaarten] ([Id])
GO
ALTER TABLE [dbo].[Tankkaarten] CHECK CONSTRAINT [FK_Tankkaarten_Tankkaarten]
GO
ALTER TABLE [dbo].[Tankkaarten_BrandstofTypes]  WITH CHECK ADD  CONSTRAINT [FK_Tankkaarten_BrandstofTypes_BrandstofTypes] FOREIGN KEY([BrandstofTypeId])
REFERENCES [dbo].[BrandstofTypes] ([Id])
GO
ALTER TABLE [dbo].[Tankkaarten_BrandstofTypes] CHECK CONSTRAINT [FK_Tankkaarten_BrandstofTypes_BrandstofTypes]
GO
ALTER TABLE [dbo].[Tankkaarten_BrandstofTypes]  WITH CHECK ADD  CONSTRAINT [FK_Tankkaarten_BrandstofTypes_Tankkaarten] FOREIGN KEY([TankkaartId])
REFERENCES [dbo].[Tankkaarten] ([Id])
GO
ALTER TABLE [dbo].[Tankkaarten_BrandstofTypes] CHECK CONSTRAINT [FK_Tankkaarten_BrandstofTypes_Tankkaarten]
GO
ALTER TABLE [dbo].[Voertuigen]  WITH CHECK ADD  CONSTRAINT [FK_WagenTypes_Voertuigen] FOREIGN KEY([WagenTypeId])
REFERENCES [dbo].[WagenTypes] ([Id])
GO
ALTER TABLE [dbo].[Voertuigen] CHECK CONSTRAINT [FK_WagenTypes_Voertuigen]
GO
