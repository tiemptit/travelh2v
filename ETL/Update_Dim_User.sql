use TravelH2V_DW
go

create proc Update_Dim_User
@user_key int,
@email nvarchar(100),
@password nvarchar(100)
As
Begin

update dim_user
set email = @email, [password] = @password
where user_key = @user_key

End