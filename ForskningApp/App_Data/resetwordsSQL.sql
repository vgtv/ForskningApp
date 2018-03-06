DELETE FROM forskningsresultater.words;
DBCC CHECKIDENT ('[forskningsresultater].[words]', RESEED, 0);