USE [FleetManagement]
GO
ALTER TABLE [dbo].[Voertuigen] DROP CONSTRAINT [FK_WagenTypes_Voertuigen]
GO
ALTER TABLE [dbo].[Voertuigen] DROP CONSTRAINT [FK_Voertuigen_BrandstofTypes]
GO
ALTER TABLE [dbo].[Tankkaarten_BrandstofTypes] DROP CONSTRAINT [FK_Tankkaarten_BrandstofTypes_Tankkaarten]
GO
ALTER TABLE [dbo].[Tankkaarten_BrandstofTypes] DROP CONSTRAINT [FK_Tankkaarten_BrandstofTypes_BrandstofTypes]
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders] DROP CONSTRAINT [FK_RijbewijsTypes_Bestuurders_RijbewijsTypes1]
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders] DROP CONSTRAINT [FK_RijbewijsTypes_Bestuurders_Bestuurders1]
GO
ALTER TABLE [dbo].[Bestuurders] DROP CONSTRAINT [FK_Bestuurders_Voertuigen]
GO
ALTER TABLE [dbo].[Bestuurders] DROP CONSTRAINT [FK_Bestuurders_Tankkaarten]
GO
/****** Object:  Table [dbo].[WagenTypes]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WagenTypes]') AND type in (N'U'))
DROP TABLE [dbo].[WagenTypes]
GO
/****** Object:  Table [dbo].[Voertuigen]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Voertuigen]') AND type in (N'U'))
DROP TABLE [dbo].[Voertuigen]
GO
/****** Object:  Table [dbo].[Tankkaarten_BrandstofTypes]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tankkaarten_BrandstofTypes]') AND type in (N'U'))
DROP TABLE [dbo].[Tankkaarten_BrandstofTypes]
GO
/****** Object:  Table [dbo].[Tankkaarten]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tankkaarten]') AND type in (N'U'))
DROP TABLE [dbo].[Tankkaarten]
GO
/****** Object:  Table [dbo].[RijbewijsTypes_Bestuurders]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RijbewijsTypes_Bestuurders]') AND type in (N'U'))
DROP TABLE [dbo].[RijbewijsTypes_Bestuurders]
GO
/****** Object:  Table [dbo].[RijbewijsTypes]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RijbewijsTypes]') AND type in (N'U'))
DROP TABLE [dbo].[RijbewijsTypes]
GO
/****** Object:  Table [dbo].[BrandstofTypes]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandstofTypes]') AND type in (N'U'))
DROP TABLE [dbo].[BrandstofTypes]
GO
/****** Object:  Table [dbo].[Bestuurders]    Script Date: 13/01/2022 13:30:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bestuurders]') AND type in (N'U'))
DROP TABLE [dbo].[Bestuurders]
GO
/****** Object:  Table [dbo].[Bestuurders]    Script Date: 13/01/2022 13:30:26 ******/
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
	[Straat] [nvarchar](255) NULL,
	[Huisnummer] [nvarchar](255) NULL,
	[Postcode] [nvarchar](255) NULL,
	[Land] [nvarchar](255) NULL,
	[Stad] [nvarchar](255) NULL,
 CONSTRAINT [PK_Bestuurders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandstofTypes]    Script Date: 13/01/2022 13:30:26 ******/
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
/****** Object:  Table [dbo].[RijbewijsTypes]    Script Date: 13/01/2022 13:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RijbewijsTypes](
	[Type] [nvarchar](255) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_RijbewijsTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RijbewijsTypes_Bestuurders]    Script Date: 13/01/2022 13:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RijbewijsTypes_Bestuurders](
	[RijbewijsTypeId] [int] NOT NULL,
	[BestuurderId] [int] NOT NULL,
 CONSTRAINT [PK_RijbewijsTypes_Bestuurders_1] PRIMARY KEY CLUSTERED 
(
	[RijbewijsTypeId] ASC,
	[BestuurderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tankkaarten]    Script Date: 13/01/2022 13:30:26 ******/
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
/****** Object:  Table [dbo].[Tankkaarten_BrandstofTypes]    Script Date: 13/01/2022 13:30:26 ******/
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
/****** Object:  Table [dbo].[Voertuigen]    Script Date: 13/01/2022 13:30:26 ******/
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
	[BrandstofId] [int] NULL,
 CONSTRAINT [PK_Voertuigen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WagenTypes]    Script Date: 13/01/2022 13:30:26 ******/
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
SET IDENTITY_INSERT [dbo].[Bestuurders] ON 

INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (1, N'De Neef', N'Olivier', CAST(N'1999-10-06T00:00:00.0000000' AS DateTime2), N'99100630515', 0, 2, 1, N'Rosstraat', N'65', N'9200', N'BELGIE', N'DENDERMONDE')
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (2, N'Bens', N'Tim', CAST(N'1977-11-10T00:00:00.0000000' AS DateTime2), N'77111000135', 0, NULL, 3, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (3, N'Hardy', N'Rayna', CAST(N'1998-07-22T00:00:00.0000000' AS DateTime2), N'98072200287', 0, 4, 7, N'Kerkstraat', N'28', N'9000', N'BELGIE', N'GENT')
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (4, N'Schmidt', N'Sydnee', CAST(N'1980-06-19T00:00:00.0000000' AS DateTime2), N'80061900283', 0, NULL, NULL, N'Grotebaan', N'51', N'5000', N'BELGIE', N'ELDEGEM')
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (5, N'Jones', N'Larry', CAST(N'1984-08-24T00:00:00.0000000' AS DateTime2), N'84082400132', 0, NULL, 4, N'Kamerstraat', N'856', N'2000', N'BELGIE', N'LOKEREN')
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (6, N'Wilcox', N'Payton', CAST(N'1969-05-11T00:00:00.0000000' AS DateTime2), N'69051100186', 0, NULL, 10, N'Dokterpesroonstraat', N'4', N'9200', N'BELGIE', N'DENDERMONDE')
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (7, N'Harvey', N'Heidy', CAST(N'1999-07-21T00:00:00.0000000' AS DateTime2), N'99072100241', 0, 3, 6, N'Denderweg', N'32', N'9255', N'BELGIE', N'GIJZEGEM')
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (8, N'Hanson', N'Brent', CAST(N'1983-07-21T00:00:00.0000000' AS DateTime2), N'83072100194', 0, 7, 8, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (9, N'Buck', N'Justus', CAST(N'1972-09-04T00:00:00.0000000' AS DateTime2), N'72090400196', 0, 2, 5, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (10, N'Knox', N'Dakota', CAST(N'1976-06-17T00:00:00.0000000' AS DateTime2), N'76061700259', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (11, N'Van Den Bossche', N'Jordy', CAST(N'1999-10-06T00:00:00.0000000' AS DateTime2), N'99100630515', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Bestuurders] OFF
GO
SET IDENTITY_INSERT [dbo].[BrandstofTypes] ON 

INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (1, N'Benzine')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (2, N'Diesel')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (3, N'LPG')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (4, N'CNG')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (5, N'LNG')
SET IDENTITY_INSERT [dbo].[BrandstofTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[RijbewijsTypes] ON 

INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'B', 1)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'CE', 2)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'AM', 3)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'A1', 4)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'A2', 5)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'A', 6)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'C1', 7)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'D', 8)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'BE', 9)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'C1E', 10)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'D1E', 11)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'DE', 12)
INSERT [dbo].[RijbewijsTypes] ([Type], [Id]) VALUES (N'G', 13)
SET IDENTITY_INSERT [dbo].[RijbewijsTypes] OFF
GO
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (1, 1)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (1, 2)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (1, 4)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (1, 6)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (1, 9)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (1, 11)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (2, 1)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (2, 4)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (2, 5)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (7, 7)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (8, 8)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (9, 3)
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (9, 10)
GO
SET IDENTITY_INSERT [dbo].[Tankkaarten] ON 

INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (2, N'5615385463', CAST(N'2024-08-12T00:00:00.0000000' AS DateTime2), N'1492', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (3, N'135466153', CAST(N'2021-11-17T00:00:00.0000000' AS DateTime2), N'1234', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (4, N'123645', CAST(N'2021-11-17T00:00:00.0000000' AS DateTime2), N'1234', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (5, N'5665665', CAST(N'2022-02-10T00:00:00.0000000' AS DateTime2), N'5325', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (6, N'9987565', CAST(N'2022-05-08T00:00:00.0000000' AS DateTime2), N'9665', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (7, N'99856532', CAST(N'2022-05-22T00:00:00.0000000' AS DateTime2), N'1235', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (8, N'9323324', CAST(N'2022-05-14T00:00:00.0000000' AS DateTime2), N'6546', 0, 0)
SET IDENTITY_INSERT [dbo].[Tankkaarten] OFF
GO
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (2, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (2, 2)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (3, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (3, 3)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (3, 5)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (4, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (4, 2)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (5, 2)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (6, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (6, 2)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (7, 2)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (7, 4)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (8, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (8, 2)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (8, 3)
GO
SET IDENTITY_INSERT [dbo].[Voertuigen] ON 

INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (1, N'Volkswagen', N'Golf', N'12345678912345678', N'1-ABC-123', 0, N'Black', 5, 0, 1, 2)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (2, N'Ford', N'Fiesta', N'12345678987456321', N'2-ABC-321', 0, N'Blue', 3, 0, 2, 1)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (3, N'Mazda', N'MX-3', N'WBSLZ9C5XCC985482', N'1-DFG-512', 0, N'Black', 3, 0, 4, 1)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (4, N'Fiat', N'500', N'2GTEK13M471577925', N'1-ADV-123', 0, N'Green', 3, 0, 5, 1)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (5, N'Maserati', N'GranCabrio', N'5TDYK3EH8AS092633', N'1-ACV-853', 0, N'Yellow', 5, 0, 1, 2)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (6, N'Seat', N'Altea', N'1FMCU22X4NUD42814', N'1-ABT-583', 0, N'Red', 4, 0, 4, 4)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (7, N'BMW', N'X5', N'JM1CW2BL5C0111646', N'1-ADG-152', 0, N'Purple', 4, 0, 3, 5)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (8, N'Lincon', N'1965', N'JN8AS5MT4DW564712', N'1-ZER-182', 0, N'Red', 5, 0, 1, 2)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (9, N'Suzuki', N'Liana', N'WBAEV33483KL16761', N'1-RTE-156', 0, N'Yellow', 5, 0, 7, 1)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassinummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (10, N'Toyoto', N'Carina', N'1D8GT58638W194531', N'1-AZE-865', 0, N'Geen kleur ingesteld', 4, 0, 6, 3)
SET IDENTITY_INSERT [dbo].[Voertuigen] OFF
GO
SET IDENTITY_INSERT [dbo].[WagenTypes] ON 

INSERT [dbo].[WagenTypes] ([Id], [Type]) VALUES (1, N'Hatchback')
INSERT [dbo].[WagenTypes] ([Id], [Type]) VALUES (2, N'Sedan')
INSERT [dbo].[WagenTypes] ([Id], [Type]) VALUES (3, N'Station')
INSERT [dbo].[WagenTypes] ([Id], [Type]) VALUES (4, N'Cabriolet')
INSERT [dbo].[WagenTypes] ([Id], [Type]) VALUES (5, N'Coupé')
INSERT [dbo].[WagenTypes] ([Id], [Type]) VALUES (6, N'Multi Purpose Vehicle (MVP)')
INSERT [dbo].[WagenTypes] ([Id], [Type]) VALUES (7, N'Terreinauto')
SET IDENTITY_INSERT [dbo].[WagenTypes] OFF
GO
ALTER TABLE [dbo].[Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurders_Tankkaarten] FOREIGN KEY([TankkaartId])
REFERENCES [dbo].[Tankkaarten] ([Id])
GO
ALTER TABLE [dbo].[Bestuurders] CHECK CONSTRAINT [FK_Bestuurders_Tankkaarten]
GO
ALTER TABLE [dbo].[Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurders_Voertuigen] FOREIGN KEY([VoertuigId])
REFERENCES [dbo].[Voertuigen] ([Id])
GO
ALTER TABLE [dbo].[Bestuurders] CHECK CONSTRAINT [FK_Bestuurders_Voertuigen]
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_RijbewijsTypes_Bestuurders_Bestuurders1] FOREIGN KEY([BestuurderId])
REFERENCES [dbo].[Bestuurders] ([Id])
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders] CHECK CONSTRAINT [FK_RijbewijsTypes_Bestuurders_Bestuurders1]
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders]  WITH CHECK ADD  CONSTRAINT [FK_RijbewijsTypes_Bestuurders_RijbewijsTypes1] FOREIGN KEY([RijbewijsTypeId])
REFERENCES [dbo].[RijbewijsTypes] ([Id])
GO
ALTER TABLE [dbo].[RijbewijsTypes_Bestuurders] CHECK CONSTRAINT [FK_RijbewijsTypes_Bestuurders_RijbewijsTypes1]
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
ALTER TABLE [dbo].[Voertuigen]  WITH CHECK ADD  CONSTRAINT [FK_Voertuigen_BrandstofTypes] FOREIGN KEY([BrandstofId])
REFERENCES [dbo].[BrandstofTypes] ([Id])
GO
ALTER TABLE [dbo].[Voertuigen] CHECK CONSTRAINT [FK_Voertuigen_BrandstofTypes]
GO
ALTER TABLE [dbo].[Voertuigen]  WITH CHECK ADD  CONSTRAINT [FK_WagenTypes_Voertuigen] FOREIGN KEY([WagenTypeId])
REFERENCES [dbo].[WagenTypes] ([Id])
GO
ALTER TABLE [dbo].[Voertuigen] CHECK CONSTRAINT [FK_WagenTypes_Voertuigen]
GO
