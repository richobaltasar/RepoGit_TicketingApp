/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
if exists(select*from DataModule)
begin
  delete from DataModule  
end
SET IDENTITY_INSERT [dbo].[DataModule] ON
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (24, N'Ticketing', N'Index', N'Ticketing', N'ImageModule//Copy_Ticketing.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (27, N'Accounting', N'Index', N'Accounting', N'ImageModule//Accounting1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (29, N'BarrierGate', N'Index', N'BarrierGate', N'ImageModule//Copy_Gate1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (30, N'Card', N'Index', N'Card', N'ImageModule//Copy_Card1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (31, N'Chasier', N'Index', N'Chasier', N'ImageModule//Copy_Chasier1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (32, N'Event', N'Index', N'Event', N'ImageModule//Copy_event1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (33, N'Food&Bevarage', N'Index', N'FNB', N'ImageModule//Copy_FOODCOURT1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (42, N'HRD', N'Index', N'HRD', N'ImageModule//Copy_HRD1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (35, N'Inventory', N'Index', N'Inventory', N'ImageModule//Copy_Inventory1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (36, N'Operational', N'Index', N'Operational', N'ImageModule//Copy_Operational1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (37, N'Parkiran', N'Index', N'Parkiran', N'ImageModule//Copy_Parkiran1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (38, N'Purchasing', N'Index', N'Purchasing', N'ImageModule//Copy_Purchasing1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (39, N'Retail', N'Index', N'Retail', N'ImageModule//Copy_retail1.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (40, N'Tenancy', N'Index', N'Tenancy', N'ImageModule//Copy_Tenancy2.png', 1)
INSERT INTO [dbo].[DataModule] ([IdModul], [NamaModule], [Action], [Controller], [Img], [Status]) VALUES (41, N'Master', N'Index', N'Master', N'ImageModule//icon.png', 1)
SET IDENTITY_INSERT [dbo].[DataModule] OFF
go
