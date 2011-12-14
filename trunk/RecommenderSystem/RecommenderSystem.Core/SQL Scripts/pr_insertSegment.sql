create proc pr_insertSegment
@budget int,
@companion int,
@familiarity int,
@mood int,
@temperature int,
@travelLength int,
@weather int,
@performance float,
@correlationAvg float
as
begin


insert into segments values (@budget, @companion, @familiarity, @mood, @temperature, @travelLength, @weather, @performance, @correlationAvg)


end