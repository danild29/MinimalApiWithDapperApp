CREATE TABLE [dbo].[Pair]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Title] nvarchar(50) not null,
	[IdFirst] int,
	[IdSecond] int
)
