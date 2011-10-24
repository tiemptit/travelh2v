Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewFamiliarity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewFamiliarity]
Go

Create proc pr_insertNewFamiliarity
@id int,
@familiarity nvarchar(50)
As
Begin

SET IDENTITY_INSERT familiarity ON
insert into FAMILIARITY(id, familiarity) values
(
	@id,
	@familiarity
)
SET IDENTITY_INSERT familiarity OFF

End