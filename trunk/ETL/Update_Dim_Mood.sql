use TravelH2V_DW
go

create proc Update_Dim_Mood
@mood_key int,
@mood nvarchar(50)
As
Begin

update dim_mood
set mood = @mood
where mood_key = @mood_key

End