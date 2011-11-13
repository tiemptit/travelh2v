use TravelH2V_DW
go

create proc Update_Dim_Temperature
@temperature_key int,
@temperature nvarchar(50)
As
Begin

update dim_temperature
set temperature = @temperature
where temperature_key = @temperature_key

End