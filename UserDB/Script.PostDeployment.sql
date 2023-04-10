if not exists (select 1 from dbo.[User])
begin 
	insert into dbo.[User]
	values ('Tim', 'Corey'),
	('John', 'Willy'),
	('Max', 'Turbo'),
	('Bill', 'Gates');
end