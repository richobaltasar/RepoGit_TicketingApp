CREATE TABLE [dbo].[Role_MenuTree] (
    [IdRole]   BIGINT IDENTITY (1, 1) NOT NULL,
    [IdModule] BIGINT NOT NULL,
    [Posisi]   BIGINT NOT NULL,
    [IdParent] BIGINT CONSTRAINT [DF_Role_MenuTree_IdParent] DEFAULT ((0)) NOT NULL,
    [Urutan]   BIGINT CONSTRAINT [DF_Role_MenuTree_Urutan] DEFAULT ((0)) NOT NULL,
    [IdMenu]   BIGINT NOT NULL
);

