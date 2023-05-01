CREATE PROCEDURE [dbo].[spPair_GetAnotherIdById]
	@Id int
AS
BEGIN
	SELECT IdFirst, IdSecond
	FROM dbo.[Pair]
	where IdFirst = @Id or IdSecond = @Id;
END
