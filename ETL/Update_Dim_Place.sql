USE [TravelH2V_DW]
GO

/****** Object:  StoredProcedure [dbo].[Update_Dim_Place]    Script Date: 12/26/2011 14:14:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Dim_Place]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Update_Dim_Place]
GO

USE [TravelH2V_DW]
GO

/****** Object:  StoredProcedure [dbo].[Update_Dim_Place]    Script Date: 12/26/2011 14:14:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Update_Dim_Place]
@place_key int,
@place_category nvarchar(100),
@name nvarchar(500),
@imgurl nvarchar(1000),
@lat float,
@lng float,
@house_number nvarchar(50),
@street nvarchar(100),
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
@general_sum_rating float,
@general_count_rating float
As
Begin

update dim_place
set
place_category = @place_category,
name= @name,
imgurl= @imgurl,
lat= @lat,
lng= @lng,
house_number= @house_number,
street= @street,
ward= @ward,
district= @district,
city= @city,
province= @province,
country= @country,
phone_number= @phone_number,
email= @email,
website= @website,
history= @history,
details= @details,
sources= @sources,
general_rating= @general_rating,
general_sum_rating= @general_sum_rating,
general_count_rating= @general_count_rating

where place_key = @place_key

End
GO


