CREATE PROCEDURE [dbo].[spUser_Update]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
BEGIN
	UPDATE dbo.[User]
	set FirstName = @FirstName, LastName = @LastName
	where Id = @Id;
END
