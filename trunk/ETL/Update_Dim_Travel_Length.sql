use TravelH2V_DW
go

create proc Update_Dim_Travel_Length
@travel_length_key int,
@travel_length nvarchar(50)
As
Begin

update dim_travel_length
set travel_length = @travel_length
where travellength_key = @travel_length_key

End