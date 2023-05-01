CREATE PROCEDURE [dbo].[spPair_Insert]
	@Title nvarchar(50),
	@IdFirst int,
	@IdSecond int
AS
BEGIN
	INSERT INTO dbo.[Pair] (Title, IdFirst, IdSecond)
	VALUES (@Title, @IdFirst, @IdSecond);
END