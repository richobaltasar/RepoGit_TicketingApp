CREATE TABLE [dbo].[Master_Form](
	[idLog] [bigint] IDENTITY(1,1) NOT NULL,
	[NamaForm] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Id] [nvarchar](max) NULL,
	[TextLabel] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[Controller] [nvarchar](max) NULL,
	[ValueInput] [nvarchar](max) NULL,
	[ListModel] [nvarchar](max) NULL,
	[Urutan] [int] NOT NULL DEFAULT 0,
	[ShowHide] [nvarchar](max) NULL,
	[ReadOnly] [nvarchar](max) NULL,
	[Enable] [nvarchar](max) NULL,
	[Mandatory] [nvarchar](max) NULL,
	[IsNumber] [int] NULL,
	[FilterBy] [int] NOT NULL,
 CONSTRAINT [PK_Master_Form] PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Master_Form] ADD  CONSTRAINT [DF_Master_Form_FilterBy]  DEFAULT ((0)) FOR [FilterBy]