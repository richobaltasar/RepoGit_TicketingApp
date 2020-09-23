CREATE TABLE [dbo].[DataInventory]
(
    [id] int NOT NULL, 
    [Test] NCHAR(10) NULL
)
WITH
(
    DISTRIBUTION = HASH (col1),
    CLUSTERED COLUMNSTORE INDEX
)
GO
