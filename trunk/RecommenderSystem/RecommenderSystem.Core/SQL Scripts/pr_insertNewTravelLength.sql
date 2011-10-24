Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewTravelLength]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewTravelLength]
Go

Create proc pr_insertNewTravelLength
@id int,
@travel_length nvarchar(50)
As
Begin

SET IDENTITY_INSERT travel_length ON
insert into Travel_Length(id, travel_length) values
(
	@id,
	@travel_length
)
SET IDENTITY_INSERT travel_length OFF

End