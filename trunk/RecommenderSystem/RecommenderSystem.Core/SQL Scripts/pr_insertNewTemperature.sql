Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewTemperature]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewTemperature]
Go

Create proc pr_insertNewTemperature
@id int,
@temperature nvarchar(50)
As
Begin

SET IDENTITY_INSERT temperature ON
insert into Temperature(id, temperature) values
(
	@id,
	@temperature
)
SET IDENTITY_INSERT temperature OFF

End