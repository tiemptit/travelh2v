use TravelH2V_DW
go

create proc Update_Dim_Weather
@weather_key int,
@weather nvarchar(50)
As
Begin

update dim_weather
set weather = @weather
where weather_key = @weather_key

End