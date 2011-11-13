use TravelH2V_DW
go

Create proc Update_Dim_Companion
@companion_key int,
@companion nvarchar(50)
As
Begin

update dim_companion
set companion = @companion
where companion_key = @companion_key

End