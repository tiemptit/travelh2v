Use TravelH2V
Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewCompanion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewCompanion]
GO

Create proc pr_insertNewCompanion
@id int,
@companion nvarchar(50)
As
Begin

SET IDENTITY_INSERT companion ON
insert into Companion(id, companion) values
(
	@id,
	@companion
)
SET IDENTITY_INSERT companion OFF

End