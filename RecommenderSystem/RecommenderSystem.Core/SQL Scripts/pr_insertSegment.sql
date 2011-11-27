create proc pr_insertSegment
@budget int,
@companion int,
@familiarity int,
@mood int,
@temperature int,
@travelLength int,
@weather int,
@performance float
as
begin

SET IDENTITY_INSERT segments ON
insert into segments values (@budget, @companion, @familiarity, @mood, @temperature, @travelLength, @weather, @performance)
SET IDENTITY_INSERT segments OFF

end