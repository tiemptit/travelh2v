Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewWeather]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewWeather]
Go

Create proc pr_insertNewWeather
@id int,
@weather nvarchar(50)
As
Begin

SET IDENTITY_INSERT weather ON
insert into weather(id, weather) values
(
	@id,
	@weather
)
SET IDENTITY_INSERT weather OFF

End