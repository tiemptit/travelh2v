Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewMood]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewMood]
Go

Create proc pr_insertNewMood
@id int,
@mood nvarchar(50)
As
Begin

SET IDENTITY_INSERT mood ON
insert into MOOD(id, mood) values
(
	@id,
	@mood
)
SET IDENTITY_INSERT mood ON

End