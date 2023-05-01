CREATE TABLE [dbo].[Question]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[Question] nvarchar(1000) NOT NULL,
	[Answer] nvarchar(1000) NOT NULL,
	[Content] varbinary(max),
	[ContentType] nvarchar(3)
)

