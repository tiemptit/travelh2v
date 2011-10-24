Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewPlaceCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewPlaceCategory]
Go

Create proc pr_insertNewPlaceCategory
@id int,
@place_category nvarchar(50)
As
Begin

SET IDENTITY_INSERT place_categories ON
insert into PLACE_CATEGORIES(id, place_category) values
(
	@id,
	@place_category
)
SET IDENTITY_INSERT place_categories OFF

End