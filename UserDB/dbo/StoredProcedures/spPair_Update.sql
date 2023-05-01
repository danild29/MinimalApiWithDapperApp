CREATE PROCEDURE [dbo].[spPair_Update]
	@Id int,
	@IdFirst int,
	@IdSecond int
AS
BEGIN
	UPDATE dbo.[Pair]
	set IdFirst = @IdFirst, IdSecond = @IdSecond
	where Id = @Id;
END
