create proc pr_insertSegment
@datetime int,
@budget int,
@companion int,
@weather int,
@performance float,
@correlationAvg float
as
begin


insert into segments values (@datetime, @budget, @companion, @weather, @performance, @correlationAvg)


end