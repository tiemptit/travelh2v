Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewUser]
Go

Create proc pr_insertNewUser
@id int,
@email nvarchar(100),
@birthday datetime,
@gender tinyint
As
Begin

SET IDENTITY_INSERT users ON
insert into users(id, email, birthday, gender) values
(
	@id,
	@email,
	@birthday,
	@gender
)
SET IDENTITY_INSERT users OFF

End