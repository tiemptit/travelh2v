Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewRating]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewRating]
Go

Create proc pr_insertNewRating
@id int,
@id_user int,
@id_place int,
@id_temperature int,
@id_companion int,
@id_farmiliarity int,
@id_mood int,
@id_budget int,
@id_weather int,
@id_travel_length int,
@time datetime,
@rating float

As
Begin

SET IDENTITY_INSERT real_ratings ON
insert into real_ratings(id, id_user, id_place, id_temperature, id_companion, id_farmiliarity, id_mood, id_budget, id_weather, id_travel_length, [time], rating) values
(
	@id,
	@id_user,
	@id_place,
	@id_temperature,
	@id_companion,
	@id_farmiliarity,
	@id_mood,
	@id_budget,
	@id_weather,
	@id_travel_length,
	@time,
	@rating

)
SET IDENTITY_INSERT real_ratings OFF

End