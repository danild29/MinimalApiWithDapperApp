CREATE PROCEDURE [dbo].[spQuestion_Insert]
	@Question nvarchar(1000),
	@Answer nvarchar(1000) ,
	@Content varbinary(max),
	@ContentType nvarchar(3)
AS
BEGIN
	INSERT INTO dbo.[Question]  (Question, Answer, Content, ContentType)
	VALUES (@Question, @Answer, @Content, @ContentType);
END