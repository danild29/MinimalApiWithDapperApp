CREATE PROCEDURE [dbo].[spQuestion_Update]
	@Id int,
	@Que nvarchar(1000),
	@Ans nvarchar(1000),
	@Content varbinary(max),
	@CT nvarchar(3)
AS
BEGIN
	UPDATE dbo.[Question]
	set Question = @Que, Answer = @Ans, Content = @Content, ContentType = @CT
	where Id = @Id;
END
