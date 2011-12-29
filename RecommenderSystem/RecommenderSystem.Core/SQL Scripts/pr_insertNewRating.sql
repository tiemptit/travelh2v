USE [TravelH2V]
GO

/****** Object:  StoredProcedure [dbo].[pr_insertNewRating]    Script Date: 12/26/2011 14:29:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewRating]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewRating]
GO

USE [TravelH2V]
GO

/****** Object:  StoredProcedure [dbo].[pr_insertNewRating]    Script Date: 12/26/2011 14:29:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[pr_insertNewRating]
@id int,
@id_user int,
@id_place int,
@id_companion int,
@id_budget int,
@id_weather int,
@time datetime,
@rating float

As
Begin

SET IDENTITY_INSERT real_ratings ON
insert into real_ratings(id, id_user, id_place, id_companion, id_budget, id_weather, [time], rating) values
(
	@id,
	@id_user,
	@id_place,
	@id_companion,
	@id_budget,
	@id_weather,
	@time,
	@rating

)
SET IDENTITY_INSERT real_ratings OFF

End
GO


