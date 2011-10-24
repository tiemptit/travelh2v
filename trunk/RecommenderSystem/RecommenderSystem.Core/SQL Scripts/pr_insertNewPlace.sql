Use TravelH2V
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_insertNewPlace]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_insertNewPlace]
Go

Create proc pr_insertNewPlace
@id int,
@id_place_category int,
@name nvarchar(500),
@imgurl nvarchar(1000),
@lat float,
@lng float,
@house_number int,
@street nvarchar(255),
@ward nvarchar(100),
@district nvarchar(100),
@city nvarchar(100),
@province nvarchar(100),
@country nvarchar(100),
@phone_number nvarchar(100),
@email nvarchar(100),
@website nvarchar(100),
@history ntext,
@details ntext,
@sources ntext,
@general_rating float,
@general_count_rating float,
@general_sum_rating float
As
Begin

SET IDENTITY_INSERT Places ON

insert into Places(id,	id_place_category,	name,	imgurl,	lat,	lng,	house_number,	street,	ward,	district,	city,	province,	country,	phone_number,	email,	website,	history,	details,	sources,	general_rating,	general_count_rating,	general_sum_rating) values
(
	@id,
	@id_place_category,
	@name,
	@imgurl,
	@lat,
	@lng,
	@house_number,
	@street,
	@ward,
	@district,
	@city,
	@province,
	@country,
	@phone_number,
	@email,
	@website,
	@history,
	@details,
	@sources,
	@general_rating,
	@general_count_rating,
	@general_sum_rating

)
SET IDENTITY_INSERT Places OFF


End