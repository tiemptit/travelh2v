Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewUser]
Go

Create proc pr_insertNewUser
@id int,
@email nvarchar(100),
@password nvarchar(100),
@year_of_birth int,
@gender tinyint
As
Begin

SET IDENTITY_INSERT users ON
insert into users(id, email, [password], year_of_birth, gender) values
(
	@id,
	@email,
	@password,
	@year_of_birth,
	@gender
)
SET IDENTITY_INSERT users OFF

End