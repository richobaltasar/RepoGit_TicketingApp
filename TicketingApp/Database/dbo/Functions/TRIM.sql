﻿CREATE FUNCTION dbo.TRIM(@String VARCHAR(MAX))
RETURNS VARCHAR(MAX)
BEGIN
    RETURN LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@String,CHAR(10),'[]'),CHAR(13),'[]'),char(9),'[]'),CHAR(32),'[]'),'][',''),'[]',CHAR(32))));
END
