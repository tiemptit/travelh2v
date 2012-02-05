alter proc pr_insertSegment
@period_of_day nvarchar(50),
@period_of_week nvarchar(50),
@season nvarchar(50),
@budget int,
@companion int,
@weather int,
@performance float,
@correlationAvg float,
@performance_root float,
@correlationAvg_root float
as
begin


insert into segments values (@period_of_day, @period_of_week, @season, @budget, @companion, @weather, @performance, @correlationAvg, @performance_root, @correlationAvg_root)


end