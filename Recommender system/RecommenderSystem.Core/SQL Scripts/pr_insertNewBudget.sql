Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewBudget]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewBudget]
GO

Create proc pr_insertNewBudget
@id int,
@budget nvarchar(50)
As
Begin

SET IDENTITY_INSERT budget ON
insert into Budget(id, budget) values
(
	@id,
	@budget
)
SET IDENTITY_INSERT budget OFF

End