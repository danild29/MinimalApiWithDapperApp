CREATE PROCEDURE [dbo].[spPair_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM dbo.[Pair]
	where Id = @Id;
END