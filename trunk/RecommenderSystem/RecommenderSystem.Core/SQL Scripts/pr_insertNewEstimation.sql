Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewEstimation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewEstimation]
GO

Create proc pr_insertNewEstimation
@segment_id int,
@user_id int,
@place_id int,
@estimated_rating float
As
Begin

insert into Estimation values
(
	@segment_id,
	@user_id,
	@place_id,
	@estimated_rating
)

End