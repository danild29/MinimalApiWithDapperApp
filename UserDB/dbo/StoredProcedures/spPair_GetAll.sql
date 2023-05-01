CREATE PROCEDURE [dbo].[spPair_GetAll]
AS
BEGIN
	SELECT Id, Title, IdFirst, IdSecond 
	FROM dbo.Pair;
END
