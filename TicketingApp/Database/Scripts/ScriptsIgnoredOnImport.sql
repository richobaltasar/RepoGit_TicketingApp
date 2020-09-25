
USE [ERP]
GO

ALTER TABLE [dbo].[Master_Form] DROP CONSTRAINT [DF_Master_Form_FilterBy]
GO

/****** Object:  Table [dbo].[Master_Form]    Script Date: 9/25/2020 6:46:03 AM ******/
DROP TABLE [dbo].[Master_Form]
GO

/****** Object:  Table [dbo].[Master_Form]    Script Date: 9/25/2020 6:46:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET IDENTITY_INSERT [dbo].[Master_Form] ON
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (1, N'Tambah Ticket', N'textbox', N'NamaTicket', N'Nama Ticket', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (2, N'Tambah Ticket', N'textbox', N'Senin', N'Senin', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (4, N'Tambah Ticket', N'textbox', N'Selasa', N'Selasa', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (5, N'Tambah Ticket', N'textbox', N'Rabu', N'Rabu', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (6, N'Tambah Ticket', N'textbox', N'Kamis', N'Kamis', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (7, N'Tambah Ticket', N'textbox', N'Jumat', N'Jumat', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (8, N'Tambah Ticket', N'textbox', N'Sabtu', N'Sabtu', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (9, N'Tambah Ticket', N'textbox', N'Minggu', N'Minggu', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10, N'Tambah Ticket', N'select', N'Status', N'Status', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (11, N'Form Diskon', N'textbox', N'NamaPromo', N'Nama Promo', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (12, N'Form Diskon', N'select', N'CategoryPromo', N'Category Promo', NULL, NULL, NULL, N'ListCategoryPromo', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (13, N'Form Diskon', N'textbox', N'Diskon', N'Diskon', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (14, N'Form Diskon', N'datepicker', N'Berlakudari', N'Berlakudari', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (16, N'Form Diskon', N'datepicker', N'BerlakuSampai', N'Berlaku Sampai', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (17, N'Form Diskon', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (18, N'Form Tenant', N'textbox', N'NamaTenant', N'Nama Tenant', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (19, N'Form Tenant', N'textbox', N'PemilikTenant', N'Owner', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20, N'Form Tenant', N'datepicker', N'OpenDateTenant', N'OpenDateTenant', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (21, N'Form Tenant', N'select', N'StatusKepemilikan', N'Status Kepemilikan', NULL, NULL, NULL, N'ListStatusKepemilikan', 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (22, N'Form Tenant', N'select', N'StatusJual', N'Status Jual', NULL, NULL, NULL, N'ListStatusJual', 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (23, N'Form Tenant', N'select', N'FollowTenant', N'Follow Tenant', NULL, NULL, NULL, N'ListFollowTenant', 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (24, N'Form Tenant', N'select', N'MonitoringStock', N'Monitoring Stock', NULL, NULL, NULL, N'ListMonitoringStock', 7, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (25, N'Form Tenant', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 8, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (26, N'Form Product', N'textbox', N'Nama Menu', N'Nama Menu', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (27, N'Form Product', N'select', N'Category Menu', N'Category Menu', NULL, NULL, NULL, N'ListCategoryMenu', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (28, N'Form Product', N'textbox', N'Harga Modal ', N'Harga Modal ', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (29, N'Form Product', N'textbox', N'Harga Jual', N'Harga Jual', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (30, N'Form Product', N'textbox', N'Stok', N'Stok', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (31, N'Form Entry Quotation', N'select', N'Kategori', N'Kategori', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (32, N'Form Entry Quotation', N'datepicker', N'TanggalRAB', N'Tanggal RAB', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (33, N'Form Entry Quotation', N'textbox_modal', N'Supplier', N'Supplier', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (34, N'Form Entry Quotation', N'textbox_modal', N'NamaBarang', N'Nama Barang', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (35, N'Form Entry Quotation', N'textbox', N'Qtx', N'Qtx', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (37, N'Form Entry Quotation', N'textbox', N'HargaSatuan', N'Harga Satuan', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (38, N'Form Entry Quotation', N'textbox', N'HargaTotal', N'Harga Total', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (39, N'Form Entry Quotation', N'textarea', N'Note', N'Note', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40, N'Form New Suplier', N'textbox', N'NamaInstansi', N'Nama Instansi', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (41, N'Form New Suplier', N'textbox', N'SalesName', N'Sales Name', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (42, N'Form New Suplier', N'textbox', N'ContactPerson', N'Contact Person', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (43, N'Form New Suplier', N'textbox', N'Telepon', N'Telepon', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (44, N'Form New Suplier', N'textbox', N'Email', N'Email', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (45, N'Form New Suplier', N'textarea', N'Alamat', N'Alamat', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (46, N'Form Entry RAB', N'select', N'Kategori', N'Kategori', NULL, NULL, NULL, N'ListCategoriRAB', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (47, N'Form Entry RAB', N'textarea', N'DeskripsiRAB', N'Deskripsi RAB', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (48, N'Form Entry RAB', N'select', N'PIC', N'Supervisi', NULL, NULL, NULL, N'DataUserSPVRAB', 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (49, N'Form Entry RAB', N'datepicker', N'TanggalRAB', N'Tanggal RAB', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50, N'Form Entry RAB', N'datepicker', N'TanggalPelaksanaan', N'Tanggal Pelaksanaan', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (51, N'Form Entry RAB', N'select', N'Urgensi', N'Urgensi', NULL, NULL, NULL, N'ListUrgensi', 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (52, N'Form Entry Item RAB', N'select', N'Kategori', N'Kategori', NULL, NULL, NULL, N'ListKategoryItemRAB', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (53, N'Form Entry Item RAB', N'textbox_modal', N'NamaBarangA', N'Nama Item', NULL, NULL, NULL, N'modal_NamaBarangA', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (54, N'Form Entry Item RAB', N'textarea', N'DeskripsiProduk', N'Deskripsi', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (55, N'Form Entry Item RAB', N'textbox', N'EstimasiHargaSatuan', N'Estimasi Harga Satuan', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (56, N'Form Entry Item RAB', N'textbox', N'Qty', N'Qty', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (59, N'Form Entry Item RAB', N'select', N'UnitSatuan', N'Unit Satuan', NULL, NULL, NULL, N'ListUnitSatuan', 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60, N'Form Revisi RAB', N'textarea', N'Note', N'Catatan Revisi', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (61, N'Form Tolak RAB', N'textarea', N'Note', N'Catatan Revisi', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (62, N'Form New Product', N'textbox', N'NamaBarang', N'Nama Item', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (63, N'Form New Product', N'textbox', N'Kategori', N'Kategori', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (64, N'Form New Product', N'textbox', N'TypeJenis', N'Tipe/Jenis', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (65, N'Form New Sales Product', N'select', N'CategoryProduct', N'Category', NULL, NULL, NULL, N'ListCategoryProduct', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (66, N'Form New Sales Product', N'textbox', N'Brand', N'Brand', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (67, N'Form New Sales Product', N'textbox', N'ProductName', N'Product Name', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (68, N'Form New Sales Product', N'textbox', N'Type', N'Type', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70, N'Form New Sales Product', N'textbox', N'HargaModal', N'Harga Modal', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (71, N'Form New Sales Product', N'textbox', N'MarginBottom', N'Margin Bottom (%)', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (72, N'Form New Sales Product', N'textbox', N'Margin', N'Margin (%)', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (77, N'Form Customer Quotation', N'textbox_modal', N'NamaInstansi', N'NamaInstansi', NULL, NULL, NULL, N'modal_ListDataCustomer', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (78, N'Form Customer Quotation', N'textbox', N'NamaPIC', N'NamaPIC', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (79, N'Form Customer Quotation', N'textbox', N'NoHp', N'NoHp', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80, N'Form Customer Quotation', N'textbox', N'Email', N'Email', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (81, N'Form Customer Quotation', N'textarea', N'Alamat', N'Alamat', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (82, N'Form Quotation Detail', N'textbox_modal', N'NamaSales', N'Nama Sales', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (83, N'Form Quotation Detail', N'textbox', N'Phone', N'Phone', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (84, N'Form Quotation Detail', N'datepicker', N'Date', N'Date', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (85, N'Form Quotation Detail', N'textbox', N'Hal', N'Hal', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (86, N'Form Quotation Detail', N'datepicker', N'ExpiredDate', N'Expired Date', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (87, N'Form_NewCustomer', N'textbox', N'NamaInstansi', N'Instansi', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (88, N'Form_NewCustomer', N'textbox', N'NamaCustomer', N'Nama', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (89, N'Form_NewCustomer', N'textbox', N'ContactPerson', N'Contact', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90, N'Form_NewCustomer', N'textbox', N'Email', N'Email', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (91, N'Form_NewCustomer', N'textarea', N'Alamat', N'Alamat', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (92, N'Form New Item Quo', N'textbox_modal', N'NamaProduct', N'Nama Item', NULL, NULL, NULL, N'modal_ListDataSalesProduct', 1, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (93, N'Form New Item Quo', N'textbox', N'Kategori', N'Kategori', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (94, N'Form New Item Quo', N'textarea', N'Deskripsi', N'Deskripsi', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (95, N'Form New Item Quo', N'textbox', N'HargaSatuan', N'HargaSatuan', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (96, N'Form New Item Quo', N'textbox', N'Qty', N'Qty', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (97, N'Form New Item Quo', N'textbox', N'Type', N'Type', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (98, N'Form New Item Quo', N'textbox', N'Brand', N'Brand', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (99, N'Form New Item Quo', N'textbox', N'IdProduct', N'IdProduct', NULL, NULL, NULL, NULL, 2, N'Hide', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (108, N'Form Create Invoice', N'select', N'JenisPembayaran', N'Jenis Pembayaran', NULL, NULL, NULL, N'ListJenisPembayaran', 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (109, N'Form Create Invoice', N'textbox', N'TotalPenawaran', N'Total Penawaran', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110, N'Form Create Invoice', N'textbox', N'TotalHarga', N'TotalHarga', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (111, N'Form Create Invoice', N'textbox', N'TotalPPN', N'TotalPPN', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (112, N'Form Create Invoice', N'textbox', N'DownPayment75', N'Down Payment 75%', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (113, N'Form Create Invoice', N'textbox', N'FullPayment', N'Full Payment', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (114, N'Form Create Invoice', N'textbox', N'SisaPayment25', N'Sisa Payment', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (115, N'Form Create Invoice', N'textbox', N'Termin1', N'Termin I', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (116, N'Form Create Invoice', N'textbox', N'Termin2', N'Termin II', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (117, N'Form Create Invoice', N'textbox', N'Termin3', N'Termin III', NULL, NULL, NULL, NULL, 10, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (118, N'Form Create Invoice', N'select', N'TerminKe', N'Termin Ke', NULL, NULL, NULL, N'ListTerminKe', 11, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10107, N'Form Barang Masuk Gudang', N'textbox_modal', N'NamaProduct', N'Nama Produk', NULL, NULL, NULL, N'modal_ListDataBarangInventory', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10108, N'Form Barang Masuk Gudang', N'textbox', N'Qty', N'Qty', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10109, N'Form Barang Keluar Gudang', N'select', N'DestinasiBarang', N'Destinasi', NULL, NULL, NULL, N'ListDestinasiBarangKeluar', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10110, N'Form Barang Keluar Gudang', N'textarea', N'Perihal', N'Perihal', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10112, N'Form Barang Keluar Gudang', N'textbox', N'Qty', N'Qty', NULL, NULL, N'0', NULL, 3, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10113, N'Form Registrasi Karyawan Baru', N'textbox', N'NamaLengkap', N'Nama Lengkap', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10114, N'Form Registrasi Karyawan Baru', N'select', N'Gender', N'Gender', NULL, NULL, NULL, N'ListGender', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10115, N'Form Registrasi Karyawan Baru', N'datepicker', N'TanggalLahir', N'TanggalLahir', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10116, N'Form Registrasi Karyawan Baru', N'textbox_modal', N'TempatLahir', N'TempatLahir', NULL, NULL, NULL, N'modal_ListDataKota', 4, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10117, N'Form Registrasi Karyawan Baru', N'textbox', N'NoHandPhone', N'NoHandPhone', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10118, N'Form Registrasi Karyawan Baru', N'textarea', N'AlamatRumah', N'AlamatRumah', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10119, N'Form Registrasi Karyawan Baru', N'textbox', N'AlamatEmail', N'AlamatEmail', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10120, N'Form Registrasi Karyawan Baru', N'select', N'NamaDivisi', N'NamaDivisi', NULL, NULL, NULL, N'ListNamaDivisi', 8, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10121, N'Form Registrasi Karyawan Baru', N'select', N'NamaPosisi', N'NamaPosisi', NULL, NULL, NULL, N'ListNamaPosisi', 9, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10122, N'Form Registrasi Karyawan Baru', N'datepicker', N'TglAwalKerja', N'TglAwalKerja', NULL, NULL, NULL, NULL, 10, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10123, N'Form Registrasi Karyawan Baru', N'select', N'Agama', N'Agama', NULL, NULL, NULL, N'ListAgama', 13, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10124, N'Form Registrasi Karyawan Baru', N'select', N'JenisIdentitas', N'JenisIdentitas', NULL, NULL, NULL, N'ListJenisIdentitas', 11, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10125, N'Form Registrasi Karyawan Baru', N'textbox', N'NoIdentitas', N'NoIdentitas', NULL, NULL, NULL, NULL, 12, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10128, N'Form Registrasi Karyawan Baru', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatusKaryawan', 14, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10129, N'Form Produk Baru', N'select', N'Category', N'Category', NULL, NULL, NULL, N'ListCategoryProduk', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10130, N'Form Produk Baru', N'textbox', N'Brand', N'Brand', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10131, N'Form Produk Baru', N'textbox', N'ProductName', N'ProductName', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10132, N'Form Produk Baru', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10133, N'Form Payroll Setting', N'textbox', N'GajiPokok', N'GajiPokok', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10134, N'Form Payroll Setting', N'textbox', N'Tunjangan', N'Tunjangan', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10135, N'Form Payroll Setting', N'textbox', N'Lemburan', N'Lemburan', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10136, N'Form Payroll Setting', N'textbox', N'THR', N'THR', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10137, N'Form Payroll Setting', N'select', N'Bank', N'Bank', NULL, NULL, NULL, N'ListBank', 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10138, N'Form Payroll Setting', N'textbox', N'NoRekening', N'No. Rekening', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10139, N'Form Payroll Setting', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatusPeriode', 7, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10140, N'Form Entry Vendor', N'textbox_modal', N'Company', N'Company', NULL, NULL, NULL, N'modal_ListSuplier', 1, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10141, N'Form Entry Vendor', N'textbox_modal', N'NamaSales', N'Nama Sales', NULL, NULL, NULL, N'modal_ListSales', 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10142, N'Form Entry Vendor', N'textbox', N'NoHandphone', N'NoHandphone', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10143, N'Form Entry Vendor', N'textbox', N'Email', N'Email', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10145, N'Form Entry Vendor', N'textbox', N'TelponKantor', N'TelponKantor', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10146, N'Form Entry Vendor', N'textarea', N'Alamat', N'Alamat', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10149, N'Form Entry Suplier', N'textbox', N'Company', N'Company', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10150, N'Form Entry Suplier', N'textbox', N'TelponKantor', N'TelponKantor', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10151, N'Form Entry Suplier', N'textbox', N'Alamat', N'Alamat', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10152, N'Form Entry Suplier', N'textbox', N'Website', N'Website', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10153, N'Form Entry Vendor', N'textbox', N'Website', N'Website', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10154, N'Form Entry Vendor', N'textbox', N'IdSuplier', N'IdSuplier', NULL, NULL, NULL, NULL, 8, N'Hide', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10155, N'Form Entry Vendor', N'textbox', N'IdSales', N'IdSales', NULL, NULL, NULL, NULL, 9, N'Hide', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10156, N'Form Entry Quotation Vendor', N'textbox_modal', N'NamaItem', N'NamaItem', NULL, NULL, NULL, N'Modal_RABItem', 1, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10157, N'Form Entry Quotation Vendor', N'textbox', N'Qty', N'Qty', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10158, N'Form Entry Quotation Vendor', N'textbox', N'Unit', N'Unit', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10159, N'Form Entry Quotation Vendor', N'textbox', N'EstimasiHargSaruan', N'Estimasi', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10160, N'Form Entry Quotation Vendor', N'textbox', N'HargaPenawaran', N'HargaPenawaran', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (10161, N'Form Entry Quotation Vendor', N'textbox', N'IdItemRAB', N'IdItemRAB', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'disable', N'1', 1, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20140, N'Form Add PPN', N'select', N'JenisPPN', N'Jenis PPN', NULL, NULL, NULL, N'ListJenisPPN', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20142, N'Form Product', N'FileInput', N'Gambar', N'Gambar', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20143, N'Form Master CRUD', N'textbox', N'IdModul', N'IdModul', NULL, NULL, NULL, NULL, 1, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20144, N'Form Master CRUD', N'textbox', N'NamaModule', N'NamaModule', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20145, N'Form Master CRUD', N'textbox', N'Action', N'Action', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20146, N'Form Master CRUD', N'textbox', N'Controller', N'Controller', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20147, N'Form Master CRUD', N'FileInput', N'Img', N'Img', NULL, NULL, NULL, N'image/*', 6, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20148, N'Form Master CRUD', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20149, N'Form Master Menu', N'textbox', N'NamaMenu', N'NamaMenu', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20150, N'Form Master Menu', N'textbox', N'Action', N'Action', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20151, N'Form Master Menu', N'textbox', N'Controller', N'Controller', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20152, N'Form Master Menu', N'textbox', N'Platform', N'Platform', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20153, N'Form Master Menu', N'FileInput', N'Img', N'Img', NULL, NULL, NULL, N'image/*', 6, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20154, N'Form Master Menu', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20155, N'Form Master Menu', N'textbox', N'idMenu', N'idMenu', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20156, N'Form Master Role Menu', N'textbox', N'IdRole', N'IdRole', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20157, N'Form Master Role Menu', N'textbox', N'IdMenu', N'Id Menu', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20158, N'Form Master Role Menu', N'select', N'IdModule', N'Nama Module', NULL, NULL, NULL, N'ListMasterModule', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20159, N'Form Master Role Menu', N'select', N'Posisi', N'Posisi', NULL, NULL, NULL, N'ListPosisiMenu', 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20160, N'Form Master Role Menu', N'textbox', N'Urutan', N'Urutan', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20162, N'Form Master Role Menu', N'textbox', N'IdParent', N'IdParent', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20163, N'Form Master Role Menu', N'textbox', N'Action', N'Action', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20164, N'Form Master Role Menu', N'textbox', N'Controller', N'Controller', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20166, N'Form Master Role Menu', N'textbox', N'Platform', N'Platform', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20167, N'Form Master Role Menu', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 10, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20168, N'Form Master Role Menu', N'FileInput', N'Img', N'Img', NULL, NULL, NULL, N'image/*', 11, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20169, N'Form Master Role Menu', N'textbox', N'NamaMenu', N'NamaMenu', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20170, N'Form Master User Akses', N'textbox', N'NamaLengkap', N'NamaLengkap', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20171, N'Form Master User Akses', N'textbox', N'Alamat', N'Alamat', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20172, N'Form Master User Akses', N'textbox', N'Email', N'Email', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20173, N'Form Master User Akses', N'textbox', N'Gender', N'Gender', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20174, N'Form Master User Akses', N'textbox', N'NoHp', N'NoHp', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20176, N'Form List Master', N'textbox', N'ListName', N'ListName', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20177, N'Form List Master', N'textbox', N'Urutan', N'Urutan', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20178, N'Form List Master', N'textbox', N'Text', N'Text', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20179, N'Form List Master', N'textbox', N'Value', N'Value', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20180, N'Form List Master', N'textbox', N'id', N'id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20181, N'Form Employee Data', N'textbox', N'id', N'id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20182, N'Form Employee Data', N'textbox', N'username', N'username', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20183, N'Form Employee Data', N'textbox_password', N'password', N'password', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20184, N'Form Employee Data', N'textbox', N'hakakses', N'hakakses', NULL, NULL, NULL, NULL, 3, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20185, N'Form Employee Data', N'textbox', N'NamaLengkap', N'NamaLengkap', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20186, N'Form Employee Data', N'select', N'Gender', N'Gender', NULL, NULL, NULL, N'ListGender', 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20187, N'Form Employee Data', N'textbox', N'Email', N'Email', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20188, N'Form Employee Data', N'textbox', N'NoHp', N'NoHp', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20189, N'Form Employee Data', N'FileInput', N'Photo', N'Photo', NULL, NULL, NULL, N'.jpg', 21, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20190, N'Form Employee Data', N'textbox', N'Alamat', N'Alamat', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20191, N'Form Employee Data', N'datepicker', N'TanggalLahir', N'TanggalLahir', NULL, NULL, NULL, NULL, 10, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20193, N'Form Employee Data', N'textbox', N'TempatLahir', N'TempatLahir', NULL, NULL, NULL, NULL, 11, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20194, N'Form Employee Data', N'select', N'Agama', N'Agama', NULL, NULL, NULL, N'ListAgama', 12, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20195, N'Form Employee Data', N'FileInput', N'ScanKTP', N'ScanKTP', NULL, NULL, NULL, N'image/*', 22, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20196, N'Form Employee Data', N'select', N'JenisIdentitas', N'JenisIdentitas', NULL, NULL, NULL, N'ListJenisIdentitas', 14, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20197, N'Form Employee Data', N'textbox', N'NoIdentitas', N'NoIdentitas', NULL, NULL, NULL, NULL, 15, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20201, N'Form Employee Data', N'select', N'NamaDivisi', N'Divisi', NULL, NULL, NULL, N'ListDivisi', 16, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20202, N'Form Employee Data', N'select', N'NamaPosisi', N'Posisi', NULL, NULL, NULL, N'ListNamaPosisi', 17, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20203, N'Form Employee Data', N'datepicker', N'TglAwalKerja', N'TglAwalKerja', NULL, NULL, NULL, NULL, 18, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20204, N'Form Employee Data', N'textbox', N'CreateDate', N'CreateDate', NULL, NULL, NULL, NULL, 19, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20205, N'Form Employee Data', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 20, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20209, N'Form Jenis Ticket', N'textbox', N'IdTicket', N'IdTicket', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20210, N'Form Jenis Ticket', N'textbox', N'namaticket', N'namaticket', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20211, N'Form Jenis Ticket', N'textbox', N'Monday', N'Monday', NULL, NULL, NULL, NULL, 2, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20212, N'Form Jenis Ticket', N'textbox', N'Tuesday', N'Tuesday', NULL, NULL, NULL, NULL, 3, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20213, N'Form Jenis Ticket', N'textbox', N'Wednesday', N'Wednesday', NULL, NULL, NULL, NULL, 4, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20215, N'Form Jenis Ticket', N'textbox', N'Thursday', N'Thursday', NULL, NULL, NULL, NULL, 5, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20216, N'Form Jenis Ticket', N'textbox', N'Friday', N'Friday', NULL, NULL, NULL, NULL, 6, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20217, N'Form Jenis Ticket', N'textbox', N'Saturday', N'Saturday', NULL, NULL, NULL, NULL, 7, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20218, N'Form Jenis Ticket', N'textbox', N'Sunday', N'Sunday', NULL, NULL, NULL, NULL, 8, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20219, N'Form Jenis Ticket', N'select', N'status', N'status', NULL, NULL, NULL, N'ListStatus', 14, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20220, N'Form Jenis Ticket', N'FileInput', N'Img', N'Img', NULL, NULL, NULL, N'image/*', 15, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20221, N'Form Promo', N'textbox', N'idPromo', N'idPromo', NULL, NULL, NULL, NULL, 0, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20222, N'Form Promo', N'textbox', N'NamaPromo', N'NamaPromo', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20223, N'Form Promo', N'select', N'CategoryPromo', N'CategoryPromo', NULL, NULL, NULL, N'ListCategoryPromo', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20224, N'Form Promo', N'textbox', N'Diskon', N'Diskon (%)', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20225, N'Form Promo', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 7, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20226, N'Form Promo', N'datepicker', N'BerlakuDari', N'BerlakuDari', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20227, N'Form Promo', N'datepicker', N'BerlakuSampai', N'BerlakuSampai', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20228, N'Form Promo', N'FileInput', N'Img', N'Img', NULL, NULL, NULL, N'image/*', 8, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20229, N'Form Promo', N'select', N'IdJenisTicket', N'JenisTicket', NULL, NULL, NULL, N'ListJenisTicket', 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20230, N'Form Voucher Belanja', N'textbox', N'id', N'id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20231, N'Form Voucher Belanja', N'textbox', N'NamaVoucher', N'NamaVoucher
', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20232, N'Form Voucher Belanja', N'select', N'JenisVoucher', N'JenisVoucher
', NULL, NULL, NULL, N'ListVoucher', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20233, N'Form Voucher Belanja', N'textbox', N'NominalVoucher', N'NominalVoucher
', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20234, N'Form Voucher Belanja', N'textbox', N'CodeVoucher', N'CodeVoucher
', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20235, N'Form Voucher Belanja', N'FileInput', N'Img', N'Img
', NULL, NULL, NULL, N'image/*', 11, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20236, N'Form Voucher Belanja', N'textbox', N'Description', N'Description
', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20237, N'Form Voucher Belanja', N'select', N'Status', N'Status
', NULL, NULL, NULL, N'ListStatus', 10, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20238, N'Form Voucher Belanja', N'datepicker', N'BerlakuDari', N'BerlakuDari
', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20239, N'Form Voucher Belanja', N'datepicker', N'BerlakuSampai', N'BerlakuSampai
', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20240, N'Form Voucher Belanja', N'textbox', N'CreateBy', N'CreateBy
', NULL, NULL, NULL, NULL, 9, N'hidden', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20241, N'Form Jenis Ticket Parkir', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20242, N'Form Jenis Ticket Parkir', N'textbox', N'JenisTicket', N'JenisTicket', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20243, N'Form Jenis Ticket Parkir', N'textbox', N'Category', N'Category', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20244, N'Form Jenis Ticket Parkir', N'textbox', N'Code', N'Code', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20245, N'Form Jenis Ticket Parkir', N'textbox', N'Harga', N'Harga', NULL, NULL, NULL, NULL, 4, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20246, N'Form Jenis Ticket Parkir', N'textbox', N'Periode', N'Periode', NULL, NULL, NULL, NULL, 5, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20247, N'Form Jenis Ticket Parkir', N'FileInput', N'Img', N'Img', NULL, NULL, NULL, N'image/*', 13, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20248, N'Form Jenis Ticket Parkir', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 12, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20249, N'Form Jenis Ticket Parkir', N'textbox', N'CreateBy', N'Create By', NULL, NULL, NULL, NULL, 8, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20250, N'Form Jenis Ticket Parkir', N'textbox', N'HargaWeekDay', N'Harga Week Day', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20251, N'Form Jenis Ticket Parkir', N'textbox', N'HargaWeekEnd', N'Harga Week End', NULL, NULL, NULL, NULL, 10, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20252, N'Form Jenis Ticket Parkir', N'textbox', N'HargaHoliday', N'Harga Holiday', NULL, NULL, NULL, NULL, 11, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20253, N'Form Jenis Ticket', N'textbox', N'HargaWeekDay', N'Harga Week Day', NULL, NULL, NULL, NULL, 11, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20254, N'Form Jenis Ticket', N'textbox', N'HargaWeekEnd', N'Harga Week End', NULL, NULL, NULL, NULL, 12, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (20255, N'Form Jenis Ticket', N'textbox', N'HargaHoliday', N'Harga Holiday', NULL, NULL, NULL, NULL, 13, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (30209, N'Form Daftar Hari libur Nasional', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (30210, N'Form Daftar Hari libur Nasional', N'textbox', N'NamaHariLibur', N'NamaHariLibur', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (30211, N'Form Daftar Hari libur Nasional', N'datepicker', N'DariTanggal', N'DariTanggal', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (30212, N'Form Daftar Hari libur Nasional', N'datepicker', N'SampaiTanggal', N'SampaiTanggal', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (30213, N'Form Daftar Hari libur Nasional', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40209, N'Form RAB', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40210, N'Form RAB', N'textbox', N'NamaRAB', N'Nama RAB', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40211, N'Form RAB', N'select', N'JenisRAB', N'Jenis RAB', NULL, NULL, NULL, N'ListJenisRAB', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40212, N'Form RAB', N'textarea', N'DescriptionRAB', N'Description RAB', NULL, NULL, NULL, NULL, 18, N'Show', NULL, N'enable', N'1', 5, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40213, N'Form RAB', N'textbox', N'CreateDate', N'Create Date', NULL, NULL, NULL, NULL, 4, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40214, N'Form RAB', N'datepicker', N'TargetImplementDate', N'Target Implement', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40215, N'Form RAB', N'selectpicker', N'PIC1', N'Main PIC', NULL, NULL, NULL, N'ListKaryawan', 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40216, N'Form RAB', N'selectpicker', N'PIC2', N'PIC Secondary', NULL, NULL, NULL, N'ListKaryawan', 7, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40217, N'Form RAB', N'selectpicker', N'DisetujuiOleh', N'Di setujui Oleh', NULL, NULL, NULL, N'ListKaryawan', 8, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40218, N'Form RAB', N'FileInput', N'Attachment1', N'Attachment 1', NULL, NULL, NULL, NULL, 20, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40219, N'Form RAB', N'FileInput', N'Attachment2', N'Attachment 2', NULL, NULL, NULL, NULL, 21, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40220, N'Form RAB', N'FileInput', N'Attachment3', N'Attachment 3', NULL, NULL, NULL, NULL, 22, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40221, N'Form RAB', N'textbox_currency', N'TotalBudget', N'Total Budget', NULL, NULL, NULL, NULL, 12, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40222, N'Form RAB', N'textbox_currency', N'EstimasiPurchase', N'Estimasi Purchase', NULL, NULL, NULL, NULL, 13, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40223, N'Form RAB', N'select', N'StatusRAB', N'Status RAB', NULL, NULL, NULL, N'ListStatusRAB', 19, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40224, N'Form List RAB', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40225, N'Form List RAB', N'textbox', N'NamaRAB', N'Nama RAB', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40226, N'Form List RAB', N'textbox', N'JenisRAB', N'Jenis RAB', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40227, N'Form List RAB', N'textbox_currency', N'TotalBudget', N'Total Budget', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40228, N'Form List RAB', N'textbox_currency', N'EstimasiPurchase', N'EstimasiPurchase', NULL, NULL, NULL, NULL, 4, N'hidden', NULL, N'disable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40229, N'Form List RAB Item', N'textbox', N'IdRAB', N'IdRab', NULL, NULL, NULL, NULL, 1, N'hidden', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40230, N'Form List RAB Item', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40231, N'Form List RAB Item', N'select', N'Category', N'Category', NULL, NULL, NULL, N'ListCategoryItemRAB', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40232, N'Form List RAB Item', N'textbox', N'NamaItem', N'Nama Item', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40233, N'Form List RAB Item', N'select', N'Satuan', N'Satuan', NULL, NULL, NULL, N'ListSatuan', 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40234, N'Form List RAB Item', N'textbox_number', N'Unit', N'Unit', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40235, N'Form List RAB Item', N'textbox_currency', N'Harga', N'Estimasi @Harga', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40236, N'Form List RAB Item', N'textbox_currency', N'SubTotal', N'SubTotal', NULL, NULL, NULL, NULL, 7, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40237, N'Form List RAB Item', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 8, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40238, N'Form List RAB', N'selectpicker', N'DisetujuiOleh', N'Disetujui Oleh', NULL, NULL, NULL, N'ListKaryawan', 5, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40239, N'Form List RAB', N'selectpicker', N'PIC1', N'PIC Utama', NULL, NULL, NULL, N'ListKaryawan', 6, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (40240, N'Form List RAB', N'selectpicker', N'PIC2', N'PIC Pendamping', NULL, NULL, NULL, N'ListKaryawan', 7, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50225, N'Form Sales Quotation Vendor', N'textbox', N'IdQuotation', N'IdQuotation', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50226, N'Form Sales Quotation Vendor', N'selectpicker', N'IdRab', N'RAB', N'NUll', N'NUll', NULL, N'ListRABApproved', 1, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50227, N'Form Sales Quotation Vendor', N'textbox', N'NumberQuotationByVendor', N'No. Quotation', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50228, N'Form Sales Quotation Vendor', N'select', N'CategoryPerusahaan', N'Category Perusahaan', NULL, NULL, NULL, N'ListCategoryPerusahaan', 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50229, N'Form Sales Quotation Vendor', N'textbox', N'CompanyName', N'Nama Perusahaan', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50230, N'Form Sales Quotation Vendor', N'textarea', N'AlamatPerusahaan', N'Alamat Perusahaan', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 2, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50231, N'Form Sales Quotation Vendor', N'textbox', N'Contact', N'Kontak Person', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50232, N'Form Sales Quotation Vendor', N'textbox', N'Sales', N'Nama Sales', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50233, N'Form Sales Quotation Vendor', N'textbox_currency', N'TotalPenawaran', N'Total Penawaran', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50234, N'Form Sales Quotation Vendor', N'textarea', N'Perihal', N'Perihal', NULL, NULL, NULL, NULL, 11, N'Show', NULL, N'enable', N'0', 4, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50235, N'Form Sales Quotation Vendor', N'textarea', N'Description', N'Description', NULL, NULL, NULL, NULL, 12, N'Show', NULL, N'enable', N'0', 4, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50236, N'Form Sales Quotation Vendor', N'datepicker', N'CreateDate', N'CreateDate', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50237, N'Form Sales Quotation Vendor', N'select', N'StatusQuotation', N'Status', NULL, NULL, NULL, N'ListStatusQuotationVendor', 10, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (50238, N'Form Sales Quotation Vendor', N'FileInput', N'Attachment1', N'Attachment1', NULL, NULL, NULL, N'.pdf', 11, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60239, N'Form Sales Quotation Vendor ListItem', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, NULL, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60240, N'Form Sales Quotation Vendor ListItem', N'textbox', N'IdQuotation', N'IdQuotation', NULL, NULL, NULL, NULL, NULL, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60241, N'Form Sales Quotation Vendor ListItem', N'textbox', N'IdRab', N'IdRab', NULL, NULL, NULL, NULL, NULL, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60242, N'Form Sales Quotation Vendor ListItem', N'textbox', N'IdItemRAB', N'IdItemRAB', NULL, NULL, NULL, NULL, NULL, N'Hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60243, N'Form Sales Quotation Vendor ListItem', N'select', N'Category', N'Category', NULL, NULL, NULL, N'ListCategoryItemRAB', NULL, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60244, N'Form Sales Quotation Vendor ListItem', N'textbox', N'NamaItem', N'NamaItem', NULL, NULL, NULL, NULL, NULL, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60245, N'Form Sales Quotation Vendor ListItem', N'select', N'Satuan', N'Satuan', NULL, NULL, NULL, N'ListSatuan', NULL, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60246, N'Form Sales Quotation Vendor ListItem', N'textbox_number', N'Unit', N'Unit', NULL, NULL, NULL, NULL, NULL, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60247, N'Form Sales Quotation Vendor ListItem', N'textbox_currency', N'Harga', N'Harga', NULL, NULL, NULL, NULL, NULL, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60248, N'Form Sales Quotation Vendor ListItem', N'textbox_currency', N'SubTotal', N'SubTotal', NULL, NULL, NULL, NULL, NULL, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60249, N'Form Sales Quotation Vendor ListItem', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', NULL, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60251, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'IdQuotation', N'Id Quotation', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60252, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'IdRab', N'Id Rab', NULL, NULL, NULL, NULL, 1, N'hidden', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60253, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'NumberQuotationByVendor', N'No. Quotation', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60254, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'CategoryPerusahaan', N'Category Perusahaan', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60255, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'CompanyName', N'Nama Perusahaan', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60256, N'Form Sales Quotation Vendor Detail Item', N'textarea', N'AlamatPerusahaan', N'Alamat Perusahaan', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'disable', N'0', 2, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60257, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'Contact', N'Kontak Person', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60258, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'Sales', N'Nama Sales', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60259, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'TotalPenawaran', N'Total Penawaran', NULL, NULL, NULL, NULL, 8, N'hidden', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60260, N'Form Sales Quotation Vendor Detail Item', N'textarea', N'Perihal', N'Perihal', NULL, NULL, NULL, NULL, 9, N'hidden', NULL, N'disable', N'0', 4, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60261, N'Form Sales Quotation Vendor Detail Item', N'textarea', N'Description', N'Description', NULL, NULL, NULL, NULL, 10, N'hidden', NULL, N'disable', N'0', 4, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60262, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'CreateDate', N'CreateDate', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60263, N'Form Sales Quotation Vendor Detail Item', N'select', N'StatusQuotation', N'Status', NULL, NULL, NULL, N'ListStatusQuotationVendor', 12, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60264, N'Form Sales Quotation Vendor Detail Item', N'textbox', N'Attachment1', N'Attachment1', NULL, NULL, NULL, NULL, 13, N'hidden', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (60265, N'Form Sales Quotation Vendor ListItem', N'FileInput', N'Attachment1', N'Brosur/Datasheet', NULL, NULL, NULL, NULL, NULL, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70238, N'Form Purchase Order', N'textbox', N'IdPO', N'IdPO', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70239, N'Form Purchase Order', N'textbox', N'IdQuotation', N'IdQuotation', NULL, NULL, NULL, NULL, 1, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70240, N'Form Purchase Order', N'datepicker', N'LastPrint', N'LastPrint', NULL, NULL, NULL, NULL, NULL, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70241, N'Form Purchase Order', N'textbox_currency', N'TotalPO', N'TotalPO', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70242, N'Form Purchase Order', N'datepicker', N'CreateDatePO', N'Create Date PO', NULL, NULL, NULL, NULL, 2, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70243, N'Form Purchase Order', N'datepicker', N'SendPODate', N'Send PO', NULL, NULL, NULL, NULL, NULL, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70244, N'Form Purchase Order', N'textbox_phoneNumber', N'NoWhatsAppSales', N'No WhatsApp Sales', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70245, N'Form Purchase Order', N'select', N'StatusPO', N'StatusPO', NULL, NULL, NULL, N'ListStatusPO', 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (70247, N'Form Purchase Order', N'selectpicker', N'PhoneCode', N'Kode Negara', NULL, NULL, NULL, N'ListKodeTelpon', 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80238, N'Form PurchaseOrder ListItem', N'textbox', N'IdPO', N'PO. No', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80239, N'Form PurchaseOrder ListItem', N'textbox', N'IdQuotation', N'Quotation No', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80240, N'Form PurchaseOrder ListItem', N'datepicker', N'LastPrint', N'Last Print', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80241, N'Form PurchaseOrder ListItem', N'textbox_currency', N'TotalPO', N'Total PO', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80242, N'Form PurchaseOrder ListItem', N'datepicker', N'CreateDatePO', N'Create Date', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80243, N'Form PurchaseOrder ListItem', N'datepicker', N'SendPODate', N'Send PO Date', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80244, N'Form PurchaseOrder ListItem', N'textbox_phoneNumber', N'NoWhatsAppSales', N'WA Sales', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80245, N'Form PurchaseOrder ListItem', N'select', N'StatusPO', N'Status PO', NULL, NULL, NULL, N'ListStatusPO', 8, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80246, N'Form PurchaseOrder ListItem', N'select', N'PhoneCode', N'Code', NULL, NULL, NULL, N'ListKodeTelpon', 3, N'Show', NULL, N'disable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80247, N'Form PurchaseOrder ProductItem', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 1, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80248, N'Form PurchaseOrder ProductItem', N'textbox', N'IdPO', N'IdPO', NULL, NULL, NULL, NULL, 2, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80249, N'Form PurchaseOrder ProductItem', N'textbox', N'IdQuotation', N'IdQuotation', NULL, NULL, NULL, NULL, 3, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80250, N'Form PurchaseOrder ProductItem', N'textbox', N'IdItemQuotation', N'IdItemQuotation', NULL, NULL, NULL, NULL, 4, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80251, N'Form PurchaseOrder ProductItem', N'select', N'Category', N'Category', NULL, NULL, NULL, N'ListCategoryItemRAB', 5, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80252, N'Form PurchaseOrder ProductItem', N'textbox', N'NamaItem', N'NamaItem', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80253, N'Form PurchaseOrder ProductItem', N'select', N'Satuan', N'Satuan', NULL, NULL, NULL, N'ListSatuan', 7, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80254, N'Form PurchaseOrder ProductItem', N'textbox_number', N'Unit', N'Unit', NULL, NULL, NULL, NULL, 8, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80255, N'Form PurchaseOrder ProductItem', N'textbox_currency', N'Harga', N'Harga', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80256, N'Form PurchaseOrder ProductItem', N'textbox_currency', N'SubTotal', N'SubTotal', NULL, NULL, NULL, NULL, 10, N'hidden', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80257, N'Form PurchaseOrder ProductItem', N'FileInput', N'Attachment1', N'Attachment1', NULL, NULL, NULL, NULL, 13, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80258, N'Form PurchaseOrder ProductItem', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatus', 11, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80259, N'Form PurchaseOrder ProductItem', N'textarea', N'Description', N'Description', NULL, NULL, NULL, NULL, 12, N'Show', NULL, N'enable', N'0', 4, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80260, N'Form Vendor Registrasi', N'textbox', N'VendorName', N'VendorName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80263, N'Form Vendor Registrasi', N'textbox', N'EmailAddress', N'EmailAddress', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80264, N'Form Vendor Registrasi', N'textbox_phoneNumber', N'PhoneNumber', N'PhoneNumber', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80265, N'Form Vendor Registrasi', N'textarea', N'Address', N'Address', NULL, NULL, NULL, NULL, 12, N'Show', NULL, N'enable', N'1', 4, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80266, N'Form Vendor Registrasi', N'selectpicker', N'Kecamatan', N'Kecamatan', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80267, N'Form Vendor Registrasi', N'selectpicker', N'ZipCode', N'ZipCode', NULL, NULL, NULL, NULL, 7, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80268, N'Form Vendor Registrasi', N'selectpicker', N'Country', N'Country', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80269, N'Form Vendor Registrasi', N'selectpicker', N'State', N'State/Province', NULL, NULL, NULL, N'ListDataProvinsi', 3, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80270, N'Form Vendor Registrasi', N'selectpicker', N'Kelurahan', N'Kelurahan', NULL, NULL, NULL, NULL, 6, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80272, N'Form Recruitment Data', N'textbox', N'id', N'id', NULL, NULL, NULL, NULL, 0, N'hidden', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80273, N'Form Recruitment Data', N'textbox', N'NamaCandidat', N'Nama Candidat', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80274, N'Form Recruitment Data', N'select', N'Divisi', N'Divisi', NULL, NULL, NULL, N'ListDivisi', 2, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80275, N'Form Recruitment Data', N'FileInput', N'Photo', N'Photo', NULL, NULL, NULL, N'image/*', 3, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (80276, N'Form Recruitment Data', N'FileInput', N'Cuvi', N'Cuvi', NULL, NULL, NULL, N'.pdf', 4, N'Show', NULL, N'enable', N'0', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90271, N'Form LogTransaksiPOS', N'textbox', N'idTrx', N'idTrx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90272, N'Form LogTransaksiPOS', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90273, N'Form LogTransaksiPOS', N'textbox', N'MerchantName', N'MerchantName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90274, N'Form LogTransaksiPOS', N'textbox', N'ChasierName', N'ChasierName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90275, N'Form LogTransaksiPOS', N'select', N'PaymentMethod', N'PaymentMethod', NULL, NULL, NULL, N'ListPaymentMethod', 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90276, N'Form LogTransaksiPOS', N'textbox', N'TotalTransaksi', N'TotalTransaksi', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90277, N'Form LogTransaksiPOS', N'textbox', N'Emoney', N'Emoney', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90278, N'Form LogTransaksiPOS', N'textbox', N'AccountNumber', N'AccountNumber', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90279, N'Form LogTransaksiPOS', N'textbox', N'TotalBayar', N'TotalBayar', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90280, N'Form LogTransaksiPOS', N'textbox', N'Tunai', N'Tunai', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90281, N'Form LogTransaksiPOS', N'textbox', N'Kembalian', N'Kembalian', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90282, N'Form LogTransaksiPOS', N'textbox', N'BankName', N'BankName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90283, N'Form LogTransaksiPOS', N'textbox', N'CardNumber', N'CardNumber', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90284, N'Form LogTransaksiPOS', N'textbox', N'Noreff', N'Noreff', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90285, N'Form LogTransaksiPOS', N'textbox', N'Status', N'Status', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90297, N'Form LogTransaksiMemberBaru', N'textbox', N'IdTrx', N'IdTrx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90298, N'Form LogTransaksiMemberBaru', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90299, N'Form LogTransaksiMemberBaru', N'textbox', N'Category', N'Category', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90300, N'Form LogTransaksiMemberBaru', N'textbox', N'NamaItem', N'NamaItem', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90301, N'Form LogTransaksiMemberBaru', N'textbox', N'Harga', N'Harga', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90302, N'Form LogTransaksiMemberBaru', N'textbox', N'Qtx', N'Qtx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90303, N'Form LogTransaksiMemberBaru', N'textbox', N'Total', N'Total', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90304, N'Form LogTransaksiMemberBaru', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90305, N'Form LogTransaksiMemberBaru', N'textbox', N'MerchantName', N'MerchantName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90306, N'Form LogTransaksiMemberBaru', N'textbox', N'ChasierName', N'ChasierName', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90307, N'Form LogTransaksiMemberBaru', N'select', N'PaymentMethod', N'PaymentMethod', NULL, NULL, NULL, N'ListPaymentMethod', 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90308, N'Form LogTransaksiTiketWahana', N'textbox', N'IdTrx', N'IdTrx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90309, N'Form LogTransaksiTiketWahana', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90310, N'Form LogTransaksiTiketWahana', N'textbox', N'Category', N'Category', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90311, N'Form LogTransaksiTiketWahana', N'textbox', N'NamaItem', N'NamaItem', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90312, N'Form LogTransaksiTiketWahana', N'textbox', N'Harga', N'Harga', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90313, N'Form LogTransaksiTiketWahana', N'textbox', N'Qtx', N'Qtx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90314, N'Form LogTransaksiTiketWahana', N'textbox', N'Total', N'Total', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90315, N'Form LogTransaksiTiketWahana', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90316, N'Form LogTransaksiTiketWahana', N'textbox', N'MerchantName', N'MerchantName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90317, N'Form LogTransaksiTiketWahana', N'textbox', N'ChasierName', N'ChasierName', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90318, N'Form LogTransaksiTiketWahana', N'select', N'PaymentMethod', N'PaymentMethod', NULL, NULL, NULL, N'ListPaymentMethod', 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90319, N'Form LogTransaksiTopup', N'textbox', N'IdTrx', N'IdTrx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90320, N'Form LogTransaksiTopup', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90321, N'Form LogTransaksiTopup', N'textbox', N'Category', N'Category', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90322, N'Form LogTransaksiTopup', N'textbox', N'NamaItem', N'NamaItem', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90323, N'Form LogTransaksiTopup', N'textbox', N'Harga', N'Harga', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90324, N'Form LogTransaksiTopup', N'textbox', N'Qtx', N'Qtx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90325, N'Form LogTransaksiTopup', N'textbox', N'Total', N'Total', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90326, N'Form LogTransaksiTopup', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90327, N'Form LogTransaksiTopup', N'textbox', N'MerchantName', N'MerchantName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90328, N'Form LogTransaksiTopup', N'textbox', N'ChasierName', N'ChasierName', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90329, N'Form LogTransaksiTopup', N'select', N'PaymentMethod', N'PaymentMethod', NULL, NULL, NULL, N'ListPaymentMethod', 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90330, N'Form LogTransaksiParkir', N'textbox', N'IdTrx', N'IdTrx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90331, N'Form LogTransaksiParkir', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90332, N'Form LogTransaksiParkir', N'textbox', N'Category', N'Category', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90333, N'Form LogTransaksiParkir', N'textbox', N'NamaItem', N'NamaItem', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90334, N'Form LogTransaksiParkir', N'textbox', N'Harga', N'Harga', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90335, N'Form LogTransaksiParkir', N'textbox', N'Qtx', N'Qtx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90336, N'Form LogTransaksiParkir', N'textbox', N'Total', N'Total', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90337, N'Form LogTransaksiParkir', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90338, N'Form LogTransaksiParkir', N'textbox', N'MerchantName', N'MerchantName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90339, N'Form LogTransaksiParkir', N'textbox', N'ChasierName', N'ChasierName', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90340, N'Form LogTransaksiParkir', N'select', N'PaymentMethod', N'PaymentMethod', NULL, NULL, NULL, N'ListPaymentMethod', 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90352, N'Form LogPenggunaanVoucher', N'textbox', N'IdTrx', N'IdTrx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90353, N'Form LogPenggunaanVoucher', N'textbox', N'Id', N'Id', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90354, N'Form LogPenggunaanVoucher', N'textbox', N'Category', N'Category', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90355, N'Form LogPenggunaanVoucher', N'textbox', N'NamaItem', N'NamaItem', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90356, N'Form LogPenggunaanVoucher', N'textbox', N'Harga', N'Harga', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90357, N'Form LogPenggunaanVoucher', N'textbox', N'Qtx', N'Qtx', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90358, N'Form LogPenggunaanVoucher', N'textbox', N'Total', N'Total', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90359, N'Form LogPenggunaanVoucher', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90360, N'Form LogPenggunaanVoucher', N'textbox', N'MerchantName', N'MerchantName', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90361, N'Form LogPenggunaanVoucher', N'textbox', N'ChasierName', N'ChasierName', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90362, N'Form LogPenggunaanVoucher', N'select', N'PaymentMethod', N'PaymentMethod', NULL, NULL, NULL, N'ListPaymentMethod', 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90363, N'Form RoleMenuMaster Filter', N'textbox', N'NamaMenu', N'NamaMenu', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90364, N'Form RoleMenuMaster Filter', N'textbox', N'Posisi', N'Posisi', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90365, N'Form RoleMenuMaster Filter', N'select', N'Platform', N'Platform', NULL, NULL, NULL, N'ListPlatform', 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90366, N'Form RoleMenuMaster Filter', N'textbox', N'IdParent', N'IdParent', NULL, NULL, NULL, NULL, 9, N'Show', NULL, N'enable', N'1', 0, 0)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90368, N'Form RoleMenuMaster Filter', N'select', N'NamaModule', N'NamaModule', NULL, NULL, NULL, N'ListMasterModuleFilter', 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90369, N'Form RoleMenuMaster Filter', N'select', N'MainMenu', N'MainMenu', NULL, NULL, NULL, N'ListMainMenu', 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90370, N'Form ReportHarian', N'datepicker', N'Datetime', N'Periode', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (90372, N'Form ReportHarian', N'select', N'JenisLaporan', N'Jenis Transaksi', NULL, NULL, NULL, N'ListJenisLaporan', 2, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (100370, N'Form ReportBulanan', N'monthpicker', N'Month', N'Bulan', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (100371, N'Form ReportBulanan', N'select', N'JenisLaporan', N'JenisLaporan', NULL, NULL, NULL, N'JenisLaporanBulanan', 2, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110370, N'Form ReportTahunan', N'yearspicker', N'Tahun', N'Tahun', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110371, N'Form ReportTahunan', N'select', N'JenisLaporan', N'JenisLaporan', NULL, NULL, NULL, N'JenisLaporanTahunan', 2, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110372, N'Form SettingReportForPajak', N'textbox_percent', N'Ticket', N'Ticket', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110373, N'Form SettingReportForPemda', N'textbox_percent', N'ParkirPEMDA', N'Parkir', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'0', 1, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110374, N'Form SettingReportForPajak', N'textbox_percent', N'Parkir', N'Parkir', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110375, N'Form SettingReportForPajak', N'textbox_percent', N'FNB', N'F&B', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110376, N'Form SettingReportForPajak', N'textbox_percent', N'PPN', N'PPN', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (110377, N'Form SettingReportForPajak', N'textbox_percent', N'PPH21', N'PPH 21', NULL, NULL, NULL, NULL, 5, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (120370, N'Form ReportBulananPajak', N'monthpicker', N'Month', N'Bulan', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (120371, N'Form SettingReportForPemda', N'textbox_percent', N'FNBPEMDA', N'FNB', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'0', 1, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (120372, N'Form SettingReportForPemda', N'textbox_percent', N'TicketPEMDA', N'Ticket', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'0', 1, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130370, N'Form DashboardFilter', N'datepicker', N'Datetime', N'Harian', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130371, N'Form DashboardFilter', N'select', N'JenisDashboard', N'Jenis Dashboard', NULL, NULL, NULL, N'ListJenisDashboard', 1, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130372, N'Form DashboardFilter', N'monthpicker', N'Bulanan', N'Bulan', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130373, N'Form DashboardFilter', N'yearspicker', N'Tahunan', N'Tahun', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'0', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130374, N'Form HistoryTransaksiChasier Filter', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130375, N'Form HistoryTransaksiChasier Filter', N'textbox', N'ChasierName', N'ChasierName', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130376, N'Form HistoryTransaksiChasier Filter', N'textbox', N'MerchantName', N'MerchantName', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130377, N'Form HistoryTransaksiChasier Filter', N'select', N'PaymentMethod', N'PaymentMethod', NULL, NULL, NULL, N'ListPaymentMethod', 4, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130379, N'Form OpeningChasier Filter', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130380, N'Form OpeningChasier Filter', N'textbox', N'OpenBy', N'Open By', NULL, NULL, NULL, NULL, 2, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130381, N'Form OpeningChasier Filter', N'textbox', N'CloseBy', N'Close By', NULL, NULL, NULL, NULL, 3, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130382, N'Form OpeningChasier Filter', N'select', N'Status', N'Status', NULL, NULL, NULL, N'ListStatusChasierBox', 5, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130383, N'Form OpeningChasier Filter', N'textbox', N'UpdateBy', N'UpdateBy', NULL, NULL, NULL, NULL, 4, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130384, N'Filter DashboardMonitoring_CashboxTotal', N'datepicker', N'Datetime', N'Datetime', NULL, NULL, NULL, NULL, 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130385, N'Filter DashboardMonitoring_CashierTransactions', N'select', N'Kasir', N'Kasir', NULL, NULL, NULL, N'ListUserKasir', 1, N'Show', NULL, N'enable', N'1', 0, 1)
GO

INSERT [dbo].[Master_Form] ([idLog], [NamaForm], [Type], [Id], [TextLabel], [Action], [Controller], [ValueInput], [ListModel], [Urutan], [ShowHide], [ReadOnly], [Enable], [Mandatory], [IsNumber], [FilterBy]) VALUES (130386, N'Filter DashboardMonitoring_CashierTransactions', N'datepicker', N'Datetime2', N'Datetime2', NULL, NULL, NULL, NULL, 0, N'Show', NULL, N'enable', N'1', 0, 1)
GO

SET IDENTITY_INSERT [dbo].[Master_Form] OFF
GO

/****** Object: Table [dbo].[Master_ListItem] Script Date: 9/25/2020 7:26:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
