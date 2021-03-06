USE [FleetManagement]
GO
SET IDENTITY_INSERT [dbo].[BrandstofTypes] ON 

INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (1, N'Benzine')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (2, N'Diesel')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (3, N'LPG')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (4, N'CNG')
INSERT [dbo].[BrandstofTypes] ([Id], [Type]) VALUES (5, N'LNG')
SET IDENTITY_INSERT [dbo].[BrandstofTypes] OFF
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
SET IDENTITY_INSERT [dbo].[Voertuigen] ON 

INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassisnummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (1, N'Volkswagen', N'Golf', N'12345678912345678', N'1-ABC-123', 0, N'Black', 5, 0, 1, 2)
INSERT [dbo].[Voertuigen] ([Id], [Merk], [Model], [Chassisnummer], [Nummerplaat], [Gearchiveerd], [Kleur], [AantalDeuren], [Hybride], [WagenTypeId], [BrandstofId]) VALUES (2, N'Ford', N'Fiesta', N'12345678987456321', N'2-ABC-321', 0, N'Blue', 3, 0, 2, 1)
SET IDENTITY_INSERT [dbo].[Voertuigen] OFF
GO
SET IDENTITY_INSERT [dbo].[Bestuurders] ON 

INSERT [dbo].[Bestuurders] ([Id], [Naam], [Voornaam], [Geboortedatum], [Rijksregisternummer], [Gearchiveerd], [TankkaartId], [VoertuigId], [Straat], [Huisnummer], [Postcode], [Land], [Stad]) VALUES (1, N'De Neef', N'Olivier', CAST(N'1999-10-06T00:00:00.0000000' AS DateTime2), N'99100630515', 0, 3, 2, N'Rosstraat', N'65', N'9200', N'Belgie', N'Dendermonde')
SET IDENTITY_INSERT [dbo].[Bestuurders] OFF
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
INSERT [dbo].[RijbewijsTypes_Bestuurders] ([RijbewijsTypeId], [BestuurderId]) VALUES (2, 1)
GO
SET IDENTITY_INSERT [dbo].[Tankkaarten] ON 

INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (2, N'5615385463', CAST(N'2024-08-12T00:00:00.0000000' AS DateTime2), N'1492', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (3, N'135466153', CAST(N'2021-11-17T16:33:30.5966667' AS DateTime2), N'1234', 0, 0)
INSERT [dbo].[Tankkaarten] ([Id], [Kaartnummer], [Geldigheidsdatum], [Pincode], [Gearchiveerd], [Geblokkeerd]) VALUES (4, N'123645', CAST(N'2021-11-17T16:39:57.1066667' AS DateTime2), N'1234', 0, 0)
SET IDENTITY_INSERT [dbo].[Tankkaarten] OFF
GO
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (2, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (2, 2)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (3, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (3, 3)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (3, 5)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (4, 1)
INSERT [dbo].[Tankkaarten_BrandstofTypes] ([TankkaartId], [BrandstofTypeId]) VALUES (4, 2)
GO
