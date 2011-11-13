use TravelH2V_DW
go

create proc Update_Dim_Familiarity
@familiarity_key int,
@familiarity nvarchar(50)
As
Begin

update dim_familiarity
set familiarity = @familiarity
where familiarity_key = @familiarity_key

End