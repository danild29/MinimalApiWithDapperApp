CREATE PROCEDURE [dbo].[spPair_Get]
	@Id int
AS
BEGIN
	SELECT Id, Title, IdFirst, IdSecond
	FROM dbo.[Pair]
	where Id = @Id;
END
