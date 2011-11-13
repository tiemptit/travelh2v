use TravelH2V_DW
go

create proc Update_Dim_Budget
@budget_key int,
@budget nvarchar(50)
As
Begin

update dim_budget
set budget = @budget
where budget_key = @budget_key

End