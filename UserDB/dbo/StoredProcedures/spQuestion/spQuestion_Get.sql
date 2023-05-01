CREATE PROCEDURE [dbo].[spQuestion_Get]
	@Id int
AS
BEGIN
	SELECT *
	FROM dbo.[Question]
	where Id = @Id;
END
